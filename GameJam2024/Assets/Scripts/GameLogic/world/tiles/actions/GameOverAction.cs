﻿using UnityEngine;

namespace GameLogic.world.tiles.actions
{
    public class GameOverAction : TileAction
    {
        private readonly bool _won;
        
        public GameOverAction(WorldTile tile, bool won) : base(tile)
        {
            _won = won;
        }

        public override void OnSetVisibility(bool state)
        {
        }

        public override void OnSetExplored(bool state)
        {
        }

        public override void OnPlayerEnterTile(PlayerController player)
        {
        }

        public override void OnPlayerExitTile(PlayerController player)
        {
        }

        protected override bool PerformAction(PlayerController player)
        {
            // TODO - End Game! - Event
            if (_won)
            {
                Debug.Log("Player won game!!!");
            }
            else
            {
                Debug.Log("Player lost game!!!");
            }

            return true;
        }
    }
}