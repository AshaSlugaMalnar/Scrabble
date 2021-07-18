using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NHunspell;
using System.Diagnostics;

namespace Scrabble
{
    class Player
    {
        #region static variables
        public static Player activePlayer = new Player();
        public static List<Player> listOfPlayers = new List<Player>();
        public static int buttonCounter = 0;
        static PictureBox mainPicsBox;
        static List<Color> colorList = new List<Color>();
        static List<Color> colorList2 = new List<Color>();
        static int currentPlayerIndex = 0;
        static int playerCount = 1;
        static ComboBox playersComboBox = new ComboBox();
        #endregion

        #region nonstatic variables
        public List<Tile> listOfTiles = new List<Tile>();
        public List<Tile> listOfTilesOnBoard = new List<Tile>();
        public bool isPC = false;
        Label playerImeLabel = new Label();
        TextBox pointsDisplyTextBox = new TextBox();
        Panel playerPanel = new Panel();
        int points = 0;
        string name;
        #endregion

        #region static methods
        public static void Init(PictureBox pictureBox)
        {

            mainPicsBox = pictureBox;

            listOfPlayers.Clear();
            playersComboBox.Items.Clear();


            playersComboBox.Items.Add("1 player");
            playersComboBox.Items.Add("2 player");
            playersComboBox.Items.Add("3 player");
            playersComboBox.Items.Add("4 player");
            playersComboBox.Items.Add("againts pc");
            playersComboBox.Items.Add("pc vs pc");

            playersComboBox.Location = new Point(700, 200);
            pictureBox.Controls.Add(playersComboBox);

            playersComboBox.SelectedValueChanged += playersComboBox_SelectedValueChanged;

            colorList.Add(Color.CornflowerBlue);
            colorList.Add(Color.OrangeRed);
            colorList.Add(Color.YellowGreen);
            colorList.Add(Color.DarkKhaki);

            colorList2.Add(Color.LightBlue);
            colorList2.Add(Color.LightPink);
            colorList2.Add(Color.LightGreen);
            colorList2.Add(Color.LightGoldenrodYellow);
            colorList2.Add(Color.LightPink);
            colorList2.Add(Color.LightGreen);

            for (int i = 0; i < 4; i++)
            {
                Player currentPlayer = new Player();

                currentPlayer.name = "player_" + i.ToString();
                currentPlayer.listOfTiles.Clear();
                currentPlayer.playerImeLabel.Location = new Point(650, i * 60 + 31);
                currentPlayer.playerImeLabel.Size = new System.Drawing.Size(50, 50);
                currentPlayer.playerImeLabel.Text = "player " + (i + 1).ToString();
                currentPlayer.playerImeLabel.Visible = false;

                currentPlayer.pointsDisplyTextBox.Location = new Point(700, i * 60 + 30);
                currentPlayer.pointsDisplyTextBox.Size = new System.Drawing.Size(100, 50);
                currentPlayer.pointsDisplyTextBox.BackColor = colorList.ElementAt(i);
                currentPlayer.pointsDisplyTextBox.Visible = false;
                currentPlayer.pointsDisplyTextBox.Text = "0";
                listOfPlayers.Add(currentPlayer);

                mainPicsBox.Controls.Add(currentPlayer.playerImeLabel);
                mainPicsBox.Controls.Add(currentPlayer.pointsDisplyTextBox);
            }

            mainPicsBox.BackColor = colorList2.ElementAt(currentPlayerIndex);


        }

        private static void playersComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            
            activePlayer = listOfPlayers.ElementAt(0);


