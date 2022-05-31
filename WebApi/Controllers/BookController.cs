using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.Application.BookOperations.DeleteBook;

namespace WebApi.AddControllers
{
        [ApiController]
        [Route("[controller]")]
        
        public class BookController: ControllerBase
        {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id = 1,
        //         Title = "Dark Blood",
        //         GenreId = 1,
        //         PageCount = 200,
        //         PublishDate = new DateTime(2001,06,12)
        //     },

        //     new Book{
        //         Id = 2,
        //         Title = "White River Burn",
        //         GenreId = 2,
        //         PageCount = 432,
        //         PublishDate = new DateTime(2009,03,11)
        //     },

        //     new Book{
        //         Id = 3,
        //         Title = "Amok Runner",
        //         GenreId = 2,
        //         PageCount = 110,
        //         PublishDate = new DateTime(2004,01,10)
        //     }
        // };

        [HttpGet]
            
            public IActionResult GetBooks(){
                
                GetBooksQuery query = new GetBooksQuery(_context,_mapper);
                var result = query.Handle();
                return Ok();
            }

            [HttpGet("{id}") ]
            
            public IActionResult GetById(int id){

                BookDetailViewModel result;
                
              
                    GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                    query.BookId = id;
                    GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                    validator.ValidateAndThrow(query);
                    result = query.Handle();
             
                    return Ok(result);
            }

            /* Bunlar get idi.Şimdi put ve post kaldı. */

            [HttpPost] //eklediğin kısım

            public IActionResult AddBook([FromBody] CreateBookModel newBook) //void veya geri dönüslü kullanılmıyor burda.
            {
                    CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                    command.Model = newBook;
                    //Model hazır olduğunda validasyonun yapılması gerekiyor.Yani handle()'dan önce validasyonu yapmamız gerekiyor.
                    CreateBookCommandValidator validator = new CreateBookCommandValidator();
                    validator.ValidateAndThrow(command); //bu şekilde kullanıcı kısmında hatayı yakaladık ve kullanıcıya hatayı dönderdik! Burdan catch kısmına gitti yanlış bir giriş yapıldığında kullanıcının gördüğü kısımda kullanıcı hatları da görecek.
                    command.Handle();

                    return Ok();
            }

            [HttpPut] //güncellediğin yer

            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {
               
                    UpdateBookCommand command = new UpdateBookCommand(_context);
                    command.BookId = id;
                    command.Model = updatedBook;
                    
                    UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                    validator.ValidateAndThrow(command);
                    command.Handle();
            
                    return Ok();
            }

            [HttpDelete("{id}")]

            public IActionResult DeleteBook(int id)
            {
               
                    DeleteBookCommand command = new DeleteBookCommand(_context);
                    command.BookId = id;
                    DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                    validator.ValidateAndThrow(command);
                    command.Handle();
              
                     return Ok();
            }

        
    }
}