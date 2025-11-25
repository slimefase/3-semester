using System;
using System.Linq;
using GameStore.Shared;
using GameStore.Entity;

namespace GameStore.BusinessLogic
{
    public class Presenter
    {
        private readonly IView _view;
        private readonly IModel _model;

        /// <summary>
        /// Инициализирует презентер и подписывается на события представления
        /// </summary>
        public Presenter(IView view, IModel model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            _view.AddClicked += OnAddClicked;
            _view.UpdateClicked += OnUpdateClicked;
            _view.DeleteClicked += OnDeleteClicked;
            _view.ClearSelectionClicked += OnClearSelectionClicked;
            _view.ShowDiscountedClicked += OnShowDiscountedClicked;
            _view.GroupByGenreClicked += OnGroupByGenreClicked;
            _view.SelectionChanged += OnSelectionChanged;

            RefreshView();
        }

        /// <summary>
        /// Обновляет таблицу в представлении из модели
        /// </summary>
        private void RefreshView()
        {
            var games = _model.ReadAll();
            _view.DisplayGames(games);
            _view.ClearEditor();
            _view.DisplayResults(string.Empty);
        }

        /// <summary>
        /// Обработчик добавления новой игры
        /// </summary>
        private void OnAddClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.Title) ||
                string.IsNullOrWhiteSpace(_view.Genre))
            {
                _view.DisplayResults("Заполните все поля корректно.");
                return;
            }

            var game = new Game
            {
                Title = _view.Title,
                Genre = _view.Genre,
                Price = _view.Price,
                DiscountPercentage = _view.Discount
            };

            _model.Add(game);
            RefreshView();
        }

        /// <summary>
        /// Обработчик обновления выбранной игры
        /// </summary>
        private void OnUpdateClicked(object? sender, EventArgs e)
        {
            if (_view.SelectedId == null) return;

            var game = _model.ReadById(_view.SelectedId.Value);
            if (game == null) return;

            game.Title = _view.Title;
            game.Genre = _view.Genre;
            game.Price = _view.Price;
            game.DiscountPercentage = _view.Discount;

            _model.Update(game);
            RefreshView();
        }

        /// <summary>
        /// Обработчик удаления выбранной игры
        /// </summary>
        private void OnDeleteClicked(object? sender, EventArgs e)
        {
            if (_view.SelectedId == null) return;

            var game = _model.ReadById(_view.SelectedId.Value);
            if (game == null) return;

            _model.Delete(game);
            RefreshView();
        }

        /// <summary>
        /// Обработчик очистки выделения/редактора
        /// </summary>
        private void OnClearSelectionClicked(object? sender, EventArgs e)
        {
            _view.ClearEditor();
            RefreshView();
        }

        /// <summary>
        /// Обработчик запроса игр со скидкой
        /// </summary>
        private void OnShowDiscountedClicked(object? sender, EventArgs e)
        {
            var discounted = _model.GetDiscountedGames();
            if (discounted == null || discounted.Count == 0)
            {
                _view.DisplayResults("Нет игр со скидкой.");
                return;
            }

            _view.DisplayResults(string.Join(Environment.NewLine,
                discounted.Select(g => $"{g.Title} — {g.DiscountPercentage}%")));
        }

        /// <summary>
        /// Обработчик группировки по жанру
        /// </summary>
        private void OnGroupByGenreClicked(object? sender, EventArgs e)
        {
            var grouped = _model.GroupByGenre();
            if (grouped == null || grouped.Count == 0)
            {
                _view.DisplayResults("Нет данных для группировки.");
                return;
            }

            _view.DisplayResults(string.Join(Environment.NewLine,
                grouped.Select(g => $"{g.Key}: {string.Join(", ", g.Value.Select(x => x.Title))}")));
        }

        /// <summary>
        /// Обработчик изменения выделения — заполняет поля редактора
        /// </summary>
        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            if (_view.SelectedId == null)
            {
                _view.ClearEditor();
                return;
            }

            var game = _model.ReadById(_view.SelectedId.Value);
            if (game != null)
            {
                _view.SetEditorFields(game);
            }
        }
    }
}
