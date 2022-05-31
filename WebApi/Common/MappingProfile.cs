using AutoMapper;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Entities;

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