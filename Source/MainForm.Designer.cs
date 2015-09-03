namespace PICO8Tool
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.colorOptions = new System.Windows.Forms.ToolStripComboBox();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblZoom = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.btnExtract = new System.Windows.Forms.ToolStripButton();
			this.btnEmbed = new System.Windows.Forms.ToolStripButton();
			this.btnToggleGrid = new System.Windows.Forms.ToolStripButton();
			this.btnResetView = new System.Windows.Forms.ToolStripButton();
			this.btnAbout = new System.Windows.Forms.ToolStripButton();
			this.btnShowSettings = new System.Windows.Forms.ToolStripButton();
			this.picViewer = new PICO8Tool.PicViewer();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.toolStripSeparator3,
            this.btnExtract,
            this.btnEmbed,
            this.toolStripSeparator2,
            this.btnToggleGrid,
            this.btnResetView,
            this.btnAbout,
            this.btnShowSettings,
            this.toolStripSeparator1,
            this.colorOptions});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(607, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// colorOptions
			// 
			this.colorOptions.AutoSize = false;
			this.colorOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.colorOptions.Items.AddRange(new object[] {
            "don\'t change colors",
            "colors from grayscale",
            "colors from difference"});
			this.colorOptions.Name = "colorOptions";
			this.colorOptions.Size = new System.Drawing.Size(130, 23);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 25);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.picViewer);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer.Panel2Collapsed = true;
			this.splitContainer.Size = new System.Drawing.Size(607, 337);
			this.splitContainer.SplitterDistance = 261;
			this.splitContainer.TabIndex = 1;
			// 
			// propertyGrid
			// 
			this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(96, 100);
			this.propertyGrid.TabIndex = 0;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblZoom});
			this.statusStrip.Location = new System.Drawing.Point(0, 362);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(607, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// lblZoom
			// 
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(35, 17);
			this.lblZoom.Text = "100%";
			// 
			// btnNew
			// 
			this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNew.Image = global::PICO8Tool.Properties.Resources.NewDocument;
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(23, 22);
			this.btnNew.Text = "New";
			this.btnNew.ToolTipText = "Clear Image and Cart";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Image = global::PICO8Tool.Properties.Resources.OpenFolder;
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(56, 22);
			this.btnOpen.Text = "Open";
			this.btnOpen.ToolTipText = "Open Image or P8 cart";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnExtract
			// 
			this.btnExtract.Enabled = false;
			this.btnExtract.Image = global::PICO8Tool.Properties.Resources.Control_PictureBox;
			this.btnExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(62, 22);
			this.btnExtract.Text = "Extract";
			this.btnExtract.ToolTipText = "Extract to PNG";
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// btnEmbed
			// 
			this.btnEmbed.Enabled = false;
			this.btnEmbed.Image = global::PICO8Tool.Properties.Resources.Resize;
			this.btnEmbed.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEmbed.Name = "btnEmbed";
			this.btnEmbed.Size = new System.Drawing.Size(64, 22);
			this.btnEmbed.Text = "Embed";
			this.btnEmbed.ToolTipText = "Embed PNG to cart";
			this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
			// 
			// btnToggleGrid
			// 
			this.btnToggleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnToggleGrid.Image = global::PICO8Tool.Properties.Resources.checker1;
			this.btnToggleGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnToggleGrid.Name = "btnToggleGrid";
			this.btnToggleGrid.Size = new System.Drawing.Size(23, 22);
			this.btnToggleGrid.Text = "Toggle Background Grid";
			this.btnToggleGrid.Click += new System.EventHandler(this.btnToggleGrid_Click);
			// 
			// btnResetView
			// 
			this.btnResetView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnResetView.Image = global::PICO8Tool.Properties.Resources.Zoom;
			this.btnResetView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnResetView.Name = "btnResetView";
			this.btnResetView.Size = new System.Drawing.Size(23, 22);
			this.btnResetView.Text = "Reset Zoom";
			this.btnResetView.Click += new System.EventHandler(this.btnResetView_Click);
			// 
			// btnAbout
			// 
			this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAbout.Image = global::PICO8Tool.Properties.Resources.Information;
			this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(23, 22);
			this.btnAbout.Text = "About";
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// btnShowSettings
			// 
			this.btnShowSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnShowSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnShowSettings.Image = global::PICO8Tool.Properties.Resources.Properties;
			this.btnShowSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnShowSettings.Name = "btnShowSettings";
			this.btnShowSettings.Size = new System.Drawing.Size(23, 22);
			this.btnShowSettings.Text = "Settings";
			this.btnShowSettings.ToolTipText = "Show Settings";
			this.btnShowSettings.Visible = false;
			this.btnShowSettings.Click += new System.EventHandler(this.btnShowSettings_Click);
			// 
			// picViewer
			// 
			this.picViewer.BackColor = System.Drawing.Color.Black;
			this.picViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picViewer.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.picViewer.Location = new System.Drawing.Point(0, 0);
			this.picViewer.Name = "picViewer";
			this.picViewer.Size = new System.Drawing.Size(607, 337);
			this.picViewer.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(607, 384);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(252, 175);
			this.Name = "MainForm";
			this.Text = "PICO8Tool";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripButton btnShowSettings;
		private PicViewer picViewer;
		private System.Windows.Forms.ToolStripButton btnToggleGrid;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblZoom;
		private System.Windows.Forms.ToolStripButton btnResetView;
		private System.Windows.Forms.ToolStripButton btnExtract;
		private System.Windows.Forms.ToolStripButton btnEmbed;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton btnAbout;
		private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox colorOptions;
		private System.Windows.Forms.ToolStripButton btnOpen;
	}
}

