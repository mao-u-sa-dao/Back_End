using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MovieCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<MoviesList> MoviesLists { get; set; } = new List<MoviesList>();
}
