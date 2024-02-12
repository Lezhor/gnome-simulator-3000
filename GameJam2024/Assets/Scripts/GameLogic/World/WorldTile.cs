using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * The data for the tile (images, connections, mustConnect) is stored in the _tileData.
 * However params vary from tile to tile are saved here: _x, _y, _visible, _dark
 */
public class WorldTile
{

    private TileData _tileData;
    public TileData Data => _tileData;
    
    // TODO add _tileContent (What's on the tile)
    
    private int _x;
    private int _y;

    private bool _visible;
    public bool IsVisible => _visible;
    private bool _explored;
    public bool IsExplored => _explored;
    
    private GameManager _gameManager;

    private Vector3Int Pos => new(_x, _y);

    public WorldTile(TileData tileData, int x, int y, GameManager gameManager) : this(tileData, x, y, gameManager, false, false)
    {
    }

    public WorldTile(TileData tileData, int x, int y, GameManager gameManager, bool visible, bool explored)
    {
        _tileData = tileData;
        _x = x;
        _y = y;
        _gameManager = gameManager;
        _visible = visible;
        _explored = explored;
        redrawOnTilemaps();
    }

    public void SetVisible(bool visible)
    {
        if (_visible != visible)
        {
            _visible = visible;
            redrawOnTilemaps();
        }
    }

    public void SetExplored(bool explored)
    {
        if (_explored != explored)
        {
            _explored = explored;
            if (_explored && !_visible)
            {
                _visible = true;
            }
            redrawOnTilemaps();
        }
    }

    private void redrawOnTilemaps()
    {
        if (_visible)
        {
            _gameManager.background.SetTile(Pos, _explored ? _tileData.imageFloor : _tileData.imageFloorDark);
            _gameManager.walls.SetTile(Pos, _explored ? _tileData.imageWalls : _tileData.imageWallsDark);
        }
        else
        {
            _gameManager.background.SetTile(Pos, _tileData.imageInvis);
            _gameManager.walls.SetTile(Pos, null);
        }
    }

}