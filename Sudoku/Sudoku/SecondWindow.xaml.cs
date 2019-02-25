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

namespace Sudoku
{
    /// <summary>
    /// Logika interakcji dla klasy SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda zmieniająca teskt na język polski
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void P_click(object sender, RoutedEventArgs e)
        {

            two.FontSize = 22;
            two.FontWeight = FontWeights.Bold;
            two.Text = "Sudoku-łamigłówka, której celem jest wypełnienie diagramu 9 × 9 w taki sposób, aby w każdym wierszu, w każdej kolumnie i w każdym z dziewięciu pogrubionych kwadratów 3 × 3 znalazło się po jednej cyfrze od 1 do 9.";
        }
        /// <summary>
        /// Metoda zmieniajaca tekst na język angielski 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void E_click(object sender, RoutedEventArgs e)
        {
            
            two.FontSize = 22;
            two.FontWeight = FontWeights.Bold;
            two.Text = "Sudoku-the objective is to fill a 9×9 grid with digits so that each column, each row, and each of the nine 3×3  subgrids that compose the grid contain all of the digits from 1 to 9.";
        }
    }
}
