using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models;

public class Category : BaseModel
{
    public string Name { get; private set; } = string.Empty;

    private Category() { }

    public Category(string name)
    {
        Name = name;
    }
}