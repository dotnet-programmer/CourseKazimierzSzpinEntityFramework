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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [Attributes] (
        [AttributeId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Attributes] PRIMARY KEY ([AttributeId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [Customers] (
        [CustomerId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Nip] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [Products] (
        [ProductId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [Addresses] (
        [AddressId] int NOT NULL IDENTITY,
        [State] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [Street] nvarchar(max) NOT NULL,
        [PostalCode] nvarchar(max) NOT NULL,
        [CustomerId] int NOT NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressId]),
        CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [Invoices] (
        [InvoiceId] int NOT NULL IDENTITY,
        [Number] int NOT NULL,
        [Year] int NOT NULL,
        [Month] tinyint NOT NULL,
        [Type] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [IsPaid] bit NOT NULL,
        [CustomerId] int NOT NULL,
        CONSTRAINT [PK_Invoices] PRIMARY KEY ([InvoiceId]),
        CONSTRAINT [FK_Invoices_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [AttributeProduct] (
        [AttributesAttributeId] int NOT NULL,
        [ProductsProductId] int NOT NULL,
        CONSTRAINT [PK_AttributeProduct] PRIMARY KEY ([AttributesAttributeId], [ProductsProductId]),
        CONSTRAINT [FK_AttributeProduct_Attributes_AttributesAttributeId] FOREIGN KEY ([AttributesAttributeId]) REFERENCES [Attributes] ([AttributeId]) ON DELETE CASCADE,
        CONSTRAINT [FK_AttributeProduct_Products_ProductsProductId] FOREIGN KEY ([ProductsProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE TABLE [InvoicePositions] (
        [InvoicePositionId] int NOT NULL IDENTITY,
        [Quantity] int NOT NULL,
        [InvoiceId] int NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_InvoicePositions] PRIMARY KEY ([InvoicePositionId]),
        CONSTRAINT [FK_InvoicePositions_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([InvoiceId]) ON DELETE CASCADE,
        CONSTRAINT [FK_InvoicePositions_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'Email', N'IsDeleted', N'Name', N'Nip', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] ON;
    EXEC(N'INSERT INTO [Customers] ([CustomerId], [Email], [IsDeleted], [Name], [Nip], [PhoneNumber])
    VALUES (1, N''jakis.mail@poczta.pl'', CAST(0 AS bit), N''Maciek Kowalski'', N''111-222-33-44'', N''1234567890'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'Email', N'IsDeleted', N'Name', N'Nip', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Customers]'))
        SET IDENTITY_INSERT [Customers] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AddressId', N'City', N'CustomerId', N'PostalCode', N'State', N'Street') AND [object_id] = OBJECT_ID(N'[Addresses]'))
        SET IDENTITY_INSERT [Addresses] ON;
    EXEC(N'INSERT INTO [Addresses] ([AddressId], [City], [CustomerId], [PostalCode], [State], [Street])
    VALUES (1, N''Kielce'', 1, N''12-345'', N''Polska'', N''Warszawska 66/6'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AddressId', N'City', N'CustomerId', N'PostalCode', N'State', N'Street') AND [object_id] = OBJECT_ID(N'[Addresses]'))
        SET IDENTITY_INSERT [Addresses] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE INDEX [IX_AttributeProduct_ProductsProductId] ON [AttributeProduct] ([ProductsProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE INDEX [IX_InvoicePositions_InvoiceId] ON [InvoicePositions] ([InvoiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE INDEX [IX_InvoicePositions_ProductId] ON [InvoicePositions] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    CREATE INDEX [IX_Invoices_CustomerId] ON [Invoices] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822160854_InitMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230822160854_InitMigration', N'7.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822161813_AddDescriptionInAttribute')
BEGIN
    ALTER TABLE [Attributes] ADD [Description] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230822161813_AddDescriptionInAttribute')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230822161813_AddDescriptionInAttribute', N'7.0.10');
END;
GO

COMMIT;
GO

