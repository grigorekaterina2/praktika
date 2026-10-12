using System;
using System.Windows.Forms;     // формы и элементы управления

namespace TaskManager
{
    public partial class Form1 : Form   // главная форма, вторая часть в Form1.Designer.cs
    {
        public Form1()   // конструктор формы
        {
            InitializeComponent();   // создание элементов из Designer
        }

        private void btnAdd_Click(object sender, EventArgs e)   // кнопка «Добавить»
        {
            string text = txtTask.Text.Trim();   // текст задачи без пробелов по краям

            if (text == "")   // проверка пустого ввода
            {
                MessageBox.Show("Введите текст задачи.", "Внимание");   // сообщение о пустом вводе
                return;   // выход без добавления
            }

            clbTasks.Items.Add(text, false);   // добавление задачи без отметки
            txtTask.Clear();   // очистка поля ввода
            txtTask.Focus();   // возврат курсора в поле ввода
        }

        private void txtTask_KeyDown(object sender, KeyEventArgs e)   // нажатие клавиши в поле ввода
        {
            if (e.KeyCode == Keys.Enter)   // проверка клавиши Enter
            {
                e.SuppressKeyPress = true;   // отключение звукового сигнала
                btnAdd_Click(sender, e);   // добавление задачи как по кнопке
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)   // кнопка «Удалить»
        {
            if (clbTasks.SelectedIndex == -1)   // проверка отсутствия выбора
            {
                MessageBox.Show("Выберите задачу для удаления.", "Внимание");   // сообщение об отсутствии выбора
                return;   // выход без удаления
            }

            clbTasks.Items.RemoveAt(clbTasks.SelectedIndex);   // удаление выбранной задачи
        }
    }
}