namespace SubmissionArbitraryForm.Domain;

public class SubmissionDto
{
    public Guid Id { get; set; }
    public List<FormFieldDto> Fields { get; set; } = new();
}

public class FormFieldDto
{
    public string FieldName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
