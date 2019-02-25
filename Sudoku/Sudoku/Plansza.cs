using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Klasa generująca planszę sudoku
    /// </summary>
    public class Plansza
    {
        public int[,] tab = new int[9, 9];
        public int[,] sud = new int[9, 9];
        static Random rnd = new Random();
        internal Queue<int> xy = new Queue<int>();
        internal Queue<int> blok = new Queue<int>();
        internal Queue<int> horizontal = new Queue<int>();
        internal Queue<int> vertical = new Queue<int>();

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public  Plansza()
        {
            Slant();
            Rest(0, 3);
        }
        /// <summary>
        /// Metoda uzupełniająca resztę sudoku 
        /// </summary>
        /// <param name="f">Liczba kolumn/wierszy</param>
        /// <param name="side">Wielkośc małego bloku-w pionie lub poziomie</param>
        /// <returns>true - można spisac liczbę</returns>
        private bool Rest(int f, int side)
        {

            if (side >= 9 && f < 9 - 1)
            {
                f++;
                side = 0;
            }
            if (f >= 9 && side >= 9)
                return true;

            if (f < 3)
            {
                if (side < 3)
                    side = 3;
            }
            else if (f < 9 - 3)
            {
                if (side == (int)(f / 3) * 3)
                    side += 3;
            }
            else
            {
                if (side == 9 - 3)
                {
                    f++;
                    side = 0;
                    if (f >= 9)
                        return true;
                }
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsKolision(f, side, num))
                {
                    tab[f, side] = num;
                    if (Rest(f, side + 1))
                        return true;

                    tab[f, side] = 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Metoda sprawdzająca czy występuje już liczba dana 
        /// </summary>
        /// <param name="f">liczba kolumn/wierszy</param>
        /// <param name="side">Wielkośc małego bloku-w pionie lub poziomie</param>
        /// <param name="num">Wylosowana liczba </param>
        /// <returns>true - jeżeli występuje kolizja
        /// false - jeżeli kolizja nie wytępuje </returns>
        private bool IsKolision(int f, int side, int num)
        {
            if ((UnUsedInRow(f, num) && UnUsedInCol(side, num) && UnUsedInBox(f - f % 3, side - side % 3, num)) == true)
                return true;
            return false;
        }
        /// <summary>
        /// Metoda sprawdza czy w bloku 3x3 znajduje sie już liczba którą chcesz wpisać
        /// </summary>
        /// <param name="v1">współrzędna "x" wskazująca początek bloku w którym liczba się znajduje  </param>
        /// <param name="v2">współrzędna "y" wskazująca początek bloku w którym liczba się znajduje </param>
        /// <param name="num"> Kandydat liczba do wpisania do tablicy sudoku</param>
        /// <returns>True- jeżeli liczba już jest użyta w bloku 3x3.
        /// </returns>
        private bool UnUsedInBox(int v1, int v2, int num)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tab[v1 + i, v2 + j] == num)
                        return false;
            return true;
        }
        /// <summary>
        /// Metoda sprawdzająca czy liczba występuje w kolumnie
        /// </summary>
        /// <param name="side">Wielkośc małego bloku-w pionie lub poziomie</param>
        /// <param name="num">Kandydat liczba do wpisanai do sudoku</param>
        /// <returns>True - jeżeli liczba w kolumnie nie została użyta</returns>
        private bool UnUsedInCol(int side, int num)
        {
            for (int i = 0; i < 9; i++)
                if (tab[i, side] == num)
                    return false;
            return true;
        }
        /// <summary>
        /// Metoda sprawdzająca czy liczba występuje w wierszu
        /// </summary>
        /// <param name="f">liczba kolumn/wierszy</param>
        /// <param name="num">Kandydat liczba do wpisanai do sudoku</param>
        /// <returns>True - jeżeli liczba w wierszu nie została użyta</returns>
        private bool UnUsedInRow(int f, int num)
        {
            for (int j = 0; j < 9; j++)
                if (tab[f, j] == num)
                    return false;
            return true;
        }
        /// <summary>
        /// Uzupełnianie cyfr w blokach 3x3 po skosie
        /// </summary>
        private void Slant()
        {
            for (int i = 0; i < 9; i += 3)
            {
                int num;
                for (int f = 0; f < 3; f++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        do
                        {
                            num = Random();
                        }
                        while (!InBlocksUsed(i, i, num));

                        tab[i + f, i + j] = num;
                    }
                }
            }
        }
        /// <summary>
        /// Metoda generująca losowe liczby 1-9
        /// </summary>
        /// <returns>(int)1-9</returns>
        internal int Random()
        {
            return rnd.Next(1, 10);
        }
        /// <summary>
        /// Metoda sprawdzająca czy liczba występuje w bloku 3x3
        /// </summary>
        /// <param name="r">Współrzędana "x" początku bloku</param>
        /// <param name="c">Współrzędana "y" początku bloku</param>
        /// <param name="num">Kandydat liczba do wpisanai do sudoku</param>
        /// <returns>True - jeżeli liczba nie została użyta w bloku</returns>
        private bool InBlocksUsed(int r, int c, int num)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tab[r + i, c + j] == num)
                        return false;

            return true;
        }
        /// <summary>
        /// Metoda dodająca do kolejki liczb które znajdują się poziomie i pionie od wylosowanej liczby
        /// </summary>
        /// <param name="x">Współrzędna "x" w tablicy wylosowanego pola</param>
        /// <param name="y">Współrzędna "y" w tablicy wylosowanego pola</param>
        internal void HorVerQ(int x, int y)
        {

            for (int i = 0; i < 9; i++)
            {
                xy.Enqueue(sud[i, y]);
                xy.Enqueue(sud[x, i]);
            }
        }
        /// <summary>
        /// Metoda dodająca do kolejki liczby które znajdują się w bloku 3x3 wylosowanej liczby
        /// </summary>
        /// <param name="x">Współrzędna "x" w tablicy wylosowanego pola</param>
        /// <param name="y">Współrzędna "y" w tablicy wylosowanego pola</param>
        internal void Block3x3Q(int x, int y)
        {

            int a = x - (x % 3);
            int b = y - (y % 3);
            for (int i = a; i <= a + 2; i++)
            {
                for (int j = b; j <= b + 2; j++)
                {
                    blok.Enqueue(sud[i, j]);
                }
            }
        }
        /// <summary>
        /// Metoda zwraca true jeżeli liczba nie powtórzyła sie w pionie, poziomie i w bloku.
        /// W przeciwnym wypadku false.
        /// </summary>
        /// <param name="x">Współrzędna "x" w tablicy wylosowanego pola</param>
        /// <param name="y">Współrzędna "y" w tablicy wylosowanego pola</param>
        /// <param name="l">Kandydat, liczba którą można wpisać w usunięte pole</param>
        /// <returns>true - jeżeli liczba nie występuje w pionie , poziomie i bloku 3x3</returns>
        private bool HorVerBlock3x3(int x, int y, int l)
        {

            for (int i = 0; i < xy.Count; i++)
            {
                if (xy.Peek() == l)
                    return false;
                xy.Enqueue(xy.Peek());
                xy.Dequeue();
            }
            for (int i = 0; i < blok.Count; i++)
            {
                if (blok.Peek() == l)
                    return false;
                blok.Enqueue(xy.Peek());
                blok.Dequeue();
            }
            return true;
        }
        /// <summary>
         /// Metoda dodająca do kolejki liczby które znajdują się w pionie i poziomie ,
         /// ale poza wierszem i kolumna w której była wylosoana liczba, oraz tak samo tylko w sąsiednich blokach   
         /// </summary>
         /// <param name="x">Współrzędna "x" w tablicy wylosowanego pola</param>
         /// <param name="y">Współrzędna "y" w tablicy wylosowanego pola</param>
         /// <param name="temp">Potencjalna liczba do usunięcia z sudoku</param>
        internal void BlocksHorVer(int x, int y, int temp)
        {
            int a = x - (x % 3);
            int b = y - (y % 3);
            for (int j = a; j <= a + 2; j++)
            {
                if (a != j)
                    for (int i = 0; i < 9; i++)
                    {
                        vertical.Enqueue(sud[x, i]);
                    }
            }
            for (int j = b; j <= b + 2; j++)
            {
                if (b != j)
                    for (int i = 0; i < 9; i++)
                    {
                        horizontal.Enqueue(sud[i, y]);
                    }
            }
        }
        /// <summary>
        /// Metoda zwracająca true jeżeli można usunąć wylosowaną liczbę, jeżeli wystąpiła ona wszecześniej 
        /// w sądiednich kolumnach i wierszach , w blokach ,odpowiednią ilośc razy 
        /// </summary>
        /// <param name="x">Współrzędna "x" w tablicy wylosowanego pola</param>
        /// <param name="y">Współrzędna "y" w tablicy wylosowanego pola</param>
        /// <param name="temp">Potencjalna liczba do usunięcia z sudoku</param>
        /// <returns>true - jeżeli liczbę można usunąć</returns>
        internal bool DeleteBlocksHorVer(int x, int y, int temp)
        {
            int verhowmany = 0;
            int horhowmany = 0;
            for (int i = 0; i < vertical.Count; i++)
            {
                if (temp == vertical.Peek())
                    verhowmany++;
                if (temp == horizontal.Peek())
                    horhowmany++;
                if (horhowmany > 1 || verhowmany > 1)
                    return true;

                vertical.Enqueue(xy.Peek());
                vertical.Dequeue();
                horizontal.Enqueue(xy.Peek());
                horizontal.Dequeue();
            }
            return false;
        }
        /// <summary>
        /// Metoda generująca tabele z usuniętymi liczbami.
        /// Usuwa z wypełnionego sudoku liczby.
        /// </summary>
        /// <param name="level">Ilość liczb do usunięcia</param>
        public void Delete(int level)
        {
            Array.Copy(tab, sud, 81);
            int temp;
            int x, y, a = 0;
            int deleted = 0;
            int[,] los = new int[2, 81];
            do
            { 
                x = Random() - 1;
                y = Random() - 1;
                if (sud[x, y] != 0)
                {
                    temp = sud[x, y];
                    sud[x, y] = 0;
                    HorVerQ(x, y);
                    Block3x3Q(x, y);
                    for (int l = 1; l < 10; l++)
                    {
                        //pion_poziom_blok3x3 zwraca true jeżeli liczbę l można wpisać w miejsce sud[x,y]
                        if (HorVerBlock3x3(x, y, l) == true)
                            a++; //jeżeli wiecej niż jedna to dwie liczby można wpisać w jedno miejsce
                        if (a > 1)
                        {
                            sud[x, y] = temp;
                            break;
                        }
                    }
                    if (sud[x, y] != 0)
                    {
                        BlocksHorVer(x, y, temp);
                        if (DeleteBlocksHorVer(x, y, temp) == true)
                            sud[x, y] = 0;
                    }
                    a = 0;
                    if (sud[x, y] == 0)
                        deleted++;
                    xy.Clear();
                    blok.Clear();
                    vertical.Clear();
                    horizontal.Clear();
                }

            } while (deleted < level);

        }

    }
}
