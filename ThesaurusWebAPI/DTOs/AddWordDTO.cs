using System.ComponentModel.DataAnnotations;

namespace ThesaurusWebAPI.DTOs
{
    public class AddWordDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets allowed.")]
        public string Word { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one synonym is required.")]
        public List<string> Synonyms { get; set; }
    }
}
