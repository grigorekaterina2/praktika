using System;
using System.IO;                // чтение и запись файлов
using System.Text;              // кодировка Encoding
using System.Windows.Forms;     // формы и элементы управления

namespace TextEditor
{
    public partial class Form1 : Form   // главная форма, вторая часть в Form1.Designer.cs
    {
        private string currentPath = null;   // путь к открытому файлу, null: файл не сохранён

        public Form1()   // конструктор формы
        {
            InitializeComponent();   // создание элементов из Designer
            UpdateTitle();   // начальный заголовок окна
        }

        private void UpdateTitle()   // обновление заголовка окна
        {
            string name = currentPath == null ? "Без имени" : Path.GetFileName(currentPath);   // имя файла
            this.Text = name + " - Текстовый редактор";   // заголовок с именем файла
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)   // пункт «Открыть»
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;   // выход при отмене выбора

            try   // защита от ошибок чтения
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);   // чтение файла в поле
                currentPath = openFileDialog1.FileName;   // запоминание пути
                UpdateTitle();   // обновление заголовка
            }
            catch (Exception ex)   // ошибка чтения
            {
                MessageBox.Show("Не удалось открыть файл:\n" + ex.Message, "Ошибка");   // сообщение об ошибке
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)   // пункт «Сохранить»
        {
            if (currentPath == null)   // проверка отсутствия пути
                SaveAs();   // выбор имени файла
            else
                SaveToFile(currentPath);   // запись в существующий файл
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)   // пункт «Сохранить как»
        {
            SaveAs();   // выбор нового имени файла
        }

        private void SaveAs()   // сохранение с выбором имени
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;   // выход при отмене выбора

            if (SaveToFile(saveFileDialog1.FileName))   // запись и проверка успеха
            {
                currentPath = saveFileDialog1.FileName;   // запоминание нового пути
                UpdateTitle();   // обновление заголовка
            }
        }

        private bool SaveToFile(string path)   // запись текста в файл, результат: успех или ошибка
        {
            try   // защита от ошибок записи
            {
                File.WriteAllText(path, textBox1.Text, Encoding.UTF8);   // запись текста в файл
                return true;   // успешная запись
            }
            catch (Exception ex)   // ошибка записи
            {
                MessageBox.Show("Не удалось сохранить файл:\n" + ex.Message, "Ошибка");   // сообщение об ошибке
                return false;   // неуспешная запись
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)   // пункт «Выход»
        {
            Close();   // закрытие окна
        }
    }
}