# Desafio de Avaliação DEV - GregPay

Projeto de avaliação para a equipe de desenvolvimento, focado na criação de uma API REST simples para [descreva o objetivo do teste, ex: "gerenciamento de clientes"].

## 🚀 Tecnologias Utilizadas

Este projeto foi desenvolvido com as seguintes tecnologias, alinhadas com a vaga de Estágio DEV:

* **.NET 9** 
* [cite_start]**C#** 
* [cite_start]**API REST** 
* [cite_start]**Entity Framework Core** 
* [cite_start]**SQL Server** 

## 📋 Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) 

## ⚙️ Como Executar o Projeto

Siga os passos abaixo para rodar a aplicação localmente:

1.  **Clone o repositório:**
    ```bash
    git clone [URL-DO-SEU-REPO-PRIVADO]
    cd [NOME-DO-SEU-REPO]
    ```

2.  **Configure a Conexão com o Banco (appsettings.json):**
    * Abra o arquivo `appsettings.json` e atualize a `ConnectionString` para apontar para o seu banco de dados SQL Server.

3.  **Aplique as Migrations (Entity Framework):**
    * Se estiver usando o EF, rode o comando abaixo no terminal dentro da pasta do projeto:
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```

5.  **Acesse a API:**
    * A aplicação estará disponível em `http://localhost:5000` (ou a porta que você configurou).
    * Acesse `http://localhost:5000/swagger` para ver a documentação dos endpoints.

## 📝 Endpoints da API

* `GET /api/Recurso` - Lista todos os itens.
* `GET /api/Recurso/{id}` - Obtém um item por ID.
* `POST /api/Recurso` - Cria um novo item.
* `PUT /api/Recurso/{id}` - Atualiza um item existente.
* `DELETE /api/Recurso/{id}` - Deleta um item.
