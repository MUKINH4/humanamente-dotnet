# Humanamente API

## Integrantes
- Samuel Heitor - RM 556731
- Lucas Nicolini - RM 557613
- Renan Olivi - RM 557680

> **Transformando a IA em uma ferramenta de empatia e valorização do ser humano**

O **Humanamente** é uma plataforma inovadora que utiliza Inteligência Artificial de forma inversa: em vez de substituir o ser humano, ela identifica e preserva as tarefas que devem permanecer essencialmente humanas em cada profissão.

## 🎯 Objetivo

Transformar a IA em uma ferramenta de **empatia e valorização do ser humano**, ajudando pessoas e organizações a redesenharem o trabalho de forma mais humana, justa e sustentável.

## 🚀 Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

## 📋 Pré-requisitos

- .NET SDK 8.0 ou superior
- PostgreSQL
- Visual Studio 2022 ou VS Code

## ⚙️ Configuração Local

1. Clone o repositório:

```bash
git clone https://github.com/MUKINH4/humanamente-api-dotnet.git
cd humanamente-api-dotnet
```

2. Configure a connection string no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "sua-connection-string"
  }
}
```

3. Execute as migrations:

```bash
dotnet ef database update
```

4. Inicie a aplicação:

```bash
dotnet run
```

## 🌐 Deploys

### Ambiente de Desenvolvimento

- **URL**: `https://localhost:5101`
- **Swagger**: `https://localhost:5101/swagger`
- **Health Check** `https://localhost:5101/health`

## 📡 Endpoints da API

### 🎓 Professions (Profissões)

#### GET /api/v1/professions

Retorna a lista paginada de profissões com suas tarefas associadas.

**Query Parameters:**

- `page` (opcional, padrão: 1) - Número da página
- `pageSize` (opcional, padrão: 10) - Quantidade de itens por página

**Request:**

```bash
curl -X GET "https://localhost:5101/api/v1/professions?page=1&pageSize=10" \
  -H "Accept: application/json"
```

**Response:** `200 OK`

```json
{
  "page": 1,
  "pageSize": 10,
  "totalItems": 25,
  "items": [
    {
      "id": 1,
      "title": "Desenvolvedor de Software",
      "description": "Profissional responsável por criar e manter sistemas e aplicações",
      "tasks": [
        {
          "id": 1,
          "name": "Code Review",
          "description": "Revisar código de outros desenvolvedores",
          "classification": "Essencialmente Humana",
          "professionId": 1
        }
      ]
    }
  ]
}
```

#### GET /api/v1/professions/{id}

Retorna os detalhes de uma profissão específica incluindo suas tarefas.

**Request:**

```bash
curl -X GET https://localhost:5101/api/v1/professions/1 \
  -H "Accept: application/json"
```

**Response:** `200 OK`

```json
{
  "id": 1,
  "title": "Desenvolvedor de Software",
  "description": "Profissional responsável por criar e manter sistemas e aplicações",
  "tasks": [
    {
      "id": 1,
      "name": "Code Review",
      "description": "Revisar código de outros desenvolvedores com feedback construtivo",
      "classification": "Essencialmente Humana",
      "professionId": 1
    },
    {
      "id": 2,
      "name": "Testes Unitários",
      "description": "Escrever testes automatizados",
      "classification": "Automatizável",
      "professionId": 1
    }
  ]
}
```

**Response:** `404 Not Found`

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

#### POST /api/v1/professions

Cria uma nova profissão.

**Request:**

```bash
curl -X POST https://localhost:5101/api/v1/professions \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Designer UX/UI",
    "description": "Profissional focado em criar experiências centradas no usuário"
  }'
```

**Request Body:**

```json
{
  "title": "Designer UX/UI",
  "description": "Profissional focado em criar experiências centradas no usuário"
}
```

**Response:** `201 Created`

```json
{
  "id": 2,
  "title": "Designer UX/UI",
  "description": "Profissional focado em criar experiências centradas no usuário",
  "tasks": []
}
```

**Response:** `400 Bad Request` (quando dados inválidos)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Title": ["The Title field is required."]
  }
}
```

#### PUT /api/v1/professions/{id}

Atualiza uma profissão existente.

**Request:**

```bash
curl -X PUT https://localhost:5101/api/v1/professions/1 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Desenvolvedor Full Stack",
    "description": "Profissional que atua em frontend e backend"
  }'
