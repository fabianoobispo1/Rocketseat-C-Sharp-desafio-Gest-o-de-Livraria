namespace Livraria;

public class Book
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int Quantidade { get; set; }

}
