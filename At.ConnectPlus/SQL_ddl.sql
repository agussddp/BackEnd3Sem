CREATE DATABASE ConnectPlus;

CREATE TABLE TipoContato(
	IdTipoUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
	Titulo NVARCHAR(100) NOT NULL
);


CREATE TABLE Contato(
    Idusuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
    Nome                NVARCHAR    (100) NOT NULL,         
    FormaDeContato      NVARCHAR    (250) NOT NULL UNIQUE,                 
    Imagem              NVARCHAR    (400),                            
    IdTipoUsuario       UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TipoContato(IdTipoUsuario)
);