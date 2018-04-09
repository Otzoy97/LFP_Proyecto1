namespace KAPPA_OK
{
	partial class main
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.ArchivoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AbrirMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GuardarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GuardarCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SalirMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtbox = new System.Windows.Forms.RichTextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Gainsboro;
			this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArchivoMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// ArchivoMenuItem
			// 
			this.ArchivoMenuItem.BackColor = System.Drawing.Color.Gainsboro;
			this.ArchivoMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AbrirMenuItem,
            this.GuardarMenuItem,
            this.GuardarCMenuItem,
            this.toolStripSeparator2,
            this.AboutMenuItem,
            this.toolStripSeparator1,
            this.SalirMenuItem});
			this.ArchivoMenuItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ArchivoMenuItem.Name = "ArchivoMenuItem";
			this.ArchivoMenuItem.Size = new System.Drawing.Size(69, 20);
			this.ArchivoMenuItem.Text = "Archivo";
			// 
			// AbrirMenuItem
			// 
			this.AbrirMenuItem.Name = "AbrirMenuItem";
			this.AbrirMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.AbrirMenuItem.Size = new System.Drawing.Size(272, 22);
			this.AbrirMenuItem.Text = "Abrir";
			this.AbrirMenuItem.Click += new System.EventHandler(this.AbrirMenuItem_Click);
			// 
			// GuardarMenuItem
			// 
			this.GuardarMenuItem.Name = "GuardarMenuItem";
			this.GuardarMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.GuardarMenuItem.Size = new System.Drawing.Size(272, 22);
			this.GuardarMenuItem.Text = "Guardar";
			this.GuardarMenuItem.Click += new System.EventHandler(this.GuardarMenuItem_Click);
			// 
			// GuardarCMenuItem
			// 
			this.GuardarCMenuItem.Name = "GuardarCMenuItem";
			this.GuardarCMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.GuardarCMenuItem.Size = new System.Drawing.Size(272, 22);
			this.GuardarCMenuItem.Text = "Guardar como";
			this.GuardarCMenuItem.Click += new System.EventHandler(this.GuardarCMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(269, 6);
			// 
			// AboutMenuItem
			// 
			this.AboutMenuItem.Name = "AboutMenuItem";
			this.AboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.AboutMenuItem.Size = new System.Drawing.Size(272, 22);
			this.AboutMenuItem.Text = "Acerca de";
			this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(269, 6);
			// 
			// SalirMenuItem
			// 
			this.SalirMenuItem.Name = "SalirMenuItem";
			this.SalirMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.SalirMenuItem.Size = new System.Drawing.Size(272, 22);
			this.SalirMenuItem.Text = "Salir";
			this.SalirMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
			// 
			// txtbox
			// 
			this.txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbox.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbox.Location = new System.Drawing.Point(12, 69);
			this.txtbox.MinimumSize = new System.Drawing.Size(400, 150);
			this.txtbox.Name = "txtbox";
			this.txtbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtbox.Size = new System.Drawing.Size(558, 280);
			this.txtbox.TabIndex = 1;
			this.txtbox.Text = "";
			this.txtbox.TextChanged += new System.EventHandler(this.txtbox_TextChanged);
			// 
			// btnStart
			// 
			this.btnStart.BackColor = System.Drawing.Color.OliveDrab;
			this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnStart.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.ForeColor = System.Drawing.Color.LightGray;
			this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
			this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnStart.Location = new System.Drawing.Point(12, 28);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(134, 35);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "¡Analizar!";
			this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.txtbox);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "main";
			this.Text = "Kappa-ΩK - Untitled";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ArchivoMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AbrirMenuItem;
		private System.Windows.Forms.ToolStripMenuItem GuardarMenuItem;
		private System.Windows.Forms.ToolStripMenuItem GuardarCMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem SalirMenuItem;
		private System.Windows.Forms.RichTextBox txtbox;
		private System.Windows.Forms.Button btnStart;
	}
}

