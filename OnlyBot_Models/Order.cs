using OnlyBot_Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string? OrderParams { get; set; }
        public string? Description { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public List<Bot>? Bots { get; set; }
    }
}