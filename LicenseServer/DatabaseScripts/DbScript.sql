        Use master
        Create Database LicenseDB
        Go

        Use LicenseDB
        Go


      

CREATE TABLE Student(
    StudentID INT NOT NULL,
    SName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(255) NOT NULL,
    GenderID NVARCHAR(255) NOT NULL,
    Birthday DATETIME NOT NULL,
    CityID NVARCHAR(255) NOT NULL,
    Adrees NVARCHAR(255) NOT NULL,
    GearboxID NVARCHAR(255) NOT NULL,
    LisenceTypeID NCHAR(255) NOT NULL,
    TeacherGender NVARCHAR(255) NULL,
    LowestPrice INT NOT NULL,
    HighestPrice INT NOT NULL,
    InstructorID INT NULL,
    ReviewID INT NOT NULL,
    LessonsCount INT NOT NULL
);
ALTER TABLE
    Student ADD CONSTRAINT student_studentid_primary PRIMARY KEY(StudentID);
CREATE UNIQUE INDEX student_email_unique ON
    Student(Email);
CREATE TABLE Instructor(
    InstructorID INT NOT NULL,
    IName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(255) NOT NULL,
    GenderID NVARCHAR(255) NOT NULL,
    Birthday DATETIME NOT NULL,
    AreaID NVARCHAR(255) NOT NULL,
    GearboxID NVARCHAR(255) NOT NULL,
    LisenceTypeID NCHAR(255) NOT NULL,
    LessonLengthID INT NOT NULL,
    Price INT NOT NULL,
    TimeRange BIGINT NOT NULL,
    DrivingSchool NVARCHAR(255) NOT NULL,
    SchoolManagerID INT NOT NULL,
    RateID INT NOT NULL,
    ReviewID INT NOT NULL
);
ALTER TABLE
    Instructor ADD CONSTRAINT instructor_instructorid_primary PRIMARY KEY(InstructorID);
CREATE UNIQUE INDEX instructor_email_unique ON
    Instructor(Email);
CREATE TABLE SchoolManager(
    SManagerID INT NOT NULL,
    SMName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(255) NOT NULL,
    GenderID NVARCHAR(255) NOT NULL,
    Birthday DATETIME NOT NULL,
    DrivingSchool NVARCHAR(255) NOT NULL,
    AreaID NVARCHAR(255) NOT NULL,
    EstablishmentYear INT NOT NULL,
    NumOfTeachers INT NOT NULL
);
ALTER TABLE
    SchoolManager ADD CONSTRAINT schoolmanager_smanagerid_primary PRIMARY KEY(SManagerID);
CREATE UNIQUE INDEX schoolmanager_email_unique ON
    SchoolManager(Email);
CREATE TABLE Lesson(
    LessonID INT NOT NULL,
    LTime TIME NOT NULL,
    LDate DATE NOT NULL,
    LDay NVARCHAR(255) NOT NULL,
    IsAvailable BIT NOT NULL,
    StuudentID INT NULL,
    IsPaid BIT NOT NULL,
    HasDone BIT NOT NULL,
    InstructorID INT NOT NULL
);
ALTER TABLE
    "Lesson" ADD CONSTRAINT "lesson_lessonid_primary" PRIMARY KEY("LessonID");
