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

                }
            }
        }

      

        private void LevelMedium_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LevelHard_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void LevelEasy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


