using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery> //Bu GetGenreDetailQuery'nin validatoru demek bu.
    {
            public GetGenreDetailQueryValidator()
            {
                RuleFor(query => query.GenreId).GreaterThan(0);
            }
    }

}