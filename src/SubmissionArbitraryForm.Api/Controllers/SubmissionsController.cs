using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SubmissionArbitraryForm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] Dictionary<string, object> formData)
        {
            //var submission = new Submission { CreatedAt = DateTime.UtcNow };
            //_dbContext.Submissions.Add(submission);

            //foreach (var field in formData)
            //{
            //    _dbContext.SubmissionFields.Add(new SubmissionField
            //    {
            //        SubmissionId = submission.Id,
            //        FieldName = field.Key,
            //        FieldValue = field.Value?.ToString(),
            //        FieldType = field.Value?.GetType().Name ?? "string"
            //    });
            //}

            //await _dbContext.SaveChangesAsync();
            //return Ok(submission.Id);
            return Ok();
        }
    }
}
