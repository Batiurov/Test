using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using УП.ApplicationData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using УП.PageUssers;
using УП.ApplicationData;
namespace УП.PageUssers
{
    /// <summary>
    /// Логика взаимодействия для Notes.xaml
    /// </summary>
    public partial class Notes : Page
    {
        private Nottes _currentNote = new Nottes();

        public Notes(Nottes selectedNote = null)
        {

            InitializeComponent();
            FrmMain13.ItemsSource = baZVaEntities.GetContext().Nottes.ToList();

        }


        private void BtnAdd_Click13(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new NotesAdd(null));
        }

        private void BtnDelete_Click13(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = FrmMain13.SelectedItems.Cast<Nottes>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    baZVaEntities.GetContext().Nottes.RemoveRange(hotelsForRemoving);
                    baZVaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    FrmMain13.ItemsSource = baZVaEntities.GetContext().Nottes.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click13(object sender, RoutedEventArgs e)
        {
            var nottes = FrmMain13.SelectedItem as Nottes; // Если это компании, замени на 'Companies'

            if (nottes != null)
            {
                // Переход на страницу редактирования с передачей выбранного элемента
                AppFrame.frameMain.Navigate(new УП.PageUssers.NotesAdd(nottes));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Page_InVisibleChaged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                baZVaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                FrmMain13.ItemsSource = baZVaEntities.GetContext().Nottes.ToList();
            }
        }
    }
}
