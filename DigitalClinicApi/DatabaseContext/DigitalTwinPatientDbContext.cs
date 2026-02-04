using DigitalClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalClinicApi.DatabaseContext;

public partial class DigitalTwinPatientDbContext : DbContext
{
    public DigitalTwinPatientDbContext(DbContextOptions<DigitalTwinPatientDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<DiagnosisCategory> DiagnosisCategories { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoseUnit> DoseUnits { get; set; }

    public virtual DbSet<Frequency> Frequencies { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientComplaint> PatientComplaints { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Severity> Severities { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<SymptomCategory> SymptomCategories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-KSRNQI5;Initial Catalog=DigitalTwinPatientDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Building).HasMaxLength(8);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("Diagnosis");

            entity.Property(e => e.CodeMkb)
                .HasMaxLength(5)
                .HasColumnName("CodeMKB");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DiagnosisCategory).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DiagnosisCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnosis_DiagnosisCategory");
        });

        modelBuilder.Entity<DiagnosisCategory>(entity =>
        {
            entity.ToTable("DiagnosisCategory");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_Department");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_Specialization");
        });

        modelBuilder.Entity<DoseUnit>(entity =>
        {
            entity.ToTable("DoseUnit");

            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Frequency>(entity =>
        {
            entity.ToTable("Frequency");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name).HasMaxLength(7);
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.ToTable("Instruction");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.ToTable("Medication");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.Address).WithMany(p => p.Patients)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Address");

            entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Gender");
        });

        modelBuilder.Entity<PatientComplaint>(entity =>
        {
            entity.ToTable("PatientComplaint");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Patient");

            entity.HasOne(d => d.Severity).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.SeverityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Severity");

            entity.HasOne(d => d.Symptom).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.SymptomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Symptom");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Doctor");

            entity.HasOne(d => d.DoseUnit).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoseUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_DoseUnit");

            entity.HasOne(d => d.Frequency).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.FrequencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Frequency");

            entity.HasOne(d => d.Instruction).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.InstructionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Instruction");

            entity.HasOne(d => d.Medication).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Medication");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Patient");

            entity.HasOne(d => d.Status).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Status");
        });

        modelBuilder.Entity<Severity>(entity =>
        {
            entity.ToTable("Severity");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.ToTable("Specialization");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.ToTable("Symptom");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.SymptomCategory).WithMany(p => p.Symptoms)
                .HasForeignKey(d => d.SymptomCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Symptom_SymptomCategory");
        });

        modelBuilder.Entity<SymptomCategory>(entity =>
        {
            entity.ToTable("SymptomCategory");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
