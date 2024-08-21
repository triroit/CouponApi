using CouponApi.Models.DTO;
using FluentValidation;

namespace CouponApi.Validations
{
    public class CouponCreateValidation : AbstractValidator<CouponCreateDto>
    {
        public CouponCreateValidation() 
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }
    }
}
