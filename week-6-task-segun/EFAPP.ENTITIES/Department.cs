using System.Collections.Generic;

namespace EFAPP.ENTITIES
{
    public class Department
    {
       
        public int DepartmentID { get; set; }
        public string Department_Name { get; set; }

        //Navigational Properties
        //public int EmployeeID { get; set; }
        public List<Employee> EMPLOYEES { get; set; }
    }
}