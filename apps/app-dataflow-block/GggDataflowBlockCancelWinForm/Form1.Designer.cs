namespace GggDataflowBlockCancelWinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddWorkItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressBar3 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressBar4 = new System.Windows.Forms.ToolStripProgressBar();
            this.labelWorkItemStatus = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddWorkItem,
            this.toolStripButtonCancel,
            this.toolStripProgressBar1,
            this.toolStripProgressBar2,
            this.toolStripProgressBar3,
            this.toolStripProgressBar4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAddWorkItem
            // 
            this.toolStripButtonAddWorkItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAddWorkItem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddWorkItem.Image")));
            this.toolStripButtonAddWorkItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddWorkItem.Name = "toolStripButtonAddWorkItem";
            this.toolStripButtonAddWorkItem.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonAddWorkItem.Text = "Add Work Items";
            this.toolStripButtonAddWorkItem.Click += new System.EventHandler(this.toolStripButtonAddWorkItem_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCancel.Enabled = false;
            this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonCancel.Text = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStripProgressBar2
            // 
            this.toolStripProgressBar2.Name = "toolStripProgressBar2";
            this.toolStripProgressBar2.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStripProgressBar3
            // 
            this.toolStripProgressBar3.Name = "toolStripProgressBar3";
            this.toolStripProgressBar3.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStripProgressBar4
            // 
            this.toolStripProgressBar4.Name = "toolStripProgressBar4";
            this.toolStripProgressBar4.Size = new System.Drawing.Size(100, 22);
            // 
            // labelWorkItemStatus
            // 
            this.labelWorkItemStatus.AutoSize = true;
            this.labelWorkItemStatus.Location = new System.Drawing.Point(13, 51);
            this.labelWorkItemStatus.Name = "labelWorkItemStatus";
            this.labelWorkItemStatus.Size = new System.Drawing.Size(92, 13);
            this.labelWorkItemStatus.TabIndex = 1;
            this.labelWorkItemStatus.Text = "Work Item Status:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelWorkItemStatus);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddWorkItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar4;
        private System.Windows.Forms.Label labelWorkItemStatus;
    }
}

