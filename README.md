# 🏛️ PatrimonioAPI

## Descrição

A **PatrimonioAPI** é uma API RESTful para gerenciamento de itens de patrimônio. Ela permite cadastrar, consultar, atualizar e excluir bens patrimoniais, armazenando informações detalhadas como depreciação, valor de aquisição, responsável, localização física e dados de veículos.

Foi construída com **ASP.NET Core (.NET 6)** e **Entity Framework Core**, utilizando **SQL Server** como banco de dados persistente.

O projeto foi criado para gerenciar o ciclo de vida de bens patrimoniais de uma organização, contemplando campos financeiros (depreciação, valor líquido) e operacionais (localização, responsável, conservação).

---

## Instrução de Instalação

**Pré-requisitos:**
- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local ou remoto)

**1. Clone o repositório**

```bash
git clone https://github.com/seu-usuario/Patrimonio.git
cd Patrimonio
```

**2. Configure a connection string**

No arquivo `appsettings.json`, adicione a sua connection string e defina o tipo de banco:

```json
{
  "TypeDatabase": "SqlServer",
  "ConnectionStrings": {
    "SqlServer": "Server=SEU_SERVIDOR;Database=PatrimonioDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
  }
}
```

**3. Aplique as migrations para criar o banco**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> Caso não tenha o EF CLI instalado: `dotnet tool install --global dotnet-ef`

**4. Restaure as dependências e execute**

```bash
dotnet restore
dotnet run
```

---

## Instruções de Uso

Após iniciar a aplicação, acesse o Swagger para testar os endpoints:

```
https://localhost:<porta>/swagger
```

> A porta será exibida no terminal ao iniciar a aplicação.

> ⚠️ O CORS está configurado para aceitar requisições apenas de `http://localhost:63296`. Para consumir a API de outra origem, ajuste a política de CORS no `Program.cs`.

### Endpoints disponíveis

Base URL: `/v1`

---

#### `GET /v1/TodosOsItens`

Retorna todos os itens de patrimônio cadastrados.

```
GET /v1/TodosOsItens
```

| Status | Descrição |
|--------|-----------|
| `200 OK` | Retorna a lista de todos os itens |

---

#### `GET /v1/TodosOsItens/{id}`

Retorna um item específico pelo ID.

```
GET /v1/TodosOsItens/1
```

| Status | Descrição |
|--------|-----------|
| `200 OK` | Retorna o item encontrado |
| `404 Not Found` | Item não encontrado |

---

#### `POST /v1/TodosOsItens`

Cadastra um novo item de patrimônio.

```
POST /v1/TodosOsItens
```

**Body:**

```json
{
  "codigo_item": "PAT-001",
  "placa_item": "PL-001",
  "descricao_item": "Notebook Dell",
  "tipo_item": "Equipamento",
  "grupo_item": "TI",
  "estado_conservacao": "Bom",
  "tipo_aquisicao": "Compra",
  "valor_aquisicao": "3500.00",
  "metodo_depreciacao": "Linear",
  "valor_residual": "350.00",
  "responsavel": "João Silva",
  "vida_util": "5",
  "depreciacao_anual": "630.00",
  "inicio_depreciacao": "2023-01-01T00:00:00",
  "data_aquisicao": "2023-01-01T00:00:00",
  "valor_depreciavel": "3150.00",
  "valor_depreciado": "630.00",
  "saldo_depreciar": "2520.00",
  "valor_liquido": "2870.00"
}
```

| Status | Descrição |
|--------|-----------|
| `201 Created` | Item criado com sucesso, retorna o objeto |
| `400 Bad Request` | Dados inválidos ou erro ao salvar |

---

#### `PUT /v1/TodosOsItens/{id}`

Atualiza um item de patrimônio existente.

```
PUT /v1/TodosOsItens/1
```

O body segue o mesmo formato do `POST`. Todos os campos serão substituídos.

| Status | Descrição |
|--------|-----------|
| `200 OK` | Item atualizado com sucesso |
| `400 Bad Request` | Dados inválidos ou erro ao salvar |
| `404 Not Found` | Item não encontrado |

---

#### `DELETE /v1/TodosOsItens/{id}`

Remove um único item pelo ID.

```
DELETE /v1/TodosOsItens/1
```

| Status | Descrição |
|--------|-----------|
| `200 OK` | Item removido com sucesso |
| `400 Bad Request` | Erro ao remover |

---

#### `DELETE /v1/TodosOsItens`

Remove múltiplos itens de uma vez, informando uma lista de IDs no body.

```
DELETE /v1/TodosOsItens
```

**Body:**

```json
[1, 2, 3]
```

| Status | Descrição |
|--------|-----------|
| `200 OK` | Itens removidos com sucesso |
| `400 Bad Request` | Lista vazia ou erro ao remover |
| `404 Not Found` | Um ou mais IDs não encontrados |

---

### Estrutura do Projeto

```
Patrimonio/
├── Controllers/
│   └── PatrimonioController.cs   # Endpoints da API
├── Data/
│   └── ApiContexto.cs            # Contexto do Entity Framework
├── Modelos/
│   └── PatrimonioItens.cs        # Modelo de dados
├── Program.cs                    # Configuração e inicialização
└── appsettings.json              # Configurações e connection string
```

---

## Licença

Este projeto está sob a licença [MIT](https://opensource.org/licenses/MIT).
