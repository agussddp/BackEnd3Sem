using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX05
{
    public class Funcionario : Pessoa
    {
        public double Salario { get; set; }

    public Funcionario(string nome, int idade, double salario)
        : base(nome, idade)
    {
        Salario = salario;
    }

    public  void ExibirDados()
    {
        base.ExibirDados();
        Console.WriteLine($"Salário: {Salario:C}");
    }
}
}