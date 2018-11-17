using System;

namespace BoardGame.ServiceClient
{
    /// <summary>
    /// The result of the login.
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Gets or sets the token string which has to be used in the request header as a bearer token.
        /// </summary>
        public string TokenString { get; set; }

        /// <summary>
        /// Gets or sets the end of token validity. UTC.
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the username of the token.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logged in user is a bot account
        /// </summary>
        public bool IsBot { get; set; }

        /// <summary>
        /// Gets or sets the e-mail address associated with the account.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
