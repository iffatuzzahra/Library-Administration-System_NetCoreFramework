namespace Winterhold.Presentation.Web.ViewModels;

public class AuthorIndexTableViewModel
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime? DeceasedDate { get; set; }

    public string? Education { get; set; }

    public string FullName { 
        get {
            return $"{Title}. {FirstName} {LastName}";
        } 
    }
    public int Age {
        get
        {
            int age = (DeceasedDate != null) ? DeceasedDate.Value.Year - BirthDate.Year : DateTime.Now.Year - BirthDate.Year;
            return age;
        }
    }
    public string DeceasedStatus { 
        get {
            return (DeceasedDate != null)? "Deceased" : "Alive";
        } 
    }
}
