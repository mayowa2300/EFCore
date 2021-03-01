using EFAPP.CORE;
using EFAPP.ENTITIES;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppServices _appServices;
        private int _Selected_index;
        private Employee employee;

        public MainWindow()
        {
            InitializeComponent();
            CompanyContext companyContext = new CompanyContext();
            _appServices = new AppServices(companyContext);

            datagrid.ItemsSource = _appServices.GetEmployees();
            // _appServices.FindDepartment(1);
            System.Collections.Generic.List<EFAPP.ENTITIES.DTO.AllEmployeesDepartmentNames> lists = _appServices.FetchAllEmployeesAndDepartmentNames();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Creates and Adds a new employee into Db
            var dialog = new UI.Widgets.AddEmployeeDialog(_appServices);
            dialog.Owner = this;
            var v = dialog.ShowDialog();
            if (v == true)
            {
                var emp = new Employee()
                {
                    FIRST_NAME = dialog.txt_firstname.Text,
                    lAST_NAME = dialog.txt_lastname.Text,
                    EMAIL = dialog.txt_email.Text,
                    PHONE_NUMBER = dialog.txt_phone.Text,
                    HIRE_DATE = dialog.hire_date.SelectedDate.Value,
                    SALARY = Convert.ToInt32(dialog.txt_salary.Text),
                    DepartmentID = dialog.deptSelectedID,
                    STATE = dialog.txt_state.Text
                };
                if (_appServices.AddEmployee(emp))
                {
                    datagrid.ItemsSource = _appServices.GetEmployees();
                    MessageBox.Show("Employee Created!");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Update Employee

            if (!(employee is Employee))
            {
                label_current_emp.Content = "Select an Employee to Update!";
                return;
            }
            var dialog = new UI.Widgets.AddEmployeeDialog(_appServices, employee);
            dialog.label_header.Content = "Update Employee details.";
            dialog.combo_depts.SelectedIndex = _Selected_index;
            dialog.Owner = this;
            var v = dialog.ShowDialog();
            if (v == true)
            {
                employee.EmployeeID = employee.EmployeeID;
                employee.SALARY = Convert.ToDouble(dialog.txt_salary.Text);
                employee.FIRST_NAME = dialog.txt_firstname.Text;
                employee.lAST_NAME = dialog.txt_lastname.Text;
                employee.EMAIL = dialog.txt_email.Text;
                employee.PHONE_NUMBER = dialog.txt_phone.Text;
                employee.HIRE_DATE = dialog.hire_date.SelectedDate.Value;
                employee.DepartmentID = dialog.deptSelectedID;
                employee.STATE = dialog.txt_state.Text;

                if (_appServices.UpdateEmployee(employee))
                {
                    MessageBox.Show("Employee Updated!");
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //updates the datagrid with employee data and department names
            datagrid.ItemsSource = _appServices.FetchAllEmployeesAndDepartmentNames();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ListCollectionView col = new ListCollectionView(_appServices.FetchAllEmployeesAndDepartmentNames());
            col.GroupDescriptions.Add(new PropertyGroupDescription("DEPARTMENT_NAME"));
            datagrid.ItemsSource = col;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (employee is Employee)
            {
                _appServices.DeleteEmployee(employee.EmployeeID);
                label_current_emp.Content = "Employee Deleted!";
            }
            else
            {
                label_current_emp.Content = "Select an Employee to Delete!.";
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                employee = (Employee)datagrid.SelectedItem;
                _Selected_index = datagrid.SelectedIndex;
            }
            catch (Exception)
            {
            }
            if (employee is Employee)
            {
                label_current_emp.Content = employee.EMAIL;
            }
        }

        private void fetchAllemployeeEV(object sender, RoutedEventArgs e)
        {
            //updates the datagrid with employee data

            datagrid.ItemsSource = _appServices.GetEmployees();
        }

        private void emp_salary_btn_Click(object sender, RoutedEventArgs e)
        {
            //fetches employees by salary range
            datagrid.ItemsSource = _appServices.fetchEmployeesBySalaryRange();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = _appServices.DepartmentNotAssignedToEmployee();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var dialog = new UI.Widgets.AddDepartmentDialog(_appServices);
            dialog.Owner = this;
            var v = dialog.ShowDialog();
            if (v == true)
            {
                MessageBox.Show("Deptartment Created");
            }
        }
    }
}