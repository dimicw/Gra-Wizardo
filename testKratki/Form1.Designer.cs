
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
			this.SuspendLayout();
			// 
			// mapBase
			// 
			this.mapBase.Location = new System.Drawing.Point(12, 35);
			this.mapBase.Name = "mapBase";
			this.mapBase.Size = new System.Drawing.Size(800, 400);
			this.mapBase.TabIndex = 1;
			// 
			// visibleHP
			// 
			this.visibleHP.Enabled = false;
			this.visibleHP.Location = new System.Drawing.Point(12, 6);
			this.visibleHP.Name = "visibleHP";
			this.visibleHP.Size = new System.Drawing.Size(100, 23);
			this.visibleHP.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(858, 447);
			this.Controls.Add(this.visibleHP);
			this.Controls.Add(this.mapBase);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel mapBase;
		private System.Windows.Forms.TextBox visibleHP;
	}
}

