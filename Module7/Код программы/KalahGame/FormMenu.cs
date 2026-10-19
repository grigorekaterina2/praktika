using System;
using System.Windows.Forms;
using System.Windows.Media;
namespace KalahGame
{
    public partial class FormMenu : Form
    {
        // Статический объект для управления звуком
        public static MediaPlayer player = new MediaPlayer();
        // Проверка загружен ли объект
        private static bool isInitialized = false;
        public FormMenu()
        {
            InitializeComponent();
            // Процесс настройки музыкального проигрывателя
            if (!isInitialized)
            {
                string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "background_music.wav");
                player.Open(new Uri(musicPath));
                player.MediaEnded += (s, e) => { player.Position = TimeSpan.Zero; player.Play(); };
                isInitialized = true;
            }
            // Включение музыки, если она выбрана
            if (Properties.Settings.Default.IsMusicOn) player.Play();
            // Подсказки при наведении мыши 
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnStart, "Запуск новой партии");
            toolTip.SetToolTip(btnSettings, "Переход к настройкам игры");
            toolTip.SetToolTip(btnRules, "Отображение правил игры");
            toolTip.SetToolTip(btnExit, "Завершение работы программы");
            toolTip.SetToolTip(btnSound, "Управление звуковыми эффектами");
            toolTip.SetToolTip(btnMusic, "Управление фоновой музыкой");
            // Загрузка настроек из памяти
            rbPvc.Checked = Properties.Settings.Default.IsAgainstComputer;
            rbPvp.Checked = !Properties.Settings.Default.IsAgainstComputer;
            int diff = Properties.Settings.Default.DifficultyLevel;
            rbEasy.Checked = (diff == 1);
            rbMed.Checked = (diff == 2);
            rbHard.Checked = (diff == 3);
            // Обновление текстовых индикаторов кнопок
            btnSound.Text = Properties.Settings.Default.IsSoundOn ? "🔊" : "🔇";
            btnMusic.Text = Properties.Settings.Default.IsMusicOn ? "🎵" : "🔕";
            // Событие изменения режима игры
            rbPvc.CheckedChanged += (sender, e) => UpdateDifficultyAccess();
            rbPvp.CheckedChanged += (sender, e) => UpdateDifficultyAccess();
            UpdateDifficultyAccess();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Исключение флага системного меню - отключение крестика и Alt+F4.
                cp.Style &= ~0x00080000;
                return cp;
            }
        }
        // Блокировки выбора сложности при выборе режима "Игрок 1 vs Игрок 2"
        private void UpdateDifficultyAccess()
        {
            bool isPvC = rbPvc.Checked;
            rbEasy.Enabled = isPvC;
            rbMed.Enabled = isPvC;
            rbHard.Enabled = isPvC;
        }
        // Обработчик события запуска игры
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Сохранение выбранных пользователем параметров
            Properties.Settings.Default.IsAgainstComputer = rbPvc.Checked;
            Properties.Settings.Default.DifficultyLevel = rbPvc.Checked ? (rbEasy.Checked ? 1 : (rbMed.Checked ? 2 : 3)) : 1;
            Properties.Settings.Default.StonesCount = rb4.Checked ? 4 : (rb5.Checked ? 5 : 6);
            Properties.Settings.Default.Save();
            // Переход к основному игровому окну
            FormGame game = new FormGame();
            game.Show();
            this.Hide();
        }
        // Обработчик события изменения музыки
        private void btnMusic_Click(object sender, EventArgs e)
        {
            // Изменение состояния и сохранение настроек
            Properties.Settings.Default.IsMusicOn = !Properties.Settings.Default.IsMusicOn;
            Properties.Settings.Default.Save();
            // Запуск или остановка плеера
            if (Properties.Settings.Default.IsMusicOn) player.Play();
            else player.Stop();
            // Смена значка кнопки
            btnMusic.Text = Properties.Settings.Default.IsMusicOn ? "🎵" : "🔕";
        }
        // Обработчик события изменения звуковых эффектов
        private void btnSound_Click(object sender, EventArgs e)
        {
            // Обновление состояния звуковых эффектов
            Properties.Settings.Default.IsSoundOn = !Properties.Settings.Default.IsSoundOn;
            Properties.Settings.Default.Save();
            btnSound.Text = Properties.Settings.Default.IsSoundOn ? "🔊" : "🔇";
        }
        // Обработчики кнопок выхода, правил, настроек 
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
        private void btnRules_Click(object sender, EventArgs e) => new FormRules().ShowDialog();
        private void btnSettings_Click(object sender, EventArgs e) => new FormSettings().ShowDialog();
    }
}