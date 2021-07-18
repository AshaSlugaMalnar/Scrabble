using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;
using System.Windows.Forms;
using System.IO;

namespace Scrabble
{
    public partial class Form3 : Form
    {
        public static string language = "ENG";
        public static string[] wordsInDictionary = System.IO.File.ReadAllLines(@"Dictionaries\WordListENG.txt");
        public static Hunspell hunspell = new Hunspell("Dictionaries/en_US.aff", "Dictionaries/en_US.dic");
        public static string allLetters = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";
        public Form3()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 sistema = new Form1();
            sistema.ShowDialog();
            
            this.Close();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Drag and drop letters to create a word. When your word is completed click Done");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I'm Asha Sluga Malnar \n 4.RB \n This is my final project Scrabble");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            

            if (button4.Text == "ENGLISH")
            {
                button4.Text = "SLOVENŠČINA";
                language = "SLO";
                wordsInDictionary = System.IO.File.ReadAllLines(@"Dictionaries\WordListSLO.txt");
                hunspell = new Hunspell("Dictionaries/sl_SI.aff", "Dictionaries/sl_SI.dic");
                allLetters = "AAAAAAAAAABBCČDDDDEEEEEEEEEEEFGGHIIIIIIIIIJJJJKKKLLLLMMNNNNNNNOOOOOOOOPPRRRRRRSSSSSSŠTTTTUUVVVVUUŽ";
               
                #region Points in Dictionary SLO
                Tile.pointsDictionary.Clear();

                Tile.pointsDictionary.Add("A", 1);
                Tile.pointsDictionary.Add("B", 4);
                Tile.pointsDictionary.Add("C", 8);
                Tile.pointsDictionary.Add("Č", 5);
                Tile.pointsDictionary.Add("D", 2);
                Tile.pointsDictionary.Add("E", 1);
                Tile.pointsDictionary.Add("F", 10);
                Tile.pointsDictionary.Add("G", 4);
                Tile.pointsDictionary.Add("H", 5);
                Tile.pointsDictionary.Add("I", 1);
                Tile.pointsDictionary.Add("J", 1);
                Tile.pointsDictionary.Add("K", 3);
                Tile.pointsDictionary.Add("L", 1);
                Tile.pointsDictionary.Add("M", 3);
                Tile.pointsDictionary.Add("N", 1);
                Tile.pointsDictionary.Add("O", 1);
                Tile.pointsDictionary.Add("P", 3);
                Tile.pointsDictionary.Add("R", 1);
                Tile.pointsDictionary.Add("S", 1);
                Tile.pointsDictionary.Add("Š", 6);
                Tile.pointsDictionary.Add("T", 1);
                Tile.pointsDictionary.Add("U", 3);
                Tile.pointsDictionary.Add("V", 2);
                Tile.pointsDictionary.Add("Z", 4);
                Tile.pointsDictionary.Add("Ž", 10);
                Tile.pointsDictionary.Add("?", 0);
                #endregion

            }
            else if (button4.Text == "SLOVENŠČINA")
            {
                button4.Text = "ENGLISH";
                language = "ENG";
                wordsInDictionary = System.IO.File.ReadAllLines(@"Dictionaries\WordListENGLow.txt");
                hunspell = new Hunspell("Dictionaries/en_US.aff", "Dictionaries/en_US.dic");
                allLetters = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";

                #region Points in Dictionary ENG
                Tile.pointsDictionary.Clear();

                Tile.pointsDictionary.Add("A", 1);
                Tile.pointsDictionary.Add("B", 3);
                Tile.pointsDictionary.Add("C", 3);
                Tile.pointsDictionary.Add("D", 2);
                Tile.pointsDictionary.Add("E", 1);
                Tile.pointsDictionary.Add("F", 4);
                Tile.pointsDictionary.Add("G", 2);
                Tile.pointsDictionary.Add("H", 4);
                Tile.pointsDictionary.Add("I", 1);
                Tile.pointsDictionary.Add("J", 8);
                Tile.pointsDictionary.Add("K", 5);
                Tile.pointsDictionary.Add("L", 1);
                Tile.pointsDictionary.Add("M", 3);
                Tile.pointsDictionary.Add("N", 1);
                Tile.pointsDictionary.Add("O", 1);
                Tile.pointsDictionary.Add("Q", 10);
                Tile.pointsDictionary.Add("P", 2);
                Tile.pointsDictionary.Add("R", 1);
                Tile.pointsDictionary.Add("S", 1);
                Tile.pointsDictionary.Add("T", 1);
                Tile.pointsDictionary.Add("U", 1);
                Tile.pointsDictionary.Add("V", 4);
                Tile.pointsDictionary.Add("W", 4);
                Tile.pointsDictionary.Add("Z", 10);
                Tile.pointsDictionary.Add("Y", 4);
                Tile.pointsDictionary.Add("X", 8);
                Tile.pointsDictionary.Add("?", 0);
                #endregion
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string allText = File.ReadAllText("Dictionaries/WordListENG.txt");
            allText = allText.ToLower();
            File.WriteAllText("Dictionaries/WordListENGLow.txt", allText);
        }
    }
}