```

**Request Body:**

```json
{
  "title": "Desenvolvedor Full Stack",
  "description": "Profissional que atua em frontend e backend"
}
```

**Response:** `204 No Content`

**Response:** `404 Not Found`

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

#### DELETE /api/v1/professions/{id}

Remove uma profissão e todas as suas tarefas associadas.

**Request:**

```bash
curl -X DELETE https://localhost:5101/api/v1/professions/1
```

**Response:** `204 No Content`

**Response:** `404 Not Found`

---

### ✅ Tasks (Tarefas)

#### GET /api/v1/tasks

Retorna a lista de todas as tarefas, com filtro opcional por profissão.

**Query Parameters:**

- `professionId` (opcional) - Filtra tarefas de uma profissão específica

**Request:**

```bash
# Todas as tarefas
curl -X GET https://localhost:5101/api/v1/tasks \
  -H "Accept: application/json"

# Tarefas de uma profissão específica
curl -X GET "https://localhost:5101/api/v1/tasks?professionId=1" \
  -H "Accept: application/json"
```

**Response:** `200 OK`

```json
[
  {
    "id": 1,
    "name": "Code Review",
    "description": "Revisar código de outros desenvolvedores com feedback construtivo",
    "classification": "Essencialmente Humana",
    "professionId": 1
  },
  {
    "id": 2,
    "name": "Testes Unitários",
    "description": "Escrever testes automatizados para validar funcionalidades",
    "classification": "Automatizável",
    "professionId": 1
  }
]
```

#### GET /api/v1/tasks/{id}

Retorna os detalhes de uma tarefa específica.

**Request:**

```bash
curl -X GET https://localhost:5101/api/v1/tasks/1 \
  -H "Accept: application/json"
```

**Response:** `200 OK`

```json
{
  "id": 1,
  "name": "Code Review",
  "description": "Revisar código de outros desenvolvedores com feedback construtivo",
  "classification": "Essencialmente Humana",
  "professionId": 1
}
```

**Response:** `404 Not Found`

#### POST /api/v1/tasks/profession/{professionId}

Cria uma nova tarefa para uma profissão específica. A tarefa é automaticamente classificada pela IA como "Essencialmente Humana" ou "Automatizável".

**Request:**

```bash
curl -X POST https://localhost:5101/api/v1/tasks/profession/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Mentoria de Equipe",
    "description": "Orientar e desenvolver membros júnior da equipe"
  }'
```

**Request Body:**

```json
{
  "name": "Mentoria de Equipe",
  "description": "Orientar e desenvolver membros júnior da equipe"
}
```

**Response:** `201 Created`

```json
{
  "id": 3,
  "name": "Mentoria de Equipe",
  "description": "Orientar e desenvolver membros júnior da equipe",
  "classification": "Essencialmente Humana",
  "professionId": 1
}
```

**Response:** `404 Not Found` (quando a profissão não existe)

```json
{
  "message": "Profession not found."
}
```

**Response:** `400 Bad Request` (quando dados inválidos)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": ["The Name field is required."]
  }
}
```

#### PUT /api/v1/tasks/{id}

Atualiza uma tarefa existente. A tarefa é reclassificada automaticamente pela IA se a descrição for alterada.

**Request:**

```bash
curl -X PUT https://localhost:5101/api/v1/tasks/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Code Review Detalhado",
    "description": "Revisar código com foco em arquitetura e boas práticas"
  }'
```

**Request Body:**

```json
{
  "name": "Code Review Detalhado",
  "description": "Revisar código com foco em arquitetura e boas práticas"
}
```

**Response:** `204 No Content`

**Response:** `404 Not Found`

#### DELETE /api/v1/tasks/{id}

Remove uma tarefa.

**Request:**

```bash
curl -X DELETE https://localhost:5101/api/v1/tasks/1
```

**Response:** `204 No Content`

**Response:** `404 Not Found`

---

### 🤖 Classificação com IA

As tarefas criadas ou atualizadas são automaticamente classificadas pela IA em duas categorias:

- **"Essencialmente Humana"**: Tarefas que requerem empatia, criatividade, julgamento moral ou conexão humana
- **"Automatizável"**: Tarefas repetitivas, baseadas em regras ou que podem ser executadas por sistemas automatizados

**Exemplos de Tarefas Essencialmente Humanas:**

- Mentoria e desenvolvimento de pessoas
- Tomada de decisões estratégicas
- Comunicação empática com clientes
- Resolução de conflitos

**Exemplos de Tarefas Automatizáveis:**

- Testes automatizados
- Geração de relatórios
- Validação de dados
- Processamento de informações estruturadas

## 📚 Documentação da API

A documentação completa está disponível através do Swagger nos ambientes de desenvolvimento e homologação.

## 🧪 Testes

Execute os testes unitários:

```bash
dotnet test
```

## 📄 Licença

Este projeto está sob a licença MIT.

## 👥 Contribuindo

Contribuições são bem-vindas! Por favor, leia o guia de contribuição antes de submeter PRs.

## 📞 Suporte

Para dúvidas ou suporte, entre em contato através de: suporte@humanamente.com

---

**Desenvolvido com ❤️ para valorizar o que há de mais humano em cada profissão**
