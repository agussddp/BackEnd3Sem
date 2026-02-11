CREATE DATABASE EX06Retomada;

CREATE TABLE tb_categoria (
    Codigo INT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL
);

SELECT * FROM tb_categoria;


CREATE TABLE Produto (
    Codigo  INT PRIMARY KEY,
    Nome    VARCHAR(100) NOT NULL,
    Preco   DECIMAL(10, 2) NOT NULL,
    CodigoCategoria INT NOT NULL,

    FOREIGN KEY (CodigoCategoria) REFERENCES tb_categoria(Codigo)
);

SELECT * FROM Produto;
