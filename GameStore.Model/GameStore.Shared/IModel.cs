using GameStore.Entity;

namespace GameStore.Shared
{
    public interface IModel
    {
        /// <summary>
        /// Добавить игру
        /// </summary>
        void Add(Game game);

        /// <summary>
        /// Удалить игру
        /// </summary>
        void Delete(Game game);

        /// <summary>
        /// Получить все игры
        /// </summary>
        List<Game> ReadAll();

        /// <summary>
        /// Получить игру по идентификатору
        /// </summary>
        Game ReadById(int id);

        /// <summary>
        /// Обновить игру
        /// </summary>
        void Update(Game game);

        /// <summary>
        /// Получить игры со скидкой
        /// </summary>
        List<Game> GetDiscountedGames();

        /// <summary>
        /// Сгруппировать игры по жанру
        /// </summary>
        Dictionary<string, List<Game>> GroupByGenre();
    }
}
