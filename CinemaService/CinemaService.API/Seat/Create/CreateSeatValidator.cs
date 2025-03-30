using FluentValidation;

namespace CinemaService.API.Seat.Create;

public class CreateSeatValidator : Validator<CreateSeatRequest>
{
    public CreateSeatValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Seat number must be provided!");

        RuleFor(x => x.RowId)
            .NotEmpty()
            .WithMessage("Row Id is required!");
    }
}
