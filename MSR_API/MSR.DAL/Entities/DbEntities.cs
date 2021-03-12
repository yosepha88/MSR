using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class DbEntities : DbContext
    {
        public DbEntities()
        {
        }

        public DbEntities(DbContextOptions<DbEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<HelpDeskTicket> HelpDeskTickets { get; set; }
        public virtual DbSet<JiraTicket> JiraTickets { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<SubTask> SubTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WebTrendLinkDatum> WebTrendLinkData { get; set; }
        public virtual DbSet<WebTrendResourceDatum> WebTrendResourceData { get; set; }
        public virtual DbSet<WebTrendSiteDatum> WebTrendSiteData { get; set; }
        public virtual DbSet<WebTrendTermDatum> WebTrendTermData { get; set; }
        public virtual DbSet<Years> Years { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=YOSEPHDESKTOP;Database=MSR_New;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.ToTable("ExceptionLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Callsite).HasMaxLength(300);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LogDateTime).HasColumnType("datetime");

                entity.Property(e => e.Logger).HasMaxLength(300);

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Port).HasMaxLength(100);

                entity.Property(e => e.RemoteAddress).HasMaxLength(100);

                entity.Property(e => e.ServerAddress).HasMaxLength(100);

                entity.Property(e => e.ServerName).HasMaxLength(200);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(2000);

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<HelpDeskTicket>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<JiraTicket>(entity =>
            {
                entity.HasOne(d => d.SubTask)
                    .WithMany(p => p.JiraTickets)
                    .HasForeignKey(d => d.SubTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JiraTickets_SubTasks");
            });

            modelBuilder.Entity<Month>(entity =>
            {
                entity.Property(e => e.MonthName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubTask>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.SubTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTasks_Tasks");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestedTasks_UserCreatedBy");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestedTask_Status_StatusId");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Icon).HasMaxLength(200);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WebTrendLinkDatum>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WebTrendResourceDatum>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WebTrendSiteDatum>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<WebTrendTermDatum>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Term).HasMaxLength(100);
            });

            modelBuilder.Entity<Years>(entity =>
            {
                entity.Property(e => e.Year).HasColumnName("Year");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
