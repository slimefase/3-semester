using GameStore.Entity;

namespace GameStore.Shared
{
    public interface IView
    {
        /// <summary>
        /// Загрузка представления
        /// </summary>
        event EventHandler ViewLoaded;

        /// <summary>
        /// Нажатие кнопки добавления игры
        /// </summary>
        event EventHandler AddClicked;

        /// <summary>
        /// Нажатие кнопки обновления игры
        /// </summary>
        event EventHandler UpdateClicked;

        /// <summary>
        /// Нажатие кнопки удаления игры
        /// </summary>
        event EventHandler DeleteClicked;

        /// <summary>
        /// Очистка выбранной игры
        /// </summary>
        event EventHandler ClearSelectionClicked;

        /// <summary>
        /// Отображение игр со скидкой
        /// </summary>
        event EventHandler ShowDiscountedClicked;

        /// <summary>
        /// Группировка игр по жанру
        /// </summary>
        event EventHandler GroupByGenreClicked;

        /// <summary>
        /// Изменение выбранной игры
        /// </summary>
        event EventHandler SelectionChanged;


        string Title { get; }
        string Genre { get; }
        decimal Price { get; }
        decimal Discount { get; }
        int? SelectedId { get; }

        /// <summary>
        /// Отобразить список игр в представлении
        /// </summary>
        void DisplayGames(List<Game> games);

        /// <summary>
        /// Отобразить результирующий текст (сообщения, группировки и т.д.)
        /// </summary>
        void DisplayResults(string text);

        /// <summary>
        /// Заполнить поля редактора из переданной игры
        /// </summary>
        void SetEditorFields(Game game);

        /// <summary>
        /// Очистить поля редактора и снять выделение
        /// </summary>
        void ClearEditor();
    }
}
