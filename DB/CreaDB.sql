/************************/
/* ELIMINAZIONE TABELLE */
/************************/

-- Drop the table 'FornitoriPwdPlainText' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'FornitoriPwdPlainText'
)
    DROP TABLE dbo.FornitoriPwdPlainText
GO

-- Drop the table 'AdminPwdPlainText' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'AdminPwdPlainText'
)
    DROP TABLE dbo.AdminPwdPlainText
GO

-- Drop the table 'ClientiPwdPlainText' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'ClientiPwdPlainText'
)
    DROP TABLE dbo.ClientiPwdPlainText
GO

-- Drop the table 'Carrello' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Carrello'
)
    DROP TABLE dbo.Carrello
GO

-- Drop the table 'DettaglioOrdini' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'DettaglioOrdini'
)
    DROP TABLE dbo.DettaglioOrdini
GO


-- Drop the table 'Prodotti' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Prodotti'
)
    DROP TABLE dbo.Prodotti
GO

-- Drop the table 'Categorie' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Categorie'
)
    DROP TABLE dbo.Categorie
GO

-- Drop the table 'Fornitori' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Fornitori'
)
    DROP TABLE dbo.Fornitori
GO

-- Drop the table 'Ordini' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Ordini'
)
    DROP TABLE dbo.Ordini
GO


-- Drop the table 'Admin' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Admin'
)
    DROP TABLE dbo.Admin
GO

-- Drop the table 'Clienti' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Clienti'
)
    DROP TABLE dbo.Clienti
GO

-- Drop the table 'Carte' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Carte'
)
    DROP TABLE dbo.Carte
GO

-- Drop the table 'TipiCarte' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'TipiCarte'
)
    DROP TABLE dbo.TipiCarte
GO

-- Drop the table 'Citta' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Citta'
)
    DROP TABLE dbo.Citta
GO

-- Drop the table 'Province' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Province'
)
    DROP TABLE dbo.Province
GO

-- Drop the table 'Regioni' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Regioni'
)
    DROP TABLE dbo.Regioni
GO

-- Drop the table 'Nazioni' in schema 'dbo'
IF EXISTS (
    SELECT *
        FROM sys.tables
        JOIN sys.schemas
            ON sys.tables.schema_id = sys.schemas.schema_id
    WHERE sys.schemas.name = N'dbo'
        AND sys.tables.name = N'Nazioni'
)
    DROP TABLE dbo.Nazioni
GO

/*********************/
/* CREAZIONE TABELLE */
/*********************/

CREATE TABLE [dbo].[Nazioni] /*Insert Ok*/
(
	[IdNazione] INT IDENTITY (1, 1) NOT NULL,
	[DescNazione] NVARCHAR(50) NOT NULL,
	[ValNazione] CHAR(1) NOT NULL,
	PRIMARY KEY ([IdNazione])
);

GO

CREATE TABLE [dbo].[Regioni] /*Insert Ok*/
(
	[IdRegione] INT IDENTITY (1, 1) NOT NULL,
	[DescRegione] NVARCHAR(50) NOT NULL,
	[IdNazione] INT NOT NULL,
	[ValRegione] CHAR(1) NOT NULL, 
	PRIMARY KEY ([IdRegione]),
    CONSTRAINT [FK_Regioni_ToNazioni] FOREIGN KEY ([IdNazione]) REFERENCES [Nazioni]([IdNazione]),
);

GO

CREATE TABLE [dbo].[Province] /*Insert Ok*/
(
	[IdProvincia] INT IDENTITY (1, 1) NOT NULL,
	[DescProvincia] NVARCHAR(50) NOT NULL,
	[IdRegione] INT NOT NULL,
	[ValProvincia] CHAR(1) NOT NULL, 
	PRIMARY KEY ([IdProvincia]),
    CONSTRAINT [FK_Province_ToRegioni] FOREIGN KEY ([IdRegione]) REFERENCES [Regioni]([IdRegione]),
);

GO

CREATE TABLE [dbo].[Citta] /*Insert Ok*/
(
    [IdCitta]     INT           IDENTITY (1, 1) NOT NULL,
    [DescCitta]   NVARCHAR (50) NOT NULL,
    [CAPCitta]    INT           NOT NULL,
    [IdProvincia] INT           NOT NULL,
    [ValCitta]    CHAR (1)      NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCitta] ASC),
    CONSTRAINT [FK_Citta_ToProvince] FOREIGN KEY ([IdProvincia]) REFERENCES [dbo].[Province] ([IdProvincia])
);

GO

CREATE TABLE [dbo].[TipiCarte] /*Insert Ok*/
(
	[IdTipoCarte] INT IDENTITY (1, 1) NOT NULL,
	[DescTipoCarte] NVARCHAR(50) NOT NULL,
	[ValTipoCarte] CHAR(1) NOT NULL,
	PRIMARY KEY ([IdTipoCarte])
);

