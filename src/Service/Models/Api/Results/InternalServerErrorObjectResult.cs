using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Models.Api.Results
{
    /// <summary>
    /// An <see cref="T:Microsoft.AspNetCore.Mvc.ObjectResult" /> that when executed performs content negotiation, formats the entity body, and
    /// will produce a <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError" /> response if negotiation and formatting succeed.
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorObjectResult" /> class.
        /// </summary>
        /// <param name="value">The content to format into the entity body.</param>
        public InternalServerErrorObjectResult(object value)
            : base(value)
        {
            StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
}