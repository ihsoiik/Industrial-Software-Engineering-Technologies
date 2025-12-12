using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UniversalPromptSolution
{
    public class Form1 : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem createListToolStripMenuItem;
        private ToolStripMenuItem connectListToolStripMenuItem;
        private ToolStripMenuItem sortListToolStripMenuItem;
        private DataGridView dataGridView2;
        private DataGridView dgwNamesPrompt;
        private DataGridView dgwTypePrompt;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;

        public static List<Names> vns = new List<Names>();
        public static List<TypeItem> vts = new List<TypeItem>();
        public static BindingSource vbs = new BindingSource();
        public static BindingSource tbs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            SubscribeToEvents();
            
            vbs.DataSource = vns;
            tbs.DataSource = vts;
        }

        private void InitializeComponent()
        {
            this.Text = "Универсальная система подсказок";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem("Файл");
            createListToolStripMenuItem = new ToolStripMenuItem("Создать список");
            connectListToolStripMenuItem = new ToolStripMenuItem("Подключить список");
            sortListToolStripMenuItem = new ToolStripMenuItem("Упорядочить");

            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                createListToolStripMenuItem, connectListToolStripMenuItem, sortListToolStripMenuItem
            });

            menuStrip1.Items.Add(fileToolStripMenuItem);
            this.MainMenuStrip = menuStrip1;

            dataGridView2 = new DataGridView();
            dataGridView2.Location = new Point(20, 40);
            dataGridView2.Size = new Size(740, 200);
            dataGridView2.Name = "dataGridView2";

            // Таблица подсказки для Названия
            dgwNamesPrompt = new DataGridView();
            dgwNamesPrompt.Location = new Point(20, 260);
            dgwNamesPrompt.Size = new Size(300, 120);
            dgwNamesPrompt.Name = "dgwNamesPrompt";
            dgwNamesPrompt.ColumnHeadersVisible = false;
            dgwNamesPrompt.RowHeadersVisible = false;
            dgwNamesPrompt.Visible = false;
            dgwNamesPrompt.BorderStyle = BorderStyle.FixedSingle;
            dgwNamesPrompt.BackgroundColor = Color.White;
            dgwNamesPrompt.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Таблица подсказки для Типа
            dgwTypePrompt = new DataGridView();
            dgwTypePrompt.Location = new Point(340, 260);
            dgwTypePrompt.Size = new Size(300, 120);
            dgwTypePrompt.Name = "dgwTypePrompt";
            dgwTypePrompt.ColumnHeadersVisible = false;
            dgwTypePrompt.RowHeadersVisible = false;
            dgwTypePrompt.Visible = false;
            dgwTypePrompt.BorderStyle = BorderStyle.FixedSingle;
            dgwTypePrompt.BackgroundColor = Color.White;
            dgwTypePrompt.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel1.Text = "Готов - Создайте список сначала";
            statusStrip1.Items.Add(toolStripStatusLabel1);

            this.Controls.Add(menuStrip1);
            this.Controls.Add(dataGridView2);
            this.Controls.Add(dgwNamesPrompt);
            this.Controls.Add(dgwTypePrompt);
            this.Controls.Add(statusStrip1);
        }

        private void InitializeDataGridView()
        {
            dataGridView2.Columns.Clear();

            DataGridViewTextBoxColumn colNN = new DataGridViewTextBoxColumn();
            colNN.HeaderText = "№";
            colNN.Name = "colNN";
            colNN.Width = 50;
            dataGridView2.Columns.Add(colNN);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Название";
            colName.Name = "colName";
            colName.Width = 200;
            dataGridView2.Columns.Add(colName);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.HeaderText = "Тип";
            colType.Name = "colType";
            colType.Width = 100;
            dataGridView2.Columns.Add(colType);

            dataGridView2.Rows.Add("1", "Лев", "Зверь");
            dataGridView2.Rows.Add("2", "Орел", "Птица");
            dataGridView2.Rows.Add("3", "Щука", "Рыба");
            dataGridView2.Rows.Add("4", "", "");

            // Инициализация подсказки для Названия
            InitializePromptGrid(dgwNamesPrompt, "Name");
            
            // Инициализация подсказки для Типа
            InitializePromptGrid(dgwTypePrompt, "TypeName");
            
            // Добавляем базовые типы
            vts.Add(new TypeItem { TypeName = "Зверь" });
            vts.Add(new TypeItem { TypeName = "Птица" });
            vts.Add(new TypeItem { TypeName = "Рыба" });
            vts.Add(new TypeItem { TypeName = "Гад" });
            vts.Add(new TypeItem { TypeName = "Насекомое" });
            vts.Add(new TypeItem { TypeName = "Растение" });
        }

        private void InitializePromptGrid(DataGridView grid, string dataPropertyName)
        {
            grid.Columns.Clear();
            
            DataGridViewTextBoxColumn promptCol = new DataGridViewTextBoxColumn();
            promptCol.Name = "PromptColumn";
            promptCol.Width = grid.Width - 5;
            promptCol.DataPropertyName = dataPropertyName;
            
            grid.Columns.Add(promptCol);
            grid.AutoGenerateColumns = false;
        }

        private void SubscribeToEvents()
        {
            createListToolStripMenuItem.Click += CreateListToolStripMenuItem_Click;
            connectListToolStripMenuItem.Click += ConnectListToolStripMenuItem_Click;
            sortListToolStripMenuItem.Click += SortListToolStripMenuItem_Click;

            dataGridView2.CellBeginEdit += DataGridView2_CellBeginEdit;
            dataGridView2.CellEndEdit += DataGridView2_CellEndEdit;
            dataGridView2.EditingControlShowing += DataGridView2_EditingControlShowing;
            dataGridView2.KeyDown += DataGridView2_KeyDown;
        }

        private void CreateListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vns.Clear();
            vns.Add(new Names { Name = "Тигр" });
            vns.Add(new Names { Name = "Волк" });
            vns.Add(new Names { Name = "Медведь" });
            vns.Add(new Names { Name = "Лиса" });
            vns.Add(new Names { Name = "Заяц" });
            vns.Add(new Names { Name = "Сова" });
            vns.Add(new Names { Name = "Сокол" });
            vns.Add(new Names { Name = "Карп" });
            vns.Add(new Names { Name = "Окунь" });

            vbs.ResetBindings(false);
            toolStripStatusLabel1.Text = $"Список создан: {vns.Count} животных";
        }

        private void ConnectListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vns.Count == 0)
            {
                toolStripStatusLabel1.Text = "Сначала создайте список животных!";
                return;
            }

            dgwNamesPrompt.DataSource = vbs;
            dgwTypePrompt.DataSource = tbs;
            toolStripStatusLabel1.Text = $"Списки подключены: {vns.Count} животных, {vts.Count} типов. Кликните на ячейки для проверки подсказок.";
        }

        private void SortListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vns.Count == 0 && vts.Count == 0)
            {
                toolStripStatusLabel1.Text = "Нет данных для упорядочивания";
                return;
            }

            if (vns.Count > 0)
            {
                var sortedList = vns.OrderBy(n => n.Name).ToList();
                vns.Clear();
                vns.AddRange(sortedList);
                vbs.ResetBindings(false);
            }

            if (vts.Count > 0)
            {
                var sortedTypes = vts.OrderBy(t => t.TypeName).ToList();
                vts.Clear();
                vts.AddRange(sortedTypes);
                tbs.ResetBindings(false);
            }

            toolStripStatusLabel1.Text = "Списки упорядочены по алфавиту";
        }

        private void DataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Редактирование ячейки: строка {e.RowIndex}, колонка {e.ColumnIndex}";
            
            if (e.ColumnIndex == 1) // Колонка Название
            {
                PromptManager.OpenPrompt(toolStripStatusLabel1, dgwNamesPrompt, dataGridView2, e, PromptType.Name);
            }
            else if (e.ColumnIndex == 2) // Колонка Тип
            {
                PromptManager.OpenPrompt(toolStripStatusLabel1, dgwTypePrompt, dataGridView2, e, PromptType.Type);
            }
        }

        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Завершено редактирование: строка {e.RowIndex}, колонка {e.ColumnIndex}";
            
            // Автоматическое добавление в подсказку происходит только при обычном завершении редактирования
            // (не при нажатии Escape)
            if (!PromptManager.EscapePressed)
            {
                string newValue = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(newValue))
                {
                    if (e.ColumnIndex == 1) // Колонка Название
                    {
                        PromptManager.StoreNewNameValue(newValue);
                    }
                    else if (e.ColumnIndex == 2) // Колонка Тип
                    {
                        PromptManager.StoreNewTypeValue(newValue);
                    }
                }
            }
            
            PromptManager.ClosePrompt();
            PromptManager.EscapePressed = false; // Сбрасываем флаг
        }

        private void DataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 1 || dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                var textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    PromptManager.EditingTextBox = textBox;
                    PromptManager.CurrentPromptType = dataGridView2.CurrentCell.ColumnIndex == 1 ? PromptType.Name : PromptType.Type;
                    PromptManager.AttachTextBoxEvents();
                    
                    string columnName = dataGridView2.CurrentCell.ColumnIndex == 1 ? "названия" : "типа";
                    toolStripStatusLabel1.Text = $"Текстовое поле готово к вводу {columnName} - используйте Escape для сохранения без добавления в подсказку";
                }
            }
        }

        private void DataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка Escape для всех ячеек DataGridView
            if (e.KeyCode == Keys.Escape && dataGridView2.IsCurrentCellInEditMode)
            {
                // Устанавливаем флаг, что нажат Escape
                PromptManager.EscapePressed = true;
                
                // Завершаем редактирование - значение сохранится в ячейке, но не добавится в подсказку
                dataGridView2.EndEdit();
                PromptManager.ClosePrompt();
                
                toolStripStatusLabel1.Text = "Редактирование завершено (Escape) - значение сохранено в ячейке, но не добавлено в подсказку";
                e.Handled = true;
            }
        }
    }

    public class Names
    {
        public string Name { get; set; } = string.Empty;
    }

    public class TypeItem
    {
        public string TypeName { get; set; } = string.Empty;
    }

    public enum PromptType
    {
        Name,
        Type
    }

    public static class PromptManager
    {
        public static TextBox EditingTextBox { get; set; }
        public static bool Active { get; set; }
        public static bool IsOpened { get; set; }
        public static int CurrentPromptRow { get; set; }
        public static PromptType CurrentPromptType { get; set; }
        public static bool EscapePressed { get; set; } // Новый флаг для отслеживания нажатия Escape

        private static DataGridView mainGrid;
        private static DataGridView promptGrid;
        private static ToolStripStatusLabel statusLabel;
        private static int editingRow;
        private static int editingColumn;

        public static void OpenPrompt(ToolStripStatusLabel status, DataGridView prompt, DataGridView main, DataGridViewCellCancelEventArgs e, PromptType promptType)
        {
            if (IsOpened) 
            {
                status.Text = "Подсказка уже открыта";
                return;
            }

            statusLabel = status;
            editingRow = e.RowIndex;
            editingColumn = e.ColumnIndex;
            mainGrid = main;
            promptGrid = prompt;
            CurrentPromptRow = 0;
            CurrentPromptType = promptType;
            EscapePressed = false; // Сбрасываем флаг при открытии подсказки

            if (promptGrid.Rows.Count == 0)
            {
                statusLabel.Text = "Подсказка пуста - создайте и подключите список сначала";
                return;
            }

            PositionPrompt();
            SetupPromptSelection();
            AttachPromptEvents();

            IsOpened = true;
            promptGrid.Visible = true;
            
            string promptName = promptType == PromptType.Name ? "названий" : "типов";
            statusLabel.Text = $"Подсказка {promptName} активирована - используйте Escape для сохранения без добавления в подсказку";
        }

        private static void PositionPrompt()
        {
            try
            {
                var cellRect = mainGrid.GetCellDisplayRectangle(editingColumn, editingRow, true);
                
                int promptTop = mainGrid.Location.Y + cellRect.Bottom + 2;
                int promptLeft = mainGrid.Location.X + cellRect.Left;
                
                promptGrid.Location = new Point(promptLeft, promptTop);
                promptGrid.Width = cellRect.Width;
                
                int rowHeight = promptGrid.RowTemplate.Height > 0 ? promptGrid.RowTemplate.Height : 22;
                int contentHeight = Math.Max(promptGrid.Rows.Count * rowHeight + 4, 50);
                promptGrid.Height = Math.Min(contentHeight, 150);

                if (promptGrid.Columns.Count > 0)
                {
                    promptGrid.Columns[0].Width = promptGrid.Width - 5;
                }

                promptGrid.BringToFront();
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Ошибка позиционирования: {ex.Message}";
            }
        }

        private static void SetupPromptSelection()
        {
            if (promptGrid.Rows.Count > 0)
            {
                promptGrid.ClearSelection();
                if (promptGrid.Rows[0].Cells[0] != null)
                {
                    promptGrid.Rows[0].Cells[0].Selected = true;
                    promptGrid.CurrentCell = promptGrid.Rows[0].Cells[0];
                }
            }
        }

        private static void AttachPromptEvents()
        {
            promptGrid.KeyDown += PromptGrid_KeyDown;
            promptGrid.CellClick += PromptGrid_CellClick;
            promptGrid.CellMouseClick += PromptGrid_CellMouseClick;
            promptGrid.CellMouseEnter += PromptGrid_CellMouseEnter;
            promptGrid.CellMouseLeave += PromptGrid_CellMouseLeave;
        }

        private static void DetachPromptEvents()
        {
            promptGrid.KeyDown -= PromptGrid_KeyDown;
            promptGrid.CellClick -= PromptGrid_CellClick;
            promptGrid.CellMouseClick -= PromptGrid_CellMouseClick;
            promptGrid.CellMouseEnter -= PromptGrid_CellMouseEnter;
            promptGrid.CellMouseLeave -= PromptGrid_CellMouseLeave;
        }

        public static void AttachTextBoxEvents()
        {
            if (EditingTextBox != null)
            {
                EditingTextBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                EditingTextBox.KeyPress -= TextBox_KeyPress;
                EditingTextBox.TextChanged -= TextBox_TextChanged;
                
                EditingTextBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                EditingTextBox.KeyPress += TextBox_KeyPress;
                EditingTextBox.TextChanged += TextBox_TextChanged;
            }
        }

        public static void DetachTextBoxEvents()
        {
            if (EditingTextBox != null)
            {
                EditingTextBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                EditingTextBox.KeyPress -= TextBox_KeyPress;
                EditingTextBox.TextChanged -= TextBox_TextChanged;
            }
        }

        public static void ClosePrompt()
        {
            if (IsOpened)
            {
                DetachPromptEvents();
                DetachTextBoxEvents();
                
                IsOpened = false;
                Active = false;
                if (promptGrid != null)
                {
                    promptGrid.Visible = false;
                }
            }
        }

        private static void PromptGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            Active = true;
        }

        private static void PromptGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            Active = false;
        }

        private static void PromptGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < promptGrid.Rows.Count)
            {
                if (promptGrid.Rows[e.RowIndex].Cells[0] != null && 
                    promptGrid.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    SelectPromptItem(e.RowIndex);
                }
            }
        }

        private static void PromptGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.RowIndex < promptGrid.Rows.Count)
            {
                if (promptGrid.Rows[e.RowIndex].Cells[0] != null && 
                    promptGrid.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    SelectPromptItem(e.RowIndex);
                }
            }
        }

        private static void PromptGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (promptGrid.CurrentRow != null)
                    {
                        SelectPromptItem(promptGrid.CurrentRow.Index);
                    }
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    // Escape в подсказке - устанавливаем флаг и закрываем
                    EscapePressed = true;
                    if (mainGrid != null && mainGrid.IsCurrentCellInEditMode)
                    {
                        mainGrid.EndEdit(); // Сохраняем значение в ячейке
                    }
                    ClosePrompt();
                    if (statusLabel != null)
                        statusLabel.Text = "Редактирование завершено (Escape в подсказке) - значение сохранено в ячейке";
                    e.Handled = true;
                    break;

                case Keys.Up:
                    MoveSelection(-1);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    MoveSelection(1);
                    e.Handled = true;
                    break;
            }
        }

        private static void SelectPromptItem(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < promptGrid.Rows.Count && mainGrid != null)
            {
                string selectedValue = promptGrid.Rows[rowIndex].Cells[0].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(selectedValue))
                {
                    mainGrid.Rows[editingRow].Cells[editingColumn].Value = selectedValue;
                    Active = true;
                    
                    // При выборе из подсказки значение автоматически добавляется в соответствующую подсказку
                    if (CurrentPromptType == PromptType.Name)
                    {
                        StoreNewNameValue(selectedValue);
                    }
                    else
                    {
                        StoreNewTypeValue(selectedValue);
                    }
                    
                    if (statusLabel != null)
                    {
                        string valueType = CurrentPromptType == PromptType.Name ? "название" : "тип";
                        statusLabel.Text = $"Выбрано {valueType}: {selectedValue} (добавлено в подсказку)";
                    }
                }
            }
            ClosePrompt();
        }

        private static void MoveSelection(int direction)
        {
            if (promptGrid.Rows.Count == 0) return;

            int currentIndex = promptGrid.CurrentRow?.Index ?? 0;
            int newIndex = (currentIndex + direction + promptGrid.Rows.Count) % promptGrid.Rows.Count;

            if (newIndex >= 0 && newIndex < promptGrid.Rows.Count)
            {
                promptGrid.ClearSelection();
                promptGrid.Rows[newIndex].Cells[0].Selected = true;
                promptGrid.CurrentCell = promptGrid.Rows[newIndex].Cells[0];
            }
        }

        private static void TextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (IsOpened && promptGrid.Rows.Count > 0)
                    {
                        promptGrid.Focus();
                        e.IsInputKey = true;
                    }
                    break;

                case Keys.Enter:
                    // Enter - обычное завершение, значение добавится в подсказку
                    Active = true;
                    EscapePressed = false;
                    if (mainGrid != null && mainGrid.IsCurrentCellInEditMode)
                    {
                        mainGrid.EndEdit();
                    }
                    ClosePrompt();
                    break;

                case Keys.Escape:
                    // Escape - устанавливаем флаг, что значение НЕ должно добавляться в подсказку
                    Active = false;
                    EscapePressed = true;
                    if (mainGrid != null && mainGrid.IsCurrentCellInEditMode)
                    {
                        mainGrid.EndEdit(); // Сохраняем значение в ячейке
                    }
                    ClosePrompt();
                    if (statusLabel != null)
                        statusLabel.Text = "Редактирование завершено (Escape) - значение сохранено в ячейке, но не добавлено в подсказку";
                    break;
            }
        }

        private static void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Обработка будет в TextChanged
        }

        private static void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!IsOpened || promptGrid.Rows.Count == 0) return;

            string searchText = EditingTextBox.Text;
            UpdatePromptSelection(searchText);
        }

        private static void UpdatePromptSelection(string searchText)
        {
            if (promptGrid.Rows.Count == 0) return;

            promptGrid.ClearSelection();
            
            bool found = false;
            for (int i = 0; i < promptGrid.Rows.Count; i++)
            {
                string cellValue = promptGrid.Rows[i].Cells[0].Value?.ToString() ?? "";
                if (cellValue.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    promptGrid.Rows[i].Cells[0].Selected = true;
                    promptGrid.CurrentCell = promptGrid.Rows[i].Cells[0];
                    CurrentPromptRow = i;
                    found = true;
                    break;
                }
            }

            if (!found && promptGrid.Rows.Count > 0)
            {
                promptGrid.Rows[0].Cells[0].Selected = true;
                promptGrid.CurrentCell = promptGrid.Rows[0].Cells[0];
            }
        }

        public static void StoreNewNameValue(string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                bool exists = Form1.vns.Any(n => n.Name.Equals(newValue, StringComparison.OrdinalIgnoreCase));
                
                if (!exists)
                {
                    Form1.vns.Add(new Names { Name = newValue });
                    var sortedList = Form1.vns.OrderBy(n => n.Name).ToList();
                    Form1.vns.Clear();
                    Form1.vns.AddRange(sortedList);
                    Form1.vbs.ResetBindings(false);
                }
            }
        }

        public static void StoreNewTypeValue(string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                bool exists = Form1.vts.Any(t => t.TypeName.Equals(newValue, StringComparison.OrdinalIgnoreCase));
                
                if (!exists)
                {
                    Form1.vts.Add(new TypeItem { TypeName = newValue });
                    var sortedList = Form1.vts.OrderBy(t => t.TypeName).ToList();
                    Form1.vts.Clear();
                    Form1.vts.AddRange(sortedList);
                    Form1.tbs.ResetBindings(false);
                }
            }
        }
    }
}