GO

CREATE TABLE [dbo].[Carte] /*Insert Ok*/
(
	[IdCarta] INT IDENTITY (1, 1) NOT NULL,
	[CodiceCarta] NVARCHAR(32) NOT NULL ,
	[IdTipoCarta] INT NOT NULL,
	[IdCliente] INT NOT NULL,
	[ValCarta] CHAR(1) NOT NULL,
	PRIMARY KEY ([IdCarta]),
    CONSTRAINT [FK_UC_CodiceCarta] UNIQUE ([CodiceCarta]),
    CONSTRAINT [FK_Carte_ToTipiCarte] FOREIGN KEY ([IdTipoCarta]) REFERENCES [dbo].[TipiCarte] ([IdTipoCarte]),
    CONSTRAINT [FK_Carte_ToClienti] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clienti] ([IdCliente])
);

GO

CREATE TABLE [dbo].[Clienti] /*Insert Ok*/
(
    [IdCliente]         INT             IDENTITY (1, 1) NOT NULL,
    [NomeCliente]       NVARCHAR(50)    NOT NULL,
    [CognomeCliente]    NVARCHAR(50)    NOT NULL,
    [DataNascitaCliente]    DATE    NOT NULL,
    [TelefonoCliente]   CHAR (10)       NOT NULL,
    [MailCliente]       NVARCHAR (50)   NOT NULL,
    [UserCliente]       NVARCHAR (50)   NOT NULL,
    [PwdCliente]       NVARCHAR (32)   NOT NULL, /*Hash*/
    [ViaCliente]       NVARCHAR (50)   NOT NULL,
    [CivicoCliente]       NVARCHAR (5)   NOT NULL,
    [IdCittaClienti]           INT             NOT NULL,
    [ValCliente]         CHAR (1)        NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCliente]),
    CONSTRAINT [FK_Clienti_ToCitta] FOREIGN KEY ([IdCittaClienti]) REFERENCES [dbo].[Citta] ([IdCitta])
);

GO

CREATE TABLE [dbo].[ClientiPwdPlainText] /*Insert Ok*/
(
    [IdCliente]         INT             IDENTITY (1, 1) NOT NULL,
    [UserCliente]       NVARCHAR (50)   NOT NULL,
    [PwdCliente]       NVARCHAR (32)   NOT NULL
    PRIMARY KEY CLUSTERED ([IdCliente])
);

GO

CREATE TABLE [dbo].[Admin] /*Insert Ok*/
(
    [IdAdmin]         INT             IDENTITY (1, 1) NOT NULL,
    [NomeAdmin]       NVARCHAR(50)    NOT NULL,
    [CognomeAdmin]    NVARCHAR(50)    NOT NULL,
    [DataNascitaAdmin]    DATE    NOT NULL,
    [TelefonoAdmin]   CHAR (10)       NOT NULL,
    [MailAdmin]       NVARCHAR (50)   NOT NULL,
    [UserAdmin]       NVARCHAR (50)   NOT NULL,
    [PwdAdmin]       NVARCHAR (32)   NOT NULL, /*Hash*/
    [ViaAdmin]       NVARCHAR (50)   NOT NULL,
    [CivicoAdmin]       NVARCHAR (5)   NOT NULL,
    [IdCittaAdmin]           INT             NOT NULL,
    [ValAdmin]         CHAR (1)        NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAdmin]),
    CONSTRAINT [FK_Admin_ToCitta] FOREIGN KEY ([IdCittaAdmin]) REFERENCES [dbo].[Citta] ([IdCitta])
);

GO

CREATE TABLE [dbo].[AdminPwdPlainText] /*Insert Ok*/
(
    [IdAdmin]         INT             IDENTITY (1, 1) NOT NULL,
    [UserAdmin]       NVARCHAR (50)   NOT NULL,
    [PwdAdmin]       NVARCHAR (32)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAdmin])
);

GO


CREATE TABLE [dbo].[Ordini] /*Insert Ok*/
(
	[IdOrdine] INT IDENTITY (1, 1) NOT NULL,
	[DataOrdine] DATETIME NOT NULL,
	[DataSpedizione] DATETIME , /*Vuoto se non ancora consegnato*/
	[PrezzoTotale]        DECIMAL(19, 4)  NOT NULL,
    [IdCarta] INT NOT NULL,
    [ValOrdine] CHAR(1) NOT NULL,
	PRIMARY KEY ([IdOrdine]),
    CONSTRAINT [FK_Ordini_ToCarte] FOREIGN KEY ([IdCarta]) REFERENCES [dbo].[Carte] ([IdCarta])
);

