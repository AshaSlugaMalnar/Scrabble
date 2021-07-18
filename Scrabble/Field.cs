using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Scrabble
{
    class Field
    {
        #region static variable
        public static int offsetI = 40;
        public static int offsetJ = 29;
        public static List<Field> FieldList = new List<Field>();
        static int[] wordscore3 = { 0, 7, 14, 105, 119, 210, 217, 224 };
        static int[] wordscore2 = { 16, 28, 32, 48, 64, 160, 42, 56, 70, 154, 168, 182, 176, 196, 192, 208 };
        static int[] letterscore3 = { 20, 24, 80, 76, 84, 88, 136, 140, 144, 148, 200, 204 };
        static int[] letterscore2 = { 3, 11, 36, 38, 45, 52, 59, 92, 96, 98, 102, 108, 116, 122, 126, 128, 132, 165, 172, 179, 186, 188, 213, 221 };
        public static Field[,] fieldArray = new Field[15, 15];
        #endregion

        #region nonstatic variable
        public Panel fieldPanel = new Panel();
        public int iks = 1000;
        public int ipsilon = 1000;
        int indeks;
        public int letterAward = 1;
        public int wordAward = 1;
        public string letter = "";
        #endregion

        #region static methods
        public static void Init(PictureBox pictureBox)
        {
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    Field currentField = new Field();
                    currentField.iks = i;
                    currentField.ipsilon = j;
                    fieldArray[i, j] = currentField;
                    currentField.fieldPanel.Location = new System.Drawing.Point(offsetI + i * 33, offsetJ + j * 33);
                    currentField.fieldPanel.Size = new System.Drawing.Size(29, 29);
                    currentField.fieldPanel.AllowDrop = true;
                    currentField.indeks = j * 15 + i;
                    currentField.fieldPanel.Name = "panel_" + i.ToString() + "_" + j.ToString() + "_" + currentField.indeks.ToString();
                    currentField.fieldPanel.BackColor = Color.Transparent;
                    currentField.fieldPanel.MouseUp += FieldPanel_MouseUp;
                    if (wordscore3.Contains(currentField.indeks))
                    {
                        currentField.wordAward = 3;
                    }
                    else if (wordscore2.Contains(currentField.indeks))
                    {
                        currentField.wordAward = 2;
                    }
                    else if (letterscore2.Contains(currentField.indeks))
                    {
                        currentField.letterAward = 2;
                    }
                    else if (letterscore3.Contains(currentField.indeks))
                    {
                        currentField.letterAward = 3;
                    }
                    FieldList.Add(currentField);
                    pictureBox.Controls.Add(currentField.fieldPanel);


                }
            }
        }


        private static void FieldPanel_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void FieldPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Panel currentPanel = (Panel)sender;
            string name = currentPanel.Name.Split('_')[3];
            int indeks = Convert.ToInt32(name);
            Field currentField = FieldList.ElementAt(indeks);


        }

        public static void NewGame()
        {
            FieldList.Clear();

        }

        #endregion

        #region nonstatic methods
        #endregion

    }
}
