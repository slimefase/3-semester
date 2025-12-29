using System.Collections.ObjectModel;
using System.Windows;
using GameStore.Shared;
using GameStore.Entity;
using System.Linq;

namespace GameStore.Presenter
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IModel _logic;
        private GameDto? _selectedGame;

        private string _inputTitle = string.Empty;
        private string _inputGenre = string.Empty;
        private string _inputPrice = "0";
        private string _inputDiscount = "0";

        /// <summary>
        /// Конструктор с инициализацией команд и загрузкой данных
        /// </summary>
        public MainViewModel(IModel logic)
        {
            _logic = logic;
            Games = new ObservableCollection<GameDto>();

            AddCommand = new RelayCommand(_ => AddGame());
            UpdateCommand = new RelayCommand(_ => UpdateGame(), _ => SelectedGame != null);
            DeleteCommand = new RelayCommand(_ => DeleteGame(), _ => SelectedGame != null);
            ShowDiscountedCommand = new RelayCommand(_ => ShowDiscounted());
            GroupByGenreCommand = new RelayCommand(_ => GroupByGenre());

            RefreshData();
        }

        /// <summary>
        /// Коллекция игр для отображения в таблице
        /// </summary>
        public ObservableCollection<GameDto> Games { get; }

        /// <summary>
        /// Текущая выбранная игра в списке, обновляет поля ввода при изменении
        /// </summary>
        public GameDto? SelectedGame
        {
            get => _selectedGame;
            set
            {
                if (SetProperty(ref _selectedGame, value) && value != null)
                {
                    InputTitle = value.Title;
                    InputGenre = value.Genre;
                    InputPrice = value.Price.ToString();
                    InputDiscount = value.DiscountPercentage.ToString();
                }
            }
        }

        public string InputTitle { get => _inputTitle; set => SetProperty(ref _inputTitle, value); }
        public string InputGenre { get => _inputGenre; set => SetProperty(ref _inputGenre, value); }
        public string InputPrice { get => _inputPrice; set => SetProperty(ref _inputPrice, value); }
        public string InputDiscount { get => _inputDiscount; set => SetProperty(ref _inputDiscount, value); }

        /// <summary>
        /// Добавление новой игры
        /// </summary>
        public RelayCommand AddCommand { get; }

        /// <summary>
        /// Обновление выбранной игры
        /// </summary>
        public RelayCommand UpdateCommand { get; }

        /// <summary>
        /// Удаление выбранной игры
        /// </summary>
        public RelayCommand DeleteCommand { get; }

        /// <summary>
        /// Отображение игр со скидкой
        /// </summary>
        public RelayCommand ShowDiscountedCommand { get; }

        /// <summary>
        /// Группировка игр по жанрам
        /// </summary>
        public RelayCommand GroupByGenreCommand { get; }

        /// <summary>
        /// Загружает актуальные данные из модели и обновляет список
        /// </summary>
        private void RefreshData()
        {
            Games.Clear();
            var data = _logic.ReadAll();
            foreach (var g in data)
            {
                Games.Add(new GameDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Genre = g.Genre,
                    Price = g.Price,
                    DiscountPercentage = g.DiscountPercentage
                });
            }
        }

        /// <summary>
        /// Выполняет логику добавления игры с валидацией
        /// </summary>
        private void AddGame()
        {
            if (string.IsNullOrWhiteSpace(InputTitle)) return;

            decimal.TryParse(InputPrice, out decimal p);
            decimal.TryParse(InputDiscount, out decimal d);

            _logic.Add(new Game { Title = InputTitle, Genre = InputGenre, Price = p, DiscountPercentage = d });
            RefreshData();
            ClearFields();
        }

        /// <summary>
        /// Выполняет логику обновления данных существующей игры
        /// </summary>
        private void UpdateGame()
        {
            if (SelectedGame == null) return;
            decimal.TryParse(InputPrice, out decimal p);
            decimal.TryParse(InputDiscount, out decimal d);

            var entity = _logic.ReadById(SelectedGame.Id);
            if (entity != null)
            {
                entity.Title = InputTitle; entity.Genre = InputGenre;
                entity.Price = p; entity.DiscountPercentage = d;
                _logic.Update(entity);
                RefreshData();
            }
        }

        /// <summary>
        /// Выполняет логику удаления игры из базы
        /// </summary>
        private void DeleteGame()
        {
            if (SelectedGame == null) return;
            var entity = _logic.ReadById(SelectedGame.Id);
            if (entity != null) _logic.Delete(entity);
            RefreshData();
            ClearFields();
        }

        /// <summary>
        /// Показывает сообщение со списком игр, имеющих скидку
        /// </summary>
        private void ShowDiscounted()
        {
            var res = _logic.GetDiscountedGames();
            string msg = string.Join("\n", res.Select(x => $"{x.Title}: {x.DiscountedPrice} руб."));
            MessageBox.Show(msg, "Скидки");
        }

        /// <summary>
        /// Показывает отчет с количеством игр по каждому жанру
        /// </summary>
        private void GroupByGenre()
        {
            var groups = _logic.GroupByGenre();
            string res = "Группировка по жанрам:\n";
            foreach (var g in groups) res += $"{g.Key}: {g.Value.Count()} шт.\n";
            MessageBox.Show(res, "Отчет");
        }

        /// <summary>
        /// Очищает поля ввода и сбрасывает выбор в таблице
        /// </summary>
        private void ClearFields()
        {
            InputTitle = ""; InputGenre = ""; InputPrice = "0"; InputDiscount = "0";
            SelectedGame = null;
        }
    }
}