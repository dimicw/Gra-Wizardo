
namespace testKratki
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mapBase = new System.Windows.Forms.Panel();
            this.characterSheet = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.visibleArcaneBolt = new System.Windows.Forms.RichTextBox();
            this.visibleMana = new System.Windows.Forms.RichTextBox();
            this.visibleHP = new System.Windows.Forms.RichTextBox();
            this.mainGraphic = new System.Windows.Forms.PictureBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.characterSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // mapBase
            // 
            this.mapBase.Location = new System.Drawing.Point(12, 12);
            this.mapBase.Name = "mapBase";
            this.mapBase.Size = new System.Drawing.Size(1280, 640);
            this.mapBase.TabIndex = 1;
            // 
            // characterSheet
            // 
            this.characterSheet.Controls.Add(this.richTextBox2);
            this.characterSheet.Controls.Add(this.richTextBox1);
            this.characterSheet.Controls.Add(this.visibleArcaneBolt);
            this.characterSheet.Controls.Add(this.visibleMana);
            this.characterSheet.Controls.Add(this.visibleHP);
            this.characterSheet.Controls.Add(this.mainGraphic);
            this.characterSheet.Location = new System.Drawing.Point(1298, 12);
            this.characterSheet.Name = "characterSheet";
            this.characterSheet.Size = new System.Drawing.Size(348, 640);
            this.characterSheet.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(14, 555);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(320, 32);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "(3) Circle of Protection - 15 mana";
            // 
            // visibleArcaneBolt
            // 
            this.visibleArcaneBolt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.visibleArcaneBolt.Enabled = false;
            this.visibleArcaneBolt.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.visibleArcaneBolt.Location = new System.Drawing.Point(14, 517);
            this.visibleArcaneBolt.Name = "visibleArcaneBolt";
            this.visibleArcaneBolt.Size = new System.Drawing.Size(320, 32);
            this.visibleArcaneBolt.TabIndex = 6;
            this.visibleArcaneBolt.Text = "(2) Arcane Bolt - 10 mana";
            // 
            // visibleMana
            // 
            this.visibleMana.BackColor = System.Drawing.Color.WhiteSmoke;
            this.visibleMana.Enabled = false;
            this.visibleMana.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.visibleMana.Location = new System.Drawing.Point(14, 409);
            this.visibleMana.Name = "visibleMana";
            this.visibleMana.Size = new System.Drawing.Size(320, 64);
            this.visibleMana.TabIndex = 5;
            this.visibleMana.Text = "";
            // 
            // visibleHP
            // 
            this.visibleHP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.visibleHP.Enabled = false;
            this.visibleHP.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.visibleHP.Location = new System.Drawing.Point(14, 339);
            this.visibleHP.Name = "visibleHP";
            this.visibleHP.Size = new System.Drawing.Size(320, 64);
            this.visibleHP.TabIndex = 4;
            this.visibleHP.Text = "";
            // 
            // mainGraphic
            // 
            this.mainGraphic.Location = new System.Drawing.Point(14, 13);
            this.mainGraphic.Name = "mainGraphic";
            this.mainGraphic.Size = new System.Drawing.Size(320, 320);
            this.mainGraphic.TabIndex = 0;
            this.mainGraphic.TabStop = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox2.Enabled = false;
            this.richTextBox2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.richTextBox2.Location = new System.Drawing.Point(14, 479);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(320, 32);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "(1) Shocking Grasp - 5 mana";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1658, 663);
            this.Controls.Add(this.characterSheet);
            this.Controls.Add(this.mapBase);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.characterSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainGraphic)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel mapBase;
        private System.Windows.Forms.Panel characterSheet;
        private System.Windows.Forms.PictureBox mainGraphic;
        private System.Windows.Forms.RichTextBox visibleHP;
        private System.Windows.Forms.RichTextBox visibleMana;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox visibleArcaneBolt;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}

