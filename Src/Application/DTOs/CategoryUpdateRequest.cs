using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public record CategoryUpdateRequest([Required] string Name);