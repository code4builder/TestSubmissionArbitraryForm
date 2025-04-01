using Microsoft.AspNetCore.Mvc;
using SubmissionArbitraryForm.Services.Interfaces;

namespace SubmissionArbitraryForm.Api.Controllers
{
    [ApiController]
    [Route("api/submission")]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionProcessingService _submissionProcessingService;

        public SubmissionController(ISubmissionProcessingService submissionProcessingService)
        {
            _submissionProcessingService = submissionProcessingService;
        }

        /// <summary>
        /// Create a new submission.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSubmission([FromBody] Dictionary<string, object> formData)
        {
            var submissionId = await _submissionProcessingService.ProcessSubmissionAsync(formData);
            return CreatedAtAction(nameof(GetSubmission), new { id = submissionId }, new { SubmissionId = submissionId });
        }

        /// <summary>
        /// Get a submission by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmission(Guid id)
        {
            var submission = await _submissionProcessingService.GetSubmissionAsync(id);
            if (submission == null) return NotFound();
            return Ok(submission);
        }

        /// <summary>
        /// Get all submissions.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSubmissions()
        {
            var submissions = await _submissionProcessingService.GetAllSubmissionsAsync();
            return Ok(submissions);
        }

        /// <summary>
        /// Update an existing submission.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubmission(Guid id, [FromBody] Dictionary<string, object> updatedData)
        {
            var updated = await _submissionProcessingService.UpdateSubmissionAsync(id, updatedData);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a submission (soft delete).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(Guid id)
        {
            var deleted = await _submissionProcessingService.DeleteSubmissionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
