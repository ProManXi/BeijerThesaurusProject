using System.ComponentModel.DataAnnotations;
using ThesaurusWebAPI.DTOs;
using ThesaurusWebAPI.Models;

namespace ThesaurusWebAPI.IApiServices
{
    public interface IValidationService
    {
        ValidationResponse ValidateAddRequest(AddWordDTO request);
    }
}
