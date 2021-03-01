using EFAPP.ENTITIES;
using EFAPP.ENTITIES.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EFAPP.CORE
{
    public class AppServices
    {
        private readonly CompanyContext _dbContext;

        public AppServices(CompanyContext dBContest)
        {
            _dbContext = dBContest;
        }

        public bool AddEmployee(Employee employee)
        {
            //Adds and employee to the database
            _dbContext.Employees.Add(employee);
            int v = _dbContext.SaveChanges();
            if(v > 0)
            {
                return true;
            }
            return false;
        }

        public void AddDepartment(Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
        }

        public Employee FindEmployee(int employeeID)
        {
            //finds and returns an employee when given an employeeID or null
            var employee = _dbContext.Employees.Find(employeeID);
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Employees.Select(emp => emp).ToList();
        }

        public Department FindDepartment(int departmentID)
        {
            //finds and returns a Department when given an departmentID or null
            var department = _dbContext.Departments.Find(departmentID);
            return department;
        }

        public bool UpdateEmployee(Employee employee)
        {
            EntityEntry<Employee> entityEntry = _dbContext.Update(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public void UpdateDepartment(Department department)
        {
            EntityEntry<Department> entityEntry = _dbContext.Update(department);
            _dbContext.SaveChanges();
        }

        public bool DeleteDepartment(int departmentID)
        {
            // Deletes A department when given an ID
            var department = _dbContext.Departments.FindAsync(departmentID).Result;
            if (department is Department)
            {
                _dbContext.Departments.Remove(department);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<AllDeptNameAndIDs> FetchAllDepartmentsNames()
        {
            var result = _dbContext.Departments.Select(dept => new AllDeptNameAndIDs() { ID = dept.DepartmentID, Name = dept.Department_Name }).ToList();
            return result;
        }

        public bool DeleteEmployee(int employeeID)
        {
            // Deletes an Employee when given an ID
            var employee = _dbContext.Employees.FindAsync(employeeID).Result;
            if (employee is Employee)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void AssignEmployeeToDepartment(int departmentID = -1, int EmployeeID = -1)
        {
            if (EmployeeID >= 1 && departmentID >= 1)
            {
                var emp = FindEmployee(EmployeeID);
                var dept = FindDepartment(departmentID);
                dept.EMPLOYEES.Add(emp);
                _dbContext.Update(dept);
                _dbContext.SaveChanges();
            }
        }

        public List<AllEmployeesDepartmentNames> FetchAllEmployeesAndDepartmentNames()
        {
            var qry = _dbContext.Employees
                .Join(_dbContext.Departments,
                employee => employee.DepartmentID,
                department => department.DepartmentID,
                (employee, department) => new AllEmployeesDepartmentNames()
                {
                    FIRST_NAME = employee.FIRST_NAME,
                    lAST_NAME = employee.lAST_NAME,
                    EMAIL = employee.EMAIL,
                    HIRE_DATE = employee.HIRE_DATE,
                    PHONE_NUMBER = employee.PHONE_NUMBER,
                    SALARY = employee.SALARY,
                    STATE = employee.STATE,
                    DEPARTMENT_NAME = department.Department_Name
                }
                );

            return qry.ToList<AllEmployeesDepartmentNames>();
        }

        public List<IGrouping<string, AllEmployeesDepartmentNames>> DisplayRecordsGroupedByDepartments()
        {
            var records = FetchAllEmployeesAndDepartmentNames().GroupBy(emp => emp.DEPARTMENT_NAME).ToList();
            return records;
        }

        public List<Department> FetchAllDepartmentsNotAssignedToAnyEmployee()
        {
            //fetches all dept whose employees count == 0
            var lists = from dept in _dbContext.Departments
                        where dept.EMPLOYEES.Count < 1
                        select new Department()
                        {
                            DepartmentID = dept.DepartmentID,
                            Department_Name = dept.Department_Name,
                            EMPLOYEES = dept.EMPLOYEES
                        };

            var l = lists.ToList();
            return l;
        }

        public IEnumerable fetchEmployeesBySalaryRange()
        {
            return _dbContext.Employees.Where(emp => emp.SALARY > 150000 & emp.SALARY < 3000000).ToList();
        }

        public IEnumerable DepartmentNotAssignedToEmployee()
        {
            var d = _dbContext.Departments.Where(dept => dept.EMPLOYEES.Count < 1).ToList();
            return d;
        }
    }
}