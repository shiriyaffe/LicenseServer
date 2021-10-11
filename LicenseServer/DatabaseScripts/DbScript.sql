     Use master
Create Database LicenseDB
Go

Use LicenseDB
Go

CREATE TABLE Gearbox(
    GearboxID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Type NVARCHAR(50) NOT NULL
);

CREATE TABLE LicenseType(
    LicenseTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LType INT NOT NULL,
    Description NVARCHAR(50) NOT NULL
);

CREATE TABLE City(
    CityID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CityName NVARCHAR(50) NOT NULL
);

CREATE TABLE Area(
    AreaID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    AreaName NVARCHAR(50) NOT NULL
);

CREATE TABLE CitiesInArea(
    CityID INT NOT NULL,
	CONSTRAINT FK_CitiesInAreaCity FOREIGN KEY(CityID) REFERENCES City(CityID),
    AreaID INT NOT NULL,
	CONSTRAINT FK_CitiesInAreaArea FOREIGN KEY(AreaID) REFERENCES Area(AreaID),
	CONSTRAINT PK_CitiesInArea PRIMARY KEY (CityID, AreaID)
);

CREATE TABLE Rate(
    RateID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    RateMeaning INT NOT NULL
);

CREATE TABLE Review(
    ReviewID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Content NVARCHAR(50) NOT NULL
);


CREATE TABLE Gender(
    GenderID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    GenderType NVARCHAR(50) NOT NULL
);

CREATE TABLE EStatus(
    StatusID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    StatusMeaning NVARCHAR(50) NOT NULL
);


CREATE TABLE LessonLength(
    LessonLengthID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    SLength INT NOT NULL
);

CREATE TABLE AppAdmin(
    AdminID INT PRIMARY KEY NOT NULL,
    AEmail NVARCHAR(50) NOT NULL,
    APass NVARCHAR(50) NOT NULL
);


CREATE TABLE SchoolManager(
    SManagerID INT PRIMARY KEY NOT NULL,
    SMName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Pass NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_SchoolManagerGender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID),
    Birthday DATETIME NOT NULL,
    DrivingSchool NVARCHAR(50) NOT NULL,
    AreaID INT NOT NULL,
	CONSTRAINT FK_SchoolManagerArea FOREIGN KEY(AreaID) REFERENCES Area(AreaID),
    EstablishmentYear INT NOT NULL,
    NumOfTeachers INT NOT NULL,
    RegistrationDate DATE NOT NULL
);
CREATE UNIQUE INDEX schoolmanager_email_unique ON
    SchoolManager(Email);

CREATE TABLE Instructor(
    InstructorID INT PRIMARY KEY NOT NULL,
    IName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Pass NVARCHAR(10) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_InstructorGender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID),
    Birthday DATETIME NOT NULL,
    AreaID INT NOT NULL,
	CONSTRAINT FK_InstructorArea FOREIGN KEY(AreaID) REFERENCES Area(AreaID),
    GearboxID INT NOT NULL,
	CONSTRAINT FK_InstructorGearbox FOREIGN KEY(GearboxID) REFERENCES Gearbox(GearboxID),
    LicenseTypeID INT NOT NULL,
	CONSTRAINT FK_InstructorLicense FOREIGN KEY(LicenseTypeID) REFERENCES LicenseType(LicenseTypeID),
    LessonLengthID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsLessonLength FOREIGN KEY(LessonLengthID) REFERENCES LessonLength(LessonLengthID),
    Price INT NOT NULL,
    TimeRange BIGINT NOT NULL,
    DrivingSchool NVARCHAR(50) NOT NULL,
    SchoolManagerID INT NOT NULL,
	CONSTRAINT FK_InstructorSchoolManager FOREIGN KEY(SchoolManagerID) REFERENCES SchoolManager(SManagerID),
    RateID INT NOT NULL,
	CONSTRAINT FK_InstructorRate FOREIGN KEY(RateID) REFERENCES Rate(RateID),
    RegistrationDate DATE NOT NULL
);
CREATE UNIQUE INDEX instructor_email_unique ON
    Instructor(Email);

	CREATE TABLE Student(
    StudentID INT PRIMARY KEY NOT NULL,
    SName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Pass NVARCHAR(10) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_StudentGender FOREIGN KEY (GenderID) REFERENCES Gender(GenderID),
    Birthday DATETIME NOT NULL,
    CityID INT NOT NULL,
	CONSTRAINT FK_StudentCity FOREIGN KEY (CityID) REFERENCES City(CityID),
    Adrees NVARCHAR(50) NOT NULL,
    GearboxID INT NOT NULL,
	CONSTRAINT FK_StudentGearbox FOREIGN KEY (GearboxID) REFERENCES Gearbox(GearboxID),
    LicenseTypeID INT NOT NULL,
	CONSTRAINT FK_StudentLicenseType FOREIGN KEY (LicenseTypeID) REFERENCES LicenseType(LicenseTypeID),
    TeacherGender NVARCHAR(50) NULL,
    LowestPrice INT NOT NULL,
    HighestPrice INT NOT NULL,
    InstructorID INT NULL,
	CONSTRAINT FK_StudentInstructor FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID),
    LessonsCount INT NOT NULL,
    RegistrationDate DATE NOT NULL
);
CREATE UNIQUE INDEX student_email_unique ON
    Student(Email);

CREATE TABLE Lesson(
    LessonID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LTime TIME NOT NULL,
    LDate DATE NOT NULL,
    LDay NVARCHAR(50) NOT NULL,
    IsAvailable BIT NOT NULL,
    StuudentID INT NULL,
	CONSTRAINT FK_LessonStudent FOREIGN KEY(StuudentID) REFERENCES Student(StudentID),
    IsPaid BIT NOT NULL,
    HasDone BIT NOT NULL,
    InstructorID INT NOT NULL,
	CONSTRAINT FK_LessonInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID),
	ReviewID INT NOT NULL,
	CONSTRAINT FK_LessonReview FOREIGN KEY(ReviewID) REFERENCES Review(ReviewID),
);

CREATE TABLE EnrollmentRequests(
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LessonID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsLesson FOREIGN KEY(LessonID) REFERENCES Lesson(LessonID),
    StatusID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsEStatus FOREIGN KEY(StatusID) REFERENCES EStatus(StatusID),
    StudentID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsStudent FOREIGN KEY(StudentID) REFERENCES Student(StudentID)
);

CREATE TABLE InstructorReviews(
    ReviewID INT NOT NULL,
	CONSTRAINT FK_InstructorReviewsReview FOREIGN KEY(ReviewID) REFERENCES Review(ReviewID),
    StudentID INT NOT NULL,
	CONSTRAINT FK_InstructorReviewsStudent FOREIGN KEY(StudentID) REFERENCES Student(StudentID),
	InstructorID INT NOT NULL,
	CONSTRAINT FK_InstructorReviewsInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID),
	TimeReview DATETIME NOT NULL
);

ALTER TABLE LicenseType
ALTER COLUMN Description NVARCHAR(255)
Go
