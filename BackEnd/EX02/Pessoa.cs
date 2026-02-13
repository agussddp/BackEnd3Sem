using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX02
{
    public class Pessoa
    {
        public string nome = "Giulia";

        public int idade = 17;

        public void ExibirDados()
        {
            Console.WriteLine($"Nome {nome}  idade {idade}");
            
            
            
        }

    }
}