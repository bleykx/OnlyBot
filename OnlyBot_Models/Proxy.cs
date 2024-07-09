using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Proxy
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter Name..")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter IP..")]
        public string? IP { get; set; }
        [Required(ErrorMessage = "Please enter Port..")]
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter Socket Type..")]
        public string? SocketType { get; set; }
        public bool IsBanned { get; set; }
        [Required(ErrorMessage = "Please enter an Expiration Date..")]
        public DateTime? PlanExpirationDate { get; set; }
        public List<Bot> Bots { get; set; }
        public string? Provider { get; set; }
    }
}