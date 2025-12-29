namespace GameStore.Presenter
{
    public class GameDto : ViewModelBase
    {
        private int _id;
        private string _title;
        private string _genre;
        private decimal _price;
        private decimal _discountPercentage;

        /// <summary>
        /// Уникальный идентификатор игры
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// Название игры
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Жанр игры
        /// </summary>
        public string Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        /// <summary>
        /// Цена игры
        /// </summary>
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        /// <summary>
        /// Размер скидки в процентах
        /// </summary>
        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        /// <summary>
        /// Форматированная строка для отображения в списке
        /// </summary>
        public string DisplayInfo => $"{Title} ({Genre}) - {Price} руб.";
    }
}