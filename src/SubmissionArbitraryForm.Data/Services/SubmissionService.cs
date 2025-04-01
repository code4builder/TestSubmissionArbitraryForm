using Microsoft.EntityFrameworkCore;
using SubmissionArbitraryForm.Domain;
using SubmissionArbitraryForm.Domain.Fields;

namespace SubmissionArbitraryForm.Data.Services;

public class SubmissionService
{
    private readonly SubmissionsDbContext _dbContext;

    public SubmissionService(SubmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateSubmissionAsync(Dictionary<string, object> formData, string? createdBy = null)
    {
        var submission = new Submission { CreatedBy = createdBy };
        _dbContext.Submissions.Add(submission);

        foreach (var field in formData)
        {
            FormField fieldEntry = CreateFormField(submission.Id, field.Key, field.Value);
            _dbContext.FormFields.Add(fieldEntry);
        }

        await _dbContext.SaveChangesAsync();
        return submission.Id;
    }

    public async Task<Submission?> GetSubmissionByIdAsync(Guid submissionId)
    {
        return await _dbContext.Submissions
            .Include(s => s.Fields)
            .FirstOrDefaultAsync(s => s.Id == submissionId && !s.IsDeleted);
    }

    public async Task<List<Submission>> GetAllSubmissionsAsync()
    {
        return await _dbContext.Submissions
            .Where(s => !s.IsDeleted)
            .Include(s => s.Fields)
            .ToListAsync();
    }

    public async Task<bool> DeleteSubmissionAsync(Guid submissionId, string? deletedBy = null)
    {
        var submission = await _dbContext.Submissions.FindAsync(submissionId);
        if (submission == null) return false;

        submission.IsDeleted = true;
        submission.DeletedAt = DateTime.UtcNow;
        submission.DeletedBy = deletedBy;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateSubmissionAsync(Guid submissionId, Dictionary<string, object> updatedData, string? modifiedBy = null)
    {
        var submission = await _dbContext.Submissions
            .Include(s => s.Fields)
            .FirstOrDefaultAsync(s => s.Id == submissionId && !s.IsDeleted);

        if (submission == null) return false;

        submission.ModifiedAt = DateTime.UtcNow;
        submission.ModifiedBy = modifiedBy;

        var existingFields = submission.Fields.ToDictionary(f => f.FieldName, f => f);

        foreach (var field in updatedData)
        {
            if (existingFields.ContainsKey(field.Key))
            {
                // Update existing field value
                UpdateFormField(existingFields[field.Key], field.Value);
            }
            else
            {
                // Add new field
                var newField = CreateFormField(submissionId, field.Key, field.Value);
                _dbContext.FormFields.Add(newField);
            }
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    private static FormField CreateFormField(Guid submissionId, string fieldName, object value)
    {
        return value switch
        {
            string text => new TextField { SubmissionId = submissionId, FieldName = fieldName, Value = text },
            bool checkbox => new CheckboxField { SubmissionId = submissionId, FieldName = fieldName, IsChecked = checkbox },
            DateTime date => new DateField { SubmissionId = submissionId, FieldName = fieldName, Value = date },
            _ => new TextField { SubmissionId = submissionId, FieldName = fieldName, Value = value?.ToString() ?? "" }
        };
    }

    private static void UpdateFormField(FormField field, object newValue)
    {
        switch (field)
        {
            case TextField textField when newValue is string newText:
                textField.Value = newText;
                break;
            case CheckboxField checkboxField when newValue is bool newChecked:
                checkboxField.IsChecked = newChecked;
                break;
            case DateField dateField when newValue is DateTime newDate:
                dateField.Value = newDate;
                break;
            default:
                field.GetType().GetProperty("Value")?.SetValue(field, newValue?.ToString());
                break;
        }
    }
}

