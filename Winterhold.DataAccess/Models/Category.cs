using System;
using System.Collections.Generic;

namespace Winterhold.DataAccess.Models;

public partial class Category
{
    public string Name { get; set; } = null!;

    public int Floor { get; set; }

    public string Isle { get; set; } = null!;

    public string Bay { get; set; } = null!;
    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
