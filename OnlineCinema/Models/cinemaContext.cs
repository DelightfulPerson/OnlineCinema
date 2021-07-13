using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class cinemaContext : DbContext
    {
        public cinemaContext()
        {
        }

        public cinemaContext(DbContextOptions<cinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddedValue> AddedValues { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenresAndFilm> GenresAndFilms { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DSMAINPC; Database=cinema; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AddedValue>(entity =>
            {
                entity.HasKey(e => e.AddValueId);

                entity.ToTable("addedValue");

                entity.HasIndex(e => e.Name, "IX_addedValue")
                    .IsUnique();

                entity.Property(e => e.AddValueId).HasColumnName("add_value_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("films");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.AgeRating).HasColumnName("age_rating");

                entity.Property(e => e.Duration)
                    .HasColumnType("time(0)")
                    .HasColumnName("duration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.HasIndex(e => e.GenreId, "IX_genres")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "IX_genres_1")
                    .IsUnique();

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<GenresAndFilm>(entity =>
            {
                entity.HasKey(e => new { e.GenresId, e.FilmsId });

                entity.ToTable("genresAndFilms");

                entity.Property(e => e.GenresId).HasColumnName("genres_id");

                entity.Property(e => e.FilmsId).HasColumnName("films_id");

                entity.HasOne(d => d.Films)
                    .WithMany(p => p.GenresAndFilms)
                    .HasForeignKey(d => d.FilmsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genresAndFilms_films");

                entity.HasOne(d => d.Genres)
                    .WithMany(p => p.GenresAndFilms)
                    .HasForeignKey(d => d.GenresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_genresAndFilms_genres");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("halls");

                entity.HasIndex(e => e.Name, "IX_halls")
                    .IsUnique();

                entity.Property(e => e.HallId).HasColumnName("hall_id");

                entity.Property(e => e.ImaxStatus).HasColumnName("imax_status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e._3dStatus).HasColumnName("3d_status");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("places");

                entity.HasIndex(e => new { e.HallId, e.Row, e.Place1 }, "IX_places")
                    .IsUnique();

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.HallId).HasColumnName("hall_id");

                entity.Property(e => e.Offset).HasColumnName("offset");

                entity.Property(e => e.Place1).HasColumnName("place");

                entity.Property(e => e.Row).HasColumnName("row");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_places_halls");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("sessions");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.HallId).HasColumnName("hall_id");

                entity.Property(e => e.TimeStart)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("time_start")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_seassions_films");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_seassions_halls");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => new { e.SesssionsId, e.PlacesId }, "IX_tickets")
                    .IsUnique();

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.PlacesId).HasColumnName("places_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.SesssionsId).HasColumnName("sesssions_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Places)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PlacesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tickets_places");

                entity.HasOne(d => d.Sesssions)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SesssionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tickets_seassions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tickets_users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => new { e.Login, e.Password }, "IX_users")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(35)
                    .HasColumnName("patronymic")
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("surname")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
