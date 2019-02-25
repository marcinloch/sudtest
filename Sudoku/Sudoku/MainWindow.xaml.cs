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
        static Random rnd = new Random();
        public List<TextBox> TextBox = new List<TextBox>();
        public int ColumnCount = 0;
        const int x = 70;
        const int y = 70;
        private int ss, mm, hh;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Plansza sudoku = new Plansza();
        char language = 'p';
        bool levelclick = false;

        public MainWindow()
        {
            InitializeComponent();

            CreateColRow();
        }
        /// <summary>
        /// Metoda tworząca kolumny i wiersze w gridzie
        /// </summary>
        void CreateColRow()
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
        }
        /// <summary>
        /// Metoda tworząca textboxy i dająca je w odpowiednie pola w gridzie
        /// </summary>
        void InitBoard()
        {
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox text = new TextBox();
                    text.Name = string.Format("Field{0}_{1}", i, j);
                    text.Text = sudoku.sud[i, j] != 0 ? sudoku.sud[i, j].ToString() : "";
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
        /// <summary>
        /// Metoda sprawdzająca czy sudoku zostało uzupełnione poprawnie. Jezeli sudoku jest uzupełnione poprawnie zatrzymuje stoper.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Koniec_click(object sender, RoutedEventArgs e)
        {
            if (levelclick == true)
            {
                if (CheckingGoodFit() == true)
                {
                    StopTimer();
                    Correct();
                    levelclick = false;
                }
                else
                {
                    Wrong();
                }
            }
        }
        /// <summary>
        /// Zmienia wygląd jeżeli odpowiedź jest zła
        /// </summary>
        private void Wrong()
        {
            line1.Stroke = Brushes.Red;
            line2.Stroke = Brushes.Red;
            line3.Stroke = Brushes.Red;
            line4.Stroke = Brushes.Red;
            if(language == 'p')
                TestWynik.Content = "Coś jest nie tak";
            if (language == 'e')
                TestWynik.Content = "Wrong...";
        }
        private void Default()
        {
            line1.Stroke = Brushes.Black;
            line2.Stroke = Brushes.Black;
            line3.Stroke = Brushes.Black;
            line4.Stroke = Brushes.Black;
            TestWynik.Content = "";
        }
        /// <summary>
        /// Zmienia wygląd Jeżeli odpowiedź jest dobra
        /// </summary>
        private void Correct()
        {
            line1.Stroke = Brushes.Green;
            line2.Stroke = Brushes.Green;
            line3.Stroke = Brushes.Green;
            line4.Stroke = Brushes.Green;
            if (language == 'p')
                TestWynik.Content = "Gratulacje !";
            if (language == 'e')
                TestWynik.Content = "Congratulations!";
            
        }
        /// <summary>
        /// Metoda sprawdzająca czy pola zostały poprawnie uzupełnione
        /// </summary>
        /// <returns>true - jeżeli pola zostały poprawnie uzupełnione</returns>
        private bool CheckingGoodFit()
        {
        
            int pomoc=0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    int index = TextBox.FindIndex(0, TextBox.Count,
                                text => text.Name == string.Format("Field{0}_{1}", i, j));
                    if (TextBox[index].Text != "")
                        pomoc = Convert.ToInt32(TextBox[index].Text);

                    if (sudoku.tab[i, j] != pomoc)
                    {
                        return false;
                    }
                    pomoc = 0;
                }
            }
            return true;
        }

        /// <summary>
        /// Metoda pozwalająca na wpisanie w tablicy samych cyfr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Important(((TextBox)sender).Text + e.Text);
        }
        /// <summary>
        /// zamienianie tekstu z textboxa na liczbę od 1-9
        /// </summary>
        /// <param name="str">zawartość textboxa</param>
        /// <returns>int 1-9</returns>
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

        /// <summary>
        /// Metoda implementująca standardową reprezentacje tekstową czasu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
       
        /// <summary>
        /// Metoda zatrzymująca stoper
        /// </summary>
        private void StopTimer()
        {
            dispatcherTimer.IsEnabled = false;
            ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));
        }
        
        /// <summary>
        /// Losowanie ilości liczb do usunięcia 
        /// </summary>
        /// <param name="a">Minimalny zakres do udunięcia</param>
        /// <param name="b">Maksymalny zakres do usunięcia</param>
        /// <returns></returns>
        private int Random(int a, int b)
        {
            return rnd.Next(a, b + 1);
        }

        /// <summary>
        /// Genegowanie planszy o średnim poziomie trudności
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelMedium_Click(object sender, RoutedEventArgs e)
        {
            Default();
            levelclick = true;
            sudoku = new Plansza();
            sudoku.Delete(Random(53, 60));
            InitBoard();
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));

        }
        /// <summary>
        /// Generowanie planszy o trudnym poziomie trudności
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelHard_Click(object sender, RoutedEventArgs e)
        {
            Default();
            levelclick = true;
            sudoku = new Plansza();
            sudoku.Delete(Random(61, 64));
            InitBoard();
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));

        }

        /// <summary>
        /// Generowanie l=planszy o łatwym poziomie trudnosci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelEasy_Click(object sender, RoutedEventArgs e)
        {
            Default();
            levelclick = true;
            sudoku = new Plansza();
            sudoku.Delete(Random(46, 52));
            InitBoard();
            dispatcherTimer.Start();
            if (LevelMedium.IsEnabled == true)
                ss = 0;
            mm = 0;
            hh = 0;
            hh = 0;
            Time.Content = string.Format("{0}:{1}:{2}", hh.ToString().PadLeft(2, '0'), mm.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));

        }

        /// <summary>
        /// Metoda służąca do zmiany na język Angielski
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Eng_click(object sender, RoutedEventArgs e)
        {
            English.IsEnabled = true;
            language = 'e';
            if (TestWynik.Content != "")
                if (language == 'e')
                    TestWynik.Content = "Wrong...";
            LevelEasy.Content = "Easy";
            LevelMedium.Content = "Medium";
            LevelHard.Content = "Hard";
            Koniec.Content = "Check";
        }
        /// <summary>
        /// Metoda służąca do zmiany na język Polski
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Pl_click(object sender, RoutedEventArgs e)
        {
            Polish.IsEnabled = true;
            language = 'p';
            if (TestWynik.Content != "")
                if (language == 'p')
                    TestWynik.Content = "Coś jest nie tak";
            LevelEasy.Content = "Łatwy";
            LevelMedium.Content = "Średni";
            LevelHard.Content = "Trudny";
            Koniec.Content = "Sprawdź";
        }
        /// <summary>
        /// Metoda otwierająca okno "SecondWindow"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Info_click(object sender, RoutedEventArgs e)
        {
           SecondWindow secondw = new SecondWindow();
            secondw.Show();

        }
    }
}


