﻿using Game.Chess.Moves;
using System;

namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// The model used by the client to the service which contains the chess move and some additional information.
    /// </summary>
    public class ChessMoveApiModel
    {
        /// <summary>
        /// Gets or sets the ID of the target game.
        /// </summary>
        public Guid TargetGameId { get; set; }

        /// <summary>
        /// Gets or sets the chess move.
        /// </summary>
        public BaseChessMove Move { get; set; }
    }
}