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
			Text = "Zombifying Adventures of Wizardo";                              // title of the game (and window)
		}

		private void Form1_Load(object sender, EventArgs e)                         // instructions done immediately after starting the program
		{
			Values.floor = new PictureBox[Values.yAxis, Values.xAxis];              // adds dimensions to floor table
			Values.board = new PictureBox[Values.yAxis, Values.xAxis];              // adds dimensions to board table
			Values.effects = new PictureBox[Values.yAxis, Values.xAxis];            // adds dimensions to effects table
			Values.occupiedTile = new bool[Values.yAxis, Values.xAxis];             // adds dimensions to occupiedTile table

			int left = 2, top = 2;                                                  // margin values, later used to create the board
			for (int i = 0; i < Values.yAxis; i++)                                  // rows of the board
			{
				left = 2;                                                           // reset of margin
				for (int j = 0; j < Values.xAxis; j++)                              // columns of the board
				{
					Values.floor[i, j] = new PictureBox();                          // create new picturebox
					Values.floor[i, j].Image                                        // fill the picturebox with floor image
						= Values.floorImage;
					Values.floor[i, j].Location = new Point(left, top);             // relocate the tile next to the previous tile
					Values.floor[i, j].Size = new Size(32, 32);                     // resize the tile
					mapBase.Controls.Add(Values.floor[i, j]);                       // fix the tiles to the base

					Values.board[i, j] = new PictureBox();                          // create new picturebox
					Values.floor[i, j].Controls.Add(Values.board[i, j]);            // make the picturebox a child of the floor tile
					Values.board[i, j].Location = new Point(0, 0);                  // relocate the tile to match the floor tile
					Values.board[i, j].BackColor = Color.Transparent;               // make the background of the image transparent

					Values.effects[i, j] = new PictureBox();                        // create new picturebox
					Values.board[i, j].Controls.Add(Values.effects[i, j]);          // make the picturebox a child of the floor tile
					Values.effects[i, j].Location = new Point(0, 0);                // relocate the tile to match the floor tile
					Values.effects[i, j].BackColor = Color.Transparent;             // make the background of the image transparent

					Values.occupiedTile[i, j] = false;                              // make the tile available for the player
					left += 32;
				}
				top += 32;
			}
			LoadMap1();                                                             // load the first level
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)       // reaction for each pressed key
		{
			bool levelCleared = false;
			switch (e.KeyData)                                          // choose action depending on the key pressed by the player
			{
				case Keys.W:
				case Keys.Up:
					Values.player.Action('w');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Values.wizardoNorthImage;
					break;

				case Keys.D:
				case Keys.Right:
					Values.player.Action('d');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Values.wizardoEastImage;
					break;

				case Keys.S:
				case Keys.Down:
					Values.player.Action('s');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Values.wizardoSouthImage;
					break;

				case Keys.A:
				case Keys.Left:
					Values.player.Action('a');                          // changes values of facing and position of the player
					Values.board[Values.player.previousPositionY, Values.player.previousPositionX].Image = null;        // clears previous tile
					Values.board[Values.player.positionY, Values.player.positionX].Image                                // places Wizardo onto a new tile
						= Values.wizardoWestImage;
					break;

				case Keys.Space:
				case Keys.Enter:
					if (Values.player.mana <= Values.player.maxMana - Values.player.manaRegen) Values.player.mana += Values.player.manaRegen;       // add 10 mana if its spent
					else if (Values.player.mana > Values.player.maxMana - Values.player.manaRegen) Values.player.mana = Values.player.maxMana;      // limit addition to maximum mana
					visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana;                 // display player's current mana

					levelCleared = true;                                                                                // assume level is completed 
					for (int i = 0; i < Values.zombieCount; i++)                                                        // attack and move (each zombie)
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
								Values.zombie[i].positionY = 1;
								Values.zombie[i].positionX = 1;
                                if(Values.player.HP < Values.player.maxHP)												// check if HP of Player is lower than maximum
                                {
                                    if (Values.player.maxHP - Values.player.HP < 6)										// if difference maxHP - HP is less than 6
                                    {
										Values.player.HP += Values.player.maxHP - Values.player.HP;						// heal at difference after zombie die
									}
                                    else
                                    {
										Values.player.HP += 6;															// heal at 6 after zombie die
                                    }
								}
							}
						}
					}

					if (levelCleared)                                                       // if all the zombies are dead, show the final message
					{
						switch (Values.currentLevel)										// loading next map
						{
							case 1:
								new LevelUpMessage().ShowDialog();
								levelCleared = false;
								LoadMap2();
								break;
							case 2:
								new LevelUpMessage().ShowDialog();
								levelCleared = false;
								LoadMap3();
								break;
							case 3:
								new LevelUpMessage().ShowDialog();
								levelCleared = false;
								LoadMap4();
								break;
							case 4:
								new LevelUpMessage().ShowDialog();
								levelCleared = false;
								LoadMap5();
								break;
							case 5:
								MessageBox.Show("You are finished game!");
								levelCleared = false;
								System.Windows.Forms.Application.Exit();
								break;
						}
					}

					visibleHP.Text = "HP:       " + Values.player.HP + " / " + Values.player.maxHP;                     // display player's current HP
					foreach (PictureBox effect in Values.effects) if (effect.Image == Values.spell1Image) effect.Image = null;      // clear spell effects
					Values.player.movement = true;                                                                      // make movement avaliable
					Values.player.attack = true;                                                                        // make attacks avaliable
					clearFog();
					break;

				case Keys.D1:
					if (Values.player.attack && Values.player.mana >= Values.spellOne.manaCost)                         // check if you have attack avaliable and enough mana
					{
						Values.spellOne.useLinearSpell(Values.spell1Image);                                             // use the first spell (linear) 
						Values.player.attack = false;                                                                   // make attacks unavaliable
					}
					visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana;                 // display player's current mana
					break;

				case Keys.D2:
					if (Values.player.spell2unlocked)
					{
						if (Values.player.attack && Values.player.mana >= Values.spellTwo.manaCost)                         // check if you have attack avaliable and enough mana
						{
							Values.spellTwo.useLinearSpell(Values.spell2Image);                                             // use the second spell (linear) 
							Values.player.attack = false;                                                                   // make attacks unavaliable
						}
						visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana;                 // display player's current mana									
					}
					break;

				case Keys.D3:
					if (Values.player.spell3unlocked)
					{
						if (Values.player.attack && Values.player.mana >= Values.spellThree.manaCost)                       // check if you have attack avaliable and enough mana
						{
							Values.spellThree.useCircularSpell();                                                           // use the third spell (circular) 
							Values.player.attack = false;                                                                   // make attacks unavaliable
						}
						visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana;                 // display player's current mana
					}
					break;

				case Keys.L:			// function for easier testing - kill all zombies
					foreach (Zombie z in Values.zombie) z.HP = 0;
					break;

				default:
					break;
			}
		}

		private void LoadMap1()																	// creator of the first level
		{
			Values.player = new Player(10, 1);                                                   // place the player
			
			mainGraphic.Image = Image.FromFile(@"..\..\..\images\wizardo\wizardo.gif");         // set the main graphic
			visibleHP.Text = "HP:       " + Values.player.HP + " / " + Values.player.maxHP;     // display player's current HP
			visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana; // display player's current mana
			healPlayer();

			Values.currentLevel = 1;
			Values.zombieCount = 5;
			Values.zombie = new Zombie[Values.zombieCount];                                     // create zombies

			for (int j = 0; j <= 39; j++)
			{
				wall(0, j);
				wall(19, j);
			}
			for (int i = 0; i <= 19; i++)
			{
				wall(i, 0);
				wall(i, 39);	
			}

			for(int i = 1; i <= 8; i++)//1
            {
				wall(i, 4);
				wall(i, 5);
			}
			for (int i = 11; i <= 19; i++)
			{
				wall(i, 4);
				wall(i, 5);
			}
			
			for (int i = 1; i <= 2; i++)//2
			{
				wall(i, 9);
				wall(i, 10);
			}
			for (int i = 5; i <= 14; i++)
			{
				wall(i, 9);
				wall(i, 10);
			}
			for (int i = 17; i <= 19; i++)
			{
				wall(i, 9);
				wall(i, 10);
			}

			for (int i = 1; i <= 8; i++)//3
			{
				wall(i, 14);
				wall(i, 15);
			}
			for (int i = 11; i <= 19; i++)
			{
				wall(i, 14);
				wall(i, 15);
			}

			for (int i = 3; i <= 5; i++)//4
				wall(i, 18);
			for (int i = 18; i <= 20; i++)
				wall(3, i);
			for (int i = 18; i <= 20; i++)
			{
				wall(8, i);
				wall(11, i);
			}
			for (int i = 8; i <= 11; i++)
				wall(i, 19);
			for (int i = 14; i <= 16; i++)
				wall(i, 18);
			for (int i = 18; i <= 20; i++)
				wall(16, i);

			for (int i = 23; i <= 27; i++)//5
			{
				wall(4, i);
				wall(5, i);
				wall(14, i);
				wall(15, i);
			}

			for (int i = 8; i <= 11; i++)
			{
				wall(i, 24);
				wall(i, 25);
				wall(i, 26);
			}
			for(int i = 30; i <=38; i++)
			{
				wall(1, i);
				wall(2, i);
				wall(17, i);
				wall(18, i);
			}
			for (int i = 33; i <= 34; i++)
			{
				wall(3, i);
				wall(16, i);
			}
			for(int i = 1; i <= 2; i++)
            {
				wall(i + 4, 30);
				wall(i + 5, 31);
				wall(i + 6, 32);
				wall(i + 7, 33);
				wall(i + 9, 33);
				wall(i + 12, 30);
				wall(i + 11, 31);
				wall(i + 10, 32);
				wall(i + 5, 36);
				wall(i + 5, 37);
				wall(i + 11, 36);
				wall(i + 11, 37);

			}

				zombieSpawn();                                                                      // set the amout of zombies
			for (int i = 0; i != Values.yAxis; i++) for (int j = 0; j != Values.xAxis; j++)     // set fog for the whole map
					Values.effects[i, j].Image = Values.fogImage;
			clearFog();                                                                         // clear the fog around the player
			Values.board[Values.player.positionY, Values.player.positionX].Image                // places Wizardo onto a new tile
						= Values.wizardoWestImage;
		}

		private void LoadMap2()																	// creator of the second level
		{
			clearMap();
			Values.player.positionX = 1;                                                        // place the player
			Values.player.positionY = 1;
			Values.player.previousPositionX = 1;
			Values.player.previousPositionY = 1;
			
			healPlayer();

			Values.currentLevel = 2;
			Values.zombieCount = 10;
			Values.zombie = new Zombie[Values.zombieCount];                                     // create zombies


			for (int j = 0; j < 39; j++)
			{
				wall(0, j);
				wall(18, j);
				Values.floor[19, j].Image = null;
			}
			for (int i = 0; i < 19; i++)
			{
				wall(i, 0);
				wall(i, 38);
				Values.floor[i, 39].Image = null;
				if (i % 2 == 0) for (int j = 0; j < 39; j += 2)
				{
					wall(i, j);
					wall(i, j);
				}
			}
			Values.floor[19, 39].Image = null;

			zombieSpawn();                                                                      // set the amout of zombies
			for (int i = 0; i != Values.yAxis; i++) for (int j = 0; j != Values.xAxis; j++)     // set fog for the whole map
					Values.effects[i, j].Image = Values.fogImage;
			clearFog();                                                                         // clear the fog around the player
			Values.board[Values.player.positionY, Values.player.positionX].Image                // places Wizardo onto a new tile
						= Values.wizardoEastImage;
		}

		private void LoadMap3()                                                                 // creator of the third level
		{
			clearMap();
			Values.player.positionX = 1;                                                        // place the player
			Values.player.positionY = 1;
			Values.player.previousPositionX = 1;
			Values.player.previousPositionY = 1;
			
			healPlayer();

			Values.currentLevel = 3;
			Values.zombieCount = 15;
			Values.zombie = new Zombie[Values.zombieCount];                                     // create zombies

			// create walls
			for (int j = 0; j <= 39; j++)
			{
				wall(0, j);
				wall(19, j);
			}
			for (int i = 0; i <= 19; i++)
			{
				wall(i, 0);
				wall(i, 39);
			}

			for (int i = 5; i <= 14; i++)
            {
				wall(i, 5);
				wall(i, 6);
				wall(i, 33);
				wall(i, 34);
			}
			for (int i = 1; i <= 2; i++)
			{
				wall(i + 2, 3);
				wall(i + 2, 4);
				wall(i + 2, 7);
				wall(i + 2, 8);
				wall(i + 2, 31);
				wall(i + 2, 32);
				wall(i + 2, 35);
				wall(i + 2, 36);

				wall(i + 14, 3);
				wall(i + 14, 4);
				wall(i + 14, 7);
				wall(i + 14, 8);
				wall(i + 14, 31);
				wall(i + 14, 32);
				wall(i + 14, 35);
				wall(i + 14, 36);
			}

			for(int i = 1; i <= 3; i++)
            {
				wall(7, i + 9);
				wall(9, i + 9);
				wall(10, i + 9);
				wall(12, i + 9);

				wall(7, i + 26);
				wall(9, i + 26);
				wall(10, i + 26);
				wall(12, i + 26);
			}

			for (int i = 1; i <= 5; i++)
            {
				wall(i + 2, 15);
				wall(i + 2, 16);
				wall(i + 2, 17);
				wall(i + 2, 18);
				wall(i + 2, 19);
				wall(i + 2, 20);
				wall(i + 2, 21);
				wall(i + 2, 22);
				wall(i + 2, 23);
				wall(i + 2, 24);
			}

			for(int i = 1; i <= 4; i++)
            {
				wall(i + 11, 15);
				wall(i + 11, 16);
				wall(i + 11, 17);
				wall(i + 11, 18);
				wall(i + 11, 19);
				wall(i + 11, 20);
				wall(i + 11, 21);
				wall(i + 11, 22);
				wall(i + 11, 23);
				wall(i + 11, 24);
			}

			clear(3, 15);
			clear(3, 24);
			clear(15, 15);
			clear(15, 24);
			clear(5, 17);
			clear(5, 22);

			for(int i = 16; i <= 23; i++)
				clear(12, i);

			for (int i = 1; i <= 2; i++)
            {
				clear(i + 6, 15);
				clear(i + 6, 24);

				clear(i + 11, 17);
				clear(i + 11, 19);
				clear(i + 11, 20);
				clear(i + 11, 22);
			}
			wall(8, 17);
			wall(8, 19);
			wall(8, 20);
			wall(8, 22);

			Values.occupiedTile[5, 17] = true;
			Values.occupiedTile[5, 22] = true;

			zombieSpawn();                                                                      // set the amout of zombies
			//for (int i = 0; i != Values.yAxis; i++) for (int j = 0; j != Values.xAxis; j++)     // set fog for the whole map
			//		Values.effects[i, j].Image = Values.fogImage;
			//clearFog();                                                                         // clear the fog around the player
			Values.board[Values.player.positionY, Values.player.positionX].Image                // places Wizardo onto a new tile
						= Values.wizardoEastImage;
		}

		private void LoadMap4()                                                                 // creator of the fourth level
		{
			clearMap();
			Values.player.positionX = 1;                                                        // place the player
			Values.player.positionY = 1;
			Values.player.previousPositionX = 1;
			Values.player.previousPositionY = 1;
			
			healPlayer();

			Values.currentLevel = 4;
			Values.zombieCount = 20;
			Values.zombie = new Zombie[Values.zombieCount];                                     // create zombies

			// create walls
			void room(int i, int j)
			{
				for (int k = 0; k != 2; k++)
				{
					wall(i + k + 2, j + 7);
					wall(i + k, j + 4);
					wall(i + k, j + 5);
				}
				for (int k = 0; k != 3; k++)
				{
					wall(i + k + 2, j);
					wall(i + k + 7, j);
					wall(i + k, j + 3);
					wall(i + k + 7, j + 4);
					wall(i + k + 2, j + 9);
					wall(i + k + 7, j + 8);
					wall(i + k + 7, j + 9);
					wall(i + k + 4, j + 2);
					wall(i + 9, j + k + 5);
				}
				for (int k = 0; k != 4; k++)
				{
					wall(i + k + 2, j + 8);
					wall(i + k + 5, j + 5);
					wall(i, j + k + 6);
				}
				wall(i, j);
				wall(i + 9, j + 3);
				wall(i + 4, j + 3);
			}
			for (int i = 0; i < 2; i++) for (int j = 0; j < 4; j++) room(10 * i, 10 * j);
			zombieSpawn();                                                                      // set the amout of zombies
			for (int i = 0; i != Values.yAxis; i++) for (int j = 0; j != Values.xAxis; j++)     // set fog for the whole map
					Values.effects[i, j].Image = Values.fogImage;
			clearFog();                                                                         // clear the fog around the player
			Values.board[Values.player.positionY, Values.player.positionX].Image                // places Wizardo onto a new tile
						= Values.wizardoEastImage;
		}

		private void LoadMap5()                                                                 // creator of the fifth level
		{
			for (int i = 5; i != Values.yAxis; i++) for (int j = 14; j != Values.xAxis; j++) wall(i, j);
			for (int i = 0; i != 5; i++) for (int j = 16; j != Values.xAxis; j++) wall(i, j);
			for (int i = 1; i != 5; i++) for (int j = 19; j != 34; j++) clear(i, j);
			for (int i = 11; i != 14; i++) for (int j = 16; j != 36; j++) clear(i, j);
			for (int i = 5; i != 11; i++) for (int j = 25; j != 28; j++) clear(i, j);
			for (int i = 0; i != 14; i++) wall(19, i);
			for (int i = 4; i != 12; i++) wall(5, i);
			for (int i = 0; i != 2; i++)
			{
				clear(12, 14 + i);
				clear(6 + i, 14);
				clear(17 + i, 14);
				wall(8 + i, 1);
				wall(15 + i, 1);
				wall(3 + i, 19);
				wall(3 + i, 33);
			}
			for (int i = 0; i != 3; i++)
			{
				wall(11, 6 + i);
				wall(13, 6 + i);
				wall(1, 25 + i);
			}
			for (int i = 0; i != 6; i++)
			{
				wall(6 + i, 7);
				wall(13 + i, 7);
			}
			for (int i = 0; i != 19; i++)
			{
				wall(i, 0);
				wall(0, i);
			}
			for (int i = 18; i < 36; i += 4)
			{
				clear(10, i);
				clear(14, i);
			}
			for (int i = 0; i != 3; i++)
			{
				for (int j = 0; j != 3; j++)
				{
					clear(i + 7, j + 17);
					clear(i + 7, j + 21);
					clear(i + 7, j + 29);
					clear(i + 7, j + 33);
					clear(i + 15, j + 17);
					clear(i + 15, j + 21);
					clear(i + 15, j + 25);
					clear(i + 15, j + 29);
					clear(i + 15, j + 33);
				}
			}
			wall(5, 1);
			wall(12, 1);
			wall(1, 5);
			wall(4, 5);                                                                         // clear the fog around the player
		}

		private void clearMap()																	// clearing the whole map
		{
			for (int y = 0; y < Values.yAxis; y++)
			{
				for (int x = 0; x < Values.xAxis; x++)
				{
					Values.board[y, x].Image = null;
					Values.occupiedTile[y, x] = false;
				}
			}
		}

		private void healPlayer()                                                               // restoring max hp and mana of player
		{
			Values.player.HP = Values.player.maxHP;
			Values.player.mana = Values.player.maxMana;
			visibleHP.Text = "HP:       " + Values.player.HP + " / " + Values.player.maxHP;     // display player's current HP
			visibleMana.Text = "Mana:   " + Values.player.mana + " / " + Values.player.maxMana; // display player's current mana

			if (!Values.player.spell2unlocked) visibleArcaneBolt.Hide();                        // set visibility of spells
			else visibleArcaneBolt.Show();
			if (!Values.player.spell3unlocked) visibleCircleProtection.Hide();
			else visibleCircleProtection.Show();
		}

		private void wall(int y, int x)                                                     // creator of a single wall
		{
			Values.board[y, x].Image = Values.wallImage;                                    // fill the tile with wall image
			Values.occupiedTile[y, x] = true;                                               // make the tile unavailable for the player
		}

		private void clear(int y, int x)                // clear a single tile
		{
			Values.board[y, x].Image = null;            // remove the tile image
			Values.occupiedTile[y, x] = false;          // make the tile available for the player
		}

		private void zombieSpawn()                                      // place a zombie on the map and assign them values
		{
			int xRand, yRand;                                           // random x and y coordinates

			for (int i = 0; i < Values.zombieCount; i++)                // assing position for each zombie on the map
			{
				xRand = new Random().Next(Values.xAxis - 1);            // random x value
				yRand = new Random().Next(Values.yAxis - 1);            // random y value

				if (Values.occupiedTile[yRand, xRand] == false)         // check if the tile is available
				{
					Values.zombie[i] = new Zombie(yRand, xRand);        // place the zombie
					Values.board[Values.zombie[i].positionY, Values.zombie[i].positionX].Image
						= Values.zombieWestImage;
				}
				else i -= 1;                                            // try this loop again
			}
		}

		public void clearFog()
		{
			for (int i = -3; i != 4; i++)
			{
				if (Values.player.positionY + i >= 0 && Values.player.positionY + i <= Values.yAxis - 1)                    // check for a map border
				{
					if (i == -3 || i == 3)
					{
						for (int j = -1; j != 2; j++)
						{
							if (Values.player.positionX + j >= 0 && Values.player.positionX + j <= Values.xAxis - 1)        // check for a map border
							{
								Values.effects[Values.player.positionY + i, Values.player.positionX + j].Image = null;      // clear the fog
							}
						}
					}
					else if (i == -2 || i == 2)
					{
						for (int j = -2; j != 3; j++)
						{
							if (Values.player.positionX + j >= 0 && Values.player.positionX + j <= Values.xAxis - 1)        // check for a map border
							{
								Values.effects[Values.player.positionY + i, Values.player.positionX + j].Image = null;      // clear the fog
							}
						}
					}
					else
					{
						for (int j = -3; j != 4; j++)
						{
							if (Values.player.positionX + j >= 0 && Values.player.positionX + j <= Values.xAxis - 1)        // check for a map border
							{
								Values.effects[Values.player.positionY + i, Values.player.positionX + j].Image = null;      // clear the fog
							}
						}
					}
				}
			}
		}

	}
}