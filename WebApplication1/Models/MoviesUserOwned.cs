using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MoviesUserOwned
{
    public int MoviesUserOwnedId { get; set; }

    public int MovieListId { get; set; }

    public int AccountId { get; set; }

    public decimal Price { get; set; }

    public virtual AccountUser? Account { get; set; }

    public virtual MoviesList? MovieList { get; set; }
}
