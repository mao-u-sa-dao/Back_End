using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class InforAccountUser
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal? AccountMoney { get; set; }

    public virtual AccountUser? Account { get; set; }
}
