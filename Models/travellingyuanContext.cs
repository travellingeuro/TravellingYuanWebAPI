using Microsoft.EntityFrameworkCore;


namespace TravellingYuanWebAPI.Models
{
    public class travellingyuanContext : DbContext
    {
        public travellingyuanContext()
        {
        }

        public travellingyuanContext(DbContextOptions<travellingyuanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mints> Mints { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Uploads> Uploads { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseMySql("server=travellingeurodb.mysql.database.azure.com;port=3306;user=travellingeuro@travellingeurodb;password=Gustavo98;database=tywebapi;sslmode=preferred");
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mints>(entity =>
            {
                entity.ToTable("mints");

                entity.HasIndex(e => e.Mintcode)
                    .HasName("mintcode_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Mintcode)
                    .IsRequired()
                    .HasColumnName("mintcode")
                    .HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.ToTable("notes");

                entity.HasIndex(e => e.MintsId)
                    .HasName("fk_notes_mints1_idx");

                entity.HasIndex(e => e.SerialNumber)
                    .HasName("idx_notes_serial_number")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MintsId)
                    .HasColumnName("mints_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasColumnName("serial_number")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(3)");

                entity.HasOne(d => d.Mints)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.MintsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notes_mints1");
            });

            modelBuilder.Entity<Uploads>(entity =>
            {
                entity.HasKey(e => new { e.NotesId, e.UsersId });

                entity.ToTable("uploads");

                entity.HasIndex(e => e.NotesId)
                    .HasName("fk_notes_has_users_notes1_idx");

                entity.HasIndex(e => e.UsersId)
                    .HasName("fk_notes_has_users_users1_idx");

                entity.Property(e => e.NotesId)
                    .HasColumnName("notes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsersId)
                    .HasColumnName("users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.UploadDate)
                    .HasColumnName("upload_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Notes)
                    .WithMany(p => p.Uploads)
                    .HasForeignKey(d => d.NotesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notes_has_users_notes1");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Uploads)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notes_has_users_users1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Alias)
                    .HasColumnName("alias")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.EmailConfirmed)
                    .HasColumnName("email_confirmed")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Keeplogged)
                    .HasColumnName("keeplogged")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Keepmeinformed)
                    .HasColumnName("keepmeinformed")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("varchar(10)");
            });
        }
    }
    class DbContextFactory
    {
        public static travellingyuanContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<travellingyuanContext>();
            optionsBuilder.UseMySql(connectionString);
            var travellingeuroContext = new travellingyuanContext(optionsBuilder.Options);
            return travellingeuroContext;
        }
    }
}