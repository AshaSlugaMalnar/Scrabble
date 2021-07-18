using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            Player.Init(pictureBox1);
            Field.Init(pictureBox1);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Player.RoundDone();

        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            
            Player.NewGame();
            Field.NewGame();
            Tile.NewGame();
            Word.NewGame();
            Player.Init(pictureBox1);
            Field.Init(pictureBox1);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
