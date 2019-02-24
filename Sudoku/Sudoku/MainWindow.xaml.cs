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
using System.Timers;
using System.Threading;
using System.Windows.Threading;


namespace Sudoku
{


    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TextBox> TextBox = new List<TextBox>();
        public int ColumnCount = 0;
        const int x = 70;
        const int y = 70;
        private int ss, mm, hh;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        SecondWindow twowindow = new SecondWindow();


        public MainWindow()
        {
            InitializeComponent();
            InitBoard();
            

        }

        void InitBoard()
        {

            for (int i = 0; i < 9; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                Grid.ColumnDefinitions.Add(column);
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                Grid.RowDefinitions.Add(row);

            }
            for (int i = 0; i <9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox text = new TextBox();
                    Grid.SetRow(text, i);
                    Grid.SetColumn(text, j);
                    text.Background = Brushes.LightGray;
                    TextBox.Add(text);
                    text.MaxLength = 1;
                    text.FontSize = 30;
                    text.TextAlignment = TextAlignment.Center;
                    Grid.Children.Add(text);
                    text.PreviewTextInput += text_PreviewTextInput;

                    

                    

                }

            }
           

         
        }

        private void text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Important(((TextBox)sender).Text + e.Text);
        }

        public static bool Important(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 9;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);

        }
        public void Timer_Tick(object sender, EventArgs e)
        {

            ss += 1;
            if (ss == 60)
            {
                ss = 0;
                mm += 1;
            }
            if (mm == 60)
            {
                mm = 0;
                hh += 1;
            }

            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));



        }
       


        private void LevelMedium_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));

        }

        private void LevelHard_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));
        }

        private void LevelEasy_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));
        }


        public void Eng_click(object sender, RoutedEventArgs e)
        {
            English.IsEnabled = true;

            LevelEasy.Content = "Easy";
            LevelMedium.Content = "Medium";
            LevelHard.Content = "Hard";
            twowindow.two.Text = "asasas";



        }
        public void Pl_click(object sender, RoutedEventArgs e)
        {
            Polish.IsEnabled = true;

            LevelEasy.Content = "Łatwy";
            LevelMedium.Content = "Średni";
            LevelHard.Content = "Trudny";


        }

        private void Info_click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondw = new SecondWindow();
            secondw.Show();

        }
    }
}


