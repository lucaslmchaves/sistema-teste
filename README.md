Com certeza. Aqui está o código Markdown completo, pronto para você copiar e colar diretamente no seu arquivo `README.md` (o que está na pasta raiz `SISTEMA-TESTE-DEV-GREGPAY`).

````markdown
# Desafio de Avaliação DEV - GregPay (Sistema de Funcionários)

Projeto de avaliação para a equipe de desenvolvimento, focado na criação de um sistema CRUD (Criar, Ler, Atualizar, Deletar) para gerenciamento de Funcionários.

O sistema é construído em uma arquitetura de dois projetos:
* `/backend`: Uma API REST em .NET 9 para gerenciar os dados.
* `/frontend`: Um aplicativo Web Razor para a interface do usuário.

## 🚀 Tecnologias Utilizadas

* **.NET 9**
* **C#** (Minimal APIs e Razor Pages)
* **API REST**
* **Entity Framework Core**
* **SQL Server**
* **Swagger (Swashbuckle)**

## 📋 Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0)
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou outra instância)
* **Ferramenta `dotnet-ef`:**
    ```bash
    dotnet tool install --global dotnet-ef
    ```

## ⚙️ Como Executar o Projeto

A solução está dividida em dois projetos (`backend` e `frontend`) e precisa que **ambos** sejam executados simultaneamente.

**Nota:** Para simplificar o desenvolvimento local, ambos os projetos estão configurados para rodar em **HTTP** (não-seguro).

### 1. Configurar o Banco de Dados

1.  Abra o arquivo `backend/appsettings.json`.
2.  Localize a `ConnectionStrings` e atualize o valor `Server` pelo nome da sua instância do SQL Server (ex: `Server=localhost\\SQLEXPRESS`).
3.  No terminal, na pasta raiz do projeto, navegue até o backend:
    ```bash
    cd backend
    ```
4.  Rode o comando para criar o banco de dados (`GregPayTesteDB`) e as tabelas:
    ```bash
    dotnet ef database update
    ```
5.  Volte para a pasta raiz:
    ```bash
    cd ..
    ```

### 2. Executar a Solução (2 Terminais)

Você precisará de **dois terminais** abertos, ambos na pasta raiz (`SISTEMA-TESTE-DEV-GREGPAY`).

**Terminal 1 (Backend - API):**
```bash
# Na pasta raiz do projeto
cd backend
dotnet run
````

*A API estará rodando (ex: `http://localhost:5270`)*

**Terminal 2 (Frontend - WebApp):**

```bash
# Na pasta raiz do projeto (em um NOVO terminal)
cd frontend
dotnet run
```

*O site estará rodando (ex: `http://localhost:5027`)*

### 3\. Acessar o Sistema

  * **Site (Frontend):** Abra a URL do Terminal 2 (ex: `http://localhost:5027`).
  * **Documentação da API (Backend):** Abra a URL da API (do Terminal 1) e adicione `/swagger` (ex: `http://localhost:5270/swagger`).

## 📝 Endpoints da API

A API gerencia um CRUD de **Funcionários** e seus **Filhos**:

#### Funcionários

  * `GET /api/funcionarios`: Lista todos os funcionários (permite filtros por `nome` e `departamento`).
  * `GET /api/funcionarios/{id}`: Obtém um funcionário e seus filhos.
  * `POST /api/funcionarios`: Cria um novo funcionário.
  * `PUT /api/funcionarios/{id}`: Atualiza os dados de um funcionário.
  * `DELETE /api/funcionarios/{id}`: Desativa um funcionário (Soft Delete).

#### Filhos

  * `POST /api/funcionarios/{id}/filhos`: Adiciona um novo filho a um funcionário.
  * `PUT /api/filhos/{filhoId}`: Atualiza um filho existente.
  * `DELETE /api/filhos/{filhoId}`: Exclui um filho.

## 📬 Instruções de Envio do Teste

Conforme a especificação:

1.  **Script do Banco:** Gere o script de criação do banco e salve-o na pasta `/database/` (a pasta está vazia por padrão).
    ```bash
    # Rode este comando de dentro da pasta /backend
    dotnet ef migrations script -i -o ../database/script_criacao.sql
    ```

<!-- end list -->
