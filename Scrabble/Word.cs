using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    class Word
    {
        #region static variable
        public static List<Word> listOfWordsOnBoard = new List<Word>();
        public static List<Word> listOfPotentialWords = new List<Word>();
        #endregion


        #region nonstatic variable
        public int startIndexX;
        public int startIndexY;
        public int endIndexX;
        public int endIndexY;
        public string text;
        public bool isVertical;
        public int point;
        public bool itFits = true;
        #endregion


        #region static methods

        public static void NewGame()
        {
            listOfWordsOnBoard.Clear();

        }
        #endregion

        #region nostatic methods
        public Word(string text, int startIndexX, int startIndexY, int endIndexX, int endIndexY, bool isVertical, int point)
        {
            this.text = text;
            this.startIndexX = startIndexX;
            this.startIndexY = startIndexY;
            this.endIndexX = endIndexX;
            this.endIndexY = endIndexY;
            this.isVertical = isVertical;
            this.point = point;

        }

        public Word()
        {
        }

        public void WordPoints()
        {
            try
            {
                int points = 0;
                int currentWordAwards = 1;

                for (int i = 0; i < text.Length; i++)
                {



                    if (isVertical)
                    {
                        int currentLetterAwards = Field.fieldArray[startIndexX, startIndexY + i].letterAward;
                        currentLetterAwards *= Tile.pointsDictionary[text.Substring(i, 1).ToUpper()];
                        points += currentLetterAwards;
                        currentWordAwards *= Field.fieldArray[startIndexX, startIndexY + i].wordAward;
                    }
                    else
                    {
                        int currentLetterAwards = Field.fieldArray[startIndexX + i, startIndexY].letterAward;
                        currentLetterAwards *= Tile.pointsDictionary[text.Substring(i, 1).ToUpper()];
                        points += currentLetterAwards;
                        currentWordAwards *= Field.fieldArray[startIndexX + i, startIndexY].wordAward;
                    }



                }
                points *= currentWordAwards;

                point = points;
            }
            catch
            {
                point = 0;
                itFits = false;

            }
        }


        public void ToFitOrNotToFit()
        {

            for (int i = 0; i < text.Length; i++)
            {
                string currentLetter = text.Substring(i, 1).ToUpper();
                string leftLetter = "";
                string rightLetter = "";
                string beforeLetter = "";
                string afterLetter = "";

                if (isVertical)
                {
                    string existingLetter = Field.fieldArray[startIndexX, startIndexY + i].letter;

                    try { leftLetter = Field.fieldArray[startIndexX - 1, startIndexY + i].letter; } catch { }
                    try { rightLetter = Field.fieldArray[startIndexX + 1, startIndexY + i].letter; } catch { }

                    if (existingLetter == "" && leftLetter == "" && rightLetter == "")
                    {
                    }
                    else if (currentLetter == existingLetter)
                    {
                    }
                    else
                    {
                        itFits = false;
                        return;
                    }

                    if (i == 0)
                    {
                        try { beforeLetter = Field.fieldArray[startIndexX, startIndexY - 1].letter; } catch { }

                        if (beforeLetter != "")
                        {
                            itFits = false;
                            return;
                        }
                    }

                    if (i == (text.Length - 1))
                    {
                        try { afterLetter = Field.fieldArray[startIndexX, (startIndexY + i) + 1].letter; } catch { }

                        if (afterLetter != "")
                        {
                            itFits = false;
                            return;
                        }
                    }
                }
                else //horizontal
                {
                    string existingLetter = Field.fieldArray[startIndexX + i, startIndexY].letter;

                    try { leftLetter = Field.fieldArray[startIndexX + i, startIndexY - 1].letter; } catch { }
                    try { rightLetter = Field.fieldArray[startIndexX + i, startIndexY + 1].letter; } catch { }

                    if (existingLetter == "" && leftLetter == "" && rightLetter == "")
                    {
                    }
                    else if (currentLetter == existingLetter)
                    {
                    }
                    else
                    {
                        itFits = false;
                        return;
                    }

                    if (i == 0)
                    {
                        try { beforeLetter = Field.fieldArray[startIndexX - 1, startIndexY].letter; } catch { }

                        if (beforeLetter != "")
                        {
                            itFits = false;
                            return;
                        }
                    }

                    if (i == (text.Length - 1))
                    {
                        try { afterLetter = Field.fieldArray[(startIndexX + i) + 1, startIndexY].letter; } catch { }

                        if (afterLetter != "")
                        {
                            itFits = false;
                            return;
                        }
                    }
                }
            }
        }
        #endregion

    }
}
