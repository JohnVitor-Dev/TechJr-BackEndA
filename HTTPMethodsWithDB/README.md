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


## Endpoints da API 🚀🔗
### GET /
Retorna a página inicial da API.

### GET /identidades
Retorna uma lista com todas as identidades cadastradas.

### POST /identidades
Adiciona uma nova identidade à lista.

Exemplo de requisição:
```json
{
    "Nome": "João",
    "Idade": 30
}
```

### PUT /identidades/{id}
Atualiza os detalhes de uma identidade existente com base no seu ID.

Exemplo de requisição:
```json
{
    "Nome": "Maria",
    "Idade": 25
}
```

### DELETE /identidades/{id}
Remove uma identidade da lista com base no seu ID.

## Observações 📝
Este projeto é apenas uma aplicação de exemplo e não é recomendado para uso em produção. O objetivo é demonstrar a implementação dos HTTP methods em conjunto com um banco de dados SQL Server. Certifique-se de ajustar as configurações do banco de dados e segurança conforme as necessidades do seu projeto.
