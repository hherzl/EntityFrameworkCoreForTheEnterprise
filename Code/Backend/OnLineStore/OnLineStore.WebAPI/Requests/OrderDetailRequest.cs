namespace OnLineStore.WebAPI.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDetailRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int? ProductID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Quantity { get; set; }
    }
}
