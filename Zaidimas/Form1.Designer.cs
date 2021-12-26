
namespace Zaidimas
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
            this.langeliuKiekisTextBox = new System.Windows.Forms.TextBox();
            this.simboliuKiekisTextBox = new System.Windows.Forms.TextBox();
            this.langeliuKiekisLabel = new System.Windows.Forms.Label();
            this.simboliuKiekisLabel = new System.Windows.Forms.Label();
            this.generuotiButton = new System.Windows.Forms.Button();
            this.GridFlexBox = new System.Windows.Forms.FlowLayoutPanel();
            this.simboliaiCheckBox = new System.Windows.Forms.CheckBox();
            this.spalvosCheckBox = new System.Windows.Forms.CheckBox();
            this.atsakymasButton = new System.Windows.Forms.Button();
            this.retryButton = new System.Windows.Forms.Button();
            this.helpLabel = new System.Windows.Forms.Label();
            this.coordsLabel = new System.Windows.Forms.Label();
            this.timeLimitLabel = new System.Windows.Forms.Label();
            this.timeLimitTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // langeliuKiekisTextBox
            // 
            this.langeliuKiekisTextBox.Location = new System.Drawing.Point(144, 36);
            this.langeliuKiekisTextBox.Name = "langeliuKiekisTextBox";
            this.langeliuKiekisTextBox.Size = new System.Drawing.Size(100, 20);
            this.langeliuKiekisTextBox.TabIndex = 0;
            this.langeliuKiekisTextBox.Text = "5";
            // 
            // simboliuKiekisTextBox
            // 
            this.simboliuKiekisTextBox.Location = new System.Drawing.Point(144, 62);
            this.simboliuKiekisTextBox.Name = "simboliuKiekisTextBox";
            this.simboliuKiekisTextBox.Size = new System.Drawing.Size(100, 20);
            this.simboliuKiekisTextBox.TabIndex = 2;
            this.simboliuKiekisTextBox.Text = "3";
            // 
            // langeliuKiekisLabel
            // 
            this.langeliuKiekisLabel.AutoSize = true;
            this.langeliuKiekisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.langeliuKiekisLabel.Location = new System.Drawing.Point(26, 39);
            this.langeliuKiekisLabel.Name = "langeliuKiekisLabel";
            this.langeliuKiekisLabel.Size = new System.Drawing.Size(92, 13);
            this.langeliuKiekisLabel.TabIndex = 3;
            this.langeliuKiekisLabel.Text = "Langelių kiekis";
            this.langeliuKiekisLabel.Click += new System.EventHandler(this.langeliuKiekisLabel_Click);
            // 
            // simboliuKiekisLabel
            // 
            this.simboliuKiekisLabel.AutoSize = true;
            this.simboliuKiekisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simboliuKiekisLabel.Location = new System.Drawing.Point(26, 65);
            this.simboliuKiekisLabel.Name = "simboliuKiekisLabel";
            this.simboliuKiekisLabel.Size = new System.Drawing.Size(91, 13);
            this.simboliuKiekisLabel.TabIndex = 4;
            this.simboliuKiekisLabel.Text = "Simbolių kiekis";
            // 
            // generuotiButton
            // 
            this.generuotiButton.Location = new System.Drawing.Point(275, 19);
            this.generuotiButton.Name = "generuotiButton";
            this.generuotiButton.Size = new System.Drawing.Size(119, 52);
            this.generuotiButton.TabIndex = 5;
            this.generuotiButton.Text = "Generuoti";
            this.generuotiButton.UseVisualStyleBackColor = false;
            this.generuotiButton.Click += new System.EventHandler(this.generuotiButton_Click);
            // 
            // GridFlexBox
            // 
            this.GridFlexBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GridFlexBox.Location = new System.Drawing.Point(29, 153);
            this.GridFlexBox.Margin = new System.Windows.Forms.Padding(0);
            this.GridFlexBox.Name = "GridFlexBox";
            this.GridFlexBox.Size = new System.Drawing.Size(500, 500);
            this.GridFlexBox.TabIndex = 6;
            this.GridFlexBox.WrapContents = false;
            // 
            // simboliaiCheckBox
            // 
            this.simboliaiCheckBox.AutoSize = true;
            this.simboliaiCheckBox.Checked = true;
            this.simboliaiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.simboliaiCheckBox.Location = new System.Drawing.Point(29, 133);
            this.simboliaiCheckBox.Name = "simboliaiCheckBox";
            this.simboliaiCheckBox.Size = new System.Drawing.Size(67, 17);
            this.simboliaiCheckBox.TabIndex = 7;
            this.simboliaiCheckBox.Text = "Simboliai";
            this.simboliaiCheckBox.UseVisualStyleBackColor = true;
            this.simboliaiCheckBox.CheckedChanged += new System.EventHandler(this.simboliaiCheckBox_CheckedChanged);
            // 
            // spalvosCheckBox
            // 
            this.spalvosCheckBox.AutoSize = true;
            this.spalvosCheckBox.Location = new System.Drawing.Point(110, 133);
            this.spalvosCheckBox.Name = "spalvosCheckBox";
            this.spalvosCheckBox.Size = new System.Drawing.Size(64, 17);
            this.spalvosCheckBox.TabIndex = 8;
            this.spalvosCheckBox.Text = "Spalvos";
            this.spalvosCheckBox.UseVisualStyleBackColor = true;
            this.spalvosCheckBox.CheckedChanged += new System.EventHandler(this.spalvosCheckBox_CheckedChanged);
            // 
            // atsakymasButton
            // 
            this.atsakymasButton.Location = new System.Drawing.Point(275, 88);
            this.atsakymasButton.Name = "atsakymasButton";
            this.atsakymasButton.Size = new System.Drawing.Size(119, 52);
            this.atsakymasButton.TabIndex = 9;
            this.atsakymasButton.Text = "Rodyti atsakymą";
            this.atsakymasButton.UseVisualStyleBackColor = false;
            this.atsakymasButton.Click += new System.EventHandler(this.atsakymasButton_Click);
            // 
            // retryButton
            // 
            this.retryButton.Location = new System.Drawing.Point(410, 19);
            this.retryButton.Name = "retryButton";
            this.retryButton.Size = new System.Drawing.Size(119, 52);
            this.retryButton.TabIndex = 10;
            this.retryButton.Text = "Bandyti iš naujo";
            this.retryButton.UseVisualStyleBackColor = false;
            this.retryButton.Click += new System.EventHandler(this.retryButton_Click);
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.helpLabel.Location = new System.Drawing.Point(400, 92);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(148, 13);
            this.helpLabel.TabIndex = 11;
            this.helpLabel.Text = "1, 1 - Apatinis kairysis kampas";
            // 
            // coordsLabel
            // 
            this.coordsLabel.AutoSize = true;
            this.coordsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.coordsLabel.Location = new System.Drawing.Point(400, 108);
            this.coordsLabel.Name = "coordsLabel";
            this.coordsLabel.Size = new System.Drawing.Size(35, 13);
            this.coordsLabel.TabIndex = 12;
            this.coordsLabel.Text = "label2";
            // 
            // timeLimitLabel
            // 
            this.timeLimitLabel.AutoSize = true;
            this.timeLimitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLimitLabel.Location = new System.Drawing.Point(26, 88);
            this.timeLimitLabel.Name = "timeLimitLabel";
            this.timeLimitLabel.Size = new System.Drawing.Size(93, 26);
            this.timeLimitLabel.TabIndex = 13;
            this.timeLimitLabel.Text = "Laiko limitas \r\ngeneravimui (s)";
            // 
            // timeLimitTextBox
            // 
            this.timeLimitTextBox.Location = new System.Drawing.Point(144, 89);
            this.timeLimitTextBox.Name = "timeLimitTextBox";
            this.timeLimitTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeLimitTextBox.TabIndex = 14;
            this.timeLimitTextBox.Text = "10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 681);
            this.Controls.Add(this.timeLimitTextBox);
            this.Controls.Add(this.timeLimitLabel);
            this.Controls.Add(this.coordsLabel);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.retryButton);
            this.Controls.Add(this.atsakymasButton);
            this.Controls.Add(this.spalvosCheckBox);
            this.Controls.Add(this.simboliaiCheckBox);
            this.Controls.Add(this.GridFlexBox);
            this.Controls.Add(this.generuotiButton);
            this.Controls.Add(this.simboliuKiekisLabel);
            this.Controls.Add(this.langeliuKiekisLabel);
            this.Controls.Add(this.simboliuKiekisTextBox);
            this.Controls.Add(this.langeliuKiekisTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox langeliuKiekisTextBox;
        private System.Windows.Forms.TextBox simboliuKiekisTextBox;
        private System.Windows.Forms.Label langeliuKiekisLabel;
        private System.Windows.Forms.Label simboliuKiekisLabel;
        private System.Windows.Forms.Button generuotiButton;
        private System.Windows.Forms.FlowLayoutPanel GridFlexBox;
        private System.Windows.Forms.CheckBox simboliaiCheckBox;
        private System.Windows.Forms.CheckBox spalvosCheckBox;
        private System.Windows.Forms.Button atsakymasButton;
        private System.Windows.Forms.Button retryButton;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.Label coordsLabel;
        private System.Windows.Forms.Label timeLimitLabel;
        private System.Windows.Forms.TextBox timeLimitTextBox;
    }
}

