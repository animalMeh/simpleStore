using SimpleStore.Models.Enums;

namespace SimpleStore.Models.Product
{
    public class Product
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public ProductCategory Category { get; private set; }

        public Product(int id, string name, ProductCategory category)
        {
            if(id > 0 && !string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name) && category != ProductCategory.None)
            {
                this.Id = id;
                this.Name = name;
                this.Category = category;
            }
        }
    }
}
