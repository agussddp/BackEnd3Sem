using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX03
{
    public class Pessoa
    {
        public string nome = "";
        public int idade = 0;

        public void ExibirDados(){
            Console.WriteLine($"qual o seu nome");
            nome = Console.ReadLine();
            Console.WriteLine($"qual a sua idade");
            idade = int.Parse(Console.ReadLine());
            

            if (idade >= 0)
            {
                Console.WriteLine($"Olá {nome}, você tem {idade} anos.");

            }
            else
            {
                Console.WriteLine($"{idade} inválida");
            }
        }
       
       
        
    }
}