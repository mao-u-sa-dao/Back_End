using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MoviesBill
{
    public int BillId { get; set; }

    public int MovieListId { get; set; }

    public int AccountId { get; set; }

    public DateTime BillCreateTime { get; set; }

    public virtual AccountUser? Account { get; set; }

    public virtual MoviesList? MovieList { get; set; }
}
