using Microsoft.AspNetCore.Mvc;
using ThesaurusLibrary.IServices;
using ThesaurusWebAPI.DTOs;
using ThesaurusWebAPI.IApiServices;

namespace ThesaurusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThesaurusController : ControllerBase
    {
        private readonly ISynonymService _synonymService;
        private readonly ILoggerService _loggerService;
        private readonly IValidationService _validationService;

        public ThesaurusController(ISynonymService synonymService, ILoggerService logger, IValidationService validationService) 
        {
            _synonymService = synonymService;       // Library service to save/fetch data.
            _loggerService = logger;                // Custom logger to save details in the file.
            _validationService = validationService; // Validation service to make sure we provde all validations
        }

        #region Add word Endpoint in dictionary after Validations 
        [HttpPost("addword")]
        public IActionResult AddWord([FromBody] AddWordDTO request)
        {
            // Extra check for synonyms
            if (request.Synonyms.Any(s => string.IsNullOrWhiteSpace(s) || !s.All(char.IsLetter)))
            {
                ModelState.AddModelError("Synonyms", "Error");
            }

            //Model Validation checks
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _loggerService.Log($"AddWord called for '{request.Word}'");

                //Validation Logic
                var result = _validationService.ValidateAddRequest(request);
                if (!result.IsValid) return BadRequest(new { message = result.ErrorMessage });

                //API call
                _synonymService.AddWord(request.Word, request.Synonyms);

                //Return
                return Ok("Word and synonyms added successfully.");
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in AddWord", ex);
                return StatusCode(500, "Something went wrong while adding the word.");
            }
        }
        #endregion

        #region Get synonyms on basis of word Endpoint
        [HttpPost("getsyn")]
        public IActionResult GetSynonyms(GetSynonymDTO request)
        {
            //Model Validation checks
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _loggerService.Log($"GetSynonyms called for '{request.Word}'");

                // API call
                var result = _synonymService.GetSynonyms(request.Word);

                // Return
                return Ok(result);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in GetSynonyms", ex);
                return StatusCode(500, "Failed to get synonyms.");
            }
        }
        #endregion

        #region Get all words present in our system Endpoint
        [HttpGet]
        public IActionResult GetAllWords()
        {
            try
            {
                _loggerService.Log("GetAllWords called");

                //API call
                var result = _synonymService.GetAllWords();

                //Return
                return Ok(result);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in GetAllWords", ex);
                return StatusCode(500, "Failed to get all words.");
            }
        }
        #endregion
    }
}