            ComboBox currentCB = (ComboBox)sender;
            playerCount = currentCB.SelectedIndex + 1;
            if (playerCount < 5) // player vs player
            {
                for (int i = 0; i < playerCount; i++)
                {

                    listOfPlayers.ElementAt(i).playerImeLabel.Visible = true;
                    listOfPlayers.ElementAt(i).pointsDisplyTextBox.Visible = true;


                }
            }
            else if (playerCount == 5) // player VS PC
            {
                listOfPlayers.ElementAt(0).playerImeLabel.Visible = true;
                listOfPlayers.ElementAt(0).pointsDisplyTextBox.Visible = true;

                listOfPlayers.ElementAt(1).playerImeLabel.Text = "PC";
                listOfPlayers.ElementAt(1).playerImeLabel.Visible = true;
                listOfPlayers.ElementAt(1).pointsDisplyTextBox.Visible = true;
                listOfPlayers.ElementAt(1).isPC = true;
                playerCount = 2;
            }
            else // PC VS PC
            {
                listOfPlayers.ElementAt(0).playerImeLabel.Text = "PC";
                listOfPlayers.ElementAt(0).playerImeLabel.Visible = true;
                listOfPlayers.ElementAt(0).pointsDisplyTextBox.Visible = true;
                listOfPlayers.ElementAt(0).isPC = true;

                listOfPlayers.ElementAt(1).playerImeLabel.Text = "PC 1";
                listOfPlayers.ElementAt(1).playerImeLabel.Visible = true;
                listOfPlayers.ElementAt(1).pointsDisplyTextBox.Visible = true;
                listOfPlayers.ElementAt(1).isPC = true;
                playerCount = 2;


            }
            Tile.Init(mainPicsBox);
            currentCB.Hide();

            if (listOfPlayers.ElementAt(0).isPC && listOfPlayers.ElementAt(1).isPC)
            {
                Word newWord = activePlayer.PlayPC();
                activePlayer.AutoPlaceTiles(newWord);
                Word.listOfWordsOnBoard.Add(newWord);

            }


            //    RoundDone();
        }

        public static void RoundDone()
        {
            #region Obracun Tock & Preverba besede
            if (!activePlayer.isPC)
            {
                int newpoints = 0;
                newpoints += CountVerticalPoints();
                newpoints += CountHorizontalPoints();
                activePlayer.points += newpoints;
                if (newpoints == 0)
                {
                    MessageBox.Show("This word doesn't exist ;-;");
                    return;
                }
                activePlayer.pointsDisplyTextBox.Text = activePlayer.points.ToString();
                activePlayer.listOfTilesOnBoard.Clear();
            }
            #endregion


            #region Zamenjava Igralca
            Tile.setVisible(false);

            currentPlayerIndex++;
            if (currentPlayerIndex > playerCount - 1)
            {
                currentPlayerIndex = 0;
            }



            activePlayer = listOfPlayers.ElementAt(currentPlayerIndex);

            buttonCounter = activePlayer.listOfTiles.Count();
            Tile.setVisible(true);
            Tile.Init(mainPicsBox);

            mainPicsBox.BackColor = colorList2.ElementAt(currentPlayerIndex);

            if (activePlayer.isPC)
            {
                Word newWord = activePlayer.PlayPC();
                activePlayer.AutoPlaceTiles(newWord);
                Word.listOfWordsOnBoard.Add(newWord);
            }

            //   Tile.setVisible(true);
            // Tile.Init(mainPicsBox);

            #endregion




        }

        public static void NewGame()
        {
            foreach (Player p in listOfPlayers)
            {
                p.playerImeLabel.Visible = false;
                p.pointsDisplyTextBox.Visible = false;
            }

            currentPlayerIndex = 0;
            mainPicsBox.BackColor = colorList2.ElementAt(currentPlayerIndex);

            playersComboBox.Visible = true;
            Tile.pointsDictionary.Clear();
        }


