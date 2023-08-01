# Use a versão 7.0 do SDK do .NET Core como imagem base para compilar o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Define o diretório de trabalho no contêiner
WORKDIR /source

# Copia os arquivos do projeto (*.csproj) para o diretório de trabalho
COPY TasksApi/*.csproj .

# Restaura as dependências do projeto
RUN dotnet restore --use-current-runtime

# Copia todos os arquivos do projeto para o diretório de trabalho
COPY TasksApi/. .

# Compila o aplicativo
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "tasksbuild.dll"]
