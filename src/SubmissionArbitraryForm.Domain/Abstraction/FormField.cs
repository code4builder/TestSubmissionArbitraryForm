namespace SubmissionArbitraryForm.Domain;

public abstract class FormField : BaseEntity
{
    public Guid SubmissionId { get; set; }
    public string FieldName { get; set; } = string.Empty;

    // Each subclass must implement how it stores its value
    public abstract string GetValue();
}
