using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Scrabble
{
    class Tile
    {
        #region static variabls
        static Random rnd = new Random();
        static Point mouseDownLocation;
        static PictureBox mainPicsBox;
        public static Dictionary<string, int> pointsDictionary = new Dictionary<string, int>();
        static List<Tile> listOfAllTiles = new List<Tile>();
        static bool debug = true;
        #endregion

        #region nonstatic variable
        public Button tileButton = new Button();
        Point startLocation;
        int startIndex;
        public int positionX = 1000;
        public int positionY = 1000;
        #endregion

        #region static methods
        public static void Init(PictureBox pictureBox)
        {

            mainPicsBox = pictureBox;
            for (int i = 0; i < 7; i++)
            {
                if (Player.activePlayer.listOfTiles.Find(j => j.startIndex == i) == null && Form3.allLetters.Length > 0)
                {
                    Tile currentTile = new Tile();
                    listOfAllTiles.Add(currentTile);
                    currentTile.startIndex = i;
                    currentTile.tileButton.Location = new System.Drawing.Point(i * 35 + 180, 600); //ce spreminjas tle se mouseUp
                    currentTile.tileButton.Size = new System.Drawing.Size(29, 29);
                    currentTile.startLocation = currentTile.tileButton.Location;
                    currentTile.tileButton.Name = "tile_" + i.ToString();
                    currentTile.tileButton.BackColor = Color.Orange;

                    int randIndex = rnd.Next(0, Form3.allLetters.Length);
                    currentTile.tileButton.Text = Form3.allLetters.Substring(randIndex, 1);

                    if (currentTile.tileButton.Text == "?")
                    {
                        currentTile.tileButton.BackColor = Color.PapayaWhip;
                    }

                    Form3.allLetters = Form3.allLetters.Remove(randIndex, 1);

                    currentTile.tileButton.MouseMove += TileButton_MouseMove;
                    currentTile.tileButton.MouseDown += TileButton_MouseDown;
                    currentTile.tileButton.MouseUp += (senderr, ee) => TileButton_MouseUp(senderr, ee, currentTile);

                    customButton(currentTile.tileButton, currentTile.tileButton.Text, pointsDictionary[currentTile.tileButton.Text].ToString());
                    
                    Player.activePlayer.listOfTiles.Add(currentTile);
                    pictureBox.Controls.Add(currentTile.tileButton);

                }

            }
            if (debug==false)
            {
                if (Player.activePlayer.isPC)
                {

                    Player.activePlayer.listOfTiles[0].tileButton.Text = "E";
                    Player.activePlayer.listOfTiles[1].tileButton.Text = "L";
                    Player.activePlayer.listOfTiles[2].tileButton.Text = "R";
                    Player.activePlayer.listOfTiles[3].tileButton.Text = "I";
                    Player.activePlayer.listOfTiles[4].tileButton.Text = "A";
                    Player.activePlayer.listOfTiles[5].tileButton.Text = "N";
                    Player.activePlayer.listOfTiles[6].tileButton.Text = "B";

                }
                else
                {

                    Player.activePlayer.listOfTiles[0].tileButton.Text = "S";
                    Player.activePlayer.listOfTiles[1].tileButton.Text = "R";
                    Player.activePlayer.listOfTiles[2].tileButton.Text = "E";
                    Player.activePlayer.listOfTiles[3].tileButton.Text = "E";
                    Player.activePlayer.listOfTiles[4].tileButton.Text = "E";
                    Player.activePlayer.listOfTiles[5].tileButton.Text = "U";
                    Player.activePlayer.listOfTiles[6].tileButton.Text = "M";
                }
                pictureBox.Refresh();
            }
        }

        public static void customButton(Button button, string line1, string line2)
        {
            Bitmap bmp = new Bitmap(button.ClientRectangle.Width, button.ClientRectangle.Height + 5);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(button.BackColor);

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 12))
                {
                    Rectangle RC = button.ClientRectangle;
                    RC.Inflate(-5, -5);
                    //G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font courier = new Font("MS Courier", 6))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString("     " + line2, courier, Brushes.DarkRed, button.ClientRectangle, SF);
                }
            }
            button.Image = bmp;
            button.ImageAlign = ContentAlignment.MiddleCenter;
        }



        public static void setVisible(bool isVisible)
        {


            for (int i = 0; i < Player.activePlayer.listOfTiles.Count; i++)
            {

                Player.activePlayer.listOfTiles.ElementAt(i).tileButton.Visible = isVisible;

            }



        }

        private static void TileButton_MouseUp(object sender, MouseEventArgs e, Tile currentTile)
        {
            Button currentButton = (Button)sender;
            string name = currentButton.Name.Split('_')[1];
            int currentStartIndex = Convert.ToInt32(name);
            Point location = new Point(Convert.ToInt32(name) * 35 + 180, 600); //ce spreminjas pozicijo gumbov spremeni se leti :)
            bool isIntersect = false;
            foreach (Field currentField in Field.FieldList)
            {
                if (currentField.fieldPanel.Bounds.IntersectsWith(currentButton.Bounds))
                {
                    if (currentField.letter != "")
                        break;


                    currentTile.positionX = currentField.iks;
                    currentTile.positionY = currentField.ipsilon;

                    isIntersect = true;
                    currentButton.Location = new Point(currentField.fieldPanel.Location.X, currentField.fieldPanel.Location.Y);
                    Player.activePlayer.listOfTilesOnBoard.Add(currentTile);
                    Player.activePlayer.listOfTiles.RemoveAll(i => i.startIndex == currentStartIndex);
                    currentField.letter = currentButton.Text;
                }
            }
            if (!isIntersect)
            {
                currentButton.Location = location;

                if (Player.activePlayer.listOfTiles.Find(i => i.startIndex == currentStartIndex) == null)
                {
                    Player.activePlayer.listOfTiles.Add(currentTile);
                    Player.activePlayer.listOfTilesOnBoard.RemoveAll(i => i.startIndex == currentStartIndex);
                    currentTile.positionX = 1000;
                    currentTile.positionY = 1000;

                }
            }

        }

        private static void TileButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button currentButton = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                if (currentButton.Text == "?")
                {
                    currentButton.MouseMove -= TileButton_MouseMove;
                    ComboBox currentComboBox = new ComboBox();
                    currentComboBox.Items.Add("A");
                    currentComboBox.Items.Add("B");
                    currentComboBox.Items.Add("C");
                    currentComboBox.Items.Add("Č");
                    currentComboBox.Items.Add("D");
                    currentComboBox.Items.Add("E");
                    currentComboBox.Items.Add("F");
                    currentComboBox.Items.Add("G");
                    currentComboBox.Items.Add("H");
                    currentComboBox.Items.Add("I");
                    currentComboBox.Items.Add("J");
                    currentComboBox.Items.Add("K");
                    currentComboBox.Items.Add("L");
                    currentComboBox.Items.Add("M");
                    currentComboBox.Items.Add("N");
                    currentComboBox.Items.Add("O");
                    currentComboBox.Items.Add("P");
                    currentComboBox.Items.Add("R");
                    currentComboBox.Items.Add("S");
                    currentComboBox.Items.Add("Š");
                    currentComboBox.Items.Add("T");
                    currentComboBox.Items.Add("U");
                    currentComboBox.Items.Add("V");
                    currentComboBox.Items.Add("Z");
                    currentComboBox.Items.Add("Ž");
                    currentComboBox.Items.Add("Q");
                    currentComboBox.Items.Add("W");
                    currentComboBox.Items.Add("X");
                    currentComboBox.Items.Add("Y");
                    currentComboBox.Text = "A";
                    currentComboBox.BringToFront();
                    currentComboBox.Location = new Point(currentButton.Location.X - 1, currentButton.Location.Y - 30);
                    currentComboBox.Size = new Size(32, 29);
                    mainPicsBox.Controls.Add(currentComboBox);
                    currentComboBox.SelectedValueChanged += (senderr, ee) => CurrentComboBox_SelectedValueChanged(senderr, ee, currentButton);

                }
                foreach (Field currentField in Field.FieldList)
                {
                    if (currentField.fieldPanel.Bounds.IntersectsWith(currentButton.Bounds))
                    {
                        currentField.letter = "";
                    }
                }
            }
        }

        private static void CurrentComboBox_SelectedValueChanged(object sender, EventArgs e, Button currentButton)
        {
            ComboBox currentCB = (ComboBox)sender;
            currentButton.Text = currentCB.SelectedItem.ToString();
            currentButton.BackColor = Color.Orange;
           // currentButton.BringToFront();
            //currentButton.Show();
            currentButton.MouseMove += TileButton_MouseMove;
            currentCB.Hide();

        }



        private static void TileButton_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Button currentButton = (Button)sender;
                currentButton.BringToFront();
                currentButton.Left = (e.X + currentButton.Left - mouseDownLocation.X);
                currentButton.Top = (e.Y + currentButton.Top - mouseDownLocation.Y);


            }

        }

        bool isOld = false;
        public static void NewGame()
        {
            foreach(Tile curtile in listOfAllTiles)
            {
                curtile.isOld = true;
                curtile.tileButton.Dispose();
            }
            listOfAllTiles.Clear();
        }

        #endregion

        #region nonstatic methods
        #endregion

    }
}