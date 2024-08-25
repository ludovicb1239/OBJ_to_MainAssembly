
namespace OBJ_to_MainAssembly
{
    partial class ConverterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterForm));
            this.convertButton = new System.Windows.Forms.Button();
            this.pathInput = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.scaleInput = new System.Windows.Forms.TextBox();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.forwardComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.invertCheckBox = new System.Windows.Forms.CheckBox();
            this.roundCheckBox = new System.Windows.Forms.CheckBox();
            this.mirrorCheckBox = new System.Windows.Forms.CheckBox();
            this.segmentsCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // convertButton
            // 
            this.convertButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.convertButton.Location = new System.Drawing.Point(300, 378);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(200, 50);
            this.convertButton.TabIndex = 0;
            this.convertButton.Text = "Convert to Main Assembly !";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // pathInput
            // 
            this.pathInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathInput.Location = new System.Drawing.Point(410, 110);
            this.pathInput.Name = "pathInput";
            this.pathInput.Size = new System.Drawing.Size(266, 25);
            this.pathInput.TabIndex = 1;
            this.pathInput.TextChanged += new System.EventHandler(this.pathInput_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.AutoSize = true;
            this.browseButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(283, 108);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(121, 27);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse 3D model";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // scaleInput
            // 
            this.scaleInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleInput.Location = new System.Drawing.Point(410, 165);
            this.scaleInput.Name = "scaleInput";
            this.scaleInput.Size = new System.Drawing.Size(100, 25);
            this.scaleInput.TabIndex = 3;
            this.scaleInput.Text = "1";
            this.scaleInput.TextChanged += new System.EventHandler(this.scaleInput_TextChanged);
            // 
            // scaleLabel
            // 
            this.scaleLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleLabel.Location = new System.Drawing.Point(313, 168);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(91, 17);
            this.scaleLabel.TabIndex = 4;
            this.scaleLabel.Text = "Creation Scale";
            this.scaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(200, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "3D file to Main Assembly Converter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // forwardComboBox
            // 
            this.forwardComboBox.CausesValidation = false;
            this.forwardComboBox.DropDownWidth = 60;
            this.forwardComboBox.FormattingEnabled = true;
            this.forwardComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.forwardComboBox.Location = new System.Drawing.Point(410, 205);
            this.forwardComboBox.Name = "forwardComboBox";
            this.forwardComboBox.Size = new System.Drawing.Size(60, 25);
            this.forwardComboBox.TabIndex = 6;
            this.forwardComboBox.Text = "X";
            this.forwardComboBox.SelectedIndexChanged += new System.EventHandler(this.forwardComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Forward";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // invertCheckBox
            // 
            this.invertCheckBox.AutoSize = true;
            this.invertCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.invertCheckBox.Location = new System.Drawing.Point(316, 245);
            this.invertCheckBox.Name = "invertCheckBox";
            this.invertCheckBox.Size = new System.Drawing.Size(107, 21);
            this.invertCheckBox.TabIndex = 9;
            this.invertCheckBox.Text = "Invert Z and Y";
            this.invertCheckBox.UseVisualStyleBackColor = true;
            this.invertCheckBox.CheckedChanged += new System.EventHandler(this.invertCheckBox_CheckedChanged);
            // 
            // roundCheckBox
            // 
            this.roundCheckBox.AutoSize = true;
            this.roundCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.roundCheckBox.Location = new System.Drawing.Point(262, 272);
            this.roundCheckBox.Name = "roundCheckBox";
            this.roundCheckBox.Size = new System.Drawing.Size(161, 21);
            this.roundCheckBox.TabIndex = 10;
            this.roundCheckBox.Text = "Round vert pos to grid";
            this.roundCheckBox.UseVisualStyleBackColor = true;
            this.roundCheckBox.CheckedChanged += new System.EventHandler(this.roundCheckBox_CheckedChanged);
            // 
            // mirrorCheckBox
            // 
            this.mirrorCheckBox.AutoSize = true;
            this.mirrorCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mirrorCheckBox.Location = new System.Drawing.Point(358, 299);
            this.mirrorCheckBox.Name = "mirrorCheckBox";
            this.mirrorCheckBox.Size = new System.Drawing.Size(65, 21);
            this.mirrorCheckBox.TabIndex = 11;
            this.mirrorCheckBox.Text = "Mirror";
            this.mirrorCheckBox.UseVisualStyleBackColor = true;
            this.mirrorCheckBox.CheckedChanged += new System.EventHandler(this.mirrorCheckBox_CheckedChanged);
            // 
            // segmentsCheckBox
            // 
            this.segmentsCheckBox.AutoSize = true;
            this.segmentsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.segmentsCheckBox.Location = new System.Drawing.Point(300, 326);
            this.segmentsCheckBox.Name = "segmentsCheckBox";
            this.segmentsCheckBox.Size = new System.Drawing.Size(123, 21);
            this.segmentsCheckBox.TabIndex = 12;
            this.segmentsCheckBox.Text = "Frame segments";
            this.segmentsCheckBox.UseVisualStyleBackColor = true;
            this.segmentsCheckBox.CheckedChanged += new System.EventHandler(this.segmentsCheckBox_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(250, 465);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 18);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Value = 40;
            this.progressBar1.Visible = false;
            // 
            // Converter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.segmentsCheckBox);
            this.Controls.Add(this.mirrorCheckBox);
            this.Controls.Add(this.roundCheckBox);
            this.Controls.Add(this.invertCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.forwardComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.scaleInput);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.pathInput);
            this.Controls.Add(this.convertButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Converter";
            this.Text = "Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.TextBox pathInput;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox scaleInput;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox forwardComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox invertCheckBox;
        private System.Windows.Forms.CheckBox roundCheckBox;
        private System.Windows.Forms.CheckBox mirrorCheckBox;
        private System.Windows.Forms.CheckBox segmentsCheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

