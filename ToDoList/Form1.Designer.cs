namespace ToDoList
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
            checkedListBox1 = new CheckedListBox();
            Daily = new Button();
            Weekly = new Button();
            Monthly = new Button();
            AddButton = new Button();
            ClearButton = new Button();
            
            // 
            // checkedListBox1
            // 
            checkedListBox1.BackColor = SystemColors.InactiveCaption;
            checkedListBox1.Font = new Font("Segoe UI", 16F);
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(87, 52);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(589, 252);
            checkedListBox1.TabIndex = 0;
            checkedListBox1.ThreeDCheckBoxes = true;
            checkedListBox1.UseWaitCursor = true;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // Daily
            // 
            Daily.Font = new Font("Segoe UI", 12F);
            Daily.Location = new Point(353, 384);
            Daily.Name = "Daily";
            Daily.Size = new Size(102, 54);
            Daily.TabIndex = 1;
            Daily.Text = "Daily";
            Daily.UseVisualStyleBackColor = true;
            Daily.Click += Daily_Click;
            // 
            // Weekly
            // 
            Weekly.Font = new Font("Segoe UI", 12F);
            Weekly.Location = new Point(473, 384);
            Weekly.Name = "Weekly";
            Weekly.Size = new Size(102, 54);
            Weekly.TabIndex = 1;
            Weekly.Text = "Weekly";
            Weekly.UseVisualStyleBackColor = true;
            Weekly.Click += Weekly_Click;
            // 
            // Monthly
            // 
            Monthly.Font = new Font("Segoe UI", 12F);
            Monthly.Location = new Point(592, 384);
            Monthly.Name = "Monthly";
            Monthly.Size = new Size(102, 54);
            Monthly.TabIndex = 1;
            Monthly.Text = "Monthly";
            Monthly.UseVisualStyleBackColor = true;
            Monthly.Click += Monthly_Click;
            // 
            // AddButton
            // 
            AddButton.BackColor = SystemColors.HotTrack;
            AddButton.ForeColor = SystemColors.ScrollBar;
            AddButton.Location = new Point(87, 399);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(59, 39);
            AddButton.TabIndex = 2;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = false;
            AddButton.Click += AddButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = Color.YellowGreen;
            ClearButton.Location = new Point(168, 399);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(59, 39);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ClearButton);
            Controls.Add(AddButton);
            Controls.Add(Monthly);
            Controls.Add(Weekly);
            Controls.Add(Daily);
            Controls.Add(checkedListBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox checkedListBox1;
        private Button Daily;
        private Button Weekly;
        private Button Monthly;
        private Button AddButton;
        private Button ClearButton;
    }
}
