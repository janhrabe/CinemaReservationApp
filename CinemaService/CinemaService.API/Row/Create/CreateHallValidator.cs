using FluentValidation;

namespace CinemaService.API.Row.Create;

public class CreateHallValidator : Validator<CreateRowRequest>
{
    public CreateHallValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Row number is required!");

        RuleFor(x => x.HallId)
            .NotEmpty()
            .WithMessage("Hall Id is required!");
    }
}
