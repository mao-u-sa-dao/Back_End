using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class AccountUser
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string AccountPassword { get; set; } = null!;

    public int AccountRole { get; set; }

    public string AccountGmail { get; set; } = null!;

    public DateTime AccountCreateTime { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<InforAccountUser> InforAccountUsers { get; set; } = new List<InforAccountUser>();

    public virtual ICollection<MoviesBill> MoviesBills { get; set; } = new List<MoviesBill>();

    public virtual ICollection<MoviesUserOwned> MoviesUserOwneds { get; set; } = new List<MoviesUserOwned>();
}
