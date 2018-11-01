using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Models.Api.Results
{
    /// <summary>
    /// An <see cref="T:Microsoft.AspNetCore.Mvc.StatusCodeResult" /> that when executed will produce an empty
    /// <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError" /> response.
    /// </summary>
    public class InternalServerErrorResult : StatusCodeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorResult" /> class.
        /// </summary>
        public InternalServerErrorResult()
            : base((int)HttpStatusCode.InternalServerError)
        {
        }
    }
}
