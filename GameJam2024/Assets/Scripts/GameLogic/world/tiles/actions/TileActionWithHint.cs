﻿using UnityEngine;

namespace GameLogic.world.tiles.actions
{
    public abstract class TileActionWithHint : TileAction
    {
        private string _hint;

        protected TileActionWithHint(WorldTile tile, string sound, string hint) : base(tile, sound)
        {
            _hint = hint;
        }

        public override void OnPlayerEnterTile(PlayerController player)
        {
            Debug.Log("Player entered");
            if (!Executed)
            {
                GameManager.Instance.MessageManager.InvokePermanentMessage(Tile.Pos2D, _hint);
            }
        }

        public override void OnPlayerExitTile(PlayerController player)
        {
            Debug.Log("Player exited");
            if (!Executed)
            {
                GameManager.Instance.MessageManager.HidePermanentMessage(Tile.Pos2D);
            }
        }

        protected override void BeforeAction(PlayerController player)
        {
            base.BeforeAction(player);
            GameManager.Instance.MessageManager.HidePermanentMessage(Tile.Pos2D);
        }
    }
}