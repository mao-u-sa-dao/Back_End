using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class MoviesList
{
    public int MovieListId { get; set; }

    public string MovieListName { get; set; } = null!;

    public string Deseribe { get; set; } = null!;

    public string AvatarMovie { get; set; } = null!;

    public decimal? Price { get; set; }

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public virtual MovieAuthor? Author { get; set; }

    public virtual MovieCategory? Category { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<MoviesUserOwned> MoviesUserOwneds { get; set; } = new List<MoviesUserOwned>();
}
