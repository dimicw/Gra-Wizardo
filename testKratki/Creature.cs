using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testKratki
{
	public class Creature                                   // base class for player and zombies
	{
		public int HP;                                      // current hit poionts left

		public int facing;                                  // 0-north, 1-east, 2-south, 3-west
		public bool movement, attack;                       // true if currently able to do so, false if not

		public int positionX, positionY,                    // player's X and Y coordinates	
			previousPositionX, previousPositionY;           // player's previous X and Y coordinates
	}
}
