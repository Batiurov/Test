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
using System.Windows.Shapes;
using УП.ApplicationData;
namespace УП.PageCompanys
{
    /// <summary>
    /// Логика взаимодействия для PageMenuCompanys.xaml
    /// </summary>
    public partial class PageMenuCompanys : Page
    {
        public PageMenuCompanys()
        {
            InitializeComponent();
            FrmMain2.ItemsSource = baZVaEntities.GetContext().Commpanys.ToList();
        }
        private void Page_InVisibleChaged2(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                baZVaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                FrmMain2.ItemsSource = baZVaEntities.GetContext().Commpanys.ToList();
            }
        }

        private void BtnEdit_Click2(object sender, RoutedEventArgs e)
        {
            var commpanys = FrmMain2.SelectedItem as Commpanys; // Если это компании, замени на 'Companies'

            if (commpanys != null)
            {
                // Переход на страницу редактирования с передачей выбранного элемента
                AppFrame.frameMain.Navigate(new УП.PageUssers.PageEditAdd2(commpanys));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnAdd_Click2(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageUssers.PageEditAdd2(null));
        }

        private void BtnDelete_Click2(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = FrmMain2.SelectedItems.Cast<Ussers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    baZVaEntities.GetContext().Ussers.RemoveRange(hotelsForRemoving);
                    baZVaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    FrmMain2.ItemsSource = baZVaEntities.GetContext().Ussers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
