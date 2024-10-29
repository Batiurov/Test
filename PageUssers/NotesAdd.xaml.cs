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
    /// Логика взаимодействия для NotesAdd.xaml
    /// </summary>
    public partial class NotesAdd : Page
    {
        private Nottes _currentNote = new Nottes();
        public NotesAdd(Nottes selectedNote = null)
        {
            InitializeComponent();
            if (selectedNote != null)
                _currentNote = selectedNote;

            DataContext = _currentNote; // Привязываем данные к интерфейсу
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentNote.NoteText))
                errors.AppendLine("Укажите NoteText");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Отладочный вывод значений
            MessageBox.Show($"NoteID: {_currentNote.NoteID}, NoteText: {_currentNote.NoteText}");

            if (_currentNote.NoteID == 0)
                baZVaEntities.GetContext().Nottes.Add(_currentNote);

            try
            {
                baZVaEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var entityValidationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                string message = $"Произошла ошибка при сохранении: {ex.Message}\n";
                if (ex.InnerException != null)
                {
                    message += $"Внутреннее исключение: {ex.InnerException.Message}\n";
                }
                MessageBox.Show(message);
            }
        }

    }
}
