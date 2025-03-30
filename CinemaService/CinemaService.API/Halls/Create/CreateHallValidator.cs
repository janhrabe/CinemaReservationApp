using FluentValidation;

namespace CinemaService.API.Halls.Create
{
    public class CreateHallValidator : Validator<GetHallRequest>
    {
        public CreateHallValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Hall name is required!")
                .MinimumLength(3)
                .WithMessage("Hall name is too short");
        }
    }
}
