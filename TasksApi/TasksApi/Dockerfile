# Use a versão 7.0 do SDK do .NET Core como imagem base para compilar o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Define o diretório de trabalho no contêiner
WORKDIR /app

# Copia os arquivos do projeto (*.csproj) para o diretório de trabalho
COPY ./*.csproj ./

# Restaura as dependências do projeto
RUN dotnet restore

# Copia todos os arquivos do projeto para o diretório de trabalho
COPY . ./

# Compila o aplicativo
RUN dotnet build -c Release -o out

# Define a imagem base para executar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Define o diretório de trabalho para o aplicativo em execução
WORKDIR /app

# Copia os arquivos do aplicativo compilado (do estágio de compilação) para o diretório de trabalho do contêiner em execução
COPY --from=build /app/out ./

# Expõe a porta em que o aplicativo ASP.NET Core irá escutar
EXPOSE 80

# Define o comando de inicialização do aplicativo
CMD ["dotnet", "tasksbuild.dll"]
