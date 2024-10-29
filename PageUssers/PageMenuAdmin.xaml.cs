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
    /// Логика взаимодействия для PageMenuAdmin.xaml
    /// </summary>
    public partial class PageMenuAdmin : Page
    {
        public PageMenuAdmin()
        {
            InitializeComponent();
            FrmMain.ItemsSource = baZVaEntities.GetContext().Ussers.ToList();
        }

        

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ussers = FrmMain.SelectedItem as Ussers; // Если это компании, замени на 'Companies'

            if (ussers != null)
            {
                // Переход на страницу редактирования с передачей выбранного элемента
                AppFrame.frameMain.Navigate(new УП.PageAdmin.AddEditPage(ussers));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = FrmMain.SelectedItems.Cast<Ussers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    baZVaEntities.GetContext().Ussers.RemoveRange(hotelsForRemoving);
                    baZVaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    FrmMain.ItemsSource = baZVaEntities.GetContext().Ussers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_InVisibleChaged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                baZVaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                FrmMain.ItemsSource = baZVaEntities.GetContext().Ussers.ToList();
            }
        }
    }
}
