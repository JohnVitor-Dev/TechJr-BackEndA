# TasksApi

## Descrição
O projeto TasksApi é uma API simples para gerenciar tarefas (tasks). Ele permite que você liste, adicione, atualize e remova tarefas de uma lista. Cada tarefa possui um nome, data de criação, data de expiração e uma marcação indicando se está completa ou não.

## Tecnologias Utilizadas
- Microsoft.EntityFrameworkCore
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

## Endpoints da API

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
    "Date": "2023-08-02",
    "ExpiryDate": "2023-08-31",
    "IsComplete": false
}

