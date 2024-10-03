CREATE PROCEDURE SP_DeleteEmployee
    @EmployeeID int
AS
BEGIN
    DELETE FROM Employees
    WHERE EmployeeID = @EmployeeID;
END

EXEC SP_DeleteEmployee
    @EmployeeID = 1; -- Replace with actual EmployeeID to delete