        public static int CountHorizontalPoints()
        {
            int currentLetterPoints = 0;
            int currentWordAwards = 1;
            int currentLetterAwards = 0;
            string currentWord = "";
            int currentPoints = 0;
            if (activePlayer.listOfTilesOnBoard.Count < 1)
            {
                MessageBox.Show("Beseda je prekratka (ಠ益ಠ)");
                return 0;
            }
            Tile currentTile = activePlayer.listOfTilesOnBoard.ElementAt(0);
            int startX = currentTile.positionX;
            int startY = currentTile.positionY;
            if (activePlayer.listOfTilesOnBoard.Count > 0)
            {

                currentTile = activePlayer.listOfTilesOnBoard.ElementAt(0);
                if (currentTile != null)
                {
                    int firstPosition = currentTile.positionX;
                    int lastPosition = currentTile.positionX;
                    for (int i = currentTile.positionX; i > -1; i--)
                    {
                        if (Field.fieldArray[i, currentTile.positionY].letter == "")
                        {
                            firstPosition = i + 1;
                            break;
                        }
                        else
                        {

                        }
                    }
                    for (int k = firstPosition; k < 15; k++)
                    {
                        if (Field.fieldArray[k, currentTile.positionY].letter == "")
                        {
                            break;
                        }
                        else
                        {
                            currentWord += Field.fieldArray[k, currentTile.positionY].letter;
                            currentLetterAwards = Field.fieldArray[k, currentTile.positionY].letterAward;
                            currentWordAwards *= Field.fieldArray[k, currentTile.positionY].wordAward;
                            currentLetterPoints = Tile.pointsDictionary[Field.fieldArray[k, currentTile.positionY].letter];
                            currentPoints += currentLetterPoints * currentLetterAwards;

                        }
                    }

                }
            }
            currentPoints *= currentWordAwards;
            if (currentWord.Length < 2)
                currentPoints = 0;
            else
            {
                if (!Form3.wordsInDictionary.Contains(currentWord.ToLower()))
                    currentPoints = 0;
                else
                {
                    mainPicsBox.Parent.Text = currentWord;
                    Word newWord = new Word(currentWord, startX, startY, startX + currentWord.Length - 1, startY, false, currentPoints);
                    Word.listOfWordsOnBoard.Add(newWord);
                }



            }

            return currentPoints;

        }

        public static int CountVerticalPoints()
        {
            int currentLetterPoints = 0;
            int currentWordAwards = 1;
            int currentLetterAwards = 0;
            string currentWord = "";
            int currentPoints = 0;
            if (activePlayer.listOfTilesOnBoard.Count < 1)
            {
                MessageBox.Show("The word is too short (ಠ益ಠ)");
                return 0;
            }
            Tile currentTile = activePlayer.listOfTilesOnBoard.ElementAt(0);
            int startX = currentTile.positionX;
            int startY = currentTile.positionY;
            if (activePlayer.listOfTilesOnBoard.Count > 0)
            {
                currentTile = activePlayer.listOfTilesOnBoard.ElementAt(0);
                if (currentTile != null)
                {
                    int firstPosition = currentTile.positionY;
                    int lastPosition = currentTile.positionY;
                    for (int i = currentTile.positionY; i > -1; i--)
                    {
                        if (Field.fieldArray[currentTile.positionX, i].letter == "")
                        {
                            firstPosition = i + 1;
                            break;
                        }
                        else
                        {

                        }
                    }
                    for (int k = firstPosition; k < 15; k++)
                    {
                        if (Field.fieldArray[currentTile.positionX, k].letter == "")
                        {
                            break;
                        }
                        else
                        {

                            currentWord += Field.fieldArray[currentTile.positionX, k].letter;
                            currentLetterAwards = Field.fieldArray[currentTile.positionX, k].letterAward;
                            currentWordAwards *= Field.fieldArray[currentTile.positionX, k].wordAward;
                            currentLetterPoints = Tile.pointsDictionary[Field.fieldArray[currentTile.positionX, k].letter];
                            currentPoints += currentLetterPoints * currentLetterAwards;
                        }
                    }

                }

            }
            currentPoints *= currentWordAwards;
            if (currentWord.Length < 2)
                currentPoints = 0;
            else
            {
                if (!Form3.wordsInDictionary.Contains(currentWord.ToLower()))
                    currentPoints = 0;
                else
                {
                    mainPicsBox.Parent.Text = currentWord;
                    Word newWord = new Word(currentWord, startX, startY, startX, startY + currentWord.Length - 1, true, currentPoints);
                    Word.listOfWordsOnBoard.Add(newWord);
                }
            }

            return currentPoints;

        }



