using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;

namespace WebApi.BookOperations.GetBookDetail
{

    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId {get; set;}


        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {

             var book = _dbContext.Books.Where(book=> book.Id == BookId).SingleOrDefault(); //_Context ile Books'a eriştik ve ordan çektik.
             if(book is null)
             {
                 throw new InvalidOperationException("Kitap Bulunamadı");
             }

                BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            //  BookDetailViewModel vm = new BookDetailViewModel();
            //  vm.Title = book.Title;
            //  vm.PageCount = book.PageCount;
            //  vm.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy");
            //  vm.Genre = ((GenreEnum)book.GenreId).ToString();
             return vm;

        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

}