using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models;

public class Brand : BaseModel
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string LogoUrl { get; private set; } = string.Empty;

    private Brand() { }

    public Brand(string name, string slug, string logoUrl = "")
    {
        Name = name;
        Slug = slug;
        LogoUrl = logoUrl;
    }
}