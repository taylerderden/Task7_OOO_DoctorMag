using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class CoreModel : DbContext
{
    private static CoreModel instance;
    public static CoreModel init()
    {
        if (instance == null)
        {
            instance = new CoreModel();
        }
        return instance;
    }
    public CoreModel()
    {
    }

    public CoreModel(DbContextOptions<CoreModel> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Pacient> Pacients { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TypeVisit> TypeVisits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cfif31.ru;port=3306;user id=ISPr23-35_ZlobinDS;password=ISPr23-35_ZlobinDS;database=ISPr23-35_ZlobinDS_Task7;character set=utf-8", ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PRIMARY");

            entity.ToTable("Doctor");

            entity.HasIndex(e => e.PositionIdPosition, "fk_Doctor_Position1_idx");

            entity.HasIndex(e => e.StatusIdStatus, "fk_Doctor_Status1_idx");

            entity.HasIndex(e => e.TypeVisitIdTypeVisit, "fk_Doctor_TypeVisit1_idx");

            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.DoctorName).HasMaxLength(45);
            entity.Property(e => e.DoctorPatronymic).HasMaxLength(45);
            entity.Property(e => e.DoctorPhoto).HasMaxLength(45);
            entity.Property(e => e.DoctorSchedule).HasMaxLength(45);
            entity.Property(e => e.DoctorSurname).HasMaxLength(45);
            entity.Property(e => e.PositionIdPosition).HasColumnName("Position_idPosition");
            entity.Property(e => e.StatusIdStatus).HasColumnName("Status_idStatus");
            entity.Property(e => e.TypeVisitIdTypeVisit).HasColumnName("TypeVisit_idTypeVisit");

            entity.HasOne(d => d.PositionIdPositionNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.PositionIdPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Doctor_Position1");

            entity.HasOne(d => d.StatusIdStatusNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.StatusIdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Doctor_Status1");

            entity.HasOne(d => d.TypeVisitIdTypeVisitNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.TypeVisitIdTypeVisit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Doctor_TypeVisit1");
        });

        modelBuilder.Entity<Pacient>(entity =>
        {
            entity.HasKey(e => e.IdPacient).HasName("PRIMARY");

            entity.ToTable("Pacient");

            entity.Property(e => e.IdPacient).HasColumnName("idPacient");
            entity.Property(e => e.PacientEmail).HasMaxLength(45);
            entity.Property(e => e.PacientName).HasMaxLength(45);
            entity.Property(e => e.PacientPatronymic).HasMaxLength(45);
            entity.Property(e => e.PacientPhone).HasMaxLength(45);
            entity.Property(e => e.PacientPolis).HasMaxLength(45);
            entity.Property(e => e.PacientSurname).HasMaxLength(45);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition).HasName("PRIMARY");

            entity.ToTable("Position");

            entity.Property(e => e.IdPosition).HasColumnName("idPosition");
            entity.Property(e => e.PositionName).HasMaxLength(45);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.StatusName).HasMaxLength(45);
        });

        modelBuilder.Entity<TypeVisit>(entity =>
        {
            entity.HasKey(e => e.IdTypeVisit).HasName("PRIMARY");

            entity.ToTable("TypeVisit");

            entity.Property(e => e.IdTypeVisit).HasColumnName("idTypeVisit");
            entity.Property(e => e.TypeVisitName).HasMaxLength(45);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.PositionIdPosition, "fk_User_Position_idx");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.PositionIdPosition).HasColumnName("Position_idPosition");
            entity.Property(e => e.UserLogin).HasMaxLength(45);
            entity.Property(e => e.UserName).HasMaxLength(45);
            entity.Property(e => e.UserPassword).HasMaxLength(45);
            entity.Property(e => e.UserPatronymic).HasMaxLength(45);
            entity.Property(e => e.UserSurname).HasMaxLength(45);

            entity.HasOne(d => d.PositionIdPositionNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.PositionIdPosition)
                .HasConstraintName("fk_User_Position");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.IdVisit).HasName("PRIMARY");

            entity.ToTable("Visit");

            entity.HasIndex(e => e.DoctorIdDoctor, "fk_Visit_Doctor1_idx");

            entity.HasIndex(e => e.PacientIdPacient, "fk_Visit_Pacient1_idx");

            entity.HasIndex(e => e.TypeVisitIdTypeVisit, "fk_Visit_TypeVisit1_idx");

            entity.Property(e => e.IdVisit).HasColumnName("idVisit");
            entity.Property(e => e.DoctorIdDoctor).HasColumnName("Doctor_idDoctor");
            entity.Property(e => e.PacientIdPacient).HasColumnName("Pacient_idPacient");
            entity.Property(e => e.TypeVisitIdTypeVisit).HasColumnName("TypeVisit_idTypeVisit");
            entity.Property(e => e.VisitTime).HasMaxLength(45);

            entity.HasOne(d => d.DoctorIdDoctorNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.DoctorIdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Visit_Doctor1");

            entity.HasOne(d => d.PacientIdPacientNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PacientIdPacient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Visit_Pacient1");

            entity.HasOne(d => d.TypeVisitIdTypeVisitNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.TypeVisitIdTypeVisit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Visit_TypeVisit1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
