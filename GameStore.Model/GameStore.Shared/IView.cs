using GameStore.Entity;

namespace GameStore.Shared
{
    public interface IView
    {
        event EventHandler AddClicked;
        event EventHandler UpdateClicked;
        event EventHandler DeleteClicked;
        event EventHandler ClearSelectionClicked;
        event EventHandler ShowDiscountedClicked;
        event EventHandler GroupByGenreClicked;
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
