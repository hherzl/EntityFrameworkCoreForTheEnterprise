using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Requests
{
    /// <summary>
    /// Represents the model to create a new product
    /// </summary>
    public class PostProductRequest
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the product category ID
        /// </summary>
        [Required]
        public int? ProductCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the unit price
        /// </summary>
        [Required]
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
    }
}
