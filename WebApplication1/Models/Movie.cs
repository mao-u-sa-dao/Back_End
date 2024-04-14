using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public int MoviePart { get; set; }

    public int MovieListId { get; set; }

    public string Deseribe { get; set; } = null!;

    public string MovieUrl { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual MoviesList MovieListIdNavigation { get; set; } = null!;
}
