using SubmissionArbitraryForm.Domain;

namespace SubmissionArbitraryForm.Services.Interfaces;

public interface ISubmissionProcessingService
{
    Task<Guid> ProcessSubmissionAsync(Dictionary<string, object> formData, string? createdBy = null);
    Task<Submission?> GetSubmissionAsync(Guid submissionId);
    Task<List<Submission>> GetAllSubmissionsAsync();
    Task<bool> UpdateSubmissionAsync(Guid submissionId, Dictionary<string, object> updatedData, string? modifiedBy = null);
    Task<bool> DeleteSubmissionAsync(Guid submissionId, string? deletedBy = null);
}

