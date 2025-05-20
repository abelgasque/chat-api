# Etapa 1: Base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /csharp-security-webapp
EXPOSE 80

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /csharp-security-webapp
COPY . ./

# Instalar dependências adicionais, como Node.js
RUN apt-get update && \ 
    apt-get install -y wget && \ 
    apt-get install -y gnupg2 && \ 
    wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \ 
    apt-get install -y build-essential nodejs

# Restaurar dependências do projeto
RUN dotnet restore "SecurityWebApp/SecurityWebApp.csproj" --disable-parallel

# Construir o projeto
RUN dotnet build "SecurityWebApp/SecurityWebApp.csproj" -c Release -o /app/build

# Etapa 3: Publicação
FROM build AS publish
RUN dotnet publish "SecurityWebApp/SecurityWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 4: Runtime (Imagem final)
FROM base AS final
WORKDIR /csharp-security-webapp
EXPOSE 80

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Configurar o ponto de entrada
ENTRYPOINT ["dotnet", "SecurityWebApp.dll"]