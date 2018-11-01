using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Models.Api.Results
{
    /// <summary>
    /// An <see cref="T:Microsoft.AspNetCore.Mvc.StatusCodeResult" /> that when executed will produce an empty
    /// <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict" /> response.
    /// </summary>
    public class ConflictResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictResult" /> class.
        /// </summary>
        public ConflictResult()
            : base((int)HttpStatusCode.Conflict)
        {
        }
    }
}