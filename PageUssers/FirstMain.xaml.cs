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
namespace УП.PageUssers
{
    /// <summary>
    /// Логика взаимодействия для FirstMain.xaml
    /// </summary>
    public partial class FirstMain : Page
    {
        public FirstMain()
        {
            InitializeComponent();
            baZVaEntities.GetContext().Commpanys.ToList();
        }


        private void Btnn1(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageAdmin.PageMenuAdmin());
        }

        private void Btnn2(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageCompanys.PageMenuCompanys());
        }

        private void Btnn3(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageUssers.Reminder());
        }

        private void Btnn4(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new УП.PageUssers.Notes(null));
        }
    }
}
