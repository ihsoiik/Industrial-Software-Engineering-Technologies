using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csWinFkey
{
    public partial class Form1 : Form
    {
        private List<string> inputHistory = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                FormatAndSaveInput();
            }
        }

        private void FormatAndSaveInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            string input = textBox1.Text.Trim();
            string formatted = FormatFIO(input);

            if (!string.IsNullOrEmpty(formatted))
            {
                // Заменяем текст на отформатированный
                textBox1.Text = formatted;
                
                // Выделяем весь текст
                textBox1.SelectAll();
                
                // Добавляем в историю
                inputHistory.Add($"{input} → {formatted}");
                UpdateHistoryList();
                
                // Показываем в label
                labelFormattedText.Text = formatted;
            }
        }

        private string FormatFIO(string input)
        {
            try
            {
                string lowerInput = input.ToLower();
                string[] parts = lowerInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length < 2) 
                {
                    MessageBox.Show("Введите фамилию и хотя бы один инициал!");
                    return null;
                }

                string surname = FormatSurname(parts[0]);
                string initials = FormatInitials(parts.Skip(1).ToArray());

                return $"{surname} {initials}";
            }
            catch
            {
                return null;
            }
        }

        private string FormatSurname(string surname)
        {
            if (surname.Contains('-'))
            {
                string[] parts = surname.Split('-');
                for (int i = 0; i < parts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(parts[i]))
                    {
                        parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
                    }
                }
                return string.Join("-", parts);
            }
            else
            {
                return char.ToUpper(surname[0]) + surname.Substring(1);
            }
        }

        private string FormatInitials(string[] initialsParts)
        {
            string result = "";
            foreach (string part in initialsParts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    result += char.ToUpper(part[0]) + ".";
                }
            }
            return result;
        }

        private void UpdateHistoryList()
        {
            listBoxHistory.Items.Clear();
            foreach (var item in inputHistory)
            {
                listBoxHistory.Items.Add(item);
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            inputHistory.Clear();
            listBoxHistory.Items.Clear();
            labelFormattedText.Text = "";
            textBox1.Clear();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            if (inputHistory.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.Title = "Сохранить историю ввода";
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllLines(saveFileDialog.FileName, inputHistory);
                    MessageBox.Show("История сохранена успешно!");
                }
            }
            else
            {
                MessageBox.Show("История пуста!");
            }
        }
    }
}