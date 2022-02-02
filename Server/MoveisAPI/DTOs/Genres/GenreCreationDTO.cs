using MoveisAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoveisAPI.DTOs
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(50)]
        [FristLetterUppercase]
        public string? Name { get; set; }
    }
}
