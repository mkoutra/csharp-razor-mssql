USE Students6DB;
GO

CREATE TABLE [dbo].[Students] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50) NOT NULL,
    [Lastname]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE INDEX [IX_Students_Lastname] ON [dbo].[Students] ([Lastname])

--- Insert Dummy Data
INSERT INTO Students (Firstname, Lastname)
VALUES
	('Nikos', 'Papadopoulos'),
	('Petros', 'Alexopoulos'),
	('Eleni', 'Alexandri');