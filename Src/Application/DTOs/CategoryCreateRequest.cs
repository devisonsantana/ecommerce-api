using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public record CategoryCreateRequest([Required] string Name);