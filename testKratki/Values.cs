using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wizardo
{
	public static class Values                                  // base of the game
	{
		public static int zombieCount;                          // amount of zombies on the map
		public static int xAxis = 40, yAxis = 20;               // size of the map (in tiles)
		public static bool[,] occupiedTile;                     // is something blocking you from moving onto the tile
		public static PictureBox[,] floor;                      // mesh of the floor
		public static PictureBox[,] board;                      // board for zombies and the Magnificent Wizardo himself
		public static PictureBox[,] effects;                    // board for effects 

		public static Player player;                            // statblock for the most potent wizard - the player (Wizardo)
		public static Zombie[] zombie;                          // table of statblocks for zombies

		public static Spell spellOne = new Spell(1, 3, 5);      // values of the first spell (linear)
		public static Spell spellTwo = new Spell(2, 4, 10);     // values of the second spell (linear)
		public static Spell spellThree = new Spell(1, 2, 15);   // values of the third spell (circular)
		public static int currentLevel;                         // current lvl

		public static Image spell1Image = Image.FromFile(@"..\..\..\images\spells\spell1.png");                     // images for spells
		public static Image spell2Image = Image.FromFile(@"..\..\..\images\spells\spell2.png");
		//public static Image spell3Image = Image.FromFile(@"..\..\..\images\spells\spell3.png");
		public static Image wallImage = Image.FromFile(@"..\..\..\images\build\wall.png");                          // images for map elements
		public static Image floorImage = Image.FromFile(@"..\..\..\images\build\floor.png");
		public static Image fogImage = Image.FromFile(@"..\..\..\images\build\fog.png");
		public static Image wizardoNorthImage = Image.FromFile(@"..\..\..\images\wizardo\wizardo-north.png");       // images for Wizardo
		public static Image wizardoEastImage = Image.FromFile(@"..\..\..\images\wizardo\wizardo-east.png");
		public static Image wizardoSouthImage = Image.FromFile(@"..\..\..\images\wizardo\wizardo-south.png");
		public static Image wizardoWestImage = Image.FromFile(@"..\..\..\images\wizardo\wizardo-west.png");
		public static Image zombieNorthImage = Image.FromFile(@"..\..\..\images\zombie\zombie-north.png");          // images for zombies
		public static Image zombieEastImage = Image.FromFile(@"..\..\..\images\zombie\zombie-east.png");
		public static Image zombieSouthImage = Image.FromFile(@"..\..\..\images\zombie\zombie-south.png");
		public static Image zombieWestImage = Image.FromFile(@"..\..\..\images\zombie\zombie-west.png");
	}

}
