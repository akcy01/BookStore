using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                else
                {
                    context.Genres.AddRange(
                        new Genre{

                            Name = "Personal Growth"

                        },

                        new Genre{

                            Name = "Science Fiction"

                        },

                        new Genre{

                            Name = "Romance"

                        }
                    );


                    context.Books.AddRange(
                new Book{
                    // Id = 1,
                    Title = "Dark Blood",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                },

                new Book{
                    // Id = 2,
                    Title = "White River Burn",
                    GenreId = 2,
                    PageCount = 432,
                    PublishDate = new DateTime(2009,03,11)
                },

                new Book{
                    // Id = 3,
                    Title = "Amok Runner",
                    GenreId = 2,
                    PageCount = 110,
                    PublishDate = new DateTime(2004,01,10)
                }
                
                );

                context.SaveChanges(); //Eklediğimiz verileri kaydetmemiz gerekir!!

                //Bunun çalışması için program.cs'de işlemler yapmamız gerekiyor.

                // Uygulama ayağa kalktığında burası hep her zaman çalışacak.
                }
            }
        }

    }
}
