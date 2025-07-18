# Etapa 1: Base para runtime
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /server
EXPOSE 8080

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /server
COPY . ./

# Restaurar dependências do projeto
RUN dotnet restore "ChatApi.csproj"

# Construir o projeto
RUN dotnet build "ChatApi.csproj" -c Release -o /app/build

# Etapa 3: Publicação
FROM build AS publish
RUN dotnet publish "ChatApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 4: Runtime (Imagem final)
FROM base AS final
WORKDIR /server
EXPOSE 8080

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Configurar o ponto de entrada
ENTRYPOINT ["dotnet", "ChatApi.dll"]