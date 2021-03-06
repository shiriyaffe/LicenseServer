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
    Content NVARCHAR(255) NOT NULL,
    writtenOn Date Not null
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
    NumOfTeachers INT NOT NULL
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
    RegistrationDate DATETIME NOT NULL,
    eStatusId int NOT NULL,
    CONSTRAINT FK_SchoolManagerEStatus FOREIGN KEY(eStatusId) REFERENCES EStatus(StatusID)
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
    Details NVARCHAR(255),
    DrivingSchoolID INT NULL,
    CONSTRAINT FK_InstructorDrivingSchools FOREIGN KEY (DrivingSchoolID) REFERENCES DrivingSchools(SchoolID),
    SchoolManagerID INT,
	CONSTRAINT FK_InstructorSchoolManager FOREIGN KEY(SchoolManagerID) REFERENCES SchoolManager(SManagerID),
    RegistrationDate DATETIME NOT NULL,
    StartTime NVARCHAR(255) NOT NULL,
    EndTime NVARCHAR(255) NOT NULL,
    eStatusId int NOT NULL,
    CONSTRAINT FK_InstructorEStatus FOREIGN KEY(eStatusId) REFERENCES EStatus(StatusID),
    RateID int null,
    CONSTRAINT FK_InstructorRate FOREIGN KEY(RateID) REFERENCES Rate(RateID)
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
    RegistrationDate DATETIME NOT NULL,
    eStatusId int
    CONSTRAINT FK_StudentEStatus FOREIGN KEY(eStatusId) REFERENCES EStatus(StatusID)
);
CREATE UNIQUE INDEX student_email_unique ON
    Student(Email);

CREATE TABLE Lesson(
    LessonID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LDate DATE NOT NULL,
    LDay NVARCHAR(255) NOT NULL,
    IsAvailable BIT NOT NULL,
    StuudentID INT NULL,
	CONSTRAINT FK_LessonStudent FOREIGN KEY(StuudentID) REFERENCES Student(StudentID),
    IsPaid BIT NOT NULL,
    HasDone BIT NOT NULL,
    InstructorID INT NOT NULL,
	CONSTRAINT FK_LessonInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID),
	ReviewID INT NULL,
	CONSTRAINT FK_LessonReview FOREIGN KEY(ReviewID) REFERENCES Review(ReviewID),
    eStatusId int NOT NULL,
    CONSTRAINT FK_LessonEStatus FOREIGN KEY(eStatusId) REFERENCES EStatus(StatusID),
    LTime NVARCHAR(255) NOT NULL
);

CREATE TABLE EnrollmentRequests(
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    LessonID INT NULL,
	CONSTRAINT FK_EnrollmentRequestsLesson FOREIGN KEY(LessonID) REFERENCES Lesson(LessonID),
    StatusID INT NOT NULL,
	CONSTRAINT FK_EnrollmentRequestsEStatus FOREIGN KEY(StatusID) REFERENCES EStatus(StatusID),
    StudentID INT NULL,
	CONSTRAINT FK_EnrollmentRequestsStudent FOREIGN KEY(StudentID) REFERENCES Student(StudentID),
	InstructorID INT NULL,
	CONSTRAINT FK_EnrollmentRequestsInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID),
    SchoolID INT NULL,
	CONSTRAINT FK_EnrollmentRequestsDrivingSchool FOREIGN KEY(SchoolID) REFERENCES DrivingSchools(SchoolID)
);

CREATE TABLE InstructorReviews(
    ReviewID INT NOT NULL,
	CONSTRAINT FK_InstructorReviewsReview FOREIGN KEY(ReviewID) REFERENCES Review(ReviewID),
	InstructorID INT NOT NULL,
	CONSTRAINT FK_InstructorReviewsInstructor FOREIGN KEY(InstructorID) REFERENCES Instructor(InstructorID)
);

ALTER TABLE InstructorReviews
ADD CONSTRAINT PK_InstructorReview PRIMARY KEY (ReviewID,InstructorID);

CREATE TABLE StudentSummarys(
    ReviewID INT NOT NULL,
	CONSTRAINT FK_InstructorStudentSummarys FOREIGN KEY(ReviewID) REFERENCES Review(ReviewID),
	StudentID INT NOT NULL,
	CONSTRAINT FK_StudentStudentSummarys FOREIGN KEY(StudentID) REFERENCES Student(StudentID),
    LessonID int NOT NULL,
    CONSTRAINT FK_LessonStudentSummarys FOREIGN KEY(LessonID) REFERENCES Lesson(LessonID)
);

ALTER TABLE StudentSummarys
ADD CONSTRAINT PK_StudentReview PRIMARY KEY (ReviewID,StudentID);

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
VALUES (N'??????????????');

INSERT INTO Gearbox(Type)
VALUES (N'????????');


INSERT INTO Gender(GenderType)
VALUES (N'????????');

INSERT INTO Gender(GenderType)
VALUES (N'??????');

INSERT INTO Gender(GenderType)
VALUES (N'??????');


INSERT INTO LessonLength(SLength)
VALUES (N'30');

INSERT INTO LessonLength(SLength)
VALUES (N'40');

INSERT INTO LessonLength(SLength)
VALUES (N'60');


INSERT INTO LicenseType([LType], [Description])
VALUES (N'A', N'???????????? ???????? ???? ?????????? ?????? ?????????? ???????? ???? ??????');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'B', N'???????????? ???????? ????????-?????????? ???? 3.5 ?????? ?????? 8 ???????????? ?????????? ????????');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'C', N'???????????? ???????????? ?????? ?????????? ????????');

INSERT INTO LicenseType([LType], [Description])
VALUES (N'D', N'???????????? ????????????????');