        Word PlayPC()
        {
            Word bestboiWord = new Word();
            Word empty = new Word();

            #region iskanje po korenu besede
            List<Word> superGoodWords = new List<Word>();
            string wordFromTiles = "";

            for (int j = 0; j < this.listOfTiles.Count; j++)
                wordFromTiles = wordFromTiles + this.listOfTiles.ElementAt(j).tileButton.Text.ToLower();
            string wordFromTiles1 = wordFromTiles;

            if (Word.listOfWordsOnBoard.Count() > 0)
            {
                foreach (Word currentWord in Word.listOfWordsOnBoard)
                {

                    string curWord = currentWord.text.ToLower();
                    superGoodWords.AddRange(GetWord(curWord, wordFromTiles, currentWord, currentWord.startIndexX, currentWord.startIndexY));
                }
            }
            else
                superGoodWords.AddRange(GetWord(wordFromTiles.Substring(0, 1).ToLower(), wordFromTiles, empty, 7, 7));
            #endregion

            #region iskanje po crkah
            foreach (Word currentWord in Word.listOfWordsOnBoard)
            {

                for (int l = 0; l < currentWord.text.Length; l++)
                {
                    int curLetterStartIndexX;
                    int curLetterStartIndexY;
                    string curLetter = currentWord.text.Substring(l, 1).ToLower();
                    if (currentWord.isVertical)
                    {
                        curLetterStartIndexX = currentWord.startIndexX;
                        curLetterStartIndexY = currentWord.startIndexY + l;
                    }
                    else
                    {
                        curLetterStartIndexX = currentWord.startIndexX + l;
                        curLetterStartIndexY = currentWord.startIndexY;
                    }

                    superGoodWords.AddRange(GetWord(curLetter, wordFromTiles, currentWord, curLetterStartIndexX, curLetterStartIndexY));
                }
            }
            #endregion


            foreach (Word currentWord in superGoodWords)
            {
                currentWord.WordPoints();
            }

            superGoodWords = superGoodWords.Where(f => f.itFits).OrderBy(o => o.point).Reverse().ToList();


            foreach (Word currentWord in superGoodWords)
            {
                currentWord.ToFitOrNotToFit();
            }

            superGoodWords = superGoodWords.Where(f => f.itFits).OrderBy(o => o.point).Reverse().ToList();

            bestboiWord = superGoodWords.FirstOrDefault();

            return bestboiWord;
        }


