namespace SubmissionArbitraryForm.Domain.Fields;

public class TextField : FormField
{
    public string Value { get; set; } = string.Empty;
    public override string GetValue() => Value;
}

// TODO: these classes should be in their own files
public class DropdownField : FormField
{
    public string SelectedOption { get; set; } = string.Empty;
    public override string GetValue() => SelectedOption;
}

public class DateField : FormField
{
    public DateTime Value { get; set; }
    public override string GetValue() => Value.ToString("o");
}

public class RadioField : FormField
{
    public string SelectedOption { get; set; } = string.Empty;
    public override string GetValue() => SelectedOption;
}

public class CheckboxField : FormField
{
    public bool IsChecked { get; set; }
    public override string GetValue() => IsChecked.ToString();
}