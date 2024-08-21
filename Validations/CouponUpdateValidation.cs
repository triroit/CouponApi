using CouponApi.Models.DTO;
using FluentValidation;

namespace CouponApi.Validations
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDto>
    {
        public CouponUpdateValidation() 
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
            RuleFor(model => model.IsActive).Must(isActive => isActive == true || false);
        }
    }
}
