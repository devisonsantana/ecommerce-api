using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models;

public class Image : BaseModel
{
    public string Url { get; private set; } = string.Empty;

    private Image() { }

    public Image(string url)
    {
        Url = url;
    }
}