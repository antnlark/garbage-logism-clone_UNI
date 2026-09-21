namespace Circuits
{
    partial class Form1
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOr = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonInputSource = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOutputLamp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonEvaluate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStartGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEndGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonInputSource,
            this.toolStripButtonOutputLamp,
            this.toolStripSeparator2,
            this.toolStripButtonAnd,
            this.toolStripButtonOr,
            this.toolStripButtonNot,
            this.toolStripSeparator3,
            this.toolStripButtonCopy,
            this.toolStripSeparator1,
            this.toolStripButtonStartGroup,
            this.toolStripButtonEndGroup,
            this.toolStripSeparator4,
            this.toolStripButtonEvaluate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2016, 42);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAnd
            // 
            this.toolStripButtonAnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAnd.Image = global::Circuits.Properties.Resources.AndIcon;
            this.toolStripButtonAnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnd.Name = "toolStripButtonAnd";
            this.toolStripButtonAnd.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonAnd.Text = "AND";
            this.toolStripButtonAnd.Click += new System.EventHandler(this.toolStripButtonAnd_Click);
            // 
            // toolStripButtonOr
            // 
            this.toolStripButtonOr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOr.Image = global::Circuits.Properties.Resources.OrIcon;
            this.toolStripButtonOr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOr.Name = "toolStripButtonOr";
            this.toolStripButtonOr.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonOr.Text = "OR";
            this.toolStripButtonOr.Click += new System.EventHandler(this.toolStripButtonOr_Click);
            // 
            // toolStripButtonNot
            // 
            this.toolStripButtonNot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNot.Image = global::Circuits.Properties.Resources.NotIcon;
            this.toolStripButtonNot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNot.Name = "toolStripButtonNot";
            this.toolStripButtonNot.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonNot.Text = "NOT";
            this.toolStripButtonNot.Click += new System.EventHandler(this.toolStripButtonNot_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButtonInputSource
            // 
            this.toolStripButtonInputSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInputSource.Image = global::Circuits.Properties.Resources.InputIcon;
            this.toolStripButtonInputSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInputSource.Name = "toolStripButtonInputSource";
            this.toolStripButtonInputSource.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonInputSource.Text = "INPUT";
            this.toolStripButtonInputSource.Click += new System.EventHandler(this.toolStripButtonInputSource_Click);
            // 
            // toolStripButtonOutputLamp
            // 
            this.toolStripButtonOutputLamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOutputLamp.Image = global::Circuits.Properties.Resources.OutputIcon;
            this.toolStripButtonOutputLamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutputLamp.Name = "toolStripButtonOutputLamp";
            this.toolStripButtonOutputLamp.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonOutputLamp.Text = "OUTPUT";
            this.toolStripButtonOutputLamp.Click += new System.EventHandler(this.toolStripButtonOutputLamp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButtonEvaluate
            // 
            this.toolStripButtonEvaluate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEvaluate.Image = global::Circuits.Properties.Resources.EvaluateIcon;
            this.toolStripButtonEvaluate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEvaluate.Name = "toolStripButtonEvaluate";
            this.toolStripButtonEvaluate.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonEvaluate.Text = "EVALUATE";
            this.toolStripButtonEvaluate.Click += new System.EventHandler(this.toolStripButtonEvaluate_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = global::Circuits.Properties.Resources.CopyIcon;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonCopy.Text = "COPY";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButtonStartGroup
            // 
            this.toolStripButtonStartGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStartGroup.Image = global::Circuits.Properties.Resources.StartCompoundIcon;
            this.toolStripButtonStartGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartGroup.Name = "toolStripButtonStartGroup";
            this.toolStripButtonStartGroup.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonStartGroup.Text = "START GROUP";
            this.toolStripButtonStartGroup.Click += new System.EventHandler(this.toolStripButtonStartGroup_Click);
            // 
            // toolStripButtonEndGroup
            // 
            this.toolStripButtonEndGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEndGroup.Image = global::Circuits.Properties.Resources.EndCompoundIcon;
            this.toolStripButtonEndGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEndGroup.Name = "toolStripButtonEndGroup";
            this.toolStripButtonEndGroup.Size = new System.Drawing.Size(46, 36);
            this.toolStripButtonEndGroup.Text = "END GROUP";
            this.toolStripButtonEndGroup.Click += new System.EventHandler(this.toolStripButtonEndGroup_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 42);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(2016, 1402);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circuits 2023";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnd;
        private System.Windows.Forms.ToolStripButton toolStripButtonOr;
        private System.Windows.Forms.ToolStripButton toolStripButtonNot;
        private System.Windows.Forms.ToolStripButton toolStripButtonInputSource;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutputLamp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonEvaluate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonEndGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}

