using GameStore.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GameStore.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly Logic _gameLogic;
        private Game? _selectedGame;

        /// <summary>
        /// Инициализирует новый экземпляр формы с готовым экземпляром бизнес-логики.
        /// </summary>
        /// <param name="gameLogic">Экземпляр бизнес-логики.</param>
        public Form1(Logic gameLogic)
        {
            InitializeComponent();
            _gameLogic = gameLogic;
        }

        /// <summary>
        /// Обрабатывает событие загрузки формы для первоначальной настройки.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadInitialData();
            SetupDataGridView();
            RefreshGrid();
        }


        /// <summary>
        /// Загружает начальный набор демонстрационных данных в бизнес-логику.
        /// </summary>
        private void LoadInitialData()
        {
            var existing = _gameLogic.GetAllGames();
            if (!existing.Any())
            {
                _gameLogic.CreateGame("Stardew Valley", "Simulator", 299m, 10);
                _gameLogic.CreateGame("Hades", "Roguelike", 899m);
                _gameLogic.CreateGame("Factorio", "Simulator", 520m, 25);
                _gameLogic.CreateGame("Slay the Spire", "Roguelike", 515m);
            }
        }

        /// <summary>
        /// Настраивает колонки и внешний вид элемента DataGridView.
        /// </summary>
        private void SetupDataGridView()
        {
            gamesDataGridView.AutoGenerateColumns = false;
            gamesDataGridView.Columns.Clear();
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 40 });
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Название", Width = 150 });
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Genre", HeaderText = "Жанр", Width = 100 });
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Цена", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" } });
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiscountPercentage", HeaderText = "Скидка, %", Width = 70 });
            gamesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiscountedPrice", HeaderText = "Цена со скидкой", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" } });
            gamesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gamesDataGridView.MultiSelect = false;
        }

        /// <summary>
        /// Обновляет данные в таблице и опционально восстанавливает выделение указанной строки.
        /// </summary>
        private void RefreshGrid(int? idToSelect = null)
        {
            gamesDataGridView.DataSource = null;
            gamesDataGridView.DataSource = _gameLogic.GetAllGames();

            if (idToSelect != null)
            {
                foreach (DataGridViewRow row in gamesDataGridView.Rows)
                {
                    if (row.DataBoundItem is Game game && game.Id == idToSelect)
                    {
                        row.Selected = true;
                        gamesDataGridView.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
            else
            {
                gamesDataGridView.ClearSelection();
                ClearInputFields();
            }
        }

        /// <summary>
        /// Очищает поля для ввода текста и сбрасывает состояние кнопок и выбора.
        /// </summary>
        private void ClearInputFields()
        {
            txtTitle.Text = string.Empty;
            txtGenre.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtDiscount.Text = "0";
            _selectedGame = null;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Обрабатывает изменение выделенной строки в таблице для отображения данных в полях ввода.
        /// </summary>
        private void gamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (gamesDataGridView.SelectedRows.Count > 0)
            {
                _selectedGame = (Game)gamesDataGridView.SelectedRows[0].DataBoundItem;
                txtTitle.Text = _selectedGame.Title;
                txtGenre.Text = _selectedGame.Genre;
                txtPrice.Text = _selectedGame.Price.ToString("F2");
                txtDiscount.Text = _selectedGame.DiscountPercentage.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Добавить" для создания новой игры.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Название и Жанр не могут быть пустыми.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Пожалуйста, введите корректную цену.", "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtDiscount.Text, out decimal discount))
            {
                MessageBox.Show("Пожалуйста, введите корректную скидку.", "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _gameLogic.CreateGame(txtTitle.Text, txtGenre.Text, price, discount);
                RefreshGrid();
                ClearInputFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Изменить" для обновления данных выбранной игры.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedGame == null) return;

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Название и Жанр не могут быть пустыми.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || !decimal.TryParse(txtDiscount.Text, out decimal discount))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для цены и скидки.", "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _gameLogic.UpdateGame(_selectedGame.Id, txtTitle.Text, txtGenre.Text, price, discount);
                RefreshGrid(_selectedGame.Id);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Удалить" для удаления выбранной игры.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedGame == null) return;
            var confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить игру '{_selectedGame.Title}'?",
                              "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                _gameLogic.DeleteGame(_selectedGame.Id);
                RefreshGrid();
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки для группировки игр по жанру и вывода результата.
        /// </summary>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            var groupedGames = _gameLogic.GroupGamesByGenre();
            resultsTextBox.Clear();
            resultsTextBox.Text = "--- Группировка по жанрам ---\n\n";
            foreach (var group in groupedGames.OrderBy(g => g.Key))
            {
                resultsTextBox.Text += $"Жанр: {group.Key}\n";
                foreach (var game in group.Value)
                {
                    resultsTextBox.Text += $"\t- {game.Title}\n";
                }
                resultsTextBox.Text += "\n";
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки для снятия выделения в таблице и очистки полей ввода.
        /// </summary>
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            gamesDataGridView.ClearSelection();
            ClearInputFields();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки для показа списка игр со скидкой.
        /// </summary>
        private void btnShowDiscounted_Click(object sender, EventArgs e)
        {
            var discountedGames = _gameLogic.GetGamesWithDiscount();
            resultsTextBox.Clear();
            resultsTextBox.Text = "--- Игры со скидкой ---\n\n";

            if (!discountedGames.Any())
            {
                resultsTextBox.Text += "Таких игр не найдено.";
                return;
            }

            foreach (var game in discountedGames)
            {
                resultsTextBox.Text += $"{game.Title} ({game.DiscountPercentage}%)\n";
                resultsTextBox.Text += $"\t{game.Price:F2} ₽ -> {game.DiscountedPrice:F2} ₽\n\n";
            }
        }
    }
}
