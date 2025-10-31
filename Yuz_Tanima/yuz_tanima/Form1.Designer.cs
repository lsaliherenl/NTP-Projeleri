namespace yuz_tanima
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
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBoxPreview = new PictureBox();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            btnStartCamera = new Button();
            btnSaveFace = new Button();
            btnTrainRecognize = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            flowLayoutPanelButtons.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBoxPreview, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanelButtons, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 420F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel1.Size = new Size(1000, 700);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.Anchor = AnchorStyles.None;
            pictureBoxPreview.Location = new Point(20, 20);
            pictureBoxPreview.Margin = new Padding(20);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(960, 380);
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 0;
            pictureBoxPreview.TabStop = false;
            pictureBoxPreview.Click += pictureBoxPreview_Click;
            // 
            // flowLayoutPanelButtons
            // 
            flowLayoutPanelButtons.Anchor = AnchorStyles.None;
            flowLayoutPanelButtons.AutoSize = true;
            flowLayoutPanelButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelButtons.Controls.Add(btnStartCamera);
            flowLayoutPanelButtons.Controls.Add(btnSaveFace);
            flowLayoutPanelButtons.Controls.Add(btnTrainRecognize);
            flowLayoutPanelButtons.Location = new Point(280, 509);
            flowLayoutPanelButtons.Margin = new Padding(0);
            flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            flowLayoutPanelButtons.Padding = new Padding(10);
            flowLayoutPanelButtons.Size = new Size(440, 80);
            flowLayoutPanelButtons.TabIndex = 1;
            // 
            // btnStartCamera
            // 
            btnStartCamera.Location = new Point(20, 20);
            btnStartCamera.Margin = new Padding(10);
            btnStartCamera.Name = "btnStartCamera";
            btnStartCamera.Size = new Size(120, 40);
            btnStartCamera.TabIndex = 0;
            btnStartCamera.Text = "Kamera Başlat";
            btnStartCamera.UseVisualStyleBackColor = true;
            // 
            // btnSaveFace
            // 
            btnSaveFace.Location = new Point(160, 20);
            btnSaveFace.Margin = new Padding(10);
            btnSaveFace.Name = "btnSaveFace";
            btnSaveFace.Size = new Size(120, 40);
            btnSaveFace.TabIndex = 1;
            btnSaveFace.Text = "Yüz Kaydet";
            btnSaveFace.UseVisualStyleBackColor = true;
            // 
            // btnTrainRecognize
            // 
            btnTrainRecognize.Location = new Point(300, 20);
            btnTrainRecognize.Margin = new Padding(10);
            btnTrainRecognize.Name = "btnTrainRecognize";
            btnTrainRecognize.Size = new Size(120, 40);
            btnTrainRecognize.TabIndex = 2;
            btnTrainRecognize.Text = "Eğit / Tanı";
            btnTrainRecognize.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 674);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1000, 26);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(117, 20);
            toolStripStatusLabel1.Text = "Hazır bekleniyor";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(900, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yüz Tanıma";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            flowLayoutPanelButtons.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button btnStartCamera;
        private System.Windows.Forms.Button btnSaveFace;
        private System.Windows.Forms.Button btnTrainRecognize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
