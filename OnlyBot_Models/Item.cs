using OnlyBot_Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public string? ImgLink { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        public ItemTypeEnum Type { get; set; }
        public string? SubType { get; set; }
        public Inventory? Inventory { get; set; }
        public Guid InventoryId { get; set; }
    }
}
