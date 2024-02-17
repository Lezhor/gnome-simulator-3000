﻿namespace GameLogic.world.tiles.actions
{
    public abstract class TileAction
    {

        public bool Executed { get; private set; }

        protected WorldTile Tile { get; }

        protected TileAction(WorldTile tile)
        {
            Tile = tile;
        }

        public abstract void OnSetVisibility(bool state);

        public abstract void OnSetExplored(bool state);

        public abstract void OnPlayerEnterTile(PlayerController player);

        public abstract void OnPlayerExitTile(PlayerController player);

        public void Invoke(PlayerController player)
        {
            if (!Executed)
            {
                BeforeAction(player);
                if (PerformAction(player))
                {
                    Executed = true;
                    Tile.RedrawOnTilemaps();
                }
            }
        }
        
        protected abstract bool PerformAction(PlayerController player);

        protected virtual void BeforeAction(PlayerController player)
        {
        }
    }
}