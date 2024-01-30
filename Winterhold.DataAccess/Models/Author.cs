using System;
using System.Collections.Generic;

namespace Winterhold.DataAccess.Models;

public partial class Author
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime? DeceasedDate { get; set; }

    public string? Education { get; set; }

    public string? Summary { get; set; }
    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
