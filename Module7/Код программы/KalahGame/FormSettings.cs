using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
namespace KalahGame
{
    public partial class FormSettings : Form
    {
        // Выбранные настройки, которые будут применены при сохранении
        public Color SelectedBoardColor { get; private set; }
        public Color SelectedStoneColor { get; private set; }
        // Словарь для хранения исходных цветов, чтобы восстанавливать их после эффектов нажатия
        private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();
        public FormSettings()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Shown += FormSettings_Shown;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            // Добавление подсказок для кнопок
            ToolTip settingsTip = new ToolTip();
            settingsTip.SetToolTip(btnSave, "Сохранить текущие цвета доски и камней");
            settingsTip.SetToolTip(btnCancel, "Закрыть окно без сохранения");
            settingsTip.SetToolTip(btnWood, "Выбрать цвет доски: Дерево");
            settingsTip.SetToolTip(btnBlue, "Выбрать цвет доски: Синий");
            settingsTip.SetToolTip(btnPink, "Выбрать цвет доски: Розовый");
            settingsTip.SetToolTip(btnGravel, "Выбрать тип камней: Галька");
            settingsTip.SetToolTip(btnMarble, "Выбрать тип камней: Мрамор");
            settingsTip.SetToolTip(btnGems, "Выбрать тип камней: Драгоценные");
        }
        private void FormSettings_Shown(object sender, EventArgs e)
        {
            // Загрузка текущих настроек из конфигурации приложения
            SelectedBoardColor = Properties.Settings.Default.BoardColor;
            SelectedStoneColor = Properties.Settings.Default.StoneColor;
            var boardButtons = new List<Button> { btnWood, btnBlue, btnPink };
            var stoneButtons = new List<Button> { btnGravel, btnMarble, btnGems };
            // Подготовка группы кнопок: задача стиля и обработки событий
            SetupChoiceButtons(boardButtons, true);
            SetupChoiceButtons(stoneButtons, false);
            // Визуализация кнопок, соответствующие текущим сохраненным цветам
            HighlightButtonByColor(SelectedBoardColor, boardButtons);
            HighlightButtonByColor(SelectedStoneColor, stoneButtons);
        }
        // Настройка визуального стиля кнопок
        private void SetupChoiceButtons(List<Button> buttons, bool isBoard)
        {
            foreach (var btn in buttons)
            {
                originalColors[btn] = btn.BackColor; // Запоминание базового цвета для сброса эффектов
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                // Эффект нажатия: временное потемнение кнопки
                btn.MouseDown += (s, e) => btn.BackColor = ControlPaint.Dark(originalColors[btn], 0.2f);
                btn.MouseUp += (s, e) => btn.BackColor = originalColors[btn];
                // Обновление выбора пользователя при клике
                btn.Click += (s, e) =>
                {
                    if (isBoard) SelectedBoardColor = originalColors[btn];
                    else SelectedStoneColor = originalColors[btn];
                    HighlightSelection(btn, buttons);
                };
            }
        }
        // Поиск кнопки, соответствующей цвету, и установка на нее фокуса
        private void HighlightButtonByColor(Color color, List<Button> group)
        {
            Button foundBtn = null;
            foreach (var btn in group)
            {
                Color orig = originalColors[btn];
                if (orig.R == color.R && orig.G == color.G && orig.B == color.B)
                {
                    foundBtn = btn;
                    break;
                }
            }
            if (foundBtn != null) HighlightSelection(foundBtn, group);
            else if (group.Count > 0) HighlightSelection(group[0], group);
        }
        // Подсветка активного выбора - добавление рамки
        private void HighlightSelection(Button clickedBtn, List<Button> group)
        {
            foreach (var btn in group) btn.FlatAppearance.BorderSize = 0;
            clickedBtn.FlatAppearance.BorderColor = Color.White;
            clickedBtn.FlatAppearance.BorderSize = 4;
        }
        // Запись настроек в файл конфигурации и закрытие формы
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BoardColor = SelectedBoardColor;
            Properties.Settings.Default.StoneColor = SelectedStoneColor;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}