using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Models.Api.Results
{
    /// <summary>
    /// An <see cref="T:Microsoft.AspNetCore.Mvc.ObjectResult" /> that when executed performs content negotiation, formats the entity body, and
    /// will produce a <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict" /> response if negotiation and formatting succeed.
    /// </summary>
    public class ConflictObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictObjectResult" /> class.
        /// </summary>
        /// <param name="value">The content to format into the entity body.</param>
        public ConflictObjectResult(object value)
            : base(value)
        {
            StatusCode = (int) HttpStatusCode.Conflict;
        }
    }
}