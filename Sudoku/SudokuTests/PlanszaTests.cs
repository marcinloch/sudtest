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
    }
    
}