using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {

        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);//Get yaparken aldığımız Id'nin sıfırdan büyük olmasını istiyorum
        }

    }

}