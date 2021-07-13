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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DSMAINPC; Database=cinema; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { RoleId = 1, Name = adminRoleName };
            Role userRole = new Role { RoleId = 2, Name = userRoleName };
            User adminUser = new User { UserId = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.RoleId};

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
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

                entity.HasIndex(e => new { e.Email, e.Password }, "IX_users")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
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

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("surname")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
