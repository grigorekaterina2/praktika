using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using MediaColor = System.Windows.Media.Color;
using MediaPlayer = System.Windows.Media.MediaPlayer;

namespace KalahGame
{
    public partial class FormGame : Form
    {
        // Игровые данные и состояние
        private int[] pits = new int[14];
        private Rectangle[] pitRects = new Rectangle[14];
        private int currentPlayer = 1;
        private List<PointF[]> stonePositions = new List<PointF[]>();
        private bool isAgainstComputer;
        private int difficultyLevel;
        private bool isGameOver = false;
        // Переменные для анимации и визуальных эффектов
        private int highlightedKalah = -1; // -1 - отсутствие подсветки
        private Timer highlightTimer = new Timer();
        private Timer moveAnimationTimer = new Timer();
        private Queue<int> animationQueue = new Queue<int>();
        private System.Media.SoundPlayer dropSound = new System.Media.SoundPlayer("stone_drop.wav");
        private System.Media.SoundPlayer captureSound = new System.Media.SoundPlayer("capture_sound.wav");
        private int animationPlayer;
        private int animationCurrentPit;
        private int animationLastPit;
        private bool isAnimating = false;
        // Таймер для имитации раздумий компьютера
        private Timer computerTimer = new Timer();
        public FormGame(int stones, bool isAgainstComputer, int difficulty, Color boardColor)
        {
            InitializeComponent();
            this.isAgainstComputer = isAgainstComputer;
            this.difficultyLevel = difficulty;
            this.DoubleBuffered = true;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(240, 230, 200);
            // Синхронизация музыки при запуске окна игрового процесса
            if (Properties.Settings.Default.IsMusicOn) FormMenu.player.Play();
            // Настройка таймеров
            computerTimer.Tick += ComputerTimer_Tick;
            moveAnimationTimer.Interval = 300;
            moveAnimationTimer.Tick += MoveAnimationTimer_Tick;
            // Генерация случайных позиций камней в ячейках
            Random posRnd = new Random(123);
            for (int i = 0; i < 14; i++)
            {
                PointF[] positions = new PointF[50];
                for (int j = 0; j < 50; j++)
                    positions[j] = new PointF((float)(posRnd.NextDouble() * 40 - 20), (float)(posRnd.NextDouble() * 40 - 20));
                stonePositions.Add(positions);
            }
            // Установка начального количества камней
            for (int i = 0; i < 14; i++) pits[i] = (i == 6 || i == 13) ? 0 : stones;
            // Инициализация координат прямоугольников лунок
            for (int i = 0; i < 6; i++) pitRects[i] = new Rectangle(200 + i * 100, 320, 70, 70);
            for (int i = 7; i < 13; i++) pitRects[i] = new Rectangle(200 + (12 - i) * 100, 150, 70, 70);
            pitRects[6] = new Rectangle(810, 150, 80, 240);
            pitRects[13] = new Rectangle(90, 150, 80, 240);
            // Настройка таймера для визуальной подсветки
            highlightTimer.Interval = 1000;
            highlightTimer.Tick += (s, e) => {
                highlightedKalah = -1;
                highlightTimer.Stop();
                Invalidate();
            };
            // Обновление состояния кнопок
            UpdateSoundButtons();
            // Настройка всплывающих подсказок
            ToolTip gameToolTip = new ToolTip();
            gameToolTip.SetToolTip(btnSound, "Звуковые эффекты");
            gameToolTip.SetToolTip(btnMusic, "Фоновая музыка");
            gameToolTip.SetToolTip(btnRules, "Правила игры");
            gameToolTip.SetToolTip(btnMenu, "Вернуться в главное меню");
        }
        // Загрузка параметров игры из памяти
        public FormGame() : this(
            Properties.Settings.Default.StonesCount,
            Properties.Settings.Default.IsAgainstComputer,
            Properties.Settings.Default.DifficultyLevel,
            Properties.Settings.Default.BoardColor)
        { }
        // Основной метод отрисовки игрового поля
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Отрисовка доски с цветом из настроек
            Color boardColor = Properties.Settings.Default.BoardColor;
            using (GraphicsPath board = new GraphicsPath())
            {
                board.AddRoundedRectangle(60, 130, 840, 290, 50);
                // Создание градиента для объема лунок
                using (LinearGradientBrush woodBrush = new LinearGradientBrush(
                    new Rectangle(60, 130, 840, 290),
                    ControlPaint.Light(boardColor, 0.2f),
                    ControlPaint.Dark(boardColor, 0.2f),
                    90f))
                {
                    g.FillPath(woodBrush, board);
                    g.DrawPath(new Pen(ControlPaint.Dark(boardColor, 0.4f), 4), board);
                }
            }
            // Отрисовка ячеек и камней
            Color stoneColor = Properties.Settings.Default.StoneColor;
            for (int i = 0; i < 14; i++)
            {
                // Отрисовка самой лунки
                g.FillEllipse(new SolidBrush(ControlPaint.Dark(boardColor, 0.3f)), pitRects[i]);
                g.DrawEllipse(new Pen(ControlPaint.Dark(boardColor, 0.6f), 3), pitRects[i]);
                // Отрисовка камней внутри лунки
                for (int j = 0; j < Math.Min(pits[i], 50); j++)
                {
                    float cx = pitRects[i].X + pitRects[i].Width / 2f + stonePositions[i][j].X;
                    float cy = pitRects[i].Y + pitRects[i].Height / 2f + stonePositions[i][j].Y;
                    g.FillEllipse(new SolidBrush(stoneColor), cx - 6, cy - 6, 12, 12);
                    g.DrawEllipse(Pens.Black, cx - 6, cy - 6, 12, 12);
                }
                // Эффект подсветки при захвате
                if (highlightedKalah != -1 && highlightedKalah == i)
                {
                    Rectangle glowRect = pitRects[highlightedKalah];
                    glowRect.Inflate(4, 4);
                    using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(50, Color.LightGreen)))
                    {
                        g.FillEllipse(glowBrush, glowRect);
                    }
                    using (Pen glowPen = new Pen(Color.FromArgb(120, Color.LightGreen), 2))
                    {
                        g.DrawEllipse(glowPen, glowRect);
                    }
                }
                // Отображение количества камней числом
                string text = pits[i].ToString();
                Font font = new Font("Segoe UI", 12, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);
                float textX = pitRects[i].X + (pitRects[i].Width - textSize.Width) / 2;
                float textY = pitRects[i].Y + (pitRects[i].Height - textSize.Height) / 2;
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, 0, 0, 0)), textX - 2, textY, textSize.Width + 4, textSize.Height);
                g.DrawString(text, font, Brushes.WhiteSmoke, textX, textY);
            }
            // Индикация активного игрока
            Font playerFont = new Font("Segoe UI", 18, FontStyle.Bold);
            // Подсветка игрока 1
            string name1 = "Игрок 1";
            SizeF size1 = g.MeasureString(name1, playerFont);
            if (currentPlayer == 1)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddRoundedRectangle(450 - 20, 450, (int)size1.Width + 40, (int)size1.Height, 20);
                    g.FillPath(new SolidBrush(Color.FromArgb(80, Color.LightGreen)), path);
                }
            }
            g.DrawString(name1, playerFont, Brushes.Black, 450, 450);
            // Подсветка игрока 2 / компьютера
            string p2Name = isAgainstComputer ? "Компьютер" : "Игрок 2";
            SizeF size2 = g.MeasureString(p2Name, playerFont);
            if (currentPlayer == 2)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddRoundedRectangle(430 - 20, 80, (int)size2.Width + 40, (int)size2.Height, 20);
                    g.FillPath(new SolidBrush(Color.FromArgb(80, Color.LightGreen)), path);
                }
            }
            g.DrawString(p2Name, playerFont, Brushes.Black, 430, 80);
        }
        // Обработчик клика по полю
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (isAnimating) return;
            if (isAgainstComputer && currentPlayer == 2) return;
            for (int i = 0; i < 14; i++)
            {
                if (pitRects[i].Contains(e.Location))
                {
                    // Проверка корректности хода
                    bool isValidPlayer1Turn = (currentPlayer == 1 && i >= 0 && i <= 5);
                    bool isValidPlayer2Turn = (!isAgainstComputer && currentPlayer == 2 && i >= 7 && i <= 12);
                    if ((isValidPlayer1Turn || isValidPlayer2Turn) && pits[i] > 0)
                    {
                        ExecuteMove(i);
                        break;
                    }
                }
            }
        }
        // Подготовка данных для анимации хода
        private void ExecuteMove(int pitIndex)
        {
            if (isAnimating) return;
            animationPlayer = currentPlayer;
            animationCurrentPit = pitIndex;
            int stones = pits[pitIndex];
            pits[pitIndex] = 0;
            animationQueue.Clear();
            int cur = pitIndex;
            while (stones > 0)
            {
                cur = (cur + 1) % 14;
                // Пропуск противоположного калаха
                if (animationPlayer == 1 && cur == 13) continue;
                if (animationPlayer == 2 && cur == 6) continue;
                animationQueue.Enqueue(cur);
                stones--;
            }
            isAnimating = true;
            moveAnimationTimer.Start();
        }
        // Таймер анимации перекладывания камней
        private void MoveAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationQueue.Count > 0)
            {
                int pit = animationQueue.Dequeue();
                pits[pit]++;
                animationLastPit = pit;
                // Проигрывание звука падения камня 
                if (Properties.Settings.Default.IsSoundOn)
                {
                    System.Threading.Tasks.Task.Run(() => {
                        using (var player = new System.Media.SoundPlayer("stone_drop.wav"))
                        {
                            player.PlaySync();
                        }
                    });
                }
                Invalidate();
                return;
            }
            // Завершение анимации, применение правил
            moveAnimationTimer.Stop();
            isAnimating = false;
            int cur = animationLastPit;
            // Логика захвата
            bool isMySide = (animationPlayer == 1 && cur >= 0 && cur <= 5) ||
                            (animationPlayer == 2 && cur >= 7 && cur <= 12);
            if (pits[cur] == 1 && isMySide)
            {
                int opposite = 12 - cur;
                if (pits[opposite] > 0)
                {
                    int myKalah = (animationPlayer == 1) ? 6 : 13;
                    pits[myKalah] += (pits[opposite] + 1);
                    pits[cur] = 0;
                    pits[opposite] = 0;
                    if (Properties.Settings.Default.IsSoundOn) captureSound.Play();
                    highlightedKalah = myKalah;
                    highlightTimer.Start();
                }
            }
            // Передача хода
            bool landsInKalah = (animationPlayer == 1 && cur == 6) || (animationPlayer == 2 && cur == 13);
            if (!landsInKalah)
            {
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }
            Invalidate();
            CheckGameOver();
            // Ход компьютера
            if (isAgainstComputer && currentPlayer == 2 && !isGameOver)
            {
                computerTimer.Interval = difficultyLevel == 1 ? 1000 : difficultyLevel == 2 ? 700 : 400;
                computerTimer.Start();
            }
        }
        // Логика хода компьютера
        private void ComputerTimer_Tick(object sender, EventArgs e)
        {
            computerTimer.Stop();
            List<int> availablePits = new List<int>();
            for (int i = 7; i <= 12; i++) if (pits[i] > 0) availablePits.Add(i);
            if (availablePits.Count == 0) return;
            int bestPit = availablePits[0];
            double bestScore = double.MinValue;
            foreach (int pit in availablePits)
            {
                double score = EvaluateMove(pit, difficultyLevel);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestPit = pit;
                }
            }
            ExecuteMove(bestPit);
        }
        // Оценка хода в зависимости от сложности
        private double EvaluateMove(int pit, int level)
        {
            if (level == 1) return pits[pit] + new Random().Next(0, 11);
            if (level == 2) return CalculateScore(pit, pits);
            if (level == 3) return Minimax(pit, pits, 2, true);
            return 0;
        }
        private double CalculateScore(int pit, int[] currentPits)
        {
            int[] tempPits = (int[])currentPits.Clone();
            int stones = tempPits[pit];
            int cur = pit;
            while (stones > 0)
            {
                cur = (cur + 1) % 14;
                if (cur == 6) continue;
                tempPits[cur]++;
                stones--;
            }
            double score = 0;
            if (cur == 13) score += 5; // + к оценке за дополнительный ход
            if (tempPits[cur] == 1 && cur >= 7 && cur <= 12 && tempPits[12 - cur] > 0)
            {
                score += (tempPits[12 - cur] + 1) * 4; // Оценка захвата
            }
            return score;
        }
        private double Minimax(int pit, int[] board, int depth, bool isComputer)
        {
            if (depth == 0) return CalculateScore(pit, board);
            return CalculateScore(pit, board);
        }
        // Проверка завершения игры
        private void CheckGameOver()
        {
            if (isGameOver) return;
            int totalStones = 0;
            foreach (int count in pits) totalStones += count;
            int winThreshold = (totalStones / 2) + 1;
            bool earlyEnd = (pits[6] >= winThreshold || pits[13] >= winThreshold);
            bool p1Empty = true; for (int i = 0; i <= 5; i++) if (pits[i] > 0) p1Empty = false;
            bool p2Empty = true; for (int i = 7; i <= 12; i++) if (pits[i] > 0) p2Empty = false;
            if (p1Empty || p2Empty || earlyEnd)
            {
                isGameOver = true;
                computerTimer.Stop();
                moveAnimationTimer.Stop();
                if (!earlyEnd)
                {
                    for (int i = 0; i <= 5; i++) { pits[6] += pits[i]; pits[i] = 0; }
                    for (int i = 7; i <= 12; i++) { pits[13] += pits[i]; pits[i] = 0; }
                }
                Invalidate();
                string msg = (pits[6] > pits[13]) ? "Игрок 1 победил!" :
                             (pits[13] > pits[6]) ? (isAgainstComputer ? "Компьютер победил!" : "Игрок 2 победил!") :
                             "Ничья!";
                MessageBox.Show($"{msg}\nСчет: {pits[6]} : {pits[13]}", "Игра окончена");
                this.Close();
                new FormMenu().Show();
            }
        }
        // Обновление состояния кнопок звуковых эффектов и музыки
        private void UpdateSoundButtons()
        {
            btnSound.Text = Properties.Settings.Default.IsSoundOn ? "🔊" : "🔇";
            btnMusic.Text = Properties.Settings.Default.IsMusicOn ? "🎵" : "🔕";
        }
        // Обработчики кнопок
        private void btnMenu_Click(object sender, EventArgs e)
        {
            computerTimer.Stop();
            new FormMenu().Show();
            this.Close();
        }
        private void btnSound_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSoundOn = !Properties.Settings.Default.IsSoundOn;
            Properties.Settings.Default.Save();
            btnSound.Text = Properties.Settings.Default.IsSoundOn ? "🔊" : "🔇";
        }
        private void btnMusic_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsMusicOn = !Properties.Settings.Default.IsMusicOn;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.IsMusicOn) FormMenu.player.Play();
            else FormMenu.player.Stop();
            UpdateSoundButtons();
        }
        private void btnRules_Click(object sender, EventArgs e) => new FormRules().ShowDialog();
    }
    // Класс-расширение для рисования закругленных прямоугольников
    public static class GraphicsPathExtensions
    {
        public static void AddRoundedRectangle(this GraphicsPath path, int x, int y, int w, int h, int r)
        {
            path.AddArc(x, y, r, r, 180, 90); path.AddArc(x + w - r, y, r, r, 270, 90);
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90); path.AddArc(x, y + h - r, r, r, 90, 90);
            path.CloseFigure();
        }
    }
}
