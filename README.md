# Desafio de Avaliação DEV - GregPay

Projeto de avaliação para a equipe de desenvolvimento, focado na criação de uma API REST simples para **gerenciamento de Participantes** de um split de pagamento.

## 🚀 Tecnologias Utilizadas

Este projeto foi desenvolvido com as seguintes tecnologias, alinhadas com a vaga de Estágio DEV:

* **.NET 9**
* **C#** (usando Minimal APIs)
* **API REST**
* **Entity Framework Core**
* **SQL Server**
* **Swagger (Swashbuckle)** para documentação e teste

## 📋 Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou outra instância)
* **Ferramenta `dotnet-ef`:**
    ```bash
    dotnet tool install --global dotnet-ef
    ```

## ⚙️ Como Executar o Projeto

Siga os passos abaixo para rodar a aplicação localmente:

1.  **Clone o repositório:**
    ```bash
    git clone [URL-DO-SEU-REPO-PRIVADO]
    cd sistema-teste-dev-gregpay
    ```

2.  **Configure a Conexão com o Banco (appsettings.json):**
    * Abra o arquivo `appsettings.json`.
    * Localize a `ConnectionStrings` e substitua o valor `Server` pelo nome da sua instância do SQL Server.
    * **Importante:** Se sua instância for nomeada (ex: `SQLEXPRESS`), use barras duplas para "escapar" o caractere: `Server=NOME_DO_PC\\SQLEXPRESS`

3.  **Aplique as Migrations (Entity Framework):**
    * Rode o comando abaixo no terminal para criar o banco de dados (`GregPayTesteDB`) e a tabela (`Participantes`).
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```

5.  **Acesse a API:**
    * A aplicação estará disponível em `http://localhost:5270`.
    * Acesse **`http://localhost:5270/swagger`** para ver a documentação e testar os endpoints.

## 📝 Endpoints da API

A API gerencia um CRUD de **Participantes**:

* `GET /api/participantes`
    * Lista todos os participantes que estão **ativos**.
* `GET /api/participantes/{id}`
    * Obtém um participante específico pelo seu ID.
* `POST /api/participantes`
    * Cria um novo participante (definido como `ativo = true` por padrão).
* `PUT /api/participantes/{id}`
    * Atualiza as informações de um participante existente.
* `DELETE /api/participantes/{id}`
    * Desativa um participante (utiliza **Soft Delete**, definindo `ativo = false`).