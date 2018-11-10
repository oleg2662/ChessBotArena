using BoardGame.Service.Models.Api.Results;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Service.Controllers.Api
{
    /// <summary>
    /// Base controller class for the other API controllers.
    /// Contains some utility methods.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Creates a <see cref="ForbiddenResult" /> object that produces an empty
        /// <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden" /> response.
        /// </summary>
        /// <returns>The created <see cref="ForbiddenResult" /> object for the response.</returns>
        [NonAction]
        public virtual ForbiddenResult Forbidden()
        {
            return new ForbiddenResult();
        }

        /// <summary>
        /// Creates an <see cref="ForbiddenObjectResult" /> object that produces an <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden" /> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="ForbiddenObjectResult" /> for the response.</returns>
        [NonAction]
        public virtual ForbiddenObjectResult Forbidden(object value)
        {
            return new ForbiddenObjectResult(value);
        }

        /// <summary>
        /// Creates a <see cref="InternalServerErrorResult" /> object that produces an empty
        /// <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError" /> response.
        /// </summary>
        /// <returns>The created <see cref="InternalServerErrorResult" /> object for the response.</returns>
        [NonAction]
        public virtual InternalServerErrorResult InternalServerError()
        {
            return new InternalServerErrorResult();
        }

        /// <summary>
        /// Creates an <see cref="InternalServerErrorObjectResult" /> object that produces an <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError" /> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="InternalServerErrorObjectResult" /> for the response.</returns>
        [NonAction]
        public virtual InternalServerErrorObjectResult InternalServerError(object value)
        {
            return new InternalServerErrorObjectResult(value);
        }

        /// <summary>
        /// Returns the name of the currently authorized user.
        /// </summary>
        /// <returns>Name of the user. If it's empty or null, then there is no user logged in.</returns>
        protected string GetCurrentUser()
        {
            return User?.Identity?.Name;
        }
    }
}
