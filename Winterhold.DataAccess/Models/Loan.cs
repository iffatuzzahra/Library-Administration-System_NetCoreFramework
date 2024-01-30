using System;
using System.Collections.Generic;

namespace Winterhold.DataAccess.Models;

public partial class Loan
{
    public long Id { get; set; }

    public string CustomerNumber { get; set; } = null!;

    public string BookCode { get; set; } = null!;

    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? Note { get; set; }

    public virtual Book BookCodeNavigation { get; set; } = null!;

    public virtual Customer CustomerNumberNavigation { get; set; } = null!;
}
