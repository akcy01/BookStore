using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.CreateGenre
{
    public class CreateGenreCommand
    {
        //genre yaratmak için bir modele ihtiyacımız var.
        public CreateGenreModel Model {get; set;}
        private readonly BookStoreDbContext _context;
        public CreateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            //Aynı isimde genre var mı onu kontrol ediyoruz burda.
            var genre = _context.Genres.SingleOrDefault(x=> x.Name == Model.Name);
            if(genre is not null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut ! ");
            }
            else
            {
                genre = new Genre();
                genre.Name = Model.Name;
                _context.Genres.Add(genre);
                _context.SaveChanges();
            }
        }


    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }

}