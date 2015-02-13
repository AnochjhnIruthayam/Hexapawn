namespace HexapawnGUI
{
    partial class HexapawnGUI
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
            this.labelPlayer = new System.Windows.Forms.Label();
            this.btnAttackRight = new System.Windows.Forms.Button();
            this.btnAttackLeft = new System.Windows.Forms.Button();
            this.labalStatus = new System.Windows.Forms.Label();
            this.btnMoveStraight = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbBruteforce = new System.Windows.Forms.RadioButton();
            this.rbMiniMax = new System.Windows.Forms.RadioButton();
            this.rbQLearning = new System.Windows.Forms.RadioButton();
            this.btnQTrain = new System.Windows.Forms.Button();
            this.labelTrainQ = new System.Windows.Forms.Label();
            this.Qprogress = new System.Windows.Forms.ProgressBar();
            this.radioButton1Q = new System.Windows.Forms.RadioButton();
            this.radioButton2M = new System.Windows.Forms.RadioButton();
            this.radioButton3B = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer
            // 
            this.labelPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer.AutoSize = true;
            this.labelPlayer.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelPlayer.Location = new System.Drawing.Point(555, 198);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(45, 13);
            this.labelPlayer.TabIndex = 18;
            this.labelPlayer.Text = "Player 1";
            this.labelPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAttackRight
            // 
            this.btnAttackRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttackRight.Location = new System.Drawing.Point(555, 383);
            this.btnAttackRight.Name = "btnAttackRight";
            this.btnAttackRight.Size = new System.Drawing.Size(86, 23);
            this.btnAttackRight.TabIndex = 17;
            this.btnAttackRight.Text = "Attack Right";
            this.btnAttackRight.UseVisualStyleBackColor = true;
            this.btnAttackRight.Click += new System.EventHandler(this.btnAttackRight_Click);
            // 
            // btnAttackLeft
            // 
            this.btnAttackLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttackLeft.Location = new System.Drawing.Point(555, 353);
            this.btnAttackLeft.Name = "btnAttackLeft";
            this.btnAttackLeft.Size = new System.Drawing.Size(86, 23);
            this.btnAttackLeft.TabIndex = 16;
            this.btnAttackLeft.Text = "Attack Left";
            this.btnAttackLeft.UseVisualStyleBackColor = true;
            this.btnAttackLeft.Click += new System.EventHandler(this.btnAttackLeft_Click);
            // 
            // labalStatus
            // 
            this.labalStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labalStatus.AutoSize = true;
            this.labalStatus.Location = new System.Drawing.Point(552, 229);
            this.labalStatus.Name = "labalStatus";
            this.labalStatus.Size = new System.Drawing.Size(36, 13);
            this.labalStatus.TabIndex = 15;
            this.labalStatus.Text = "All OK";
            this.labalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMoveStraight
            // 
            this.btnMoveStraight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveStraight.Location = new System.Drawing.Point(555, 323);
            this.btnMoveStraight.Name = "btnMoveStraight";
            this.btnMoveStraight.Size = new System.Drawing.Size(86, 23);
            this.btnMoveStraight.TabIndex = 14;
            this.btnMoveStraight.Text = "Move Straight";
            this.btnMoveStraight.UseVisualStyleBackColor = true;
            this.btnMoveStraight.Click += new System.EventHandler(this.btnMoveStraight_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(555, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Player 2";
            // 
            // rbBruteforce
            // 
            this.rbBruteforce.AutoSize = true;
            this.rbBruteforce.Location = new System.Drawing.Point(558, 70);
            this.rbBruteforce.Name = "rbBruteforce";
            this.rbBruteforce.Size = new System.Drawing.Size(74, 17);
            this.rbBruteforce.TabIndex = 20;
            this.rbBruteforce.Text = "Bruteforce";
            this.rbBruteforce.UseVisualStyleBackColor = true;
            this.rbBruteforce.CheckedChanged += new System.EventHandler(this.rbBruteforce_CheckedChanged);
            // 
            // rbMiniMax
            // 
            this.rbMiniMax.AutoSize = true;
            this.rbMiniMax.Location = new System.Drawing.Point(558, 94);
            this.rbMiniMax.Name = "rbMiniMax";
            this.rbMiniMax.Size = new System.Drawing.Size(64, 17);
            this.rbMiniMax.TabIndex = 21;
            this.rbMiniMax.Text = "MiniMax";
            this.rbMiniMax.UseVisualStyleBackColor = true;
            this.rbMiniMax.CheckedChanged += new System.EventHandler(this.rbMiniMax_CheckedChanged);
            // 
            // rbQLearning
            // 
            this.rbQLearning.AutoSize = true;
            this.rbQLearning.Location = new System.Drawing.Point(558, 118);
            this.rbQLearning.Name = "rbQLearning";
            this.rbQLearning.Size = new System.Drawing.Size(77, 17);
            this.rbQLearning.TabIndex = 22;
            this.rbQLearning.Text = "Q Learning";
            this.rbQLearning.UseVisualStyleBackColor = true;
            this.rbQLearning.CheckedChanged += new System.EventHandler(this.rbQLearning_CheckedChanged);
            // 
            // btnQTrain
            // 
            this.btnQTrain.Location = new System.Drawing.Point(525, 162);
            this.btnQTrain.Name = "btnQTrain";
            this.btnQTrain.Size = new System.Drawing.Size(75, 23);
            this.btnQTrain.TabIndex = 23;
            this.btnQTrain.Text = "Train Q";
            this.btnQTrain.UseVisualStyleBackColor = true;
            this.btnQTrain.Click += new System.EventHandler(this.btnQTrain_Click);
            // 
            // labelTrainQ
            // 
            this.labelTrainQ.AutoSize = true;
            this.labelTrainQ.Location = new System.Drawing.Point(529, 256);
            this.labelTrainQ.Name = "labelTrainQ";
            this.labelTrainQ.Size = new System.Drawing.Size(89, 13);
            this.labelTrainQ.TabIndex = 24;
            this.labelTrainQ.Text = "Q Training Status";
            // 
            // Qprogress
            // 
            this.Qprogress.Location = new System.Drawing.Point(532, 272);
            this.Qprogress.Maximum = 10000;
            this.Qprogress.Name = "Qprogress";
            this.Qprogress.Size = new System.Drawing.Size(100, 23);
            this.Qprogress.TabIndex = 25;
            // 
            // radioButton1Q
            // 
            this.radioButton1Q.AutoSize = true;
            this.radioButton1Q.Location = new System.Drawing.Point(473, 118);
            this.radioButton1Q.Name = "radioButton1Q";
            this.radioButton1Q.Size = new System.Drawing.Size(77, 17);
            this.radioButton1Q.TabIndex = 29;
            this.radioButton1Q.Text = "Q Learning";
            this.radioButton1Q.UseVisualStyleBackColor = true;
            this.radioButton1Q.CheckedChanged += new System.EventHandler(this.radioButton1Q_CheckedChanged);
            // 
            // radioButton2M
            // 
            this.radioButton2M.AutoSize = true;
            this.radioButton2M.Location = new System.Drawing.Point(473, 94);
            this.radioButton2M.Name = "radioButton2M";
            this.radioButton2M.Size = new System.Drawing.Size(64, 17);
            this.radioButton2M.TabIndex = 28;
            this.radioButton2M.Text = "MiniMax";
            this.radioButton2M.UseVisualStyleBackColor = true;
            this.radioButton2M.CheckedChanged += new System.EventHandler(this.radioButton2M_CheckedChanged);
            // 
            // radioButton3B
            // 
            this.radioButton3B.AutoSize = true;
            this.radioButton3B.Checked = true;
            this.radioButton3B.Location = new System.Drawing.Point(473, 70);
            this.radioButton3B.Name = "radioButton3B";
            this.radioButton3B.Size = new System.Drawing.Size(74, 17);
            this.radioButton3B.TabIndex = 27;
            this.radioButton3B.Text = "Bruteforce";
            this.radioButton3B.UseVisualStyleBackColor = true;
            this.radioButton3B.CheckedChanged += new System.EventHandler(this.radioButton3B_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(470, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Player 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(511, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Select AI";
            // 
            // HexapawnGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 515);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton1Q);
            this.Controls.Add(this.radioButton2M);
            this.Controls.Add(this.radioButton3B);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Qprogress);
            this.Controls.Add(this.labelTrainQ);
            this.Controls.Add(this.btnQTrain);
            this.Controls.Add(this.rbQLearning);
            this.Controls.Add(this.rbMiniMax);
            this.Controls.Add(this.rbBruteforce);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.btnAttackRight);
            this.Controls.Add(this.btnAttackLeft);
            this.Controls.Add(this.labalStatus);
            this.Controls.Add(this.btnMoveStraight);
            this.Name = "HexapawnGUI";
            this.Text = "Hexapawn Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Button btnAttackRight;
        private System.Windows.Forms.Button btnAttackLeft;
        private System.Windows.Forms.Label labalStatus;
        private System.Windows.Forms.Button btnMoveStraight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbBruteforce;
        private System.Windows.Forms.RadioButton rbMiniMax;
        private System.Windows.Forms.RadioButton rbQLearning;
        private System.Windows.Forms.Button btnQTrain;
        private System.Windows.Forms.Label labelTrainQ;
        private System.Windows.Forms.ProgressBar Qprogress;
        private System.Windows.Forms.RadioButton radioButton1Q;
        private System.Windows.Forms.RadioButton radioButton2M;
        private System.Windows.Forms.RadioButton radioButton3B;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

