using EFAPP.CORE;
using EFAPP.ENTITIES;
using System.Windows;

namespace EfApp.UI.Widgets
{
    /// <summary>
    /// Interaction logic for AddDepartmentDialog.xaml
    /// </summary>
    public partial class AddDepartmentDialog : Window
    {
        private AppServices appService;
        public AddDepartmentDialog(EFAPP.CORE.AppServices _appServices)
        {
            InitializeComponent();
            appService = _appServices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Deptname.Text.Length > 2)
            {
                appService.AddDepartment(new Department() { Department_Name = Deptname.Text });
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}