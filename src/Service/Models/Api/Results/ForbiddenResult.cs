using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Models.Api.Results
{
    /// <summary>
    /// An <see cref="T:Microsoft.AspNetCore.Mvc.StatusCodeResult" /> that when executed will produce an empty
    /// <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden" /> response.
    /// </summary>
    public class ForbiddenResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenResult" /> class.
        /// </summary>
        public ForbiddenResult()
            : base((int)HttpStatusCode.Forbidden)
        {
        }
    }
}