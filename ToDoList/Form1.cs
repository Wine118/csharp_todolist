using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDoList;
using static ToDoList.Form1;

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
            public int Id { get; set; }
            public string Text { get; set; }
            public bool IsChecked { get; set; }
            public string Mode { get; set; }


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
            SQLiteHelper.InitializeDatabase();
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
                TodoTask task = new TodoTask 
                {
                    Text = newTask,
                    IsChecked = false,
                    Mode = currentMode.ToString() // This line sets the mode correctly
                };

                // Correct usage — pass task.Text and task.IsChecked
                SQLiteHelper.AddTask(task);

                RefreshTaskList();
            }
        }


        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                var task = new TodoTask
                {
                    Text = checkedListBox1.Items[e.Index].ToString(),
                    IsChecked = e.NewValue == CheckState.Checked,
                    Mode = currentMode.ToString()
                };

                SQLiteHelper.UpdateCheckState(task);
            }));
        }

        private void RefreshTaskList()
        {
            checkedListBox1.Items.Clear();
            var tasks = SQLiteHelper.GetTasks(currentMode.ToString());


            foreach (var task in tasks)
            {
                checkedListBox1.Items.Add(task.Text, task.IsChecked);
            }
        }


        private void ClearButton_Click(object sender, EventArgs e)
        {
            SQLiteHelper.ClearTasks(currentMode.ToString());
            RefreshTaskList();

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            List<string> toDelete = new List<string>();
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                string fullText = checkedListBox1.Items[index].ToString();
                string taskText = fullText.Substring(fullText.IndexOf(". ") + 2); // remove numbering
                toDelete.Add(fullText);
            }

            SQLiteHelper.DeleteCheckedTasks(currentMode.ToString(), toDelete);
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