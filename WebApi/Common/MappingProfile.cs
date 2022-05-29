using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Common
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                //Source => CreateBookModel Target => Book!!!
                CreateMap<CreateBookModel,Book>(); //CreateBookModel objesi Book'a maplenebilir olsun demek bu.
                
                CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

                CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }

    }

}