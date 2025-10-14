namespace GameStore.Entity
{
    /// <summary>
    /// Класс представляющий игру в магазине
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Уникальный идентификатор игры
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название игры
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Жанр игры
        /// </summary>
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Цена игры
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Процент скидки
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Цена со скидкой (вычисляемое свойство)
        /// </summary>
        public decimal DiscountedPrice => Price * (1 - DiscountPercentage / 100);
    }
}
