using System.Drawing;
using System.Windows.Forms;

namespace form
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
            panelOriginal = new Panel();
            pictureOriginal = new PictureBox();
            panelProcessed = new Panel();
            pictureProcessed = new PictureBox();
            btnLoad = new Button();
            btnGray = new Button();
            btnBlur = new Button();
            btnDownload = new Button();
            btnRotateLeft = new Button();
            btnRotateRight = new Button();
            btnFlipH = new Button();
            btnFlipV = new Button();
            btnReset = new Button();
            btnUndo = new Button();
            btnRedo = new Button();
            btnSepia = new Button();
            btnSharpen = new Button();
            panelOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureOriginal).BeginInit();
            panelProcessed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureProcessed).BeginInit();
            SuspendLayout();
            // 
            // panelOriginal
            // 
            panelOriginal.AutoScroll = true;
            panelOriginal.BorderStyle = BorderStyle.FixedSingle;
            panelOriginal.Controls.Add(pictureOriginal);
            panelOriginal.Location = new Point(12, 48);
            panelOriginal.Name = "panelOriginal";
            panelOriginal.Size = new Size(462, 404);
            panelOriginal.TabIndex = 0;
            // 
            // pictureOriginal
            // 
            pictureOriginal.BorderStyle = BorderStyle.FixedSingle;
            pictureOriginal.Location = new Point(0, 0);
            pictureOriginal.Name = "pictureOriginal";
            pictureOriginal.Size = new Size(460, 402);
            pictureOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureOriginal.TabIndex = 0;
            pictureOriginal.TabStop = false;
            pictureOriginal.Click += pictureBox1_Click;
            pictureOriginal.MouseEnter += pictureBox_MouseEnter;
            pictureOriginal.MouseWheel += pictureOriginal_MouseWheel;
            // 
            // panelProcessed
            // 
            panelProcessed.AutoScroll = true;
            panelProcessed.BorderStyle = BorderStyle.FixedSingle;
            panelProcessed.Controls.Add(pictureProcessed);
            panelProcessed.Location = new Point(480, 48);
            panelProcessed.Name = "panelProcessed";
            panelProcessed.Size = new Size(460, 404);
            panelProcessed.TabIndex = 1;
            // 
            // pictureProcessed
            // 
            pictureProcessed.BorderStyle = BorderStyle.FixedSingle;
            pictureProcessed.Location = new Point(0, 0);
            pictureProcessed.Name = "pictureProcessed";
            pictureProcessed.Size = new Size(458, 402);
            pictureProcessed.SizeMode = PictureBoxSizeMode.Zoom;
            pictureProcessed.TabIndex = 0;
            pictureProcessed.TabStop = false;
            pictureProcessed.Click += pictureBox2_Click;
            pictureProcessed.MouseEnter += pictureBox_MouseEnter;
            pictureProcessed.MouseWheel += pictureProcessed_MouseWheel;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(12, 486);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(103, 36);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Görsel Yükle";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnGray
            // 
            btnGray.Location = new Point(233, 485);
            btnGray.Name = "btnGray";
            btnGray.Size = new Size(89, 37);
            btnGray.TabIndex = 3;
            btnGray.Text = "Gray";
            btnGray.UseVisualStyleBackColor = true;
            btnGray.Click += btnGray_Click;
            // 
            // btnBlur
            // 
            btnBlur.Location = new Point(138, 485);
            btnBlur.Name = "btnBlur";
            btnBlur.Size = new Size(89, 35);
            btnBlur.TabIndex = 4;
            btnBlur.Text = "Blur";
            btnBlur.UseVisualStyleBackColor = true;
            btnBlur.Click += btnBlur_Click;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(834, 486);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(106, 36);
            btnDownload.TabIndex = 12;
            btnDownload.Text = "İndir";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnRotateLeft
            // 
            btnRotateLeft.Location = new Point(342, 485);
            btnRotateLeft.Name = "btnRotateLeft";
            btnRotateLeft.Size = new Size(89, 36);
            btnRotateLeft.TabIndex = 5;
            btnRotateLeft.Text = "Sol 90°";
            btnRotateLeft.UseVisualStyleBackColor = true;
            btnRotateLeft.Click += btnRotateLeft_Click;
            // 
            // btnRotateRight
            // 
            btnRotateRight.Location = new Point(437, 485);
            btnRotateRight.Name = "btnRotateRight";
            btnRotateRight.Size = new Size(93, 37);
            btnRotateRight.TabIndex = 6;
            btnRotateRight.Text = "Sağ 90°";
            btnRotateRight.UseVisualStyleBackColor = true;
            btnRotateRight.Click += btnRotateRight_Click;
            // 
            // btnFlipH
            // 
            btnFlipH.Location = new Point(342, 527);
            btnFlipH.Name = "btnFlipH";
            btnFlipH.Size = new Size(89, 37);
            btnFlipH.TabIndex = 7;
            btnFlipH.Text = "Ayna Yatay";
            btnFlipH.UseVisualStyleBackColor = true;
            btnFlipH.Click += btnFlipH_Click;
            // 
            // btnFlipV
            // 
            btnFlipV.Location = new Point(437, 526);
            btnFlipV.Name = "btnFlipV";
            btnFlipV.Size = new Size(93, 37);
            btnFlipV.TabIndex = 8;
            btnFlipV.Text = "Ayna Dikey";
            btnFlipV.UseVisualStyleBackColor = true;
            btnFlipV.Click += btnFlipV_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(596, 526);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(116, 36);
            btnReset.TabIndex = 9;
            btnReset.Text = "Orijinale Dön";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(557, 484);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(93, 37);
            btnUndo.TabIndex = 10;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.Location = new Point(656, 486);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(93, 36);
            btnRedo.TabIndex = 11;
            btnRedo.Text = "Redo";
            btnRedo.UseVisualStyleBackColor = true;
            btnRedo.Click += btnRedo_Click;
            // 
            // btnSepia
            // 
            btnSepia.Location = new Point(138, 527);
            btnSepia.Name = "btnSepia";
            btnSepia.Size = new Size(89, 37);
            btnSepia.TabIndex = 13;
            btnSepia.Text = "Sepya";
            btnSepia.UseVisualStyleBackColor = true;
            btnSepia.Click += btnSepia_Click;
            // 
            // btnSharpen
            // 
            btnSharpen.Location = new Point(233, 528);
            btnSharpen.Name = "btnSharpen";
            btnSharpen.Size = new Size(89, 36);
            btnSharpen.TabIndex = 14;
            btnSharpen.Text = "Keskinleştir";
            btnSharpen.UseVisualStyleBackColor = true;
            btnSharpen.Click += btnSharpen_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 575);
            Controls.Add(btnSharpen);
            Controls.Add(btnSepia);
            Controls.Add(btnRedo);
            Controls.Add(btnUndo);
            Controls.Add(btnReset);
            Controls.Add(btnFlipV);
            Controls.Add(btnFlipH);
            Controls.Add(btnRotateRight);
            Controls.Add(btnRotateLeft);
            Controls.Add(btnDownload);
            Controls.Add(btnBlur);
            Controls.Add(btnGray);
            Controls.Add(btnLoad);
            Controls.Add(panelProcessed);
            Controls.Add(panelOriginal);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureOriginal).EndInit();
            panelProcessed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureProcessed).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureOriginal;
        private PictureBox pictureProcessed;
        private Button btnLoad;
        private Button btnGray;
        private Button btnBlur;
        private Button btnDownload;
        private Button btnRotateLeft;
        private Button btnRotateRight;
        private Button btnFlipH;
        private Button btnFlipV;
        private Button btnReset;
        private Button btnUndo;
        private Button btnRedo;
        private Button btnSepia;
        private Button btnSharpen;
        private Panel panelOriginal;
        private Panel panelProcessed;
    }
}
