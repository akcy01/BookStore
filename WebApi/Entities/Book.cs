using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{

    public class Book
    {
        //Id'yi increment yapmalıyız
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Id'yi otomatik verecek artık.O yüzden generator da Id vermemize gerek yok.
        public int Id { get ; set ; }
        public string Title {get; set;}
        public int GenreId {get; set;}
        public int PageCount {get; set;}

        public DateTime PublishDate{get; set;}

    }

}