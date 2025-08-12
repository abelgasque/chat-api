# Etapa 1: Base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /server
EXPOSE 8080

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar arquivos de solução e projeto
COPY ChatApi/ChatApi.csproj ChatApi/
RUN dotnet restore "ChatApi/ChatApi.csproj"

# Copiar o restante do código
COPY . .

# Instalar coverlet global tool para cobertura
RUN dotnet tool install -g coverlet.console

# Ajustar PATH para dotnet tools global
ENV PATH="$PATH:/root/.dotnet/tools"

# Construir o projeto
WORKDIR /src/ChatApi
RUN dotnet build "ChatApi.csproj" -c Release -o /app/build

# Etapa 3: Testes
WORKDIR /src/ChatApi.Tests
RUN dotnet test --no-build --verbosity normal

# Etapa 4: Publicação
FROM build AS publish
WORKDIR /src/ChatApi
RUN dotnet publish "ChatApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 5: Runtime (Imagem final)
FROM base AS final
WORKDIR /server
EXPOSE 8080

RUN mkdir -p /app/keys

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Configurar o ponto de entrada
ENTRYPOINT ["dotnet", "ChatApi.dll"]