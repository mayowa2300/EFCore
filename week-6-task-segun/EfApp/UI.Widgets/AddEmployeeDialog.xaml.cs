using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfApp.UI.Widgets
{
    /// <summary>
    /// Interaction logic for AddEmployeeDialog.xaml
    /// </summary>
    public partial class AddEmployeeDialog : Window
    {
        public int deptSelectedID = -1;
        public int SelectedIndex = -1;
        public List<int> DeptIDs;

        public AddEmployeeDialog(EFAPP.CORE.AppServices _appServices, EFAPP.ENTITIES.Employee employee = null)
        {
            InitializeComponent();
            DeptIDs = _appServices.FetchAllDepartmentsNames().Select(d => d.ID).ToList();
            combo_depts.ItemsSource = _appServices.FetchAllDepartmentsNames().Select(d => d.Name);
            // Populate the Text fields when This function is called with an instance of employee
            if (employee != null)
            {
                txt_email.Text = employee.EMAIL;
                txt_firstname.Text = employee.FIRST_NAME;
                txt_lastname.Text = employee.lAST_NAME;
                txt_phone.Text = employee.PHONE_NUMBER;
                txt_salary.Text = employee.SALARY.ToString();
                combo_depts.SelectedItem = employee.DepartmentID;
                hire_date.SelectedDate = employee.HIRE_DATE;
                txt_state.Text = employee.STATE;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void combo_depts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo_depts.SelectedIndex >= 0)
            {
                deptSelectedID = DeptIDs[combo_depts.SelectedIndex];
                SelectedIndex = combo_depts.SelectedIndex;
            }
            else
            {
                deptSelectedID = -1;
            }
        }
    }
}