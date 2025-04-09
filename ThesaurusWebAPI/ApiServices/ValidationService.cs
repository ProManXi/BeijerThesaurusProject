using ThesaurusLibrary.IServices;
using ThesaurusWebAPI.DTOs;
using ThesaurusWebAPI.IApiServices;
using ThesaurusWebAPI.Models;

namespace ThesaurusWebAPI.ApiServices
{
    public class ValidationService : IValidationService
    {
        private readonly ICacheService _cache;

        public ValidationService(ICacheService cache)
        {
            _cache = cache;
        }

        public ValidationResponse ValidateAddRequest(AddWordDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Word))
                return ValidationResponse.Fail("Word cannot be empty.");

            if (request.Synonyms == null || !request.Synonyms.Any())
                return ValidationResponse.Fail("Synonyms list cannot be empty.");

            if (_cache.CaheContains(request.Word))
                return ValidationResponse.Fail("Word already exists.");

            if (request.Synonyms.Any(s => string.IsNullOrWhiteSpace(s)))
                return ValidationResponse.Fail("Synonyms cannot contain empty values.");

            if (request.Synonyms.Contains(request.Word, StringComparer.OrdinalIgnoreCase))
                return ValidationResponse.Fail("Word cannot be its own synonym.");

            return ValidationResponse.Success();
        }
    }
}