CREATE TABLE [dbo].[Fornitori] /*Insert Ok*/
(
    [IdFornitore]   INT           IDENTITY (1, 1) NOT NULL,
    [NomeFornitore] NVARCHAR (50) NOT NULL,
    [Email]          NVARCHAR (30),       
    [Telefono]      CHAR (10),
    [UserFornitore]       NVARCHAR (50)   NOT NULL,
    [PwdFornitore]       NVARCHAR (32)   NOT NULL, /*Hash*/
    [ViaFornitore]       NVARCHAR (50)   NOT NULL,
    [CivicoFornitore]       NVARCHAR (5)   NOT NULL,
    [IdCitta]       INT           NOT NULL,
    [ValFornitore]  CHAR (1)      NOT NULL,
    PRIMARY KEY CLUSTERED ([IdFornitore] ASC),
    CONSTRAINT [FK_Fornitori_ToCitta] FOREIGN KEY ([IdCitta]) REFERENCES [dbo].[Citta] ([IdCitta])
);

GO

GO

CREATE TABLE [dbo].[FornitoriPwdPlainText] /*Insert Ok*/
(
    [IdFornitore]         INT             IDENTITY (1, 1) NOT NULL,
    [UserFornitore]       NVARCHAR (50)   NOT NULL,
    [PwdFornitore]       NVARCHAR (32)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IdFornitore])
);

GO

CREATE TABLE [dbo].[Categorie] /*Insert Ok*/
(
    [IdCategoria]           INT           IDENTITY (1, 1) NOT NULL,
    [DescrizioneCategoria]  NVARCHAR (50) NOT NULL,
    [ValCategoria]          CHAR (1)      NOT NULL,
    PRIMARY KEY ([IdCategoria] ASC),
);

GO

CREATE TABLE [dbo].[Prodotti] /*Insert Ok*/
(
    [IdProdotto]            INT             IDENTITY (1, 1) NOT NULL,
    [ModelloProdotto]       NVARCHAR (50)   NOT NULL,
    [DescrizioneProdotto]       NVARCHAR (1000)   NOT NULL,
    [MarcaProdotto]       NVARCHAR (500)   NOT NULL,
	[ImmagineProdotto]		NVARCHAR (50)	NOT NULL,/*Percorso*/
    [IdCategoria]           INT             NOT NULL,
    [IdFornitore]               INT             NOT NULL,
    [Prezzo]        DECIMAL(19, 4)  NOT NULL, /*Da Scontare*/
    [Sconto]        INT  , /*In percentuale*/
    [QtaGiacenza]           INT             NOT NULL,
    [LivRiordino]           INT             NOT NULL,
    [LottoMinimo]           INT             NOT NULL,
    [ValProdotto]           CHAR (1)        NOT NULL,
    PRIMARY KEY ([IdProdotto] ASC),
    CONSTRAINT [FK_Prodotti_ToCategorie] FOREIGN KEY ([IdCategoria]) REFERENCES [Categorie]([IdCategoria]),
    CONSTRAINT [FK_Prodotti_ToFornitori] FOREIGN KEY ([IdFornitore]) REFERENCES [Fornitori]([IdFornitore])
);

CREATE TABLE [dbo].[DettaglioOrdini] /*Insert Ok*/
(
    [IdOrdine]           INT         NOT NULL,
    [IdProdotto]            INT         NOT NULL,
    [QtaOrdine]             INT         NOT NULL,
    [PrezzoUnitario]        DECIMAL(19, 4)  NOT NULL,
    [ValDettaglioOrdini]    CHAR (1)    NOT NULL,
    PRIMARY KEY CLUSTERED ([IdOrdine], [IdProdotto]),
    CONSTRAINT [FK_DettaglioOrdini_ToOrdini] FOREIGN KEY ([IdOrdine]) REFERENCES [Ordini]([IdOrdine]),
    CONSTRAINT [FK_DettaglioOrdini_ToProdotti] FOREIGN KEY ([IdProdotto]) REFERENCES [Prodotti]([IdProdotto]),
);

CREATE TABLE [dbo].[Carrello] /*Insert Ok*/
(
    [IdCliente]           INT         NOT NULL,
    [IdProdotto]            INT         NOT NULL,
    [DataAggiunta]	       DATETIME          NOT  NULL,
    [QtaProd]             INT         NOT NULL, /*Qta di ogni singolo prodotto messo nel carrello*/
    [PrezzoUnitario]        DECIMAL(19, 4)  NOT NULL, /*Prezzo unitario * quantità nel carrello*/
    [Ordinato]        INT  NOT NULL CHECK(Ordinato = 0 OR Ordinato = 1), /*0=>Prodotto NON ancora ordinato 1=>Prodotto già ordinato*/
    [ValCarrello]    CHAR (1)    NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCliente], [IdProdotto], DataAggiunta),
    CONSTRAINT [FK_Carrello_ToCliente] FOREIGN KEY ([IdCliente]) REFERENCES [Clienti]([IdCliente]),
    CONSTRAINT [FK_Carrello_ToProdotti] FOREIGN KEY ([IdProdotto]) REFERENCES [Prodotti]([IdProdotto]),
);

GO