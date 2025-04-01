namespace SubmissionArbitraryForm.Domain;

public class BaseEntity
{
    public Guid Id { get; set; }

    // Creation tracking
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }


    // Modification tracking
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }


    // Soft delete: logical deletion instead of permanent removal
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
