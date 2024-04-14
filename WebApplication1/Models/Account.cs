using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string AccountPassword { get; set; } = null!;

    public string AccountGmail { get; set; } = null!;

    public decimal? AccountMoney { get; set; }

    public DateTime AccountCreateTime { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<MoviesUserOwned> MoviesUserOwneds { get; set; } = new List<MoviesUserOwned>();
}
