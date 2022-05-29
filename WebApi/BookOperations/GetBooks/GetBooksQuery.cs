using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;


namespace WebApi.BookOperations.GetBooks
{

    public class GetBooksQuery
    {

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var BookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(BookList);

            // List<BooksViewModel> vm = new List<BooksViewModel>();
            // foreach (var book in BookList)
            // {
            //     vm.Add(new BooksViewModel()
            //     {
            //         Title = book.Title,                                          
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
            //         PageCount = book.PageCount
            //     });
            // }

            /* Bu mapping yani AutoMapp bizi bütün bu kod kalabalığından bu şekilde kurtarıyor işte..!
            Mapping Profileden çalışıyoruz. */

            return vm;
        }

    }


    public class BooksViewModel
    {

        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}