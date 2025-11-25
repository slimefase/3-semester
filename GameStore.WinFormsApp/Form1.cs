using GameStore.BusinessLogic;
using GameStore.Entity;
using GameStore.Shared;
using Ninject;

namespace GameStore.WinFormsApp
{
    public partial class Form1 : Form, IView
    {
        private Presenter _presenter;
        private int? _selectedGameId = null;

        public event EventHandler AddClicked;
        public event EventHandler UpdateClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler ClearSelectionClicked;
        public event EventHandler ShowDiscountedClicked;
        public event EventHandler GroupByGenreClicked;
        public event EventHandler SelectionChanged;

        public Form1()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<Logic>();
            _presenter = new Presenter(this, logic);
        }

        /// <summary>
        /// Загрузка данных при старте формы
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Начальные действия выполняет Presenter
        }

        /// <summary>
        /// Отобразить список игр в таблице
        /// </summary>
        public void DisplayGames(List<Game> games)
        {
            gamesDataGridView.DataSource = games.Select(g => new
            {
                g.Id,
                g.Title,
                g.Genre,
                g.Price,
                g.DiscountPercentage
            }).ToList();
            gamesDataGridView.ClearSelection();
        }

        /// <summary>
        /// Отобразить резултатирующий текст
        /// </summary>
        public void DisplayResults(string text)
        {
            resultsTextBox.Text = text;
        }

        /// <summary>
        /// Заполнить поля редактора из объекта игры
        /// </summary>
        public void SetEditorFields(Game game)
        {
            _selectedGameId = game.Id;
            txtTitle.Text = game.Title;
            txtGenre.Text = game.Genre;
            txtPrice.Text = game.Price.ToString();
            txtDiscount.Text = game.DiscountPercentage.ToString();
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        /// <summary>
        /// Очистить поля редактора
        /// </summary>
        public void ClearEditor()
        {
            _selectedGameId = null;
            txtTitle.Text = "";
            txtGenre.Text = "";
            txtPrice.Text = "";
            txtDiscount.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            gamesDataGridView.ClearSelection();
        }

        /// <summary>
        /// Заголовок из поля ввода
        /// </summary>
        public string Title => txtTitle.Text;

        /// <summary>
        /// Жанр из поля ввода
        /// </summary>
        public string Genre => txtGenre.Text;

        /// <summary>
        /// Цена из поля ввода
        /// </summary>
        public decimal Price
        {
            get
            {
                if (decimal.TryParse(txtPrice.Text, out var p)) return p;
                return 0m;
            }
        }

        /// <summary>
        /// Скидка из поля ввода
        /// </summary>
        public decimal Discount
        {
            get
            {
                if (decimal.TryParse(txtDiscount.Text, out var d)) return d;
                return 0m;
            }
        }

        /// <summary>
        /// Идентификатор выбранной игры
        /// </summary>
        public int? SelectedId => _selectedGameId;

        /// <summary>
        /// Обработчик кнопки добавить
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик кнопки изменить
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик кнопки удалить
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик кнопки снять выделение
        /// </summary>
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            ClearSelectionClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик кнопки показать игры со скидкой
        /// </summary>
        private void btnShowDiscounted_Click(object sender, EventArgs e)
        {
            ShowDiscountedClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик кнопки сгруппировать
        /// </summary>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            GroupByGenreClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик изменения выделения в таблице — обновляет SelectedId и уведомляет Presenter
        /// </summary>
        private void gamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (gamesDataGridView.SelectedRows.Count == 0)
            {
                _selectedGameId = null;
                SelectionChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            var row = gamesDataGridView.SelectedRows[0];
            _selectedGameId = Convert.ToInt32(row.Cells["Id"].Value);
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
