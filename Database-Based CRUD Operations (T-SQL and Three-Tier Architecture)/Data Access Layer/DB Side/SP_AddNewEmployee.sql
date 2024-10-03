CREATE PROCEDURE SP_AddNewEmployee
    @FirstName nvarchar(100),
    @LastName nvarchar(100),
    @Age tinyint,
    @Salary decimal(10, 2),
    @HireDate DateTime,
    @TerminationDate DateTime,
    @EmployeeID int OUT
AS
BEGIN
    INSERT INTO Employees
    (FirstName, LastName, Age, Salary, HireDate, TerminationDate)
    VALUES
    (@FirstName, @LastName, @Age, @Salary, @HireDate, @TerminationDate);

    SET @EmployeeID = SCOPE_IDENTITY();
END

DECLARE @id int;
EXEC SP_AddNewEmployee
    @FirstName = 'Mustafa',
    @LastName = 'Yilmaz',
    @Age = 34,
    @Salary = 20000,
    @HireDate = '2020-09-01',
    @TerminationDate = '2023-10-20',
    @EmployeeID = @id OUT;
