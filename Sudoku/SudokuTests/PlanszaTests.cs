using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    [TestClass()]
    public class PlanszaTests
    {
        [TestMethod()]
        public void PlanszaTest_Czy_Wypelnione_Sa_Wartosci_w_uzupelnionej_tablicy_sudoku_rozne_od_zera()
        {
            bool actual = true;
            Plansza target = new Plansza();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (target.tab[i, j] == 0)
                        actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void PlanszaTest_Czy_Wypelnione_w_uzupelnionej_tablicy_sudoku_Sa_Wartosci_od_1_do_9()
        {
            bool actual = true;
            Plansza target = new Plansza();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (target.tab[i, j] > 9 || target.tab[i, j] < 1)
                        actual = false;
                }
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void DeleteTest_Czy_została_usunieta_odpowiednia_ilosc_pol_50_level_easy()
        {
            int ile = 0;
            int level = 50;
            Plansza target = new Plansza();
            target.Delete(level);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (target.sud[i, j] == 0)
                        ile++;
                }
            }
            Assert.AreEqual(level, ile);
        }
        [TestMethod]
        public void DeleteTest_Czy_została_usunieta_odpowiednia_ilosc_pol_55_level_medium()
        {
            int ile = 0;
            int level = 55;
            Plansza target = new Plansza();
            target.Delete(level);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (target.sud[i, j] == 0)
                        ile++;
                }
            }
            Assert.AreEqual(level, ile);
        }
        [TestMethod]
        public void DeleteTest_Czy_została_usunieta_odpowiednia_ilosc_pol_63_level_hard()
        { 
            int ile = 0;
            int level = 63;
            Plansza target = new Plansza();
            target.Delete(level);
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (target.sud[i, j] == 0)
                        ile++;
                }
            }
            Assert.AreEqual(level, ile);
        }
        [TestMethod]
        public void RandomTest_Czy_Metoda_Random_w_planszy_losuje_odpowiednie_liczby()
        {
            Plansza target = new Plansza();

            Assert.IsTrue(target.Random() >= 1 && target.Random() <= 9);
        }
        [TestMethod]
        public void HorVerQTest_Czy_zostaly_dodane_prawidlowe_liczby_do_kolejki_xy()
        {
            bool actual = true;
            int a = 1;
            Plansza target = new Plansza();
            target.HorVerQ(2, 5);

            for (int j = 0; j < 9; j++)
            {
                if (target.xy.Peek() != target.sud[j, 5])
                    actual = false;
                target.xy.Enqueue(target.xy.Peek());
                target.xy.Dequeue();
                if (target.xy.Peek() != target.sud[2, j])
                    actual = false;
                target.xy.Enqueue(target.xy.Peek());
                target.xy.Dequeue();

            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void Block3x3QTest_Czy_zostaly_dodane_prawidlowe_liczby_do_kolejki_blok()
        {
            bool actual = true;
            Plansza target = new Plansza();
            target.Block3x3Q(2, 5);

            int a = 2 - (2 % 3);
            int b = 5 - (5 % 3);
            for (int i = a; i <= a + 2; i++)
            {
                for (int j = b; j <= b + 2; j++)
                {
                    if (target.blok.Peek() != target.sud[i, j])
                        actual = false;
                    target.blok.Enqueue(target.blok.Peek());
                    target.blok.Dequeue();
                }
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void BlocksHorVerTest_Kolejka_horizontal_czy_prawidlowe_liczby()
        {
            bool actual = true;
            Plansza target = new Plansza();
            target.BlocksHorVer(7, 8, 4);

            int a = 7 - (7 % 3);
            for (int i = a; i <= a + 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (a != i)
                    {
                        if (target.horizontal.Peek() != target.sud[i, j])
                            actual = false;
                        target.horizontal.Enqueue(target.horizontal.Peek());
                        target.horizontal.Dequeue();
                    }
                }
            }
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void BlocksHorVerTest_Kolejka_vertical_czy_prawidlowe_liczby()
        {
            bool actual = true;
            Plansza target = new Plansza();
            target.BlocksHorVer(7, 8, 4);

            int a = 8 - (8 % 3);
            for (int i = a; i <= a + 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (a != i)
                    {
                        if (target.vertical.Peek() != target.sud[i, j])
                            actual = false;
                        target.vertical.Enqueue(target.vertical.Peek());
                        target.vertical.Dequeue();
                    }
                }
            }
            Assert.IsTrue(actual);
        }

    }



}