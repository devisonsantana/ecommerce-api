using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public virtual ICollection<Product> Products { get; private set; } = [];

    private Category() { }

    public Category(string name)
    {
        Name = name.ToLower();
    }

    public void UpdateName(string name)
    {
        Name = name.ToLower();
    }
}