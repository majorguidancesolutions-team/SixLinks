using System.ComponentModel.DataAnnotations;

namespace MyDataModels
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<Item> Items { get; set; } = new List<Item>();

        public override string ToString()
        {
            return Name;
        }
    }
}