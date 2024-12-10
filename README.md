# Employee- Web Api for Search, Filter & Pagination
This project is a Web API that provides search, filter, and pagination functionality for managing employee data. The data is stored in an MS SQL Server database and can be accessed through RESTful API endpoints. Additionally, the search, filter, and pagination operations are optimized using stored procedures directly in the database, ensuring better performance for large datasets.

## Key Features:
* **Search**: Allows searching employees by name or designation.
* **Filter**: Provides filtering capabilities for employees based on various criteria (e.g., salary).
* **Pagination**: Supports pagination to handle large datasets, allowing clients to fetch a specific number of records per request.
* **Stored Procedures**: The API uses stored procedures in the MS SQL Server to handle search, filter, and pagination operations on the database side for improved performance.

## Technologies Used:
* **ASP.NET Core 8.0**: Framework for building the Web API.
* **MS SQL Server**: Database management system used to store employee data.
* **Entity Framework Core**: ORM used to interact with the SQL Server database.
* **Stored Procedures**: For performing efficient search, filter, and pagination operations directly on the database.
* **Swagger/OpenAPI**: For API documentation and testing (available in the development environment).

## Learning Outcomes:
* Understanding how to build a RESTful API using ASP.NET Core.
* Implementing pagination to manage large datasets effectively.
* Adding search and filtering functionality to API endpoints.
* Utilizing stored procedures to handle data operations efficiently on the server side.
* Working with MS SQL Server and Entity Framework Core to perform CRUD operations.
* Setting up and configuring Swagger for API documentation and testing.

## SQL Stored Procedure for Search, Filter, and Pagination Operations

```sql
CREATE PROCEDURE GetEmployees
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SearchString NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Pagination calculations
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    -- Variable to store numeric conversion of @SearchString
    DECLARE @SearchSalary DECIMAL(18, 2);

    -- Try to convert @SearchString to a numeric value (for salary comparison)
    BEGIN TRY
        SET @SearchSalary = CAST(@SearchString AS DECIMAL(18, 2));
    END TRY
    BEGIN CATCH
        -- If conversion fails, set @SearchSalary to NULL (no salary filter)
        SET @SearchSalary = NULL;
    END CATCH

    -- Query the employees with filters
    SELECT EmployeeId, Name, Designation, Salary
    FROM Employees
    WHERE
        (
            -- Search by Name or Designation
            (@SearchString IS NULL OR Name LIKE '%' + @SearchString + '%') 
            OR (@SearchString IS NULL OR Designation LIKE '%' + @SearchString + '%')
        )
        -- Only apply Salary filter if SearchString is a valid number (search for Salary)
        AND (@SearchSalary IS NULL OR Salary = @SearchSalary)
    ORDER BY EmployeeId -- You can change this to any column for sorting
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
```
