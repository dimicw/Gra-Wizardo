using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wizardo
{
	public class Player : Creature                          // class only for the player
	{
		public bool spell2unlocked, spell3unlocked;         // true if Wizardo can use those spells
		public int mana, maxMana, manaRegen, maxHP;         // current, regeneration and maximum of magical power, maximum hp

		public Player(int positionY, int positionX)        // function to create a new plyer and assingn custom location
		{
			maxMana = 40;
			maxHP = 40;
			HP = maxHP;
			mana = maxMana;
			manaRegen = 3;

			facing = 0;
			movement = true;
			attack = true;

			spell2unlocked = false;
			spell3unlocked = false;

			this.positionX = positionX;
			this.positionY = positionY;
			previousPositionX = positionX;
			previousPositionY = positionY;
		}

		public Player(int HP, int facing, int mana, int manaRegen,          // function to create a new plyer and assing custom values (new lvl probably)
			int maxMana, int maxHP, bool movement, bool attack, bool spell2unlocked, bool spell3unlocked, int positionX, int positionY)
		{
			this.HP = HP;
			this.mana = mana;
			this.manaRegen = manaRegen;
			this.maxHP = maxHP;
			this.maxMana = maxMana;

			this.facing = facing;
			this.movement = movement;
			this.attack = attack;

			this.spell2unlocked = spell2unlocked;
			this.spell3unlocked = spell3unlocked;

			this.positionX = positionX;
			this.positionY = positionY;
			previousPositionX = positionX;
			previousPositionY = positionY;
		}


		public void Action(char input)                      // change values of the player depending on the key pressed
		{
			if (input == 'w')
			{
				if (this.facing == 0)                       // check if the player is facing north
				{
					if (movement == true && positionY != 0 && !Values.occupiedTile[positionY - 1, positionX])       // check if movement is avaliable and check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionY--;                        // move the player north
						movement = false;                   // mark movement as unavaliable
					}
				}
				else facing = 0;                            // rotate the player north
			}
			else if (input == 'd')
			{
				if (this.facing == 1)                       // check if the player is facing east
				{
					if (movement == true && positionX != Values.xAxis - 1 && !Values.occupiedTile[positionY, positionX + 1])        // check if movement is avaliable and check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionX++;                        // move the player north
						movement = false;                   // mark movement as unavaliable
					}
				}
				else facing = 1;                            // rotate the player north
			}
			else if (input == 's')
			{
				if (this.facing == 2)                       // check if the player is facing south
				{
					if (movement == true && positionY != Values.yAxis - 1 && !Values.occupiedTile[positionY + 1, positionX])        // check if movement is avaliable and check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionY++;                        // move the player south
						movement = false;                   // mark movement as unavaliable
					}
				}
				else facing = 2;                            // rotate the player south
			}
			else if (input == 'a')
			{
				if (this.facing == 3)                       // check if the player is facing west
				{
					if (movement == true && positionX != 0 && !Values.occupiedTile[positionY, positionX - 1])       // check if movement is avaliable and check for obstacles and map borders
					{
						previousPositionY = positionY;      // save current position
						previousPositionX = positionX;
						positionX--;                        // move the player west
						movement = false;                   // mark movement as unavaliable
					}
				}
				else facing = 3;                            // rotate the player west
			}
			Values.occupiedTile[Values.player.previousPositionY, Values.player.previousPositionX] = false;      // make the previous tile available
			Values.occupiedTile[Values.player.positionY, Values.player.positionX] = true;                       // make the previous tile unavailable
		}
	}

}
