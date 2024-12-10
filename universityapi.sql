CREATE DATABASE universityapi;

CREATE TABLE University (
    UniversityId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UniversityName VARCHAR(255) NOT NULL,
    Contact VARCHAR(255),
    Location VARCHAR(255),
    EstablishmentYear VARCHAR(4) NOT NULL,
    UniversityType VARCHAR(50)
);

CREATE TABLE Department (
    DepartmentId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    DepartmentName VARCHAR(255) NOT NULL,
    DepartmentHead VARCHAR(255),
    UniversityId INT NOT NULL,
    FOREIGN KEY (UniversityId) REFERENCES University(UniversityId)
);

CREATE TABLE Course (
    CourseId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    CourseName VARCHAR(255) NOT NULL,
    Credit_Hours INT NOT NULL,
    Description VARCHAR(255),
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

/*Insert data*/
INSERT INTO University (UniversityName, Contact, Location, EstablishmentYear, UniversityType)
VALUES
('DkIT', '123-456-7890', 'Dundalk, Ireland', '1990', 'Public'),
('UCD', '987-654-3210', 'Dublin Ireland', '2005', 'Private');

INSERT INTO Department (DepartmentName, DepartmentHead, UniversityId)
VALUES
('Computer Science', 'Dr. Smith', 1),
('Physics', 'Prof. Johnson', 2);

INSERT INTO Course (CourseName, Credit_Hours, Description, DepartmentId)
VALUES
('Introduction to Programming', 3, 'Basic programming concepts', 1),
('Quantum Mechanics', 4, 'Advanced physics', 2);
