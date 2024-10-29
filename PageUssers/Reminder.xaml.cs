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
using System.Media;
using System.IO;
using System.Threading;

namespace УП.PageUssers
{
    struct Timer
    {
        public StackPanel stackPanel;
        public TimeSpan timeSpan;
        public Label nameLabel;
        public TextBox nameTextBox;
        public Label timeLabel;
        public TextBox timeTextBox;
        public Button startButton;
        public Button deleteButton;
        public CancellationTokenSource cts;
    }
    public partial class Reminder : Page
    {
        private Button buttonADD;
        private int margintop = 0;
        private int numberTimer = 0;
        List<string> newData = new List<string>();
        List<Timer> timers = new List<Timer>();
        public Reminder()
        {
            InitializeComponent();
            buttonADD = (Button)grid1.Children[0];
            if (File.Exists("Data.txt") == true)
            {
                string[] Data = File.ReadAllLines("Data.txt");
                newData = Data.ToList();
                for (int i = 0; i < Data.Length; i++)
                {
                    CreateTimer();
                    newData.RemoveAt(0);
                }
            }
        }

        private void AddTimer(object sender, RoutedEventArgs e)
        {
            CreateTimer();
        }
        private void CreateTimer()
        {
            Thickness margin = buttonADD.Margin;
            margin.Top = margintop + 60;
            buttonADD.Margin = margin;
            StackPanel stackPanel = new StackPanel();
            stackPanel.Name = "N" + numberTimer;
            stackPanel.Margin = new Thickness(0, 0 + margintop, 0, 0);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            grid1.Children.Add(stackPanel);
            Timer timer = new Timer();
            timer.stackPanel = stackPanel;
            if (newData.Count > 0)
            {
                int seconds = Int32.Parse(newData[0].Split(';')[1]);
                timer.timeSpan = TimeSpan.FromSeconds(seconds);
            }
            else
            {
                timer.timeSpan = TimeSpan.Zero;
            }
            CreateLabelName(ref timer);
            CreateTexBoxName(ref timer);
            CreateLabelTime(ref timer);
            CreateTexBoxTime(ref timer);
            CreateButtonStart(ref timer);
            CreateButtonDelete(ref timer);
            numberTimer++;
            margintop += 60;
            timers.Add(timer);
            SaveFile();
        }
        private void CreateLabelName(ref Timer timer)
        {
            Label label = new Label();
            if (newData.Count > 0)
            {
                string name = newData[0].Split(';')[0];
                label.Content = name;
            }
            else
            {
                label.Content = "Таймер";
            }
            label.FontSize = 36;
            label.Width = 264;
            label.MouseLeftButtonUp += new MouseButtonEventHandler(NameClick);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            timer.stackPanel.Children.Add(label);
            timer.nameLabel = label;
        }
        private void CreateTexBoxName(ref Timer timer)
        {
            TextBox textBox = new TextBox();
            textBox.FontSize = 36;
            textBox.Width = 264;
            textBox.KeyUp += new KeyEventHandler(TextBoxNameKeyUp);
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.Margin = new Thickness(2, -54, 0, 0);
            textBox.Visibility = Visibility.Hidden;
            timer.stackPanel.Children.Add(textBox);
            timer.nameTextBox = textBox;
        }
        private void CreateLabelTime(ref Timer timer)
        {
            Label label = new Label();
            label.FontSize = 36;
            label.MouseLeftButtonUp += new MouseButtonEventHandler(TimeClick);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.Margin = new Thickness(262, -54, 0, 0);
            label.Content = timer.timeSpan.Hours + ":" + timer.timeSpan.Minutes + ":" + timer.timeSpan.Seconds;
            timer.stackPanel.Children.Add(label);
            timer.timeLabel = label;
        }
        private void CreateTexBoxTime(ref Timer timer)
        {
            TextBox textBox = new TextBox();
            textBox.FontSize = 36;
            textBox.Width = 140;
            textBox.KeyUp += new KeyEventHandler(TextBoxTimeKeyUp);
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.Margin = new Thickness(264, -54, 0, 0);
            textBox.Visibility = Visibility.Hidden;
            timer.stackPanel.Children.Add(textBox);
            timer.timeTextBox = textBox;
        }
        private void CreateButtonStart(ref Timer timer)
        {
            Button button = new Button();
            button.FontSize = 36;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Margin = new Thickness(412, -54, 0, 0);
            button.Content = "Start";
            button.Click += new RoutedEventHandler(StartStopTimer);
            button.Background = Brushes.Green;
            timer.stackPanel.Children.Add(button);
            timer.startButton = button;
        }
        private void CreateButtonDelete(ref Timer timer)
        {
            Button button = new Button();
            button.FontSize = 36;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Margin = new Thickness(520, -54, 0, 0);
            button.Content = " X ";
            button.Click += new RoutedEventHandler(DeleteTimer);
            timer.stackPanel.Children.Add(button);
            timer.deleteButton = button;
        }
        private void NameClick(object sender, MouseButtonEventArgs e)
        {
            int index = ((StackPanel)((Label)sender).Parent).Name[1] - '0';
            Label nameLabel = timers[index].nameLabel;
            TextBox nameTextBox = timers[index].nameTextBox;
            nameLabel.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Visible;
            nameTextBox.Text = nameLabel.Content.ToString();
            nameTextBox.Focus();
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            nameTextBox.SelectionStart = nameTextBox.Text.Length;
        }
        private void TextBoxNameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int index = ((StackPanel)((TextBox)sender).Parent).Name[1] - '0';
                TextBox textBoxName = timers[index].nameTextBox;
                Label labelName = timers[index].nameLabel;
                labelName.Content = textBoxName.Text;
                labelName.Visibility = Visibility.Visible;
                textBoxName.Visibility = Visibility.Hidden;
                SaveFile();
            }
        }
        private void TimeClick(object sender, MouseButtonEventArgs e)
        {
            int index = ((StackPanel)((Label)sender).Parent).Name[1] - '0';
            Label timeLabel = timers[index].timeLabel;
            TextBox timeTextBox = timers[index].timeTextBox;
            timeLabel.Visibility = Visibility.Hidden;
            timeTextBox.Visibility = Visibility.Visible;
            timeTextBox.Text = "";
            timeTextBox.Focus();
            timeTextBox.SelectionStart = timeTextBox.Text.Length;
        }
        private void TextBoxTimeKeyUp(object sender, KeyEventArgs e)
        {
            int index = ((StackPanel)((TextBox)sender).Parent).Name[1] - '0';
            TextBox textBoxTime = timers[index].timeTextBox;
            Label labelTime = timers[index].timeLabel;
            string tempText = "";
            for (int i = 0; i < 6; i++)
            {
                if (i < textBoxTime.Text.Length)
                {
                    if (Char.IsNumber(textBoxTime.Text[i]) == true)
                    {
                        tempText += textBoxTime.Text[i];
                    }
                }
            }
            if (e.Key == Key.Enter)
            {
                string zero = "";
                for (int i = 0; i < 6; i++)
                {
                    if (i >= tempText.Length)
                    {
                        zero += "0";
                    }
                }
                tempText = zero + tempText;
                int number1 = Int32.Parse(tempText[0].ToString() + tempText[1].ToString());
                if (number1 > 23)
                {
                    number1 = 23;
                }
                int number2 = Int32.Parse(tempText[2].ToString() + tempText[3].ToString());
                if (number2 > 59)
                {
                    number2 = 59;
                }
                int number3 = Int32.Parse(tempText[4].ToString() + tempText[5].ToString());
                if (number3 > 59)
                {
                    number3 = 59;
                }
                TimeSpan timeSpan = new TimeSpan(number1, number2, number3);
                labelTime.Content = timeSpan.Hours + ":" + timeSpan.Minutes + ":" + timeSpan.Seconds;
                ChangeTime(index, timeSpan);
                labelTime.Visibility = Visibility.Visible;
                textBoxTime.Visibility = Visibility.Hidden;
                SaveFile();
                return;
            }
            textBoxTime.Text = tempText;
            textBoxTime.SelectionStart = textBoxTime.Text.Length;
        }
        private void StartStopTimer(object sender, RoutedEventArgs e)
        {
            int index = ((StackPanel)((Button)sender).Parent).Name[1] - '0';
            Button button = timers[index].startButton;
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            string name = button.Content.ToString();
            if (name == "Start")
            {
                button.Content = "Reset";
                button.Background = Brushes.Red;
                CancellationTokenSource cts;
                cts = new CancellationTokenSource();
                ChangeToken(index, cts);
                WorkTimer(index, cts.Token);
            }
            else
            {
                timers[index].cts.Cancel();
                button.Content = "Start";
                button.Background = Brushes.Green;
                timers[index].timeLabel.Content = timers[index].timeSpan.Hours + ":" + timers[index].timeSpan.Minutes + ":" + timers[index].timeSpan.Seconds;
            }
        }
        private void DeleteTimer(object sender, RoutedEventArgs e)
        {
            int index = ((StackPanel)((Button)sender).Parent).Name[1] - '0';
            if (timers[index].cts != null)
            {
                timers[index].cts.Cancel();
            }
            grid1.Children[index + 1].Visibility = Visibility.Collapsed;
            margintop = 0;
            for (int i = 1; i < grid1.Children.Count; i++)
            {
                StackPanel stackPanel = (StackPanel)grid1.Children[i];
                if (stackPanel.Visibility == Visibility.Visible)
                {
                    stackPanel.Margin = new Thickness(0, 0 + margintop, 0, 0);
                    margintop += 60;
                }
            }
            Thickness margin = buttonADD.Margin;
            margin.Top = margintop;
            buttonADD.Margin = margin;
            SaveFile();

        }
        private async void WorkTimer(int index, CancellationToken token)
        {
            TimeSpan newTS = timers[index].timeSpan;
            while (newTS.TotalSeconds > 0)
            {
                await Task.Delay(1000);
                try
                {
                    token.ThrowIfCancellationRequested();
                    newTS -= TimeSpan.FromSeconds(1);
                    timers[index].timeLabel.Content = newTS.Hours + ":" + newTS.Minutes + ":" + newTS.Seconds;
                }
                catch
                {
                    return;
                }
            }
            PlaySound();
            ChangeButtonColor(index, token);
        }
        private void PlaySound()
        {
            try
            {
                // Укажите путь к вашему звуковому файлу .wav
                SoundPlayer player = new SoundPlayer("D:\\DOWN\\прилож капча1\\frantsuzskaya-sirena.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения звука: " + ex.Message);
            }
        }
        private async void ChangeButtonColor(int index, CancellationToken token)
        {
            while (true)
            {
                await Task.Delay(500);
                try
                {
                    token.ThrowIfCancellationRequested();
                    timers[index].startButton.Background = Brushes.Yellow;
                    await Task.Delay(500);
                }
                catch
                {
                    return;
                }

                await Task.Delay(500);
                try
                {
                    token.ThrowIfCancellationRequested();
                    timers[index].startButton.Background = Brushes.Red;
                }
                catch
                {
                    return;
                }
            }
        }
        void ChangeToken(int index, CancellationTokenSource cts)
        {
            Timer timer = timers[index];
            timers.RemoveAt(index);
            timer.cts = cts;
            timers.Insert(index, timer);
        }
        void ChangeTime(int index, TimeSpan timeSpan)
        {
            Timer timer = timers[index];
            timers.RemoveAt(index);
            timer.timeSpan = timeSpan;
            timers.Insert(index, timer);
        }
        void SaveFile()
        {
            string Data = "";
            for (int i = 1; i < grid1.Children.Count; i++)
            {
                StackPanel stackPanel = (StackPanel)grid1.Children[i];
                if (stackPanel.Visibility == Visibility.Visible)
                {
                    int index = stackPanel.Name[1] - '0';
                    Data += timers[index].nameLabel.Content + ";";
                    Data += timers[index].timeSpan.TotalSeconds + "\n";
                }
            }
            File.WriteAllText("Data.txt", Data);
        }
    }
}