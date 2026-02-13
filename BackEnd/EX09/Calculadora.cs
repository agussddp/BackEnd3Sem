using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EX09
{
    public class Calculadora
    {
        public static double Somar(double a, double b)
    {
        return a + b;
    }

    public static double Subitrair(double a, double b)
    {
        return a - b;
    }

    public static double Multiplicar(double a, double b)
    {
        return a * b;
    }

    public static double Dividir(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Erro: Divisão por zero não é permitida.");
            return double.NaN; 
        }

        return a / b;

        
    }
    }
}