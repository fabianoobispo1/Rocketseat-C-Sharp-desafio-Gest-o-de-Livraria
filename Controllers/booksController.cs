using Livraria.Communication.Requests;
using Livraria.Communication.Responses;
using Microsoft.AspNetCore.Mvc;


namespace Livraria.Controllers;
[Route("bookstore/[controller]")]
[ApiController]
public class booksController : ControllerBase
{
    List<Book> books = new List<Book>();


    [HttpPost]
    [ProducesResponseType(typeof(ResponseBookRegisterJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult createBook([FromBody] RequestBookRegisterJson resquest)
    {
        int ultId = 1;
        foreach (var book in books)
        {
            if (book.Id > ultId)
            { ultId = book.Id; }
        }

        books.Add(new Book
        {
            Id = ultId + 1,
            Titulo = resquest.Titulo,
            Autor = resquest.Autor,
            Genero = resquest.Genero,
            Preco = resquest.Preco,
            Quantidade = resquest.Quantidade
        });

        var response = new ResponseBookRegisterJson
        {
            Id = ultId,
            Titulo = resquest.Titulo
        };

        return Created(string.Empty, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult getBook()
    {
        return Ok(books);
    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult updateBook([FromBody] RequestBookUpdateJson request, [FromRoute] int id)
    {
        int index = -1;
        foreach (var book in books)
        {
            if (book.Id == id)
            {
                index = books.IndexOf(book);
            }
        }
        if (index >= 0)
        {
            books[index] = new Book
            {
                Id = id,
                Autor = request.Autor,
                Genero = request.Genero,
                Preco = request.Preco,
                Quantidade = request.Quantidade,
                Titulo = request.Titulo
            };

            return NoContent();
        }
        return BadRequest("Id inválido");

    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult deleteBook([FromRoute] int id)
    {

        int index = -1;
        foreach (var book in books)
        {
            if (book.Id == id)
            {
                index = books.IndexOf(book);
            }
        }

        if (index >= 0)
        {
            books.RemoveAt(index);
            return NoContent();
        }

        return BadRequest("Id inválido");

    }
}