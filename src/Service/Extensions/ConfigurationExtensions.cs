namespace BoardGame.Service.Extensions
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Shorthand to the configuration values.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the base URL of the service.
        /// </summary>
        /// <param name="root">The configuration root</param>
        /// <returns>The base URL of the service.</returns>
        public static string GetBaseUrl(this IConfiguration root)
        {
            return root["TheTurkEngineService:BaseUrl"];
        }

        /// <summary>
        /// Gets the connection string of the service's database.
        /// </summary>
        /// <param name="root">The configuration root.</param>
        /// <returns>The main connection string.</returns>
        public static string GetMainConnectionString(this IConfiguration root)
        {
            return root["TheTurkEngineService:ConnectionStrings:Main"];
        }

        /// <summary>
        /// Gets the security key used to sign the JWT tokens.
        /// </summary>
        /// <param name="root">The configuration root.</param>
        /// <returns>The security key used to sign the JWT tokens.</returns>
        public static string GetSecurityKey(this IConfiguration root)
        {
            return root["TheTurkEngineService:SecurityKey"];
        }
    }
}
