using System;
using System.Globalization;     // разделитель дробной части из настроек Windows
using System.Windows.Forms;     // формы и элементы управления

namespace Calculator
{
    public partial class Form1 : Form   // главная форма, вторая часть в Form1.Designer.cs
    {
        private decimal firstNumber = 0;   // первое число операции
        private string operation = null;   // выбранная операция, null: операции нет
        private bool newInput = true;   // признак начала ввода нового числа

        private readonly string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;   // запятая или точка
        private const string Format = "0.############################";   // формат вывода без лишних нулей

        public Form1()   // конструктор формы
        {
            InitializeComponent();   // создание элементов из Designer
            btnDot.Text = separator;   // разделитель на кнопке по настройкам Windows
            txtDisplay.Text = "0";   // начальное значение экрана
        }

        private decimal GetValue()   // число с экрана
        {
            decimal value;   // результат разбора
            return decimal.TryParse(txtDisplay.Text, out value) ? value : 0;   // ноль при тексте «Ошибка»
        }

        private void ShowError()   // вывод ошибки и сброс состояния
        {
            txtDisplay.Text = "Ошибка";   // текст ошибки на экране
            firstNumber = 0;   // сброс первого числа
            operation = null;   // сброс операции
            newInput = true;   // следующая цифра начинает новое число
        }

        private bool TryCompute(decimal a, decimal b, string op, out decimal result)   // вычисление, результат: успех или ошибка
        {
            result = 0;   // значение по умолчанию

            try   // защита от переполнения
            {
                switch (op)   // выбор операции
                {
                    case "+":   // сложение
                        result = a + b;   // сумма
                        return true;   // успех
                    case "−":   // вычитание
                        result = a - b;   // разность
                        return true;   // успех
                    case "×":   // умножение
                        result = a * b;   // произведение
                        return true;   // успех
                    case "÷":   // деление
                        if (b == 0) return false;   // ошибка деления на ноль
                        result = a / b;   // частное
                        return true;   // успех
                }
            }
            catch (OverflowException)   // слишком большое число
            {
                return false;   // ошибка переполнения
            }

            return false;   // неизвестная операция
        }

        private void btnDigit_Click(object sender, EventArgs e)   // любая кнопка с цифрой
        {
            Button b = (Button)sender;   // нажатая кнопка

            if (newInput || txtDisplay.Text == "0")   // начало нового числа
                txtDisplay.Text = b.Text;   // замена текста цифрой
            else
                txtDisplay.Text += b.Text;   // дописывание цифры

            newInput = false;   // продолжение ввода числа
        }

        private void btnDot_Click(object sender, EventArgs e)   // кнопка разделителя
        {
            if (newInput)   // начало нового числа
            {
                txtDisplay.Text = "0" + separator;   // ноль с разделителем
                newInput = false;   // продолжение ввода числа
            }
            else if (!txtDisplay.Text.Contains(separator))   // проверка отсутствия разделителя
            {
                txtDisplay.Text += separator;   // дописывание разделителя
            }
        }

        private void btnOperator_Click(object sender, EventArgs e)   // любая кнопка операции
        {
            Button b = (Button)sender;   // нажатая кнопка

            if (operation != null && !newInput)   // цепочка: предыдущая операция не вычислена
            {
                decimal result;   // промежуточный результат
                if (!TryCompute(firstNumber, GetValue(), operation, out result))   // вычисление и проверка успеха
                {
                    ShowError();   // вывод ошибки
                    return;   // выход при ошибке
                }
                firstNumber = result;   // результат как первое число
                txtDisplay.Text = result.ToString(Format);   // вывод промежуточного результата
            }
            else
            {
                firstNumber = GetValue();   // число с экрана как первое
            }

            operation = b.Text;   // запоминание операции
            newInput = true;   // следующая цифра начинает новое число
        }

        private void btnEquals_Click(object sender, EventArgs e)   // кнопка «=»
        {
            if (operation == null) return;   // выход при отсутствии операции

            decimal result;   // итоговый результат
            if (!TryCompute(firstNumber, GetValue(), operation, out result))   // вычисление и проверка успеха
            {
                ShowError();   // вывод ошибки
                return;   // выход при ошибке
            }

            txtDisplay.Text = result.ToString(Format);   // вывод результата
            operation = null;   // сброс операции
            newInput = true;   // следующая цифра начинает новое число
        }

        private void btnClear_Click(object sender, EventArgs e)   // кнопка «C»
        {
            firstNumber = 0;   // сброс первого числа
            operation = null;   // сброс операции
            newInput = true;   // следующая цифра начинает новое число
            txtDisplay.Text = "0";   // сброс экрана
        }

        private void btnBackspace_Click(object sender, EventArgs e)   // кнопка «⌫»
        {
            if (newInput) return;   // выход: результат стирать нельзя

            string text = txtDisplay.Text;   // текущий текст экрана

            if (text.Length > 1 && !(text.Length == 2 && text.StartsWith("-")))   // проверка достаточной длины
                txtDisplay.Text = text.Substring(0, text.Length - 1);   // удаление последнего символа
            else
                txtDisplay.Text = "0";   // возврат к нулю
        }
    }
}