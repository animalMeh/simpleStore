using SimpleStore.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleStore.Models.Product
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get;  set; }

        [Required]
        public ProductCategory Category { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Product obj1)
            {
                return obj1.Id == this.Id && obj1.Name == this.Name && obj1.Category == this.Category;
            }
            else
            {
                return false;
            }
        }
    }
}
