using OnlyBot_Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Bot
    {
        [Key]
        public Guid Id { get; set; }
        public string? AnkabotInstance { get; set; }
        [Required(ErrorMessage = "AccountName is required")]
        public string? AccountName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }
        public BreedEnum Breed { get; set; }
        public ServerEnum Server { get; set; }
        public string? BreedImgLink { get; set; }
        public List<Inventory>? Inventories { get; set; }
        public Script? Script { get; set; }
        public Guid? ScriptId { get; set; }
        [Required(ErrorMessage = "Proxy is required")]
        public Proxy? Proxy = null!;
        public Guid? ProxyId { get; set; }
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }
        public bool ScriptIsRunning { get; set; }
        public bool IsConnected { get; set; }
        public int Level { get; set; }
        public int Life { get; set; }
        public int MaxLife { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Experience { get; set; }
        public int ExperienceFloor { get; set; }
        public int ExperienceNextFloor { get; set; }
        public int ExperiencePercent { get; set; }
    }
}