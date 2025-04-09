using System.ComponentModel.DataAnnotations;

namespace ThesaurusWebAPI.DTOs
{
    public class GetSynonymDTO
    {
        [Required(ErrorMessage = "Word is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string Word { get; set; }
    }
}
