namespace SubmissionArbitraryForm.Domain;

public class Submission : BaseEntity
{
    public List<FormField> Fields { get; set; } = new();
}
