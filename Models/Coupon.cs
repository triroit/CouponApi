using System.ComponentModel.DataAnnotations;

namespace CouponApi.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
