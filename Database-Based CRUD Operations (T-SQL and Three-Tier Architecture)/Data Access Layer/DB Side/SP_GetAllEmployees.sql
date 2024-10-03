CREATE PROCEDURE SP_GetAllEmployees
AS
BEGIN
    SELECT *
    FROM Employees;
END

EXEC SP_GetAllEmployees;
