using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public record CategoryCreateRequest
{
    [Required]
    public string Name
    {
        get;
        init => field = value.ToLower();
    } = null!;
}