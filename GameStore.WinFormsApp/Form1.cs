using System.Data;
using GameStore.Model;
using GameStore.DataAccessLayer;

namespace GameStore.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly Logic logic = new Logic(new EntityRepository());
        private int? selectedGameId = null;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка данных в таблицу при запуске.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGames();
        }

        /// <summary>
        /// Обновить таблицу игр и очистить поля.
        /// </summary>
        private void RefreshGames()
        {
            var games = logic.ReadAll();
            gamesDataGridView.DataSource = games.Select(g => new {
                g.Id,
                g.Title,
                g.Genre,
                g.Price,
                g.DiscountPercentage
            }).ToList();

            ClearEditor();
        }

        /// <summary>
        /// Очистить форму редактирования и снять выделение.
        /// </summary>
        private void ClearEditor()
        {
            selectedGameId = null;
            txtTitle.Text = "";
            txtGenre.Text = "";
            txtPrice.Text = "";
            txtDiscount.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            gamesDataGridView.ClearSelection();
        }

        /// <summary>
        /// Добавить новую игру.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtGenre.Text) ||
                !decimal.TryParse(txtPrice.Text, out var price) ||
                !decimal.TryParse(txtDiscount.Text, out var discount))
            {
                MessageBox.Show("Заполните все поля корректно!");
                return;
            }

            var game = new Game
            {
                Title = txtTitle.Text,
                Genre = txtGenre.Text,
                Price = price,
                DiscountPercentage = discount
            };

            logic.Add(game);
            RefreshGames();
        }

        /// <summary>
        /// Изменить выбранную игру.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedGameId == null) return;
            var game = logic.ReadById(selectedGameId.Value);
            if (game == null) return;

            game.Title = txtTitle.Text;
            game.Genre = txtGenre.Text;
            if (decimal.TryParse(txtPrice.Text, out var price)) game.Price = price;
            if (decimal.TryParse(txtDiscount.Text, out var discount)) game.DiscountPercentage = discount;

            logic.Update(game);
            RefreshGames();
        }

        /// <summary>
        /// Удалить выбранную игру.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedGameId == null) return;
            var game = logic.ReadById(selectedGameId.Value);
            if (game == null) return;

            logic.Delete(game);
            RefreshGames();
        }

        /// <summary>
        /// Снять выделение.
        /// </summary>
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }

        /// <summary>
        /// Выводит только игры со скидкой.
        /// </summary>
        private void btnShowDiscounted_Click(object sender, EventArgs e)
        {
            var discounted = logic.ReadAll().Where(g => g.DiscountPercentage > 0).ToList();
            if (discounted.Count == 0)
            {
                resultsTextBox.Text = "Нет игр со скидкой";
            }
            else
            {
                resultsTextBox.Text = string.Join(Environment.NewLine,
                    discounted.Select(g => $"{g.Title} — {g.DiscountPercentage}%"));
            }
        }

        /// <summary>
        /// Группирует игры по жанру.
        /// </summary>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            var games = logic.ReadAll();
            if (games.Count == 0)
            {
                resultsTextBox.Text = "Нет данных для группировки.";
                return;
            }

            var grouped = games.GroupBy(g => g.Genre)
                .Select(g => $"{g.Key}: {string.Join(", ", g.Select(x => x.Title))}");

            resultsTextBox.Text = string.Join(Environment.NewLine, grouped);
        }

        /// <summary>
        /// Заполняет редактор при клике по строке в таблице.
        /// </summary>
        private void gamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (gamesDataGridView.SelectedRows.Count == 0)
                return;

            var row = gamesDataGridView.SelectedRows[0];
            selectedGameId = Convert.ToInt32(row.Cells["Id"].Value);

            var game = logic.ReadById(selectedGameId.Value);
            if (game != null)
            {
                txtTitle.Text = game.Title;
                txtGenre.Text = game.Genre;
                txtPrice.Text = game.Price.ToString();
                txtDiscount.Text = game.DiscountPercentage.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}
