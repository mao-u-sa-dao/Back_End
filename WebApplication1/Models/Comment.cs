using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string CommentContent { get; set; } = null!;

    public int MovieId { get; set; }

    public int AccountId { get; set; }

    public DateTime CommentCreateTime { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Movie? Movie { get; set; }
}
