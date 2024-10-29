using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using УП.ApplicationData;
using УП.PageUssers; // или другой namespace, в котором определен PageMenuAdmin

namespace УП.PageUssers
{
    /// <summary>
    /// Логика взаимодействия для PageEditAdd2.xaml
    /// </summary>
    public partial class PageEditAdd2 : Page
    {
        private Commpanys _currentHotel2 = new Commpanys();
        public PageEditAdd2(Commpanys selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentHotel2 = selectedHotel;

            DataContext = _currentHotel2;
        }

        private void BtnBack_Click2(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageCompanys.PageMenuCompanys());


        }

        private void BtnSave_Click2(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel2.CompanyName))
                errors.AppendLine("Укажите Артикул");
            if (string.IsNullOrWhiteSpace(_currentHotel2.CompanyAddress))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentHotel2.CompanyEmail))
                errors.AppendLine("Укажите Единица_измерения");
            if (string.IsNullOrWhiteSpace(_currentHotel2.CompanyTelephone))
                errors.AppendLine("Укажите Категория_товара");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel2.CompanysID == 0)
                baZVaEntities.GetContext().Commpanys.Add(_currentHotel2);


            try
            {

                baZVaEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show(
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
