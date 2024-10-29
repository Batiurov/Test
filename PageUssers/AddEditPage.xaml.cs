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

namespace УП.PageAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Ussers _currentHotel = new Ussers();
        public AddEditPage(Ussers selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageMenuAdmin());
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.FirstName))
                errors.AppendLine("Укажите Артикул");
            if (string.IsNullOrWhiteSpace(_currentHotel.LastName))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentHotel.Telephone))
                errors.AppendLine("Укажите Единица_измерения");
            if (string.IsNullOrWhiteSpace(_currentHotel.Email))
                errors.AppendLine("Укажите Категория_товара");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.UsersID == 0)
                baZVaEntities.GetContext().Ussers.Add(_currentHotel);


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
