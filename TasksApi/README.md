# TasksApi 📋🔧

## Descrição 📜
O projeto TasksApi é uma API simples para gerenciar tarefas (tasks). Ele permite que você liste, adicione, atualize e remova tarefas de uma lista. Cada tarefa possui um nome, data de criação, data de expiração e uma marcação indicando se está completa ou não.

## Tecnologias Utilizadas 💻🛠️
- Microsoft.EntityFrameworkCore
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

## Endpoints da API 🚀🔗

### GET /tasks
Retorna uma lista com todas as tarefas cadastradas.

### GET /tasks/complete
Retorna uma lista com todas as tarefas completas.

### GET /tasks/incomplete
Retorna uma lista com todas as tarefas incompletas.

### GET /tasks/{id}
Retorna os detalhes de uma tarefa específica com base no seu ID.

### POST /tasks
Adiciona uma nova tarefa à lista.

Exemplo de requisição:
```json
{
    "Name": "Tarefa de exemplo",
    "Date": "02/08/2023",
    "ExpiryDate": "31/08/2023",
    "IsComplete": false
}
```

### PUT /tasks/{id}
Atualiza os detalhes de uma tarefa existente com base no seu ID.

Exemplo de requisição:
```json
{
    "Name": "Tarefa atualizada",
    "Date": "02/08/2023",
    "ExpiryDate": "15/09/2023",
    "IsComplete": true
}
```

### DELETE /tasks/{id}
Remove uma tarefa da lista com base no seu ID.

## Configuração do Banco de Dados 🗄️🔧
A API utiliza um banco de dados em memória (In-Memory Database) para armazenar as tarefas. Portanto, não é necessário configurar um banco de dados externo.

## Como Executar o Projeto 🏃‍♂️💻
Antes de executar verifique se você tem o [.NET 7.0 (SDK ou Runtime)](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0) e o navegador Chrome instalados.

Para executar o projeto "TasksApi", você pode:


### 1. Executável
Windows:

Baixe o arquivo Executável_TasksApi.zip da última versão do projeto a partir da página de [releases](https://github.com/JohnVitor-Dev/TechJr-BackEndA/releases/tag/TasksApi). Em seguida, extraia e execute o arquivo do executável para iniciar a API.

### 2. Arquivos
Linux & Windows:

Baixe o arquivo Arquivos_TasksApi.zip da última versão do projeto a partir da página de [releases](https://github.com/JohnVitor-Dev/TechJr-BackEndA/releases/tag/TasksApi) ou baixe pelo Wget na pasta que desejar.
```shell
wget https://github.com/JohnVitor-Dev/TechJr-BackEndA/releases/download/TasksApi/Arquivos_TasksApi.zip
```
Extraia o arquivo .zip e acesse a pasta /TasksApi/TasksApi/ pelo terminal.
Execute a API com:
```shell
dotnet run
```


