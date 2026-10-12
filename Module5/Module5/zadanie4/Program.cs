using System;
using System.Windows.Forms;

namespace ImageViewer
{
    internal static class Program   // класс запуска приложения
    {
        [STAThread]   // однопоточная модель для окон Windows
        static void Main()   // точка входа
        {
            Application.EnableVisualStyles();   // современный вид элементов
            Application.SetCompatibleTextRenderingDefault(false);   // стиль отрисовки текста
            Application.Run(new Form1());   // запуск главной формы
        }
    }
}