namespace CouponApi.Models.DTO
{
    public class CouponUpdateDto
    {
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
    }
}
