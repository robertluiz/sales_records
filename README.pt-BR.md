# Ambev Developer Evaluation

Sistema de gerenciamento de vendas desenvolvido em .NET 8 seguindo princípios de Clean Architecture e DDD.

[🇺🇸 English Version](README.md)

## 🚀 Tecnologias

- .NET 8.0
- PostgreSQL 13
- Entity Framework Core
- MediatR
- AutoMapper
- FluentValidation
- BCrypt.NET
- Rebus (Message Bus)
- xUnit
- Docker & Docker Compose

## 📁 Estrutura do Projeto

```
src/
├── Ambev.DeveloperEvaluation.Application  # Camada de aplicação (casos de uso)
├── Ambev.DeveloperEvaluation.Common       # Componentes compartilhados
├── Ambev.DeveloperEvaluation.Domain       # Camada de domínio
├── Ambev.DeveloperEvaluation.IoC          # Configuração de injeção de dependência
├── Ambev.DeveloperEvaluation.ORM          # Camada de persistência
├── Ambev.DeveloperEvaluation.Services     # Serviços de integração e handlers de mensagens
└── Ambev.DeveloperEvaluation.WebApi       # API REST

tests/
├── Ambev.DeveloperEvaluation.Unit         # Testes unitários
├── Ambev.DeveloperEvaluation.Integration  # Testes de integração
└── Ambev.DeveloperEvaluation.Functional   # Testes funcionais
```

## 🏗️ Arquitetura

O projeto segue os princípios de Clean Architecture e Domain-Driven Design (DDD):

- **Domain Layer**: Contém as entidades, regras de negócio e interfaces dos repositórios
- **Application Layer**: Implementa os casos de uso da aplicação usando o padrão CQRS com MediatR
- **Infrastructure Layer**: Implementação dos repositórios e acesso a dados usando Entity Framework Core
- **Services Layer**: Gerencia serviços de integração e operações do message bus com Rebus
- **WebApi Layer**: Controllers REST e configuração da API

## 🗃️ Dados Iniciais (Seeds)

### Usuários

- Admin:
  - Id: 7c9e6679-7425-40de-944b-e07fc1f90ae1
  - Email: admin@ambev.com.br
  - Senha: Admin@123
  - Role: Admin
- Cliente:
  - Id: 7c9e6679-7425-40de-944b-e07fc1f90ae2
  - Email: customer@email.com
  - Senha: Admin@123
  - Role: Customer

### Produtos

- Brahma Duplo Malte 350ml (Id: 1, Código: BEER-001, R$ 4,99)
- Skol Puro Malte 350ml (Id: 2, Código: BEER-002, R$ 4,49)
- Original 600ml (Id: 3, Código: BEER-003, R$ 8,99)
- Corona Extra 330ml (Id: 4, Código: BEER-004, R$ 7,99)

### Filiais

- São Paulo Headquarters (Id: 7c9e6679-7425-40de-944b-e07fc1f90ae7, Código: MATRIX-001)
- Rio de Janeiro Branch (Id: 9c9e6679-7425-40de-944b-e07fc1f90ae8, Código: BRANCH-RJ-001)
- Belo Horizonte Branch (Id: 5c9e6679-7425-40de-944b-e07fc1f90ae9, Código: BRANCH-BH-001)
- Curitiba Branch (Id: 3c9e6679-7425-40de-944b-e07fc1f90ae0, Código: BRANCH-CWB-001)

## 🚦 Regras de Negócio

- Descontos por quantidade:
  - 4-9 itens: 10% de desconto
  - 10-20 itens: 20% de desconto
- Restrições:
  - Máximo de 20 itens idênticos por venda
  - Sem desconto para menos de 4 itens

## 🛠️ Como Executar

### Pré-requisitos

- Docker
- Docker Compose
- .NET 8 SDK (para desenvolvimento)

### Usando Docker Compose

1. Clone o repositório:

```bash
git clone <repository-url>
cd sales_records
```

2. Execute o projeto usando Docker Compose:

```bash
docker-compose up -d
```

A API estará disponível em: http://localhost:8080
Swagger UI: http://localhost:8080/swagger

### Desenvolvimento Local

1. Restaure os pacotes:

```bash
dotnet restore Ambev.DeveloperEvaluation.sln
```

2. Execute os testes:

```bash
dotnet test Ambev.DeveloperEvaluation.sln
```

3. Execute o projeto:

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```

## 📡 API

Uma coleção do Postman está disponível no arquivo `Ambev.DeveloperEvaluation.postman_collection.json` com exemplos de todas as requisições.

### Endpoints Principais:

- `POST /api/auth/login` - Autenticação
- `GET /api/sales` - Listar vendas
- `POST /api/sales` - Criar venda
- `GET /api/sales/{id}` - Detalhes da venda
- `PUT /api/sales/{id}/cancel` - Cancelar venda
- `PUT /api/sales/{id}/items/{itemId}/cancel` - Cancelar item da venda

### Documentação

A documentação completa da API está disponível através do Swagger UI em:

- Desenvolvimento: http://localhost:8080/swagger
- Produção: https://seu-dominio/swagger

## 🔐 Segurança

- Autenticação usando JWT
- Senhas criptografadas usando BCrypt
- HTTPS habilitado
- Validação de entrada usando FluentValidation

## 📊 Banco de Dados

- PostgreSQL 13
- Migrations automáticas
- Dados iniciais (seeds) para teste
- Índices otimizados para consultas frequentes

## 🧪 Testes

O projeto inclui:

- 144 testes unitários

Execute os testes com:

```bash
dotnet test Ambev.DeveloperEvaluation.sln
```

## 📝 Licença

Este projeto está sob a licença MIT.