        public static List<Word> GetWord(string curWord, string wordFromTiles, Word currentWord, int curLetterStartIndexX, int curLetterStartIndexY)
        {
            List<Word> superGoodWords = new List<Word>();
            string wordFromTiles1 = wordFromTiles;
            List<string> potentialWords = new List<string>();
            List<string> wordsonboard = new List<string>();
            foreach (Word wordek in Word.listOfWordsOnBoard)
                wordsonboard.Add(wordek.text);
            #region iskanje besed

            {
                potentialWords = (from word in Form3.wordsInDictionary
                                  where word.Contains(curWord)
                                  && word != curWord
                                  && !wordsonboard.Contains(word)
                                  select word).ToList();
            }


            foreach (string potWord in potentialWords)
            {

                wordFromTiles1 = wordFromTiles;
                string potWord1 = ReplaceFirst(potWord, curWord, "");
                if (potWord1 != "")
                {
                    bool isGoodWord = true;
                    for (int i = 0; i < potWord1.Length; i++)
                    {
                        string currentLetter = potWord1.Substring(i, 1);

                        if (!(wordFromTiles1.Contains(currentLetter)))
                        {
                            isGoodWord = false;

                        }
                        else
                        {
                            wordFromTiles1 = wordFromTiles1.Replace(currentLetter, "");
                        }
                    }
                    #endregion

                    #region sestavljanje Word
                    if (isGoodWord)
                    {
                        

                        if (Form3.hunspell.Spell(potWord))
                        {
                            string potWord2 = potWord.Replace(curWord, "/");
                            string[] wordTab = potWord2.Split('/');
                            int LeftLengtn = 0;
                            int countLetter = potWord2.Count(f => f == '/');

                            for (int i = 0; i < countLetter; i++)
                            {

                                LeftLengtn = LeftLengtn + wordTab[i].Length + 1;

                                Word tempWord = new Word();

                                tempWord.text = potWord;
                                if (curWord.Length > 1)
                                    tempWord.isVertical = currentWord.isVertical;
                                else if (curWord.Length == 1)
                                    tempWord.isVertical = !(currentWord.isVertical);
                                else
                                    tempWord.isVertical = true;

                                if (tempWord.isVertical)
                                {
                                    tempWord.startIndexX = curLetterStartIndexX;
                                    tempWord.startIndexY = curLetterStartIndexY - (LeftLengtn - 1);
                                    tempWord.endIndexX = curLetterStartIndexX;
                                    tempWord.endIndexY = tempWord.startIndexY + (tempWord.text.Length - 1);
                                }
                                else
                                {
                                    tempWord.startIndexX = curLetterStartIndexX - (LeftLengtn - 1);
                                    tempWord.startIndexY = curLetterStartIndexY;
                                    tempWord.endIndexX = tempWord.startIndexX + (tempWord.text.Length - 1);
                                    tempWord.endIndexY = curLetterStartIndexY;

                                }
                                superGoodWords.Add(tempWord);
                            }


                        }

                    }
                    #endregion
                }
            }

            return superGoodWords;
        }


        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
        #endregion

        #region nonstatic methods
        public void AutoPlaceTiles(Word bestboiWord)
        {
            mainPicsBox.Parent.Text = bestboiWord.text;
            for (int i = 0; i < bestboiWord.text.Length; i++)
            {
                string currentLetter = bestboiWord.text.Substring(i, 1).ToUpper();

                if (bestboiWord.isVertical)
                {
                    string existingLetter = Field.fieldArray[bestboiWord.startIndexX, bestboiWord.startIndexY + i].letter;


                    if (existingLetter == "")
                    {
                        Tile currentTile = listOfTiles.Where(o => o.tileButton.Text == currentLetter).First();
                        Field.fieldArray[bestboiWord.startIndexX, bestboiWord.startIndexY + i].letter = currentLetter;
                        currentTile.tileButton.Location = new Point(Field.offsetI + bestboiWord.startIndexX * 33, Field.offsetJ + ((bestboiWord.startIndexY + i) * 33));
                        Player.activePlayer.listOfTilesOnBoard.Add(currentTile);
                        currentTile.tileButton.BringToFront();
                        this.listOfTiles.Remove(currentTile);
                    }


                }
                else //horizontal
                {
                    string existingLetter = Field.fieldArray[bestboiWord.startIndexX + i, bestboiWord.startIndexY].letter;


                    if (existingLetter == "")
                    {
                        Tile currentTile = listOfTiles.Where(o => o.tileButton.Text == currentLetter).First();
                        Field.fieldArray[bestboiWord.startIndexX + i, bestboiWord.startIndexY].letter = currentLetter;
                        currentTile.tileButton.Location = new Point(Field.offsetI + ((bestboiWord.startIndexX + i) * 33), Field.offsetJ + bestboiWord.startIndexY * 33);
                        currentTile.tileButton.BringToFront();
                        Player.activePlayer.listOfTilesOnBoard.Add(currentTile);
                        this.listOfTiles.Remove(currentTile);
                    }


                }

            }
            activePlayer.points += bestboiWord.point;


            activePlayer.pointsDisplyTextBox.Text = activePlayer.points.ToString();

            if (!listOfPlayers.ElementAt(0).isPC)
                RoundDone();


        }





        #endregion
    }
}
