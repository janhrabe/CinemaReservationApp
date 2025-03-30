using FluentValidation;

namespace CinemaService.API.Halls.Update
{
    public class UpdateHallValidator : Validator<UpdateHallRequest>
    {
        public UpdateHallValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Hall status out of range");
        }
    }
}
