using Moq;
using Xunit;
using ChatApi.Infrastructure.Services;
using ChatApi.Domain.Entities.Models;
using ChatApi.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;

public class UserServiceTests
{
    private readonly Mock<IRepository<UserModel>> _repositoryMock;
    private readonly Mock<IDistributedCache> _cacheMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IRepository<UserModel>>();
        _cacheMock = new Mock<IDistributedCache>();
        _service = new UserService(_repositoryMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Call_Repository_CreateAsync()
    {
        var user = new UserModel { Id = Guid.NewGuid(), Name = "Test User" };
        await _service.CreateAsync(user);
        _repositoryMock.Verify(r => r.CreateAsync(user), Times.Once);
    }

    [Fact]
    public async Task CreateLeadAsync_Should_Create_New_UserModel_And_Call_CreateAsync()
    {
        var leadRequest = new UserLeadRequest
        {
            Username = "leaduser",
            Email = "lead@example.com",
            Phone = "123456789"
        };

        UserModel createdUser = null;
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<UserModel>()))
            .Callback<UserModel>(u => createdUser = u)
            .Returns(Task.CompletedTask);

        await _service.CreateLeadAsync(leadRequest);

        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserModel>()), Times.Once);
        Assert.NotNull(createdUser);
        Assert.Equal(leadRequest.Username, createdUser.Name);
        Assert.Equal(leadRequest.Email, createdUser.Email);
        Assert.Equal(leadRequest.Phone, createdUser.Phone);
        Assert.Null(createdUser.BlockedAt);
        Assert.True(createdUser.ActiveAt <= DateTime.UtcNow);
        Assert.Equal(0, createdUser.NuLogged);
        Assert.Equal(0, createdUser.NuRefreshed);
        Assert.NotEqual(Guid.Empty, createdUser.Id);
    }

    [Fact]
    public async Task ReadById_Should_Return_Cached_User_If_Found()
    {
        var userId = Guid.NewGuid();
        var user = new UserModel { Id = userId, Name = "Cached User" };
        var json = JsonSerializer.Serialize(user);
        var bytes = Encoding.UTF8.GetBytes(json);

        _cacheMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bytes);

        var result = await _service.ReadById(userId);

        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        _repositoryMock.Verify(r => r.FindAsync(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Never);
    }

    [Fact]
    public async Task ReadById_Should_Return_User_From_Repository_And_Set_Cache_If_Not_Found_In_Cache()
    {
        var userId = Guid.NewGuid();
        var user = new UserModel { Id = userId, Name = "Repo User" };

        _cacheMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[])null);

        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new List<UserModel> { user });

        _cacheMock.Setup(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _service.ReadById(userId);

        _cacheMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ReadByMail_Should_Return_Cached_User_If_Found()
    {
        var email = "cache@example.com";
        var user = new UserModel { Email = email };
        var json = JsonSerializer.Serialize(user);
        var bytes = Encoding.UTF8.GetBytes(json);

        _cacheMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bytes);

        var result = await _service.ReadByMail(email);

        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
        _repositoryMock.Verify(r => r.GetByConditionAsync(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Never);
    }

    [Fact]
    public async Task ReadByMail_Should_Return_User_From_Repository_And_Set_Cache_If_Not_Found_In_Cache()
    {
        var email = "repo@example.com";
        var user = new UserModel { Email = email };

        _cacheMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[])null);

        _repositoryMock.Setup(r => r.GetByConditionAsync(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(user);

        _cacheMock.Setup(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _service.ReadByMail(email);

        _cacheMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Read_Should_Return_PaginationResponse_With_Filtered_Data()
    {
        var users = new List<UserModel>
        {
            new UserModel { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, ActiveAt = DateTime.UtcNow },
            new UserModel { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, ActiveAt = null },
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

        var filter = new UserFilterRequest
        {
            Page = 1,
            PageSize = 10,
            Active = true
        };

        var response = await _service.Read(filter);

        Assert.NotNull(response);
        Assert.All(response.Data, userResp => Assert.NotNull(userResp));
        Assert.True(response.Total <= users.Count);
        Assert.All(response.Data, userResp => Assert.NotNull(userResp));
    }

    [Fact]
    public async Task UpdateAsync_Should_Call_Repository_UpdateAsync()
    {
        var user = new UserModel { Id = Guid.NewGuid(), Name = "Update User" };
        await _service.UpdateAsync(user);
        _repositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_Repository_DeleteAsync_If_User_Exists()
    {
        var userId = Guid.NewGuid();
        var user = new UserModel { Id = userId };

        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new List<UserModel> { user });

        await _service.DeleteAsync(userId);

        _repositoryMock.Verify(r => r.DeleteAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Not_Call_DeleteAsync_If_User_Not_Found()
    {
        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new List<UserModel>());

        await _service.DeleteAsync(Guid.NewGuid());

        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UserModel>()), Times.Never);
    }
}