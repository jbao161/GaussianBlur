﻿namespace GaussianBlur
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.panel_bothimages = new System.Windows.Forms.Panel();
            this.splitContainer_bothimages = new System.Windows.Forms.SplitContainer();
            this.pictureBox_leftpanel = new System.Windows.Forms.PictureBox();
            this.pictureBox_rightpanel = new System.Windows.Forms.PictureBox();
            this.panel_controls = new System.Windows.Forms.Panel();
            this.button_blur = new System.Windows.Forms.Button();
            this.button_swapimages = new System.Windows.Forms.Button();
            this.button_verticalhorizontal = new System.Windows.Forms.Button();
            this.button_releaseleft = new System.Windows.Forms.Button();
            this.button_releaseright = new System.Windows.Forms.Button();
            this.button_preview = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenu_image = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenu_image_item_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_image_item_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_boxblur = new System.Windows.Forms.Button();
            this.panel_bothimages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bothimages)).BeginInit();
            this.splitContainer_bothimages.Panel1.SuspendLayout();
            this.splitContainer_bothimages.Panel2.SuspendLayout();
            this.splitContainer_bothimages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_leftpanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rightpanel)).BeginInit();
            this.panel_controls.SuspendLayout();
            this.contextMenu_image.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_bothimages
            // 
            this.panel_bothimages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_bothimages.Controls.Add(this.splitContainer_bothimages);
            this.panel_bothimages.Location = new System.Drawing.Point(0, 0);
            this.panel_bothimages.Name = "panel_bothimages";
            this.panel_bothimages.Size = new System.Drawing.Size(884, 518);
            this.panel_bothimages.TabIndex = 0;
            // 
            // splitContainer_bothimages
            // 
            this.splitContainer_bothimages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_bothimages.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_bothimages.Name = "splitContainer_bothimages";
            // 
            // splitContainer_bothimages.Panel1
            // 
            this.splitContainer_bothimages.Panel1.Controls.Add(this.pictureBox_leftpanel);
            // 
            // splitContainer_bothimages.Panel2
            // 
            this.splitContainer_bothimages.Panel2.Controls.Add(this.pictureBox_rightpanel);
            this.splitContainer_bothimages.Size = new System.Drawing.Size(884, 518);
            this.splitContainer_bothimages.SplitterDistance = 311;
            this.splitContainer_bothimages.SplitterWidth = 10;
            this.splitContainer_bothimages.TabIndex = 0;
            // 
            // pictureBox_leftpanel
            // 
            this.pictureBox_leftpanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBox_leftpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_leftpanel.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_leftpanel.Name = "pictureBox_leftpanel";
            this.pictureBox_leftpanel.Size = new System.Drawing.Size(311, 518);
            this.pictureBox_leftpanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_leftpanel.TabIndex = 0;
            this.pictureBox_leftpanel.TabStop = false;
            this.pictureBox_leftpanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
            this.pictureBox_leftpanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
            this.pictureBox_leftpanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick_copypastemenu);
            // 
            // pictureBox_rightpanel
            // 
            this.pictureBox_rightpanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBox_rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_rightpanel.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_rightpanel.Name = "pictureBox_rightpanel";
            this.pictureBox_rightpanel.Size = new System.Drawing.Size(563, 518);
            this.pictureBox_rightpanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rightpanel.TabIndex = 0;
            this.pictureBox_rightpanel.TabStop = false;
            this.pictureBox_rightpanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
            this.pictureBox_rightpanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
            this.pictureBox_rightpanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick_copypastemenu);
            // 
            // panel_controls
            // 
            this.panel_controls.Controls.Add(this.button_boxblur);
            this.panel_controls.Controls.Add(this.button_blur);
            this.panel_controls.Controls.Add(this.button_swapimages);
            this.panel_controls.Controls.Add(this.button_verticalhorizontal);
            this.panel_controls.Controls.Add(this.button_releaseleft);
            this.panel_controls.Controls.Add(this.button_releaseright);
            this.panel_controls.Controls.Add(this.button_preview);
            this.panel_controls.Controls.Add(this.button_save);
            this.panel_controls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_controls.Location = new System.Drawing.Point(0, 524);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(884, 122);
            this.panel_controls.TabIndex = 1;
            // 
            // button_blur
            // 
            this.button_blur.Location = new System.Drawing.Point(81, 0);
            this.button_blur.Name = "button_blur";
            this.button_blur.Size = new System.Drawing.Size(57, 55);
            this.button_blur.TabIndex = 1;
            this.button_blur.Text = "Blur";
            this.button_blur.UseVisualStyleBackColor = true;
            this.button_blur.Click += new System.EventHandler(this.button_blur_Click);
            // 
            // button_swapimages
            // 
            this.button_swapimages.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_swapimages.Location = new System.Drawing.Point(240, 0);
            this.button_swapimages.Name = "button_swapimages";
            this.button_swapimages.Size = new System.Drawing.Size(136, 122);
            this.button_swapimages.TabIndex = 6;
            this.button_swapimages.Text = "Swap Images";
            this.button_swapimages.UseVisualStyleBackColor = true;
            this.button_swapimages.Click += new System.EventHandler(this.button_swapimages_Click);
            // 
            // button_verticalhorizontal
            // 
            this.button_verticalhorizontal.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_verticalhorizontal.Location = new System.Drawing.Point(376, 0);
            this.button_verticalhorizontal.Name = "button_verticalhorizontal";
            this.button_verticalhorizontal.Size = new System.Drawing.Size(133, 122);
            this.button_verticalhorizontal.TabIndex = 5;
            this.button_verticalhorizontal.Text = "Stack images vertically";
            this.button_verticalhorizontal.UseVisualStyleBackColor = true;
            this.button_verticalhorizontal.Click += new System.EventHandler(this.button_verticalhorizontal_Click);
            // 
            // button_releaseleft
            // 
            this.button_releaseleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_releaseleft.Location = new System.Drawing.Point(0, 0);
            this.button_releaseleft.Name = "button_releaseleft";
            this.button_releaseleft.Size = new System.Drawing.Size(75, 122);
            this.button_releaseleft.TabIndex = 4;
            this.button_releaseleft.Text = "<- Clear Image";
            this.button_releaseleft.UseVisualStyleBackColor = true;
            this.button_releaseleft.Click += new System.EventHandler(this.button_releaseleft_Click);
            // 
            // button_releaseright
            // 
            this.button_releaseright.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_releaseright.Location = new System.Drawing.Point(509, 0);
            this.button_releaseright.Name = "button_releaseright";
            this.button_releaseright.Size = new System.Drawing.Size(66, 122);
            this.button_releaseright.TabIndex = 3;
            this.button_releaseright.Text = "Clear Image ->";
            this.button_releaseright.UseVisualStyleBackColor = true;
            this.button_releaseright.Click += new System.EventHandler(this.button_releaseright_Click);
            // 
            // button_preview
            // 
            this.button_preview.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_preview.Location = new System.Drawing.Point(575, 0);
            this.button_preview.Name = "button_preview";
            this.button_preview.Size = new System.Drawing.Size(159, 122);
            this.button_preview.TabIndex = 2;
            this.button_preview.Text = "Preview";
            this.button_preview.UseVisualStyleBackColor = true;
            this.button_preview.Click += new System.EventHandler(this.button_preview_Click);
            // 
            // button_save
            // 
            this.button_save.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_save.Location = new System.Drawing.Point(734, 0);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(150, 122);
            this.button_save.TabIndex = 0;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // contextMenu_image
            // 
            this.contextMenu_image.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenu_image_item_copy,
            this.contextMenu_image_item_paste,
            this.clearToolStripMenuItem});
            this.contextMenu_image.Name = "contextMenuStrip1";
            this.contextMenu_image.Size = new System.Drawing.Size(103, 70);
            // 
            // contextMenu_image_item_copy
            // 
            this.contextMenu_image_item_copy.Name = "contextMenu_image_item_copy";
            this.contextMenu_image_item_copy.Size = new System.Drawing.Size(102, 22);
            this.contextMenu_image_item_copy.Text = "Copy";
            this.contextMenu_image_item_copy.Click += new System.EventHandler(this.contextMenu_image_item_copy_Click);
            // 
            // contextMenu_image_item_paste
            // 
            this.contextMenu_image_item_paste.Name = "contextMenu_image_item_paste";
            this.contextMenu_image_item_paste.Size = new System.Drawing.Size(102, 22);
            this.contextMenu_image_item_paste.Text = "Paste";
            this.contextMenu_image_item_paste.Click += new System.EventHandler(this.contextMenu_image_item_paste_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // button_boxblur
            // 
            this.button_boxblur.Location = new System.Drawing.Point(144, 0);
            this.button_boxblur.Name = "button_boxblur";
            this.button_boxblur.Size = new System.Drawing.Size(56, 55);
            this.button_boxblur.TabIndex = 1;
            this.button_boxblur.Text = "Box Blur";
            this.button_boxblur.UseVisualStyleBackColor = true;
            this.button_boxblur.Click += new System.EventHandler(this.button_boxblur_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 646);
            this.Controls.Add(this.panel_controls);
            this.Controls.Add(this.panel_bothimages);
            this.MinimumSize = new System.Drawing.Size(470, 450);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Image Stitcher";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel_bothimages.ResumeLayout(false);
            this.splitContainer_bothimages.Panel1.ResumeLayout(false);
            this.splitContainer_bothimages.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bothimages)).EndInit();
            this.splitContainer_bothimages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_leftpanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rightpanel)).EndInit();
            this.panel_controls.ResumeLayout(false);
            this.contextMenu_image.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_bothimages;
        private System.Windows.Forms.Panel panel_controls;
        private System.Windows.Forms.SplitContainer splitContainer_bothimages;
        private System.Windows.Forms.PictureBox pictureBox_leftpanel;
        private System.Windows.Forms.PictureBox pictureBox_rightpanel;
        private System.Windows.Forms.Button button_preview;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button_releaseright;
        private System.Windows.Forms.Button button_releaseleft;
        private System.Windows.Forms.ContextMenuStrip contextMenu_image;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_image_item_copy;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_image_item_paste;
        private System.Windows.Forms.Button button_verticalhorizontal;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Button button_swapimages;
        private System.Windows.Forms.Button button_blur;
        private System.Windows.Forms.Button button_boxblur;
    }
}

