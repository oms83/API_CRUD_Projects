CREATE TABLE Employees
(
    EmployeeID Int PRIMARY KEY IDENTITY(1, 1),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Age TINYINT,
    Salary DECIMAL(10, 2), 
    HireDate DATETIME,
    TerminationDate DATETIME NULL
);

INSERT INTO Employees (FirstName, LastName, Age, Salary, HireDate, TerminationDate)
VALUES 
('Omer', 'Memes', 24, 100000, '2020-01-01', NULL),
('Ali', 'Yilmaz', 30, 95000, '2024-02-01', NULL),
('Ayşe', 'Demir', 28, 85000, '2022-03-01', NULL),
('Fatma', 'Kaya', 26, 75000, '2024-04-01', NULL),
('Mehmet', 'Çelik', 35, 120000, '2023-05-01', '2024-01-15'),
('Ahmet', 'Şahin', 29, 98000, '2024-06-01', NULL),
('Selin', 'Kara', 27, 92000, '2024-07-01', NULL),
('Cem', 'Aydın', 33, 110000, '2024-08-01', NULL),
('Can', 'Polat', 31, 105000, '2024-09-01', NULL),
('Elif', 'Tekin', 25, 87000, '2024-10-01', NULL),
('Berk', 'Turan', 32, 115000, '2024-11-01', NULL),
('Zeynep', 'Koç', 22, 72000, '2024-12-01', NULL),
('Burak', 'Eren', 34, 113000, '2024-01-15', '2024-08-15'),
('Derya', 'Gündüz', 23, 68000, '2024-02-15', NULL),
('Emre', 'Çakır', 36, 118000, '2024-03-15', '2024-09-15'),
('Gizem', 'Doğan', 29, 95000, '2024-04-15', NULL),
('Tugba', 'Arslan', 28, 90000, '2024-05-15', NULL),
('Hakan', 'Bozkurt', 27, 85000, '2024-06-15', NULL),
('Merve', 'Ersoy', 31, 108000, '2024-07-15', NULL),
('Serkan', 'Sezer', 33, 112000, '2024-08-15', NULL);