CREATE TABLE "Gearbox"(
    "GearboxID" INT NOT NULL,
    "Type" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Gearbox" ADD CONSTRAINT "gearbox_gearboxid_primary" PRIMARY KEY("GearboxID");
CREATE TABLE "LisenceType"(
    "LisenceTypeID" INT NOT NULL,
    "LType" INT NOT NULL,
    "Description" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "LisenceType" ADD CONSTRAINT "lisencetype_lisencetypeid_primary" PRIMARY KEY("LisenceTypeID");
CREATE TABLE "City"(
    "CityID" INT NOT NULL,
    "CityName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "City" ADD CONSTRAINT "city_cityid_primary" PRIMARY KEY("CityID");
CREATE TABLE "Area"(
    "AreaID" INT NOT NULL,
    "AreaName" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Area" ADD CONSTRAINT "area_areaid_primary" PRIMARY KEY("AreaID");
CREATE TABLE "CitiesInArea"(
    "CityID" INT NOT NULL,
    "AreaID" INT NOT NULL
);
ALTER TABLE
    "CitiesInArea" ADD CONSTRAINT "citiesinarea_cityid_primary" PRIMARY KEY("CityID");
ALTER TABLE
    "CitiesInArea" ADD CONSTRAINT "citiesinarea_areaid_primary" PRIMARY KEY("AreaID");
CREATE TABLE "Rate"(
    "RateID" INT NOT NULL,
    "RateMeaning" INT NOT NULL
);
ALTER TABLE
    "Rate" ADD CONSTRAINT "rate_rateid_primary" PRIMARY KEY("RateID");
CREATE TABLE "Review"(
    "ReviewID" INT NOT NULL,
    "Content" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Review" ADD CONSTRAINT "review_reviewid_primary" PRIMARY KEY("ReviewID");
CREATE TABLE "Gender"(
    "GenderID" INT NOT NULL,
    "GenderType" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Gender" ADD CONSTRAINT "gender_genderid_primary" PRIMARY KEY("GenderID");
CREATE TABLE "EnrollmentRequests"(
    "EnrollmentID" INT NOT NULL,
    "LessonID" INT NOT NULL,
    "StatusID" NVARCHAR(255) NOT NULL,
    "StudentID" INT NOT NULL
);
ALTER TABLE
    "EnrollmentRequests" ADD CONSTRAINT "enrollmentrequests_enrollmentid_primary" PRIMARY KEY("EnrollmentID");
CREATE TABLE "Status"(
    "StatusID" INT NOT NULL,
    "StatusMeaning" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Status" ADD CONSTRAINT "status_statusid_primary" PRIMARY KEY("StatusID");
CREATE TABLE "LessonLength"(
    "LessonLengthID" INT NOT NULL,
    "Length (min)" INT NOT NULL
);
ALTER TABLE
    "LessonLength" ADD CONSTRAINT "lessonlength_lessonlengthid_primary" PRIMARY KEY("LessonLengthID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_genderid_foreign" FOREIGN KEY("GenderID") REFERENCES "Gender"("GenderID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_cityid_foreign" FOREIGN KEY("CityID") REFERENCES "City"("CityID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_gearboxid_foreign" FOREIGN KEY("GearboxID") REFERENCES "Gearbox"("GearboxID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_lisencetypeid_foreign" FOREIGN KEY("LisenceTypeID") REFERENCES "LisenceType"("LisenceTypeID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_instructorid_foreign" FOREIGN KEY("InstructorID") REFERENCES "Instructor"("InstructorID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_genderid_foreign" FOREIGN KEY("GenderID") REFERENCES "Gender"("GenderID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_areaid_foreign" FOREIGN KEY("AreaID") REFERENCES "Area"("AreaID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_gearboxid_foreign" FOREIGN KEY("GearboxID") REFERENCES "Gearbox"("GearboxID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_lisencetypeid_foreign" FOREIGN KEY("LisenceTypeID") REFERENCES "LisenceType"("LisenceTypeID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_schoolmanagerid_foreign" FOREIGN KEY("SchoolManagerID") REFERENCES "SchoolManager"("SManagerID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_reviewid_foreign" FOREIGN KEY("ReviewID") REFERENCES "Review"("ReviewID");
ALTER TABLE
    "SchoolManager" ADD CONSTRAINT "schoolmanager_genderid_foreign" FOREIGN KEY("GenderID") REFERENCES "Gender"("GenderID");
ALTER TABLE
    "SchoolManager" ADD CONSTRAINT "schoolmanager_areaid_foreign" FOREIGN KEY("AreaID") REFERENCES "Area"("AreaID");
ALTER TABLE
    "Lesson" ADD CONSTRAINT "lesson_stuudentid_foreign" FOREIGN KEY("StuudentID") REFERENCES "Student"("StudentID");
ALTER TABLE
    "Lesson" ADD CONSTRAINT "lesson_instructorid_foreign" FOREIGN KEY("InstructorID") REFERENCES "Instructor"("InstructorID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_rateid_foreign" FOREIGN KEY("RateID") REFERENCES "Rate"("RateID");
ALTER TABLE
    "Student" ADD CONSTRAINT "student_reviewid_foreign" FOREIGN KEY("ReviewID") REFERENCES "Review"("ReviewID");
ALTER TABLE
    "EnrollmentRequests" ADD CONSTRAINT "enrollmentrequests_lessonid_foreign" FOREIGN KEY("LessonID") REFERENCES "Lesson"("LessonID");
ALTER TABLE
    "EnrollmentRequests" ADD CONSTRAINT "enrollmentrequests_studentid_foreign" FOREIGN KEY("StudentID") REFERENCES "Student"("StudentID");
ALTER TABLE
    "EnrollmentRequests" ADD CONSTRAINT "enrollmentrequests_statusid_foreign" FOREIGN KEY("StatusID") REFERENCES "Status"("StatusID");
ALTER TABLE
    "Instructor" ADD CONSTRAINT "instructor_lessonlengthid_foreign" FOREIGN KEY("LessonLengthID") REFERENCES "LessonLength"("LessonLengthID");
