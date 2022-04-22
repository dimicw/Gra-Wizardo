using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testKratki
{
	//test
	public partial class Form1 : Form
	{
		Player player = new Player(); # siemka

		public Form1()
		{
			InitializeComponent();
		}

		int xAxis, yAxis;
		PictureBox[,] Floor;
		PictureBox[,] Board;
		bool[,] occupiedTile;

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			char key = '0';
			switch (e.KeyData)
			{
				case Keys.W:
					key = 'w';
					Board[player.positionY, player.positionX].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\wizardo\wizardo-north.png");
					break;
				case Keys.D:
					key = 'd';
					Board[player.positionY, player.positionX].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\wizardo\wizardo-east.png");
					break;
				case Keys.S:
					key = 's';
					Board[player.positionY, player.positionX].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\wizardo\wizardo-south.png");
					break;
				case Keys.A:
					key = 'a';
					Board[player.positionY, player.positionX].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\wizardo\wizardo-west.png");
					break;
				default:
					break;
			}

			player.Action(key);


		}

		private void Form1_Load(object sender, EventArgs e)
		{
			xAxis = 40;
			yAxis = 20;
			Floor = new PictureBox[yAxis, xAxis];
			Board = new PictureBox[yAxis, xAxis];
			occupiedTile = new bool[yAxis,xAxis];

			int left = 2, top = 2;
			for (int i=0; i < yAxis; i++)
			{
				left = 2;
				for (int j = 0; j < xAxis; j++)
				{
					Floor[i, j] = new PictureBox();
					Floor[i, j].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\build\floor.png");
					Floor[i, j].Location = new Point(left, top);
					Floor[i, j].Size = new Size(20, 20);

					mapBase.Controls.Add(Floor[i, j]);

					Board[i, j] = new PictureBox();
					if (i==0 && j==0) Board[i, j].Image = Image.FromFile(@"C:\Users\domin\source\repos\testKratki\testKratki\images\wizardo\wizardo-west.png");
					player.positionY = 0;
					player.positionX = 0;
					Floor[i, j].Controls.Add(Board[i, j]);
					Board[i, j].Location = new Point(0, 0);
					Board[i, j].BackColor = Color.Transparent;

					occupiedTile[i, j] = false;
					left += 20;
				}
				top += 20;
			}
		}
	}


	public class Player
	{
		int HP, DMG;				// current hit poionts left, damage dealt with each attack

		int facing;					// 0-north, 1-east, 2-south, 3-west
		bool movement, attack;      // true if currently able to do so, false if not

		public int positionX, positionY,					// player's X and Y coordinates	
			previousPositionX, previousPositionY;			// player's previous X and Y coordinates

		public Player()
		{
			this.HP = 40;
			this.DMG = 2;

			this.facing = 0;
			this.movement = true;
			this.attack = true;
		}

		public Player(int HP, int DMG, int facing, bool movement, bool attack)
		{
			this.HP = HP;
			this.DMG = DMG;

			this.facing = facing;
			this.movement = movement;
			this.attack = attack;
		}

		public void Action(char input)
		{
			if (input == 'w')
			{
				if (this.facing == 0) positionY--;
				else
				{
					facing = 0;
				}
			}
			else if (input == 'd')
			{
				if (this.facing == 1) positionX++;
				else
				{
					facing = 1;
				}
			}
			else if (input == 's')
			{
				if (this.facing == 2) positionY++;
				else
				{
					facing = 2;
				}
			}
			else if (input == 'a')
			{
				if (this.facing == 3) positionX--;
				else
				{
					facing = 3;
				}
			}
			else if (input == '1') CastArcaneBolt();
		}

		private void CastArcaneBolt()
		{

		}
	}
}
