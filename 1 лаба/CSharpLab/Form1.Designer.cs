namespace csWinFkey
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelFormatted = new System.Windows.Forms.Label();
            this.labelFormattedText = new System.Windows.Forms.Label();
            this.labelHistory = new System.Windows.Forms.Label();
            this.listBoxHistory = new System.Windows.Forms.ListBox();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // textBox1
            this.textBox1.Location = new System.Drawing.Point(15, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(500, 25);
            this.textBox1.TabIndex = 0;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            
            // labelInput
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(15, 20);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(83, 16);
            this.labelInput.TabIndex = 1;
            this.labelInput.Text = "Ввод ФИО:";
            this.labelInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            
            // labelFormatted
            this.labelFormatted.AutoSize = true;
            this.labelFormatted.Location = new System.Drawing.Point(15, 80);
            this.labelFormatted.Name = "labelFormatted";
            this.labelFormatted.Size = new System.Drawing.Size(141, 16);
            this.labelFormatted.TabIndex = 2;
            this.labelFormatted.Text = "Исправленный вид:";
            this.labelFormatted.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            
            // labelFormattedText
            this.labelFormattedText.AutoSize = true;
            this.labelFormattedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.labelFormattedText.ForeColor = System.Drawing.Color.Green;
            this.labelFormattedText.Location = new System.Drawing.Point(15, 100);
            this.labelFormattedText.Name = "labelFormattedText";
            this.labelFormattedText.Size = new System.Drawing.Size(0, 18);
            this.labelFormattedText.TabIndex = 3;
            this.labelFormattedText.MaximumSize = new System.Drawing.Size(500, 0);
            
            // labelHistory
            this.labelHistory.AutoSize = true;
            this.labelHistory.Location = new System.Drawing.Point(15, 140);
            this.labelHistory.Name = "labelHistory";
            this.labelHistory.Size = new System.Drawing.Size(119, 16);
            this.labelHistory.TabIndex = 4;
            this.labelHistory.Text = "История ввода:";
            this.labelHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            
            // listBoxHistory
            this.listBoxHistory.FormattingEnabled = true;
            this.listBoxHistory.ItemHeight = 16;
            this.listBoxHistory.Location = new System.Drawing.Point(15, 160);
            this.listBoxHistory.Name = "listBoxHistory";
            this.listBoxHistory.Size = new System.Drawing.Size(500, 180);
            this.listBoxHistory.TabIndex = 5;
            this.listBoxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            
            // btnClearHistory
            this.btnClearHistory.Location = new System.Drawing.Point(15, 360);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(240, 40);
            this.btnClearHistory.TabIndex = 6;
            this.btnClearHistory.Text = "Очистить историю";
            this.btnClearHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            
            // btnSaveToFile
            this.btnSaveToFile.Location = new System.Drawing.Point(275, 360);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(240, 40);
            this.btnSaveToFile.TabIndex = 7;
            this.btnSaveToFile.Text = "Сохранить в файл";
            this.btnSaveToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            
            // Form1
            this.ClientSize = new System.Drawing.Size(534, 421);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.btnClearHistory);
            this.Controls.Add(this.listBoxHistory);
            this.Controls.Add(this.labelHistory);
            this.Controls.Add(this.labelFormattedText);
            this.Controls.Add(this.labelFormatted);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Ввод ФИО с двойной фамилией";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelFormatted;
        private System.Windows.Forms.Label labelFormattedText;
        private System.Windows.Forms.Label labelHistory;
        private System.Windows.Forms.ListBox listBoxHistory;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Button btnSaveToFile;
    }
}