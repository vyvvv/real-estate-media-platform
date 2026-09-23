using FluentValidation;
using RealEstateMediaPlatform.API.DTOs.ListingCase;

namespace RealEstateMediaPlatform.API.Validators
{
    public class ListingCaseCreateValidator:AbstractValidator<ListingCaseCreateRequestDto>
    {

        public ListingCaseCreateValidator()
        {
            RuleFor(x => x.Title).Length(1, 50).When(x => !string.IsNullOrEmpty(x.Title)).WithMessage("Title must be between 1 and 50 characters");
            RuleFor(x => x.Description).MaximumLength(1000).When(x=>!string.IsNullOrEmpty(x.Description)).WithMessage("Description cannot exceed 1000 characters");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to zero").LessThan(10000000000).WithMessage("no greater than 10000000000");
        }

    }
}
