using OnlyBot_Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlyBot_Models
{
    public class Script
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public ScriptTypeEnum Type { get; set; }
        public List<Bot>? Bots { get; set; }
    }
}