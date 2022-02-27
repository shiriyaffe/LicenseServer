using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LicenseServerBL.Models
{
    public partial class LicenseDBContext : DbContext
    {
        public LicenseDBContext()
        {
        }

        public LicenseDBContext(DbContextOptions<LicenseDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppAdmin> AppAdmins { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<DrivingSchool> DrivingSchools { get; set; }
        public virtual DbSet<EnrollmentRequest> EnrollmentRequests { get; set; }
        public virtual DbSet<Estatus> Estatuses { get; set; }
        public virtual DbSet<Gearbox> Gearboxes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<InstructorReview> InstructorReviews { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<LessonLength> LessonLengths { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SchoolManager> SchoolManagers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<WorkingHour> WorkingHours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=LicenseDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<AppAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__AppAdmin__719FE4E80BCC2FB4");

                entity.ToTable("AppAdmin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.Aemail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("AEmail");

                entity.Property(e => e.Apass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("APass");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_AreaCity");
            });

            modelBuilder.Entity<DrivingSchool>(entity =>
            {
                entity.HasKey(e => e.SchoolId)
                    .HasName("PK__DrivingS__3DA4677BBA800C6B");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DrivingSchools)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolManagerArea");
            });

            modelBuilder.Entity<EnrollmentRequest>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId)
                    .HasName("PK__Enrollme__7F6877FBB2CCB16B");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.LessonId).HasColumnName("LessonID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.EnrollmentRequests)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentRequestsInstructor");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.EnrollmentRequests)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentRequestsLesson");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.EnrollmentRequests)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentRequestsEStatus");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.EnrollmentRequests)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentRequestsStudent");
            });

            modelBuilder.Entity<Estatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__EStatus__C8EE204398B8F0EB");

                entity.ToTable("EStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusMeaning)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Gearbox>(entity =>
            {
                entity.ToTable("Gearbox");

                entity.Property(e => e.GearboxId).HasColumnName("GearboxID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.GenderType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.HasIndex(e => e.Email, "instructor_email_unique")
                    .IsUnique();

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Details).HasMaxLength(255);

                entity.Property(e => e.DrivingSchoolId).HasColumnName("DrivingSchoolID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.GearboxId).HasColumnName("GearboxID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Iname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("IName");

                entity.Property(e => e.LessonLengthId).HasColumnName("LessonLengthID");

                entity.Property(e => e.LicenseTypeId).HasColumnName("LicenseTypeID");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.SchoolManagerId).HasColumnName("SchoolManagerID");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorArea");

                entity.HasOne(d => d.DrivingSchool)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.DrivingSchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorDrivingSchools");

                entity.HasOne(d => d.Gearbox)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.GearboxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorGearbox");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorGender");

                entity.HasOne(d => d.LessonLength)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.LessonLengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentRequestsLessonLength");

                entity.HasOne(d => d.LicenseType)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.LicenseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorLicense");

                entity.HasOne(d => d.Rate)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.RateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorRate");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_InstructorReview");

                entity.HasOne(d => d.SchoolManager)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.SchoolManagerId)
                    .HasConstraintName("FK_InstructorSchoolManager");
            });

            modelBuilder.Entity<InstructorReview>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.TimeReview).HasColumnType("datetime");

                entity.HasOne(d => d.Instructor)
                    .WithMany()
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorReviewsInstructor");

                entity.HasOne(d => d.Review)
                    .WithMany()
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorReviewsReview");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorReviewsStudent");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.LessonId).HasColumnName("LessonID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.Ldate)
                    .HasColumnType("date")
                    .HasColumnName("LDate");

                entity.Property(e => e.Lday)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("LDay");

                entity.Property(e => e.Ltime).HasColumnName("LTime");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.StuudentId).HasColumnName("StuudentID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonInstructor");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonReview");

                entity.HasOne(d => d.Stuudent)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.StuudentId)
                    .HasConstraintName("FK_LessonStudent");
            });

            modelBuilder.Entity<LessonLength>(entity =>
            {
                entity.ToTable("LessonLength");

                entity.Property(e => e.LessonLengthId).HasColumnName("LessonLengthID");

                entity.Property(e => e.Slength).HasColumnName("SLength");
            });

            modelBuilder.Entity<LicenseType>(entity =>
            {
                entity.ToTable("LicenseType");

                entity.Property(e => e.LicenseTypeId).HasColumnName("LicenseTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ltype)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("LType")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.RateMeaning)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SchoolManager>(entity =>
            {
                entity.HasKey(e => e.SmanagerId)
                    .HasName("PK__SchoolMa__A19B2388B2B04563");

                entity.ToTable("SchoolManager");

                entity.HasIndex(e => e.Email, "schoolmanager_email_unique")
                    .IsUnique();

                entity.Property(e => e.SmanagerId).HasColumnName("SManagerID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.Smname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SMName");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.SchoolManagers)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolManagerGender");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolManagers)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolManagerDrivingSchools");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.Email, "student_email_unique")
                    .IsUnique();

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.GearboxId).HasColumnName("GearboxID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.LessonLengthId).HasColumnName("LessonLengthID");

                entity.Property(e => e.LicenseTypeId).HasColumnName("LicenseTypeID");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.Saddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SAddress");

                entity.Property(e => e.Sname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SName");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCity");

                entity.HasOne(d => d.Gearbox)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GearboxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGearbox");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGender");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_StudentInstructor");

                entity.HasOne(d => d.LessonLength)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.LessonLengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentLessonLength");

                entity.HasOne(d => d.LicenseType)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.LicenseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentLicenseType");
            });

            modelBuilder.Entity<WorkingHour>(entity =>
            {
                entity.HasKey(e => e.HourId)
                    .HasName("PK__WorkingH__18DFA33E7D245D2C");

                entity.Property(e => e.HourId).HasColumnName("HourID");

                entity.Property(e => e.Whour)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("WHour");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
