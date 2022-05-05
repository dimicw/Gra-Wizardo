using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testKratki
{
	public class Zombie : Creature                          // class for zombies
	{
		int dmg;                                            // damage dealt with each attack
		public bool alive;                                  // true if it can move and attack

		public Zombie(int positionY, int positionX)     // function to create a new zombie and assingn custom location
		{
			HP = 10;
			dmg = 2;
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

		public void brainless()                                                 // zombie movement and attack
		{
			int xDifference = Values.player.positionX - positionX;              // the difference between the player and the zombie on x axis
			int yDifference = Values.player.positionY - positionY;              // the difference between the player and the zombie on y axis

			if (Math.Abs(xDifference) <= 1 && Math.Abs(yDifference) <= 1)       // check if the zombie is near the player, so it can attack 
			{
				if (Values.player.HP > dmg) Values.player.HP -= dmg;            // check if the hp will be positive after the damage
				else
				{
					Values.player.HP = 0;                                       // change to 0 instead of negative integer
					MessageBox.Show("You Died!");                               // information of the failiure
					Application.Exit();                                         // close the game
				}
			}
			else                                                                // if it can't attack, it moves to the player
			{
				if (xDifference > 0 && (positionX != Values.xAxis - 1 && Values.occupiedTile[positionY, positionX + 1] == false)) move(0, 1);
				else if (xDifference < 0 && (positionX != 0 && Values.occupiedTile[positionY, positionX - 1] == false)) move(0, -1);
				else if (yDifference > 0 && (positionY != Values.yAxis - 1 && Values.occupiedTile[positionY + 1, positionX] == false)) move(1, 0);
				else if (yDifference < 0 && (positionY != 0 && Values.occupiedTile[positionY - 1, positionX] == false)) move(-1, 0);
				else
				{
					switch (new Random().Next(4))                               // if it cant find a direct path to the player, it wonders in random direction
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
			previousPositionX = positionX;                                                          // save current position
			previousPositionY = positionY;

			positionX += x;                                                                         // move to the new tile
			positionY += y;

			Values.board[previousPositionY, previousPositionX].Image = null;                        // clear the previous tile

			if (x > 0) Values.board[positionY, positionX].Image = Values.zombieEastImage;           // place and rotate zombie on the new tile
			else if (x < 0) Values.board[positionY, positionX].Image = Values.zombieWestImage;
			else if (y > 0) Values.board[positionY, positionX].Image = Values.zombieSouthImage;
			else if (y < 0) Values.board[positionY, positionX].Image = Values.zombieNorthImage;

			Values.occupiedTile[previousPositionY, previousPositionX] = false;                      // make the previous tile available
			Values.occupiedTile[positionY, positionX] = true;                                       // make the new tile unavailable

		}
	}
}
