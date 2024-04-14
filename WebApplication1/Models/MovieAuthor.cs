using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MovieAuthor
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public virtual ICollection<MoviesList> MoviesLists { get; set; } = new List<MoviesList>();
}
