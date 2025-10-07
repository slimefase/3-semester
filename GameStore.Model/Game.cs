namespace GameStore.Model
{
    public class Game : IDomainObject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }

        /// <summary>
        /// Процент скидки на игру (от 0 до 100).
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Возвращает цену с учетом примененной скидки.
        /// </summary>
        public decimal DiscountedPrice => Price * (1 - DiscountPercentage / 100);
    }
}
