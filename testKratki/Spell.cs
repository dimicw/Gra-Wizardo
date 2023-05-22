using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wizardo
{
	public class Spell                                      // class for spells
	{
		private int range;                                  // range of the effect (in tiles)
		private int dmg;                                    // damage dealt for every zombie affected
		public int manaCost;                                // cost of casting the spell

		public Spell(int range, int dmg, int manaCost)
		{
			this.range = range;
			this.dmg = dmg;
			this.manaCost = manaCost;
		}

		public void useLinearSpell(Image spellImage)
		{
			for (int i = 1; i <= range; i++)
			{
				switch (Values.player.facing)
				{
					case 0:     // north - up - w
						if (Values.player.positionY - i < Values.yAxis && Values.player.positionY - i + 1 > 0)                          // check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY - i, Values.player.positionX])                              // is it occupied
							{
								if (!isZombie(Values.player.positionY - i, Values.player.positionX, dmg)) i = range + 1;                // if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY - i, Values.player.positionX].Image = spellImage;           // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY - i, Values.player.positionX].Image = spellImage;               // visual effect on the free tile
						}
						break;
					case 1:     // east - right - d
						if (Values.player.positionX + i < Values.xAxis && Values.player.positionX + i + 1 > 0)                          // check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY, Values.player.positionX + i])                              // is it occupied
							{
								if (!isZombie(Values.player.positionY, Values.player.positionX + i, dmg)) i = range + 1;                // if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY, Values.player.positionX + i].Image = spellImage;           // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY, Values.player.positionX + i].Image = spellImage;               // visual effect on the free tile
						}
						break;
					case 2:     // south - down - s
						if (Values.player.positionY + i < Values.yAxis && Values.player.positionY + i + 1 > 0)                          // check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY + i, Values.player.positionX])                              // is it occupied
							{
								if (!isZombie(Values.player.positionY + i, Values.player.positionX, dmg)) i = range + 1;                // if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY + i, Values.player.positionX].Image = spellImage;           // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY + i, Values.player.positionX].Image = spellImage;               // visual effect on the free tile
						}
						break;
					case 3:     // west - left - a
						if (Values.player.positionX - i < Values.xAxis && Values.player.positionX - i + 1 > 0)                          // check if the tile exists
						{
							if (Values.occupiedTile[Values.player.positionY, Values.player.positionX - i])                              // is it occupied
							{
								if (!isZombie(Values.player.positionY, Values.player.positionX - i, dmg)) i = range + 1;                // if its a wall - stop the loop, else deal damage
								else Values.effects[Values.player.positionY, Values.player.positionX - i].Image = spellImage;           // visual effect on the zombie
							}
							else Values.effects[Values.player.positionY, Values.player.positionX - i].Image = spellImage;               // visual effect on the free tile
						}
						break;
					default:
						break;
				}
			}
			Values.player.mana -= manaCost;             // pay the spell's cost in mana
		}

		public void useCircularSpell()
		{
			circular(-1, -1);                           // affect each surrounding tile
			circular(-1, 0);
			circular(-1, 1);
			circular(0, -1);
			circular(0, 1);
			circular(1, -1);
			circular(1, 0);
			circular(1, 1);

			Values.player.mana -= manaCost;             // pay the spell's cost in mana

			void circular(int y, int x)
			{
				if (Values.player.positionY + y < Values.yAxis && Values.player.positionY + y + 1 > 0                               // check if the tile exists
					&& Values.player.positionX + x < Values.xAxis && Values.player.positionX + x + 1 > 0)
				{
					if (Values.occupiedTile[Values.player.positionY + y, Values.player.positionX + x])                              // is it occupied
					{
						if (isZombie(Values.player.positionY + y, Values.player.positionX + x, dmg))                                // if its a zombie - make an effect
						{
							Values.effects[Values.player.positionY + y, Values.player.positionX + x].Image = Values.spell1Image;    // visual effect on the zombie
						}
					}
					else Values.effects[Values.player.positionY + y, Values.player.positionX + x].Image = Values.spell1Image;       // visual effect on the free tile
				}
			}
		}

		private bool isZombie(int y, int x, int dmg)                                            // check if the tile is zombie or wall
		{
			for (int j = 0; j < Values.zombieCount; j++)                                        // for each zombie
			{
				if (Values.zombie[j].positionY == y && Values.zombie[j].positionX == x)         // check position
				{
					Values.zombie[j].HP -= dmg;                                                 // deal damage to the zombie on tile
					return true;                                                                // it was a zombie				
				}
			}
			return false;                                                                       // it was a wall
		}
	}

}
