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

		private void Form1_Load(object sender, EventArgs e)				// instructions done immediately after starting the program
		{	
			Values.floor = new PictureBox[Values.yAxis, Values.xAxis];				// adds dimensions to floor table
			Values.board = new PictureBox[Values.yAxis, Values.xAxis];				// adds dimensions to board table
			Values.effects = new PictureBox[Values.yAxis, Values.xAxis];			// adds dimensions to effects table

			Values.occupiedTile = new bool[Values.yAxis, Values.xAxis];				// adds dimensions to occupiedTile table
			Values.zombie = new Zombie[6];

			int left = 2, top = 2;													// margin values, later used to create the board
			for (int i=0; i < Values.yAxis; i++)									// rows of the board
			{
				left = 2;															// reset of margin
				for (int j = 0; j < Values.xAxis; j++)								// columns of the board
				{
					Values.floor[i, j] = new PictureBox();							// create new picturebox
					Values.floor[i, j].Image										// fill the picturebox with floor image
						= Image.FromFile(@"..\..\..\images\build\floor.png");
					Values.floor[i, j].Location = new Point(left, top);				// relocate the tile next to the previous tile
					Values.floor[i, j].Size = new Size(20, 20);						// resize the tile
					mapBase.Controls.Add(Values.floor[i, j]);						// fix the tiles to the base

					Values.board[i, j] = new PictureBox();							// create new picturebox
					Values.floor[i, j].Controls.Add(Values.board[i, j]);			// make the picturebox a child of the floor tile
					Values.board[i, j].Location = new Point(0, 0);					// relocate the tile to match the floor tile
					Values.board[i, j].BackColor = Color.Transparent;				// make the background of the image transparent

					Values.effects[i, j] = new PictureBox();						// create new picturebox
					Values.board[i, j].Controls.Add(Values.effects[i, j]);			// make the picturebox a child of the floor tile
					Values.effects[i, j].Location = new Point(0, 0);				// relocate the tile to match the floor tile
					Values.effects[i, j].BackColor = Color.Transparent;				// make the background of the image transparent

					Values.occupiedTile[i, j] = false;								// make the tile available for the player
					left += 20;
				}
				top += 20;
			}
			LoadMap1();																	// load the first level
			zombieSpawn();																// spawn a zombie
			Values.player = new Player(1, 1);											// place the player
			Values.board[Values.player.positionY, Values.player.positionX].Image        // places Wizardo onto a new tile
						= Image.FromFile(@"..\..\..\images\wizardo\wizardo-east.png");
			visibleHP.Text = Values.player.HP + " / 40";                                // display player's current HP
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)       // reaction for each pressed key
		{
			bool levelCleared = false;
			switch (e.KeyData)											// choose action depending on the key pressed by the player
			{
				case Keys.W:
				case Keys.Up:
					Values.player.Action('w');							// changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;		// clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image								// places Wizardo onto a new tile
						= Image.FromFile(@"..\..\..\images\wizardo\wizardo-north.png");
					break;
				case Keys.D:
				case Keys.Right:
					Values.player.Action('d');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"..\..\..\images\wizardo\wizardo-east.png");
					break;
				case Keys.S:
				case Keys.Down:
					Values.player.Action('s');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"..\..\..\images\wizardo\wizardo-south.png");
					break;
				case Keys.A:
				case Keys.Left:
					Values.player.Action('a');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Image.FromFile(@"..\..\..\images\wizardo\wizardo-west.png");
					break;
				case Keys.Space:
				case Keys.Enter:
					levelCleared = true;																				// assume level is completed 
					for (int i = 0; i <= Values.zombieCount; i++)														// attack and move (each zombie)
					{
						if (Values.zombie[i].HP > 0)                                                                    // check if its still alive (kind of)
						{
							Values.zombie[i].brainless();                                                               // move and attack
							levelCleared = false;                                                                       // make level not completed yet
						}
						else
						{
							if (Values.zombie[i].alive)                                                                 // check if it already died
							{
								Values.zombie[i].alive = false;                                                         // make it considered dead
								Values.board[Values.zombie[i].positionY, Values.zombie[i].positionX].Image = null;      // remove zombie from the map
								Values.occupiedTile[Values.zombie[i].positionY, Values.zombie[i].positionX] = false;    // make its tile available}
							}
						}
					}
					if (levelCleared) MessageBox.Show("You won!");														// if all the zombies are dead, show the final message
					visibleHP.Text = Values.player.HP + " / 40";														// display player's current HP 
					foreach (PictureBox effect in Values.effects) effect.Image = null;									// clear spell effects
					break;
				case Keys.D1:
					Values.spellOne.useLinearSpell();																			// use the first spell
					break;
				case Keys.D2:
					Values.spellTwo.useCircularSpell();
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

		private void wall(int y, int x)														// creator of a single wall
		{
			Values.board[y, x].Image = Image.FromFile(@"..\..\..\images\build\wall.png");	// fill the tile with wall image
			Values.occupiedTile[y, x] = true;												// make the tile unavailable for the player
		}

		private void clear(int y, int x)				// clear single tile
		{
			Values.board[y, x].Image = null;            // remove the tile image
			Values.occupiedTile[y, x] = false;          // make the tile available for the player
		}

		private void zombieSpawn()										// place a zombie on the map and assign them values
		{
			int xRand, yRand;											// random x and y coordinates

			for (int i = 0; i <= Values.zombieCount; i++)				// assing position for each zombie on the map
            {
                xRand = new Random().Next(Values.xAxis - 1);			// random x value
                yRand = new Random().Next(Values.yAxis - 1);			// random y value

                if (Values.occupiedTile[yRand, xRand] == false)         // check if the tile is available
				{
                    Values.zombie[i] = new Zombie(yRand, xRand);		// place the zombie
                    Values.board[Values.zombie[i].positionY, Values.zombie[i].positionX].Image  
                        = Image.FromFile(@"..\..\..\images\zombie\zombie-west.png");
                }
				else i -= 1;											// try this loop again
            }
        }
	}

	public static class Values								// base of the game
	{
		public static int zombieCount = 4;						// amount of zombies on the map
		public static int xAxis = 40, yAxis = 20;				// size of the map (in tiles)
		public static bool[,] occupiedTile;						// is something blocking you from moving onto the tile
		public static PictureBox[,] floor;						// mesh of the floor
		public static PictureBox[,] board;						// board for zombies and the Magnificent Wizardo himself
		public static PictureBox[,] effects;					// board for effects 
														
		public static Player player;							// statblock for the most potent wizard - the player (Wizardo)
		public static Zombie[] zombie;                          // table of statblocks for zombies

		public static Spell spellOne = new Spell(2, 4, 10);     // values of the first spell (linear)
		public static Spell spellTwo = new Spell(1, 2, 15);		// values of the second spell (circular)
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

		public Player (int positionY, int positionX)        // function to create a new plyer and assingn custom location
		{
			HP = 40;

			facing = 0;
			movement = true;
			attack = true;

			spell1unlocked = true;
			spell2unlocked = false;

			this.positionX = positionX;
			this.positionY = positionY;
			previousPositionX = positionX;
			previousPositionY = positionY;
		}

		public Player(int HP, int DMG, int facing,          // function to create a new plyer and assing custom values (new lvl probably)
			bool movement, bool attack, bool spell1unlocked, bool spell2unlocked, int positionX, int positionY)		
		{


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
            Values.occupiedTile[Values.player.previousPositionY, Values.player.previousPositionX] = false;      // make the previous tile available
			Values.occupiedTile[Values.player.positionY, Values.player.positionX] = true;                       // make the previous tile unavailable
		}
	}									
	
	public class Spell										// class for spells
    {
		private int range;									// range of the effect (in tiles)
		private int dmg;									// damage dealt for every zombie affected
		private int manaCost;								// cost of casting the spell

		public Spell(int range, int dmg, int manaCost)
        {
			this.range = range;
			this.dmg = dmg;
			this.manaCost = manaCost;
		}
	
		public void useLinearSpell()
        {
			for(int i = 1; i <= range; i++)
            {
                switch (Values.player.facing) {
					case 0:		// north - up - w
                        if (Values.player.positionY - i < Values.yAxis && Values.player.positionY - i + 1 > 0)						// check if the tile exists
                        {
                            if (Values.occupiedTile[Values.player.positionY - i, Values.player.positionX])							// is it occupied
                            {
                                if (!isZombie(Values.player.positionY - i, Values.player.positionX, dmg)) i = range + 1;			// if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY - i, Values.player.positionX].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png"); // visual effect on the zombie
							}
                            else Values.effects[Values.player.positionY - i, Values.player.positionX].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");		// visual effect on the free tile
						}
                        break;
					case 1:		// east - right - d
						if (Values.player.positionX + i < Values.xAxis && Values.player.positionX + i + 1 > 0)						// check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY, Values.player.positionX + i])							// is it occupied
							{
								if (!isZombie(Values.player.positionY, Values.player.positionX + i, dmg)) i = range + 1;			// if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY, Values.player.positionX + i].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png"); // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY, Values.player.positionX + i].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");     // visual effect on the free tile
						}
						break;
					case 2:		// south - down - s
						if (Values.player.positionY + i < Values.yAxis && Values.player.positionY + i + 1 > 0)						// check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY + i, Values.player.positionX])							// is it occupied
							{
								if (!isZombie(Values.player.positionY + i, Values.player.positionX, dmg)) i = range + 1;			// if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY + i, Values.player.positionX].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png"); // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY + i, Values.player.positionX].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");     // visual effect on the free tile
						}
						break;
					case 3:		// west - left - a
						if (Values.player.positionX - i < Values.xAxis && Values.player.positionX - i + 1 > 0)						// check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY, Values.player.positionX - i])							// is it occupied
							{
								if (!isZombie(Values.player.positionY, Values.player.positionX - i, dmg)) i = range + 1;			// if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY, Values.player.positionX - i].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");	// visual effect on the zombie
							}
							else Values.effects[Values.player.positionY, Values.player.positionX - i].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");     // visual effect on the free tile
						}
						break;
					default:
						break;
                }
            }
		}

		public void useCircularSpell()
		{
			circular(-1, -1); 
			circular(-1, 0);
			circular(-1, 1);
			circular(0, -1);
			circular(0, 1);
			circular(1, -1);
			circular(1, 0);
			circular(1, 1);

			void circular(int y, int x) 
			{
				if (Values.player.positionY + y < Values.yAxis && Values.player.positionY + y + 1 > 0                      // check if the tile exists
					&& Values.player.positionX + x < Values.xAxis && Values.player.positionX + x + 1 > 0)                     
				{
					if (Values.occupiedTile[Values.player.positionY + y, Values.player.positionX + x])                     // is it occupied
					{
						if (isZombie(Values.player.positionY + y, Values.player.positionX + x, dmg))                       // if its a zombie - make an effect
						{
							Values.effects[Values.player.positionY + y, Values.player.positionX + x].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");  // visual effect on the zombie
						}
					}
					else Values.effects[Values.player.positionY + y, Values.player.positionX + x].Image = Image.FromFile(@"..\..\..\images\spells\spell1.png"); // visual effect on the free tile
				}
			}
		}

		private bool isZombie(int y, int x, int dmg)											// check if the tile is zombie or wall
		{
            for (int j = 0; j <= Values.zombieCount; j++)										// for each zombie
            {	
                if (Values.zombie[j].positionY == y && Values.zombie[j].positionX == x)			// check position
                {
                    Values.zombie[j].HP -= dmg;													// deal damage to the zombie on tile
                    return true;																// it was a zombie				
                }
            }
            return false;																		// it was a wall
        }
	}									
	
	public class Zombie : Creature							// class for zombies
	{
		int DMG;                                            // damage dealt with each attack
		public bool alive;									// true if it can move and attack

		public Zombie (int positionY, int positionX)		// function to create a new zombie and assingn custom location
		{
			HP = 10;
			DMG = 2;
			alive = true;

			facing = 0;
			movement = true;
			attack = true;

			this.positionX = positionX;
			this.positionY = positionY;
			previousPositionX = positionX;
			previousPositionY = positionY;

			Values.occupiedTile[positionY, positionX] = true;
		}

		public void brainless()													// zombie movement and attack
		{
			int xDifference = Values.player.positionX - positionX;				// the difference between the player and the zombie on x axis
			int yDifference = Values.player.positionY - positionY;				// the difference between the player and the zombie on y axis
			
			if (Math.Abs(xDifference) <= 1 && Math.Abs(yDifference) <= 1)		// check if the zombie is near the player, so it can attack 
			{
				if (Values.player.HP > DMG) Values.player.HP -= DMG;			// check if the hp will be positive after the damage
                else
				{
					Values.player.HP = 0;                                       // change to 0 instead of negative integer
					MessageBox.Show("You Died!");								// information of the failiure
				}
			}
            else		                                                        // if it can't attack, it moves to the player
			{
				if (xDifference > 0 && (positionX != Values.xAxis - 1 && Values.occupiedTile[positionY, positionX + 1] == false)) move(0, 1);
				else if (xDifference < 0 && (positionX != 0 && Values.occupiedTile[positionY, positionX - 1] == false)) move(0, -1);
				else if (yDifference > 0 && (positionY != Values.yAxis - 1 && Values.occupiedTile[positionY + 1, positionX] == false)) move(1, 0);
				else if (yDifference < 0 && (positionY != 0 && Values.occupiedTile[positionY - 1, positionX] == false)) move(-1, 0);
				else
				{
                    switch (new Random().Next(4))		// if it cant find a direct path to the player, it wonders in random direction
                    {
						case 0:
							if (positionX != Values.xAxis - 1 && Values.occupiedTile[positionY, positionX + 1] == false) move(0, 1);
							break;
						case 1:
							if (positionX != 0 && Values.occupiedTile[positionY, positionX - 1] == false) move(0, -1);
							break;
						case 2:
							if (positionY != Values.yAxis - 1 && Values.occupiedTile[positionY + 1, positionX] == false) move(1, 0);
							break;
						case 3:
							if (positionY != 0 && Values.occupiedTile[positionY - 1, positionX] == false) move(-1, 0);
							break;
						default:
							break;
					}
				}
			}
		}

		private void move(int y, int x)
		{
			previousPositionX = positionX;			// save current position
			previousPositionY = positionY;

			positionX += x;							// move to the new tile
			positionY += y;	

			Values.board[previousPositionY, previousPositionX].Image = null;		// clear the previous tile

			if (x > 0) Values.board[positionY, positionX].Image = Image.FromFile(@"..\..\..\images\zombie\zombie-east.png");			// place and rotate zombie on the new tile
			else if (x < 0) Values.board[positionY, positionX].Image = Image.FromFile(@"..\..\..\images\zombie\zombie-west.png");
			else if (y > 0) Values.board[positionY, positionX].Image = Image.FromFile(@"..\..\..\images\zombie\zombie-south.png"); 
			else if (y < 0) Values.board[positionY, positionX].Image = Image.FromFile(@"..\..\..\images\zombie\zombie-north.png");

			Values.occupiedTile[previousPositionY, previousPositionX] = false;      // make the previous tile available
			Values.occupiedTile[positionY, positionX] = true;                       // make the new tile unavailable
		}
	}																		
}
