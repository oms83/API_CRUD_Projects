CREATE PROCEDURE SP_UpdateEmployee
    @EmployeeID int,
    @FirstName nvarchar(100),
    @LastName nvarchar(100),
    @Age tinyint,
    @Salary decimal(10, 2),
    @HireDate DateTime,
    @TerminationDate DateTime
AS
BEGIN
    UPDATE Employees
    SET
        FirstName = @FirstName,
        LastName = @LastName,
        Age = @Age,
        Salary = @Salary,
        HireDate = @HireDate,
        TerminationDate = @TerminationDate
    WHERE EmployeeID = @EmployeeID;
END


EXEC SP_UpdateEmployee
    @EmployeeID = 1, -- Replace with actual EmployeeID
    @FirstName = 'Ali',
    @LastName = 'Kara',
    @Age = 35,
    @Salary = 25000,
    @HireDate = '2021-01-01',
    @TerminationDate = NULL; -- NULL if still employed
