Use master
Create Database LicenseDB
Go

Use LicenseDB
Go

CREATE TABLE Gearbox(
    GearboxID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Type NVARCHAR(255) NOT NULL
);

CREATE TABLE LicenseType(
    LicenseTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LType NCHAR NOT NULL,
    Description NVARCHAR(255) NOT NULL
);


CREATE TABLE Area(
    AreaID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    AreaName NVARCHAR(255) NOT NULL
);

CREATE TABLE City(
    CityID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CityName NVARCHAR(255) NOT NULL,
	AreaID INT,
	CONSTRAINT FK_AreaCity FOREIGN KEY (AreaID) REFERENCES Area(AreaID)
);


CREATE TABLE Rate(
    RateID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    RateMeaning NVARCHAR(255) NOT NULL
);

CREATE TABLE Review(
    ReviewID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Content NVARCHAR(255) NOT NULL
);


CREATE TABLE Gender(
    GenderID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    GenderType NVARCHAR(255) NOT NULL
);

CREATE TABLE EStatus(
    StatusID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    StatusMeaning NVARCHAR(255) NOT NULL
);


CREATE TABLE LessonLength(
    LessonLengthID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    SLength INT NOT NULL
);

CREATE TABLE AppAdmin(
    AdminID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    AEmail NVARCHAR(255) NOT NULL,
    APass NVARCHAR(255) NOT NULL
);

CREATE TABLE DrivingSchools(
    SchoolID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    SchoolName NVARCHAR(255) NOT NULL,
    AreaID INT NOT NULL,
	CONSTRAINT FK_SchoolManagerArea FOREIGN KEY(AreaID) REFERENCES Area(AreaID),
    EstablishmentYear INT NOT NULL,
    NumOfTeachers INT NOT NULL,
);

CREATE TABLE SchoolManager(
    SManagerID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    SMName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(12) NOT NULL,
    PhoneNumber NVARCHAR(10) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_SchoolManagerGender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID),
    Birthday DATE,
    SchoolID INT,
    CONSTRAINT FK_SchoolManagerDrivingSchools FOREIGN KEY(SchoolID) REFERENCES DrivingSchools(SchoolID),
    RegistrationDate DATETIME NOT NULL
);
CREATE UNIQUE INDEX schoolmanager_email_unique ON
    SchoolManager(Email);

    
CREATE TABLE Instructor(
    InstructorID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(12) NOT NULL,
    PhoneNumber NVARCHAR(10) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_InstructorGender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID),
    Birthday DATE NOT NULL,
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
    DrivingSchoolID INT NOT NULL,
    CONSTRAINT FK_InstructorDrivingSchools FOREIGN KEY (DrivingSchoolID) REFERENCES DrivingSchools(SchoolID),
    SchoolManagerID INT,
	CONSTRAINT FK_InstructorSchoolManager FOREIGN KEY(SchoolManagerID) REFERENCES SchoolManager(SManagerID),
    RateID INT NOT NULL,
	CONSTRAINT FK_InstructorRate FOREIGN KEY(RateID) REFERENCES Rate(RateID),
    RegistrationDate DATETIME NOT NULL
);
    CREATE UNIQUE INDEX instructor_email_unique ON
    Instructor(Email);

	CREATE TABLE Student(
    StudentID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    SName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(12) NOT NULL,
    PhoneNumber NVARCHAR(10) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_StudentGender FOREIGN KEY (GenderID) REFERENCES Gender(GenderID),
    Birthday DATE NOT NULL,
    CityID INT NOT NULL,
	CONSTRAINT FK_StudentCity FOREIGN KEY (CityID) REFERENCES City(CityID),
    SAddress NVARCHAR(255) NOT NULL,
    GearboxID INT NOT NULL,
	CONSTRAINT FK_StudentGearbox FOREIGN KEY (GearboxID) REFERENCES Gearbox(GearboxID),
    LicenseTypeID INT NOT NULL,
	CONSTRAINT FK_StudentLicenseType FOREIGN KEY (LicenseTypeID) REFERENCES LicenseType(LicenseTypeID),
    TeacherGender INT NULL,
    LessonLengthID INT NOT NULL,
    CONSTRAINT FK_StudentLessonLength FOREIGN KEY (LessonLengthID) REFERENCES LessonLength(LessonLengthID),
    HighestPrice INT NOT NULL,
    InstructorID INT,
	CONSTRAINT FK_StudentInstructor FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID),
    LessonsCount INT NOT NULL,
    RegistrationDate DATETIME NOT NULL
);
CREATE UNIQUE INDEX student_email_unique ON
    Student(Email);

