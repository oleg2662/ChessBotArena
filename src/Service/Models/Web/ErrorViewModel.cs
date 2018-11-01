namespace BoardGame.Service.Models.Web
{
    /// <summary>
    /// The model for error handling and logging.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the id of the request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request id should be shown.
        /// True ir the request id is not empty.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}