namespace BoardGame.Service.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Extensions;
    using BoardGame.Service.Models.Data;
    using BoardGame.Service.Models.Data.Moves;

    /// <summary>
    /// The database context of the application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _config;

        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        /// <summary>
        /// Gets or sets the chess games in the database.
        /// </summary>
        public DbSet<DbChessGame> ChessGames { get; set; }

        /// <inheritdoc />>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetMainConnectionString());
        }

        /// <inheritdoc />>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbBaseMove>()
                .ToTable("ChessMoves")
                .HasDiscriminator<string>("ChessMoveType")
                .HasValue<DbKingCastlingMove>(nameof(DbKingCastlingMove))
                .HasValue<DbPawnEnPassantMove>(nameof(DbPawnEnPassantMove))
                .HasValue<DbPawnPromotionalMove>(nameof(DbPawnPromotionalMove))
                .HasValue<DbSpecialMove>(nameof(DbSpecialMove))
                .HasValue<DbChessMove>(nameof(DbChessMove));
        }
    }
}
