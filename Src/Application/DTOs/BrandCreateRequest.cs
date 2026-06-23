using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public record BrandCreateRequest([Required] string Name, string? LogoUrl = "");