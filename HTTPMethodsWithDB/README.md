# HTTPMethodsWithDB 📋🔧

## Descrição 📜
O projeto HTTPMethodsWithDB é uma aplicação de exemplo que demonstra o uso de HTTP methods (GET, POST, PUT e DELETE) em conjunto com um banco de dados SQL Server para criar, ler, atualizar e deletar registros de uma tabela chamada "Identidade". Cada registro na tabela representa uma identidade com um nome e idade.

## Tecnologias Utilizadas 💻🛠️
- .NET 7.0 (SDK ou Runtime)
- Newtonsoft.Json para a manipulação de JSON
- Microsoft.Data.SqlClient para a conexão com o banco de dados SQL Server

## Configuração do Banco de Dados 🗄️🔧
A aplicação utiliza um banco de dados SQL Server para armazenar as identidades. A string de conexão com o banco de dados pode ser configurada no arquivo SqlServerConect.cs, na variável "connectionString".

## Como Executar o Projeto 🏃‍♂️💻
Antes de executar o projeto, certifique-se de ter o [.NET 7.0 (SDK ou Runtime)](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0) instalado em sua máquina.

Existem duas maneiras de executar o projeto:

### 1. Executável (Windows)
Baixe o arquivo Executável_HTTPMethodsWithDB.zip da última versão do projeto a partir da página de [releases](https://github.com/seu-usuario-aqui/HTTPMethodsWithDB/releases). Em seguida, extraia e execute o arquivo do executável para iniciar o servidor HTTP.

### 2. Arquivos (Linux & Windows)
Baixe o arquivo Arquivos_HTTPMethodsWithDB.zip da última versão do projeto a partir da página de [releases](https://github.com/seu-usuario-aqui/HTTPMethodsWithDB/releases) ou baixe pelo Wget na pasta que desejar.
```shell
wget https://github.com/seu-usuario-aqui/HTTPMethodsWithDB/releases/download/v1.0.0/Arquivos_HTTPMethodsWithDB.zip
