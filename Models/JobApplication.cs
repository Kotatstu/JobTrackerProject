using System.ComponentModel.DataAnnotations;

public class JobApplication
{
    //Primary key
    public int Id {get; set;}

    //Non-Nullale, max length 100
    [Required]
    [MaxLength(100)]
    public required string CompanyName {get; set;}

    [MaxLength(100)]
    public string? Position {get; set;}

    [MaxLength(500)]
    public string? Address {get; set;}

    public ApplicationStatus Status {get; set;} = ApplicationStatus.Applied;

    public DateTime SubmitDate {get; set;} = DateTime.Now;
    
    [MaxLength(500)]
    public string? Note {get; set;}

}