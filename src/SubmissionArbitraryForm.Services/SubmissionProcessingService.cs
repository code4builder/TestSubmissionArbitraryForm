using SubmissionArbitraryForm.Data.Services;
using SubmissionArbitraryForm.Domain;
using SubmissionArbitraryForm.Services.Interfaces;

namespace SubmissionArbitraryForm.Services
{
    public class SubmissionProcessingService : ISubmissionProcessingService
    {
        private readonly SubmissionService _submissionService;

        public SubmissionProcessingService(SubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        public async Task<Guid> ProcessSubmissionAsync(Dictionary<string, object> formData, string? createdBy = null)
        {
            return await _submissionService.CreateSubmissionAsync(formData, createdBy);
        }

        public async Task<Submission?> GetSubmissionAsync(Guid submissionId)
        {
            return await _submissionService.GetSubmissionByIdAsync(submissionId);
        }

        public async Task<List<Submission>> GetAllSubmissionsAsync()
        {
            return await _submissionService.GetAllSubmissionsAsync();
        }

        public async Task<bool> UpdateSubmissionAsync(Guid submissionId, Dictionary<string, object> updatedData, string? modifiedBy = null)
        {
            return await _submissionService.UpdateSubmissionAsync(submissionId, updatedData, modifiedBy);
        }

        public async Task<bool> DeleteSubmissionAsync(Guid submissionId, string? deletedBy = null)
        {
            return await _submissionService.DeleteSubmissionAsync(submissionId, deletedBy);
        }
    }
}
