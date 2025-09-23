using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model
{
    /// <summary>
    /// Класс, представляющий игру в системе.
    /// </summary>
    public class Game : IDomainObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Процент скидки на игру (от 0 до 100).
        /// </summary>
        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Возвращает цену с учетом примененной скидки.
        /// </summary>
        [NotMapped]
        public decimal DiscountedPrice => Price * (1 - DiscountPercentage / 100);
    }
}