INSERT INTO Rate(RateMeaning)
VALUES (N'????????');

INSERT INTO Rate(RateMeaning)
VALUES (N'????????');

INSERT INTO Rate(RateMeaning)
VALUES (N'??????');

INSERT INTO Rate(RateMeaning)
VALUES (N'????????');

INSERT INTO Rate(RateMeaning)
VALUES (N'??????????');

INSERT INTO Rate(RateMeaning)
VALUES (N'?????? ??????????');


INSERT INTO Area(AreaName)
VALUES (N'????????');

INSERT INTO Area(AreaName)
VALUES (N'????????');

INSERT INTO Area(AreaName)
VALUES (N'????????');


INSERT INTO City([CityName], [AreaID])
VALUES (N'?????????? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???????? ????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???????? ??????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???????? ??????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ??????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???? ????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',2);

INSERT INTO City([CityName], [AreaID])
VALUES (N'???? ????????-??????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'??????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',3);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'????????',1);

INSERT INTO City([CityName], [AreaID])
VALUES (N'?????? ????????????', 3);

INSERT INTO EStatus(StatusMeaning)
VALUES (N'????????????');

INSERT INTO EStatus(StatusMeaning)
VALUES (N'??????????');

INSERT INTO EStatus(StatusMeaning)
VALUES (N'????????');

INSERT INTO EStatus(StatusMeaning)
VALUES (N'?????????? ???? ????????');

INSERT INTO DrivingSchools([SchoolName],[AreaID],[EstablishmentYear],[NumOfTeachers])
VALUES (N'GalDa',3,2017,18);

INSERT INTO SchoolManager([SMName],[SchoolID],[RegistrationDate],[PhoneNumber],[Pass],[GenderID],[Email],[Birthday], [eStatusId])
VALUES (N'???? ????????????',1,CAST(N'2021-1-10 12:23:00.000' AS DateTime),N'0545403304',13579,1,N'g@d.com',CAST(N'1950-08-13' AS Date),2);

Insert INTO Instructor([IName],[Email],[Pass],[PhoneNumber],[GenderID],[Birthday],[AreaID],[Details],[DrivingSchoolID],[GearboxID],[LessonLengthID],[LicenseTypeID],[Price],[RateID],[RegistrationDate],[SchoolManagerID],[StartTime],[EndTime],[eStatusId])
VALUES (N'?????? ????????', N'n@g.com', N'24680', N'0505689857',1, CAST(N'1954-08-24' AS Date), 3, N'???????????? ???????? ?????????? ???????? 15 ??????, ???????? 5 ?????????? ???????? ???????? ???????????? ????????????. ?????????? ???????????????? ??????????', 1, 1,3,1,200, 4, CAST(N'2021-1-17 8:23:00.000' AS DateTime),1,N'07:00',N'20:00', 2);

Insert INTO Instructor([IName],[Email],[Pass],[PhoneNumber],[GenderID],[Birthday],[AreaID],[Details],[DrivingSchoolID],[GearboxID],[LessonLengthID],[LicenseTypeID],[Price],[RateID],[RegistrationDate],[SchoolManagerID],[StartTime],[EndTime],[eStatusId])
VALUES (N'???????? ??????', N'y@l.com', N'67890', N'0542130122',1, CAST(N'1960-07-02' AS Date), 3, N'???????????? ???????? ?????????? ???????? ??????????, ???????????? ????????, ?????????? 20 ??????, ?????????????? ???????????? ???????????? ?????????? ???? ?????????? ????????. ?????????? ???????????????? ??????????', 1, 1,3,1,210, 4, CAST(N'2021-1-23 10:04:00.000' AS DateTime),1,N'08:00',N'21:00', 1);

Insert INTO Student([SName],[Email], [Pass],[PhoneNumber], [GenderID], [Birthday], [CityID], [SAddress], [GearboxID], [LicenseTypeID], [TeacherGender], [HighestPrice], [LessonsCount], [RegistrationDate], [LessonLengthID], [InstructorID],[eStatusId])
VALUES (N'???????? ??????', N's@y.com', N'123456', N'0534261684',1, CAST(N'2004-09-22' AS Date), 20, N'???????? 6', 1, 1, 1, 220, 0, CAST(N'2021-12-12 8:23:00.000' AS DateTime), 3, 1, 2);

INSERT INTO EnrollmentRequests([InstructorID],[SchoolID],[StatusID])
VALUES (2,1,1);

Insert INTO Student([SName],[Email], [Pass],[PhoneNumber], [GenderID], [Birthday], [CityID], [SAddress], [GearboxID], [LicenseTypeID], [TeacherGender], [HighestPrice], [LessonsCount], [RegistrationDate], [LessonLengthID],[eStatusId], [InstructorID])
VALUES (N'???????? ??????????', N's@u.com', N'123456', N'0522394165',1, CAST(N'2004-04-04' AS Date), 20, N'???????????? 2', 1, 1, 1, 200, 0, CAST(N'2022-05-30 8:23:00.000' AS DateTime), 3, 1, 1);

INSERT INTO EnrollmentRequests([InstructorID],[StudentID],[StatusID])
VALUES (1,2,1);

Insert INTO Lesson([HasDone],[InstructorID],[IsPaid],[LDate],[LDay],[StuudentID],[IsAvailable],[eStatusId],[LTime])
VALUES (1, 1, 0, CAST(N'2022-05-27' AS Date), N'????????', 1, 0 ,2, N'10:00');

Insert INTO Lesson([HasDone],[InstructorID],[IsPaid],[LDate],[LDay],[StuudentID],[IsAvailable],[eStatusId],[LTime])
VALUES (0, 1, 0, CAST(N'2022-06-20' AS Date), N'??????', 1, 0 ,1, N'14:00');