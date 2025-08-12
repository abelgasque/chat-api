using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi.Infrastructure.Services
{
    public class ChatService : IBaseController<ChatModel>
    {
        private readonly IRepository<ChatModel> _repository;
        private readonly IRepository<ChatMessageModel> _messagesRepository;

        public ChatService(
            IRepository<ChatModel> repository,
            IRepository<ChatMessageModel> messagesRepository
        )
        {
            _repository = repository;
            _messagesRepository = messagesRepository;
        }

        public async Task CreateAsync(ChatModel model)
        {
            await _repository.CreateAsync(model);

            var results = await _repository.FindAsync(m => m.ReceiverId == model.SenderId && m.SenderId == model.ReceiverId);
            if (!results.Any())
            {
                var mirrorChat = new ChatModel
                {
                    SenderId = model.ReceiverId,
                    ReceiverId = model.SenderId,
                    CreatedAt = model.CreatedAt,
                };

                await _repository.CreateAsync(mirrorChat);
            }
        }

        public async Task<ChatModel> ReadById(Guid id)
        {
            var results = await _repository.FindAsync(m => m.Id == id);
            return results.FirstOrDefault();
        }

        public async Task<ChatResponse> ReadByIdWithMessages(Guid id)
        {
            var chat = (await _repository.FindAsync(m => m.Id == id)).FirstOrDefault();
            if (chat == null)
                throw new Exception($"Chat with id {id} not found");

            var mirrorChat = (await _repository.FindAsync(m =>
                m.SenderId == chat.ReceiverId && m.ReceiverId == chat.SenderId))
                .FirstOrDefault();

            var chatIds = new List<Guid> { chat.Id };
            if (mirrorChat != null)
                chatIds.Add(mirrorChat.Id);

            var messages = await _messagesRepository.FindAsync(m => chatIds.Contains(m.ChatId));

            return new ChatResponse(chat)
            {
                Messages = messages.OrderBy(m => m.Timestamp).ToList()
            };
        }

        public async Task<PaginationResponse> Read(ChatFilterRequest filter)
        {
            var entities = await _repository.GetAllAsync();

            if (filter.SenderId.HasValue)
                entities = entities.Where(e => e.SenderId == filter.SenderId.Value);


            if (filter.ReceiverId.HasValue)
                entities = entities.Where(e => e.ReceiverId == filter.ReceiverId.Value);

            var filtered = entities.ToList();
            var skip = (filter.Page - 1) * filter.PageSize;

            List<ChatModel> paged = filtered
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToList();

            return new PaginationResponse
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                Total = filtered.Count,
                Data = paged
            };
        }

        public async Task UpdateAsync(ChatModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await ReadById(id);
            if (model != null)
            {
                await _repository.DeleteAsync(model);
            }
        }
    }
}