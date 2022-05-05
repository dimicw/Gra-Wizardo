using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testKratki
{
	public partial class LevelUpMessage : Form                  // form for level ups
    {
		public LevelUpMessage()
		{
			Text = "Level Up!";
			Size = new Size(640, 128);

			Label info = new Label();
			Controls.Add(info);
			info.Size = new Size(200, 32);
			info.Location = new Point(220, 10);
			info.Text = "Choose Upgrade!";
			info.Font = new Font(info.Font.FontFamily, 16, info.Font.Style);

			Button buttonMoreHP = addCustomButton("More HP", 128, 32, 21, 50);
			Button buttonManaRegen = addCustomButton("Mana Regeneration", 128, 32, 171, 50);
			Button buttonAddSpell2 = addCustomButton("Arcane Bolt", 128, 32, 321, 50);
			Button buttonAddSpell3 = addCustomButton("Circle of Protection", 128, 32, 471, 50);

			if (Values.player.spell2unlocked) buttonAddSpell2.Enabled = false;

			if (Values.player.spell3unlocked) buttonAddSpell3.Enabled = false;

			buttonMoreHP.Click += new EventHandler(buttonMoreHPClicked);
			buttonManaRegen.Click += new EventHandler(buttonManaRegenClicked);
			buttonAddSpell2.Click += new EventHandler(buttonAddSpell2Clicked);
			buttonAddSpell3.Click += new EventHandler(buttonAddSpell3Clicked);
		}

		private Button addCustomButton(string text, int width, int height, int positionX, int positionY)    // create new button with custom paramenters
		{
			Button button1 = new Button();
			Controls.Add(button1);
			button1.Text = text;
			button1.Size = new Size(width, height);
			button1.Location = new Point(positionX, positionY);

			return button1;
		}

		protected void buttonMoreHPClicked(object sender, EventArgs e)
		{
			Values.player.maxHP += 15;              // increase maximum HP by 15 points
			Close();
		}
		protected void buttonManaRegenClicked(object sender, EventArgs e)
		{
			Values.player.manaRegen += 3;           // increase mana regeneration by 3 points
			Close();
		}
		protected void buttonAddSpell2Clicked(object sender, EventArgs e)
		{
			Values.player.spell2unlocked = true;    // unlock Arcane Bolt
			Close();
		}
		protected void buttonAddSpell3Clicked(object sender, EventArgs e)
		{
			Values.player.spell3unlocked = true;    // unlock Circle of Protection
			Close();
		}


	}

}
