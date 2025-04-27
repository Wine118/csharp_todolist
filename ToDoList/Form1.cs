using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int taskCount = 0;
        private List<string> DailyTasks = new List<string>();
        private List<string> WeeklyTasks = new List<string>();
        private List<string> MonthlyTasks = new List<string>();

        private enum TaskMode { Daily, Weekly, Monthly }
        private TaskMode currentMode = TaskMode.Daily;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomCode();
        }

        private void InitializeCustomCode()
        {
            // Add event handlers
            AddButton.Click += AddButton_Click;
            Daily.Click += Daily_Click;
            Weekly.Click += Weekly_Click;
            Monthly.Click += Monthly_Click;
            // You will add ClearButton yourself and connect like this:
            // ClearButton.Click += ClearButton_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Daily_Click(object sender, EventArgs e)
        {
            currentMode = TaskMode.Daily;
            RefreshTaskList();
        }

        private void Weekly_Click(object sender, EventArgs e)
        {
            currentMode = TaskMode.Weekly;
            RefreshTaskList();
        }

        private void Monthly_Click(object sender, EventArgs e)
        {
            currentMode = TaskMode.Monthly;
            RefreshTaskList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string newTask = PromptDialog.ShowDialog("Enter your task:", "Add New Task");
            if (!string.IsNullOrWhiteSpace(newTask))
            {
                switch (currentMode)
                {
                    case TaskMode.Daily:
                        DailyTasks.Add(newTask);
                        break;
                    case TaskMode.Weekly:
                        WeeklyTasks.Add(newTask);
                        break;
                    case TaskMode.Monthly:
                        MonthlyTasks.Add(newTask);
                        break;
                }
                RefreshTaskList();
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshTaskList()
        {
            checkedListBox1.Items.Clear();

            List<string> currentList = new List<string>();
            switch (currentMode)
            {
                case TaskMode.Daily:
                    currentList = DailyTasks;
                    break;
                case TaskMode.Weekly:
                    currentList = WeeklyTasks;
                    break;
                case TaskMode.Monthly:
                    currentList = MonthlyTasks;
                    break;
            }

            for (int i = 0; i < currentList.Count; i++)
            {
                checkedListBox1.Items.Add($"{i + 1}. {currentList[i]}");
            }
        }


        private void ClearButton_Click(object sender, EventArgs e)
        {
            switch (currentMode)
            {
                case TaskMode.Daily:
                    DailyTasks.Clear();
                    break;
                case TaskMode.Weekly:
                    WeeklyTasks.Clear();
                    break;
                case TaskMode.Monthly:
                    MonthlyTasks.Clear();
                    break;
            }
            RefreshTaskList();
        }
    }

    public static class PromptDialog
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }
}
