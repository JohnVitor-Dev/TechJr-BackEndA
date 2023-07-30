# Define a imagem base. Use a imagem oficial do SDK do .NET 7.0 para construir o aplicativo.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Defina o diretório de trabalho dentro do contêiner.
WORKDIR /app

# Copie o arquivo do projeto e o arquivo csproj para o diretório de trabalho.
COPY *.csproj ./

# Restaure as dependências do projeto usando o dotnet CLI.
RUN dotnet restore

# Copie todo o código do projeto para o diretório de trabalho.
COPY . ./

# Compile o projeto para produzir o arquivo executável.
RUN dotnet publish -c Release -o out

# Define a imagem base para a implantação. Use a imagem oficial do runtime do .NET 7.0.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Defina o diretório de trabalho dentro do contêiner.
WORKDIR /app

# Copie o executável do projeto compilado a partir do estágio anterior.
COPY --from=build-env /app/out .

# Exponha as portas definidas para HTTP e HTTPS.
EXPOSE 8080

# Comando para iniciar a aplicação ao executar o contêiner.
ENTRYPOINT ["dotnet", "TasksApi.dll"]

