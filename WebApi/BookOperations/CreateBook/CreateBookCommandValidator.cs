using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> // Burda yaptığımız şey bu validator classı createbookcommand ı valide etsin.Onun nesnelerini objelerini valide eder. demek.Validason constructor aracılığıyla çalışır o yüzden constructor oluşturman gerekir muhakkak.
    {

        //Kısacası validation kısmına kuralları belirlediğimiz yer diyebiliriz !
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0); //GenreId 0'dan büyük olmalı diyoruz
            RuleFor(command => command.Model.PageCount).GreaterThan(0);//Sayfa sayısı 0 dan büyük olmalı
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);//Çıktığı zaman şimdiki zamandan önce olmalı
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);//Kitabın ismi minimun 4 karakterde olmalı !
        }

        //Kurallarımızı belirledik şimdi bunu çalıştırmamız lazım.

    }

}