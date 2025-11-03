IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251103150950_CriarTabelasFuncionariosEFilhos'
)
BEGIN
    CREATE TABLE [Funcionarios] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Cpf] nvarchar(max) NOT NULL,
        [Departamento] nvarchar(max) NOT NULL,
        [Salario] decimal(10,2) NOT NULL,
        [DataNascimento] datetime2 NOT NULL,
        [Ativo] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_Funcionarios] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251103150950_CriarTabelasFuncionariosEFilhos'
)
BEGIN
    CREATE TABLE [Filhos] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Cpf] nvarchar(max) NOT NULL,
        [DataNascimento] datetime2 NOT NULL,
        [FuncionarioId] int NOT NULL,
        CONSTRAINT [PK_Filhos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Filhos_Funcionarios_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [Funcionarios] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251103150950_CriarTabelasFuncionariosEFilhos'
)
BEGIN
    CREATE INDEX [IX_Filhos_FuncionarioId] ON [Filhos] ([FuncionarioId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251103150950_CriarTabelasFuncionariosEFilhos'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251103150950_CriarTabelasFuncionariosEFilhos', N'9.0.10');
END;

COMMIT;
GO

