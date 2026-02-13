using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX06
{
    public class Pessoa
    {
         public string nome { get; set; }

    // Método sem parâmetro
    public void Apresentar()
    {
        Console.WriteLine($"Ola, meu nome e {nome}.");
    }

    // Sobrecarga do método com sobrenome
    public void Apresentar(string sobrenome)
    {
        Console.WriteLine($"Ola, meu nome e {nome} {sobrenome}.");
    }
    }
}