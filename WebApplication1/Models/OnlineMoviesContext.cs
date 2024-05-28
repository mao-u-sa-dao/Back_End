using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class OnlineMoviesContext : DbContext
{
    public OnlineMoviesContext()
    {
    }

    public OnlineMoviesContext(DbContextOptions<OnlineMoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<InforAccountUser> InforAccountUsers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieAuthor> MovieAuthors { get; set; }

    public virtual DbSet<MovieCategory> MovieCategories { get; set; }

    public virtual DbSet<MoviesBill> MoviesBills { get; set; }

    public virtual DbSet<MoviesList> MoviesLists { get; set; }

    public virtual DbSet<MoviesUserOwned> MoviesUserOwneds { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BI83N0Q;Initial Catalog=Online_Movies;Persist Security Info=True;User ID=sa;Password=quan154;Encrypt=True;Trust Server Certificate=True");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

       => optionsBuilder.UseSqlServer("Data Source=tcp:azure-movie.database.windows.net,1433;Initial Catalog=azure_db_movie;Persist Security Info=True;User ID=quan154;Password=vkiuoi154@;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account___B19E45C900B1C1C8");

            entity.ToTable("Account_User");

            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.AccountCreateTime)
                .HasColumnType("datetime")
                .HasColumnName("Account_CreateTime");
            entity.Property(e => e.AccountGmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Account_Gmail");
            entity.Property(e => e.AccountName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Account_Name");
            entity.Property(e => e.AccountPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Account_Password");
            entity.Property(e => e.AccountRole).HasColumnName("Account_Role");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__99FC143B3B73F136");

            entity.Property(e => e.CommentId).HasColumnName("Comment_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.CommentContent)
                .HasMaxLength(300)
                .HasColumnName("Comment_Content");
            entity.Property(e => e.CommentCreateTime)
                .HasColumnType("datetime")
                .HasColumnName("Comment_CreateTime");
            entity.Property(e => e.MovieId).HasColumnName("Movie_ID");

            entity.HasOne(d => d.Account).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade) // Thay đổi hành vi xóa
                .HasConstraintName("FK_Comments_Account");

            entity.HasOne(d => d.Movie).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Movies");
        });

        modelBuilder.Entity<InforAccountUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Infor_Ac__3214EC2740EAD5ED");

            entity.ToTable("Infor_Account_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.AccountMoney)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Account_Money");

            entity.HasOne(d => d.Account).WithMany(p => p.InforAccountUsers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade) // Thay đổi hành vi xóa
                .HasConstraintName("FK_InforAccount_Account");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__7A880405C744AB05");

            entity.Property(e => e.MovieId).HasColumnName("Movie_ID");
            entity.Property(e => e.Deseribe).HasMaxLength(150);
            entity.Property(e => e.MovieListId).HasColumnName("Movie_List_ID");
            entity.Property(e => e.MoviePart).HasColumnName("Movie_Part");
            entity.Property(e => e.MovieUrl)
                .HasMaxLength(100)
                .HasColumnName("Movie_URL");

            entity.HasOne(d => d.MovieList).WithMany(p => p.Movies)
                .HasForeignKey(d => d.MovieListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movies_MoviesList");
        });

        modelBuilder.Entity<MovieAuthor>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Movie_Au__55B9F6DFB4C28BA7");

            entity.ToTable("Movie_Author");

            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("Author_Name");
        });

        modelBuilder.Entity<MovieCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Movie_Ca__6DB38D4E71C93A2C");

            entity.ToTable("Movie_Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<MoviesBill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Movies_B__CF6E7D434A78976A");

            entity.ToTable("Movies_Bill");

            entity.Property(e => e.BillId).HasColumnName("Bill_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.BillCreateTime)
                .HasColumnType("datetime")
                .HasColumnName("Bill_CreateTime");
            entity.Property(e => e.MovieListId).HasColumnName("Movie_List_ID");

            entity.HasOne(d => d.Account).WithMany(p => p.MoviesBills)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade) // Thay đổi hành vi xóa
                .HasConstraintName("FK_Bill_Account");

            entity.HasOne(d => d.MovieList).WithMany(p => p.MoviesBills)
                .HasForeignKey(d => d.MovieListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bill_MoviesList");
        });

        modelBuilder.Entity<MoviesList>(entity =>
        {
            entity.HasKey(e => e.MovieListId).HasName("PK__Movies_L__FE67504B327F67C4");

            entity.ToTable("Movies_List");

            entity.Property(e => e.MovieListId).HasColumnName("Movie_List_ID");
            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.AvatarMovie)
                .HasMaxLength(100)
                .HasColumnName("Avatar_Movie");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.Deseribe).HasMaxLength(150);
            entity.Property(e => e.MovieListName)
                .HasMaxLength(50)
                .HasColumnName("Movie_List_Name");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Author).WithMany(p => p.MoviesLists)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoviesList_MoviesAuthor");

            entity.HasOne(d => d.Category).WithMany(p => p.MoviesLists)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoviesList_MoviesCategory");
        });

        modelBuilder.Entity<MoviesUserOwned>(entity =>
        {
            entity.HasKey(e => e.MoviesUserOwnedId).HasName("PK__Movies_U__096897ECD358DDA1");

            entity.ToTable("Movies_User_Owned");

            entity.Property(e => e.MoviesUserOwnedId).HasColumnName("Movies_User_Owned_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.MovieListId).HasColumnName("Movie_List_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.MoviesUserOwneds)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade) // Thay đổi hành vi xóa
                .HasConstraintName("FK_MoviesUserOwned_Account");

            entity.HasOne(d => d.MovieList).WithMany(p => p.MoviesUserOwneds)
                .HasForeignKey(d => d.MovieListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoviesUserOwned_MoviesList");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
