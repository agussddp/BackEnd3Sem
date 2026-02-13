using EX08;

Usuario usuario = new Usuario("Giulia", "377");

Administrador administrador = new Administrador("Giuka", "admin");

Console.WriteLine("Autenticando usuário:");
usuario.Autenticar("senha123");
Console.WriteLine($"");

Console.WriteLine("Autenticando usuário:");
usuario.Autenticar("12345");
Console.WriteLine($"");


Console.WriteLine("Autenticando administrador:");
administrador.Autenticar("admin456");
Console.WriteLine($"");


Console.WriteLine("Autenticando administrador:");
administrador.Autenticar("534567");
