using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX04
{
    public class Pessoa
    {
        public string Nome { get; set; }
    public int Idade { get; set; }

   
    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

   
    public void ExibirDados()
    {
        Console.WriteLine($"Nome: {Nome}");
     
        Console.WriteLine($"Idade: {Idade}");
    }
    }
}