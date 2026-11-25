using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.UserDtos;
public class UpdateDto
{
 [Required, MaxLength(120)] public string Name { get; set; } = null!;
[Required, EmailAddress, MaxLength(200)] public string Email { get; set; } = null!;
public DateTime RegistrationDate { get; set; }
}