CREATE TABLE Lesson(
    LessonID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LTime TIME NOT NULL,
    LDate DATE NOT NULL,
    LDay NVARCHAR(255) NOT NULL,
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
	CONSTRAINT FK_EnrollmentRequestsStudent FOREIGN KEY(StudentID) REFERENCES Student(StudentID),
	InstructorID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID)
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

CREATE TABLE WorkingHours(
    HourID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    WHour NVARCHAR(255) NOT NULL
);

INSERT INTO WorkingHours(WHour)
VALUES(N'00:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'01:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'02:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'03:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'04:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'05:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'06:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'07:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'08:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'09:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'10:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'11:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'12:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'13:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'14:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'15:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'16:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'17:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'18:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'19:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'20:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'21:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'22:00');
INSERT INTO WorkingHours(WHour)
VALUES(N'23:00');


INSERT INTO Gearbox(Type)
VALUES (N'אוטומטי');

INSERT INTO Gearbox(Type)
VALUES (N'ידני');


INSERT INTO Gender(GenderType)
VALUES (N'נקבה');

INSERT INTO Gender(GenderType)
VALUES (N'זכר');

INSERT INTO Gender(GenderType)
VALUES (N'אחר');


INSERT INTO LessonLength(SLength)
VALUES (N'30');

INSERT INTO LessonLength(SLength)
VALUES (N'40');

INSERT INTO LessonLength(SLength)
VALUES (N'60');


INSERT INTO LicenseType([LType], [Description])
VALUES (N'A', N'רישיון לרכב דו גלגלי ללא הגבלת הספק או נפח');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'B', N'רישיון לרכב פרטי-מסחרי עד 3.5 טון ועד 8 נוסעים בנוסף לנהג');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'C', N'רישיון למשאית ללא הגבלת משקל');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'D', N'רישיון לאוטובוס');


INSERT INTO Rate(RateMeaning)
VALUES (N'גרוע');

INSERT INTO Rate(RateMeaning)
VALUES (N'בסדר');

INSERT INTO Rate(RateMeaning)
VALUES (N'טוב');

INSERT INTO Rate(RateMeaning)
VALUES (N'אחלה');

INSERT INTO Rate(RateMeaning)
VALUES (N'מעולה');

INSERT INTO Rate(RateMeaning)
VALUES (N'אין דירוג');


INSERT INTO Area(AreaName)
VALUES (N'צפון');

INSERT INTO Area(AreaName)
VALUES (N'דרום');

INSERT INTO Area(AreaName)
VALUES (N'מרכז');


INSERT INTO City([CityName], [AreaID])
VALUES (N'ראשון לציון',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'רחובות',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'רמלה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'רמת גן',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'רמת השרון',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'רעננה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'פתח תקווה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'אור יהודה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נתיבות',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נתניה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נס ציונה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נהריה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'לוד',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'קרית אונו',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'קרית שמונה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'קרית אתא',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'כפר סבא',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'ירושלים',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'חולון',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'הוד השרון',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'הרצליה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'חיפה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'חדרה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'גבעתיים',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'אילת',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'בני ברק',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'דימונה',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'באר שבע',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'בת ים',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'אשדוד',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'אשקלון',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'אריאל',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'ערד',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'עכו',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'עפולה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'ראש העין',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'שדרות',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'תל אביב-יפו',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'טבריה',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'צפת',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'יהוד',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'יקנעם',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'כפר יונה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נשר',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'מתן',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'סביון',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'יבנה',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'כרמיאל',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'נצרת',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'כפר שמריהו', 3);

Insert INTO Student([SName],[Email], [Pass],[PhoneNumber], [GenderID], [Birthday], [CityID], [SAddress], [GearboxID], [LicenseTypeID], [TeacherGender], [HighestPrice], [LessonsCount], [RegistrationDate], [LessonLengthID])
VALUES (N'שירי יפה', N'6363shiri@gmail.com', N'123456', N'0534261684',1, CAST(N'2004-09-22' AS Date), 20, N'תמוז 6', 1, 1, 1, 220, 0, CAST(N'2021-12-12 8:23:00.000' AS DateTime), 3);
