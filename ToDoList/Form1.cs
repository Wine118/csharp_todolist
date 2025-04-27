using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {

        private List<TodoTask> GetCurrentList()
        {
            switch (currentMode)
            {
                case TaskMode.Daily:
                    return DailyTasks;
                case TaskMode.Weekly:
                    return WeeklyTasks;
                case TaskMode.Monthly:
                    return MonthlyTasks;
                default:
                    return new List<TodoTask>();
            }
        }

        public class TodoTask
        {
            public string Text { get; set; }
            public bool IsChecked { get; set; }
        }

        private int taskCount = 0;
        private List<TodoTask> DailyTasks = new List<TodoTask>();
        private List<TodoTask> WeeklyTasks = new List<TodoTask>();
        private List<TodoTask> MonthlyTasks = new List<TodoTask>();


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
            DeleteButton.Click += DeleteButton_Click;
            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;


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
                TodoTask task = new TodoTask { Text = newTask, IsChecked = false };
                switch (currentMode)
                {
                    case TaskMode.Daily:
                        DailyTasks.Add(task);
                        break;
                    case TaskMode.Weekly:
                        WeeklyTasks.Add(task);
                        break;
                    case TaskMode.Monthly:
                        MonthlyTasks.Add(task);
                        break;
                }
                RefreshTaskList();
            }

        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                List<TodoTask> currentList = GetCurrentList();
                if (e.Index >= 0 && e.Index < currentList.Count)
                {
                    currentList[e.Index].IsChecked = (checkedListBox1.GetItemChecked(e.Index));
                }
            }));
        }

        private void RefreshTaskList()
        {
            checkedListBox1.Items.Clear();

            List<TodoTask> currentList = GetCurrentList();

            for (int i = 0; i < currentList.Count; i++)
            {
                checkedListBox1.Items.Add($"{i + 1}. {currentList[i].Text}", currentList[i].IsChecked);
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // First, collect which indexes are checked
            List<int> checkedIndexes = new List<int>();
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                checkedIndexes.Add(index);
            }

            // Delete from the task list, starting from highest index to avoid shifting issues
            List<TodoTask> currentList = GetCurrentList();

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

            // Important: Delete backwards
            checkedIndexes.Sort();
            checkedIndexes.Reverse();
            foreach (int index in checkedIndexes)
            {
                if (index >= 0 && index < currentList.Count)
                {
                    currentList.RemoveAt(index);
                }
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