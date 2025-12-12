using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChainListApp
{
    class ListIsEmpty : InvalidOperationException { }

    public partial class Form1 : Form
    {
        class MiniList<T> : IEnumerable<T>
        {
            private int num = 0;
            public Node Top;

            public class Node
            {
                public T content;
                public Node Next = null;
            }

            public int Append(T s)
            {
                Node p = new Node();
                p.content = s;
                if (Top != null) p.Next = Top;
                Top = p;
                return num++;
            }

            public class Numerator : IEnumerator<T>
            {
                bool active = false;
                MiniList<T> lst;
                MiniList<T>.Node current = null;
                
                public Numerator(MiniList<T> vl)
                {
                    lst = vl;
                }

                T IEnumerator<T>.Current
                {
                    get
                    {
                        if (current == null || !active) throw new InvalidOperationException();
                        else return current.content;
                    }
                }

                object IEnumerator.Current
                {
                    get
                    {
                        if (current == null || !active) throw new InvalidOperationException();
                        else return current.content;
                    }
                }

                public bool MoveNext()
                {
                    if (active)
                    {
                        if (current.Next == null) 
                            return false;
                        else 
                            current = current.Next;
                    }
                    else
                    {
                        if (lst.Top != null)
                        {
                            active = true;
                            current = lst.Top;
                        }
                        else 
                            throw new ListIsEmpty();
                    }
                    return true;
                }

                public void Reset()
                {
                    active = false;
                    current = null;
                }

                public void Dispose() { }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new Numerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Numerator(this);
            }
        }

        MiniList<CheckBox> v;
        IEnumerator<CheckBox> ven;
        IEnumerator vn;

        public Form1()
        {
            InitializeComponent();
        }

        private void создатьСписокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            v = new MiniList<CheckBox>();
            panel1.Controls.Clear();
            toolStripStatusLabel1.Text = "Список создан.";
        }

        private void добавитьЭлементToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (v != null)
            {
                CheckBox vcb = new CheckBox();
                vcb.Appearance = Appearance.Button;
                vcb.FlatStyle = FlatStyle.Standard;
                vcb.Top = 50;
                vcb.Height = 30;
                vcb.Width = 30;
                vcb.Left = 40 * v.Append(vcb);
                vcb.Text = Convert.ToString(vcb.Left);
                this.panel1.Controls.Add(vcb);
                toolStripStatusLabel1.Text = "";
            }
            else 
            {
                toolStripStatusLabel1.Text = "Список не создан.";
            }
        }

        // Перебор кнопок - Создать энумератор
        private void создатьЭнумераторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (v != null)
            {
                ven = v.GetEnumerator();
                toolStripStatusLabel1.Text = "Энумератор создан.";
            }
            else
            {
                toolStripStatusLabel1.Text = "Список не создан!";
            }
        }

        // Перебор кнопок - MoveNext (ИСПРАВЛЕНО)
        private void moveNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ven != null)
                {
                    if (ven.MoveNext())
                    {
                        ven.Current.CheckState = CheckState.Checked;
                        toolStripStatusLabel1.Text = $"O.K. Текущий: {ven.Current.Text}";
                    }
                    else 
                    {
                        // Сбрасываем все галочки в конце перебора
                        foreach (CheckBox cb in panel1.Controls)
                        {
                            cb.CheckState = CheckState.Unchecked;
                        }
                        toolStripStatusLabel1.Text = "Перебор закончен!";
                    }
                }
                else 
                {
                    toolStripStatusLabel1.Text = "Энумератор не создан!";
                }
            }
            catch (ListIsEmpty) 
            { 
                toolStripStatusLabel1.Text = "Список пуст!"; 
            }
        }

        // Перебор кнопок - Current
        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ven != null)
                {
                    if (ven.Current.Checked)
                        ven.Current.CheckState = CheckState.Unchecked;
                    else 
                        ven.Current.CheckState = CheckState.Checked;
                    toolStripStatusLabel1.Text = $"Переключен: {ven.Current.Text}";
                }
                else 
                {
                    toolStripStatusLabel1.Text = "Энумератор не создан!";
                }
            }
            catch (InvalidOperationException) 
            { 
                toolStripStatusLabel1.Text = "Перебор не начат!"; 
            }
        }

        // Перебор кнопок - Reset
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ven != null) 
            { 
                ven.Reset();
                // Сбрасываем все галочки
                foreach (CheckBox cb in panel1.Controls)
                {
                    cb.CheckState = CheckState.Unchecked;
                }
                toolStripStatusLabel1.Text = "Энумератор обновлён!"; 
            }
            else 
            {
                toolStripStatusLabel1.Text = "Энумератор не создан!";
            }
        }

        // Перебор объектов - Создать энумератор
        private void создатьЭнумераторToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (v != null)
            {
                vn = v.GetEnumerator();
                toolStripStatusLabel1.Text = "Энумератор создан.";
            }
            else
            {
                toolStripStatusLabel1.Text = "Список не создан!";
            }
        }

        // Перебор объектов - MoveNext (ИСПРАВЛЕНО)
        private void moveNextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (vn != null)
                {
                    if (vn.MoveNext())
                    {
                        (vn.Current as CheckBox).CheckState = CheckState.Checked;
                        toolStripStatusLabel1.Text = $"O.K. Текущий: {(vn.Current as CheckBox).Text}";
                    }
                    else 
                    {
                        // Сбрасываем все галочки в конце перебора
                        foreach (CheckBox cb in panel1.Controls)
                        {
                            cb.CheckState = CheckState.Unchecked;
                        }
                        toolStripStatusLabel1.Text = "Перебор закончен!";
                    }
                }
                else 
                {
                    toolStripStatusLabel1.Text = "Энумератор объектов не создан!";
                }
            }
            catch (ListIsEmpty) 
            { 
                toolStripStatusLabel1.Text = "Список пуст!"; 
            }
        }

        // Перебор объектов - Current
        private void currentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (vn != null)
                {
                    CheckBox currentCheckBox = vn.Current as CheckBox;
                    if (currentCheckBox.Checked)
                        currentCheckBox.CheckState = CheckState.Unchecked;
                    else 
                        currentCheckBox.CheckState = CheckState.Checked;
                    toolStripStatusLabel1.Text = $"Переключен: {currentCheckBox.Text}";
                }
                else 
                {
                    toolStripStatusLabel1.Text = "Энумератор объектов не создан!";
                }
            }
            catch (InvalidOperationException) 
            { 
                toolStripStatusLabel1.Text = "Перебор не начат!"; 
            }
        }

        // Перебор объектов - Reset
        private void resetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (vn != null) 
            { 
                vn.Reset();
                // Сбрасываем все галочки
                foreach (CheckBox cb in panel1.Controls)
                {
                    cb.CheckState = CheckState.Unchecked;
                }
                toolStripStatusLabel1.Text = "Энумератор объектов обновлён!"; 
            }
            else 
            {
                toolStripStatusLabel1.Text = "Энумератор объектов не создан!";
            }
        }
    }
}