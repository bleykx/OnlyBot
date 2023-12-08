using OnlyBot_Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BotId { get; set; }
        public Bot? Bot { get; set; }
        public int? Kamas { get; set; }
        public int? Pods { get; set; }
        public int? MaxPods { get; set; }
        public int? SlotsMax { get; set; }
        public int? SlotsUsed { get; set; }
        public List<Item>? Items { get; set; }
        public InventoryTypeEnum Type { get; set; }
    }
}
