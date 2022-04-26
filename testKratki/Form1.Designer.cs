
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
            this.visibleHP = new System.Windows.Forms.TextBox();
            this.characterSheet = new System.Windows.Forms.Panel();
            this.visibleMana = new System.Windows.Forms.TextBox();
            this.mainGraphic = new System.Windows.Forms.PictureBox();
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
            // visibleHP
            // 
            this.visibleHP.Enabled = false;
            this.visibleHP.Location = new System.Drawing.Point(14, 339);
            this.visibleHP.Name = "visibleHP";
            this.visibleHP.Size = new System.Drawing.Size(320, 23);
            this.visibleHP.TabIndex = 2;
            // 
            // characterSheet
            // 
            this.characterSheet.Controls.Add(this.visibleMana);
            this.characterSheet.Controls.Add(this.visibleHP);
            this.characterSheet.Controls.Add(this.mainGraphic);
            this.characterSheet.Location = new System.Drawing.Point(1298, 12);
            this.characterSheet.Name = "characterSheet";
            this.characterSheet.Size = new System.Drawing.Size(348, 640);
            this.characterSheet.TabIndex = 2;
            // 
            // visibleMana
            // 
            this.visibleMana.Enabled = false;
            this.visibleMana.Location = new System.Drawing.Point(14, 368);
            this.visibleMana.Name = "visibleMana";
            this.visibleMana.Size = new System.Drawing.Size(320, 23);
            this.visibleMana.TabIndex = 3;
            // 
            // mainGraphic
            // 
            this.mainGraphic.Location = new System.Drawing.Point(14, 13);
            this.mainGraphic.Name = "mainGraphic";
            this.mainGraphic.Size = new System.Drawing.Size(320, 320);
            this.mainGraphic.TabIndex = 0;
            this.mainGraphic.TabStop = false;
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
            this.characterSheet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraphic)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel mapBase;
        private System.Windows.Forms.TextBox visibleHP;
        private System.Windows.Forms.Panel characterSheet;
        private System.Windows.Forms.TextBox visibleMana;
        private System.Windows.Forms.PictureBox mainGraphic;
    }
}

