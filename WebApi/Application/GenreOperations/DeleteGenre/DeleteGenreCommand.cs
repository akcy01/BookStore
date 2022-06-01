using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.DeleteGenre
{

    public class DeleteGenreCommand
    {
        public int GenreId {get; set;}
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;

        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Id == GenreId); //Id ile işlem yaptığımız için SingleOrDefault
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü bulunamadı.");
            }
            else
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
        }
    }

}