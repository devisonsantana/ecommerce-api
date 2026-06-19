using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models;

public class Brand : BaseModel
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string LogoUrl { get; private set; } = string.Empty;
    public virtual ICollection<Product> Products { get; private set; } = [];

    private Brand() { }

    public Brand(string name, string? logoUrl = "")
    {
        Name = name;
        Slug = SlugHelper.Generate(name);
        LogoUrl = logoUrl ?? string.Empty;
    }
}