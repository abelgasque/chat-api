FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /app

RUN apt-get update && \ 
apt-get install -y wget && \ 
apt-get install -y gnupg2 && \ 
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \ 
apt-get install -y build-essential nodejs 

COPY ["SecurityApp.Web/SecurityApp.Web.csproj", "SecurityApp.Web/"]

RUN dotnet restore "SecurityApp.Web/SecurityApp.Web.csproj" --disable-parallel
COPY . .
WORKDIR "/app/SecurityApp.Web"
RUN dotnet build "SecurityApp.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecurityApp.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecurityApp.Web.dll"]