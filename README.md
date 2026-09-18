# Locadora de Veículos - API

API REST desenvolvida em C# com ASP.NET Core para gerenciamento de uma locadora de veículos.

O projeto foi desenvolvido como parte da disciplina de Desenvolvimento de Aplicações Web / TADS, utilizando Entity Framework Core para o acesso ao banco de dados SQL Server.

## 📋 Sobre o projeto

A API permite o gerenciamento das principais informações de uma locadora de veículos, incluindo:

- Veículos
- Fabricantes
- Clientes
- Categorias de veículos
- Aluguéis
- Pagamentos

O sistema possui operações de cadastro, consulta, atualização e exclusão dos dados por meio de endpoints HTTP.

## 🛠️ Tecnologias utilizadas

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- Git e GitHub

## 🗂️ Estrutura do projeto

```text
LocadoraVeiculos.API
│
├── Controllers
│   ├── AlugueisController.cs
│   ├── CategoriasVeiculoController.cs
│   ├── ClientesController.cs
│   ├── FabricantesController.cs
│   ├── FiltrosController.cs
│   ├── PagamentosController.cs
│   └── VeiculosController.cs
│
├── Data
│   └── AppicationContext.cs
│
├── Migrations
│
├── Models
│   ├── Aluguel.cs
│   ├── CategoriaVeiculo.cs
│   ├── Cliente.cs
│   ├── Fabricante.cs
│   ├── Pagamento.cs
│   └── Veiculo.cs
│
├── Program.cs
└── appsettings.json
