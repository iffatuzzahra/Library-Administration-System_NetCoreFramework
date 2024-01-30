using System;
using System.Collections.Generic;

namespace Winterhold.DataAccess.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
