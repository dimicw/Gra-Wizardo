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
	public partial class Form1 : Form
	{				

		public Form1()
		{
			InitializeComponent();
			Text = "Zombifying Adventures of Wizardo";							// title of the game (and window)
		}

		private void Form1_Load(object sender, EventArgs e)						// instructions done immediately after starting the program
		{	
			Values.floor = new PictureBox[Values.yAxis, Values.xAxis];			// adds dimensions to floor table
			Values.board = new PictureBox[Values.yAxis, Values.xAxis];          // adds dimensions to board table
			Values.occupiedTile = new bool[Values.yAxis, Values.xAxis];         // adds dimensions to occupiedTile table
			Values.zombie = new Zombie[6];

			int left = 2, top = 2;												// margin values, later used to create the board
			for (int i=0; i < Values.yAxis; i++)								// rows of the board
			{
				left = 2;														// reset of margin
				for (int j = 0; j < Values.xAxis; j++)							// columns of the board
				{
					Values.floor[i, j] = new PictureBox();						// create new picturebox
					Values.floor[i, j].Image									// fill the picturebox with floor image
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\build\floor.png");
					Values.floor[i, j].Location = new Point(left, top);			// relocate the tile next to the previous tile
					Values.floor[i, j].Size = new Size(20, 20);					// resize the tile
					mapBase.Controls.Add(Values.floor[i, j]);					// fix the tiles to the base

					Values.board[i, j] = new PictureBox();						// create new picturebox
					Values.floor[i, j].Controls.Add(Values.board[i, j]);		// make the picturebox a child of the floor tile
					Values.board[i, j].Location = new Point(0, 0);              // relocate the tile to match the floor tile
					Values.board[i, j].BackColor = Color.Transparent;			// make the background of the image transparent

					Values.occupiedTile[i, j] = false;							// make the tile available for the player
					left += 20;
				}
				top += 20;
			}
			LoadMap1();																	// load the first level
			Values.player = new Player(1, 1);											// place the player
			Values.board[Values.player.positionY, Values.player.positionX].Image        // places Wizardo onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\wizardo\wizardo-east.png");
			visibleHP.Text = Values.player.HP + " / 40";                                // display player's current HP 

			Values.zombie[0] = new Zombie(6, 6);                                        // place the zombie
			Values.board[Values.zombie[0].positionY, Values.zombie[0].positionX].Image  // places the zombie onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\zombie\zombie-west.png");
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)       // reaction for each pressed key
		{
			switch (e.KeyData)											// choose action depending on the key pressed by the player
			{
				case Keys.W:
				case Keys.Up:
					Values.player.Action('w');							// changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;		// clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image								// places Wizardo onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\wizardo\wizardo-north.png");
					break;
				case Keys.D:
				case Keys.Right:
					Values.player.Action('d');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\wizardo\wizardo-east.png");
					break;
				case Keys.S:
				case Keys.Down:
					Values.player.Action('s');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\wizardo\wizardo-south.png");
					break;
				case Keys.A:
				case Keys.Left:
					Values.player.Action('a');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\wizardo\wizardo-west.png");
					break;
				case Keys.Space:
				case Keys.Enter:
					Values.zombie[0].brainless();
					visibleHP.Text = Values.player.HP + " / 40";        // display player's current HP 
					break;
				default:
					break;
			}
		}

		private void LoadMap1()							// creator of the first level
		{
			for (int i = 0; i < 10; i++)
			{
				wall(0, i);
				wall(i, 7);
				wall(11, i + 1);
				wall(Values.yAxis - 1 - i, 4);
			}
			wall(3, 3);
			wall(3, 4);
			wall(4, 3);
			wall(4, 4);
			clear(15, 4);
			for (int i = 0; i < Values.yAxis; i++) for (int j = 12; j < Values.xAxis; j++) wall(i, j);
		}

		private void wall(int y, int x)					// creator of a single wall
		{
			Values.board[y, x].Image					// fill the tile with wall image
				= Image.FromFile(@"D:\gitHubRepositories\testKratki\testKratki\images\build\wall.png");
			Values.occupiedTile[y, x] = true;           // make the tile unavailable for the player
		}

		private void clear(int y, int x)				// clear single tile
		{
			Values.board[y, x].Image = null;            // remove the tile image
			Values.occupiedTile[y, x] = false;          // make the tile available for the player
		}


	}

	public static class Values							// base of the game
	{
		public static int xAxis = 40, yAxis = 20;       // size of the map (in tiles)
		public static bool[,] occupiedTile;             // is something blocking you from moving onto the tile
		public static PictureBox[,] floor;              // mesh of the floor
		public static PictureBox[,] board;              // board for zombies and the Magnificent Wizardo himself
		public static Player player;                    // statblock for the most potent wizard - the player (Wizardo)
		public static Zombie[] zombie;					// table of statblocks for zombies
	}
	public class Creature									// base class for player and zombies
	{
		public int HP;                                      // current hit poionts left

		public int facing;                                  // 0-north, 1-east, 2-south, 3-west
		public bool movement, attack;                       // true if currently able to do so, false if not

		public int positionX, positionY,                    // player's X and Y coordinates	
			previousPositionX, previousPositionY;           // player's previous X and Y coordinates
	}

	public class Player : Creature							// class only for the player
	{
		bool spell1unlocked, spell2unlocked;				// true if Wizardo can use those spells

		public Player (int positionX, int posotionY)        // function to create a new plyer and assingn custom location
		{
			HP = 40;

			facing = 0;
			movement = true;
			attack = true;

			spell1unlocked = true;
			spell2unlocked = false;

			this.positionX = positionX;
			this.positionY = posotionY;
			previousPositionX = positionX;
			previousPositionY = posotionY;
		}

		public Player(int HP, int DMG, int facing,           // function to create a new plyer and assing custom values
			bool movement, bool attack, bool spell1unlocked, bool spell2unlocked, int positionX, int positionY)		
		{
			this.HP = HP;

			this.facing = facing;
			this.movement = movement;
			this.attack = attack;

			this.spell1unlocked = spell1unlocked;
			this.spell2unlocked = spell2unlocked;

			this.positionX = positionX;
			this.positionY = positionY;
			previousPositionX = positionX;
			previousPositionY = positionY;
		}

		public void Action(char input)						// change values of the player depending on the key pressed
		{
			if (input == 'w')
			{
				if (this.facing == 0)						// check if the player is facing north
				{
					if (positionY != 0 && Values.occupiedTile[positionY - 1, positionX] == false)       // check for obstacles and map borders
					{
						previousPositionY = positionY;		// save current position
						previousPositionX = positionX;		
						positionY--;						// move the player north
					}
				}
				else facing = 0;							// rotate the player north
			}
			else if (input == 'd')
			{
				if (this.facing == 1)						// check if the player is facing east
				{
					if (positionX != Values.xAxis - 1 && Values.occupiedTile[positionY, positionX + 1] == false)        // check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionX++;                        // move the player north
					}
				}
				else facing = 1;                            // rotate the player north
			}
			else if (input == 's')
			{
				if (this.facing == 2)                       // check if the player is facing south
				{
					if (positionY != Values.yAxis - 1 && Values.occupiedTile[positionY + 1, positionX] == false)        // check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionY++;                        // move the player south
					}
				}
				else facing = 2;                            // rotate the player south
			}
			else if (input == 'a')
			{
				if (this.facing == 3)                       // check if the player is facing west
				{
					if (positionX != 0 && Values.occupiedTile[positionY, positionX - 1] == false)       // check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionX--;                        // move the player west
					}
				}
				else facing = 3;                            // rotate the player west
			}
		}
	}

	public class Zombie : Creature							// class for zombies
	{
		int DMG;											// damage dealt with each attack

		public Zombie (int positionX, int posotionY)		// function to create a new zombie and assingn custom location
		{
			HP = 10;
			DMG = 2;

			facing = 0;
			movement = true;
			attack = true;

			this.positionX = positionX;
			this.positionY = posotionY;
			previousPositionX = positionX;
			previousPositionY = posotionY;
		}

		public void brainless()
		{
			int xDifference = Values.player.positionX - positionX;
			int yDifference = Values.player.positionY - positionY;

			if (Math.Abs(xDifference) <= 1 && Math.Abs(yDifference) <= 1)
			{
				Values.player.HP -= DMG;
			}
			else
			{
				if (xDifference > 1)
				{
					
				}
				else if (xDifference < -1) 
				{
					
				}
				else if (yDifference > 1) 
				{
					
				}
				else if (yDifference < -1) 
				{
					
				}

			}
		}
	}
}
