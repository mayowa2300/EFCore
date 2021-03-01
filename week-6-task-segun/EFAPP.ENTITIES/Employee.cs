using System;

namespace EFAPP.ENTITIES
{
    public class Employee
    {
        public int    EmployeeID { get; set; }
        public string FIRST_NAME { get; set; }
        public string lAST_NAME { get; set; }
        public string EMAIL { get; set; }
        public string STATE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public DateTime HIRE_DATE { get; set; }
        public double SALARY { get; set; }
        //NAVIGATIONAL REFRENCE PROPERTY
        public int DepartmentID { get; set; }
    }
}