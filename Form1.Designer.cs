namespace Launcher
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LaunchButton = new System.Windows.Forms.Button();
            this.UpdatePart = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.UpdateTotal = new System.Windows.Forms.Label();
            this.UpdatePartProgress = new Launcher.CustomProgressBar();
            this.UpdateTotalProgress = new Launcher.CustomProgressBar();
            this.SuspendLayout();
            // 
            // LaunchButton
            // 
            this.LaunchButton.BackColor = System.Drawing.Color.Transparent;
            this.LaunchButton.BackgroundImage = global::Launcher.Properties.Resources.GameStartDisabled_btn;
            this.LaunchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LaunchButton.Enabled = false;
            this.LaunchButton.FlatAppearance.BorderSize = 0;
            this.LaunchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaunchButton.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.LaunchButton.Location = new System.Drawing.Point(521, 500);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(154, 36);
            this.LaunchButton.TabIndex = 1;
            this.LaunchButton.UseVisualStyleBackColor = false;
            this.LaunchButton.Click += new System.EventHandler(this.Launch);
            // 
            // UpdatePart
            // 
            this.UpdatePart.AutoSize = true;
            this.UpdatePart.BackColor = System.Drawing.Color.Transparent;
            this.UpdatePart.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.UpdatePart.ForeColor = System.Drawing.Color.White;
            this.UpdatePart.Location = new System.Drawing.Point(59, 499);
            this.UpdatePart.Name = "UpdatePart";
            this.UpdatePart.Size = new System.Drawing.Size(55, 15);
            this.UpdatePart.TabIndex = 4;
            this.UpdatePart.Text = "Verifying";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.button1.BackgroundImage = global::Launcher.Properties.Resources.Exit_btn;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.button1.Location = new System.Drawing.Point(662, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(17, 16);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.button2.BackgroundImage = global::Launcher.Properties.Resources.Minimize_btn;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.button2.Location = new System.Drawing.Point(642, 13);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(17, 16);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UpdateTotal
            // 
            this.UpdateTotal.AutoSize = true;
            this.UpdateTotal.BackColor = System.Drawing.Color.Transparent;
            this.UpdateTotal.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.UpdateTotal.ForeColor = System.Drawing.Color.White;
            this.UpdateTotal.Location = new System.Drawing.Point(138, 430);
            this.UpdateTotal.Name = "UpdateTotal";
            this.UpdateTotal.Size = new System.Drawing.Size(143, 19);
            this.UpdateTotal.TabIndex = 10;
            this.UpdateTotal.Text = "파일을 검사중입니다.";
            // 
            // UpdatePartProgress
            // 
            this.UpdatePartProgress.BackColor = System.Drawing.Color.Transparent;
            this.UpdatePartProgress.Location = new System.Drawing.Point(62, 524);
            this.UpdatePartProgress.Name = "UpdatePartProgress";
            this.UpdatePartProgress.Size = new System.Drawing.Size(316, 6);
            this.UpdatePartProgress.TabIndex = 12;
            this.UpdatePartProgress.Value = 0;
            // 
            // UpdateTotalProgress
            // 
            this.UpdateTotalProgress.BackColor = System.Drawing.Color.Transparent;
            this.UpdateTotalProgress.Location = new System.Drawing.Point(62, 464);
            this.UpdateTotalProgress.Name = "UpdateTotalProgress";
            this.UpdateTotalProgress.Size = new System.Drawing.Size(552, 8);
            this.UpdateTotalProgress.TabIndex = 11;
            this.UpdateTotalProgress.Value = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(691, 548);
            this.Controls.Add(this.UpdatePartProgress);
            this.Controls.Add(this.UpdateTotalProgress);
            this.Controls.Add(this.UpdateTotal);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.UpdatePart);
            this.Controls.Add(this.LaunchButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FROZEN Game Launcher";
            this.TransparencyKey = System.Drawing.Color.DarkOliveGreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.Label UpdatePart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label UpdateTotal;
        private CustomProgressBar UpdateTotalProgress;
        private CustomProgressBar UpdatePartProgress;
    }
}

