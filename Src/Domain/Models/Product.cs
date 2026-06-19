using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Models;

public class Product : BaseModel
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public decimal OriginalPrice { get; private set; }
    public int DiscountPercentage { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public Availability Availability { get; private set; }
    public Gender Gender { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFeatured { get; private set; }
    public string CoverImage { get; private set; } = string.Empty;
    public virtual Brand Brand { get; private set; }
    public virtual ICollection<Image> Images { get; private set; } = [];
    public virtual ICollection<Category> Categories { get; private set; } = [];

    private Product() { }
}