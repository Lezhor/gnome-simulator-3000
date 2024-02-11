using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "World/Tile Data")]
public class TileData : ScriptableObject
{
    
    [Header("Ressources")]
    public TileBase imageFloor;
    public TileBase imageWalls;
    
    [Header("Connections")]
    public bool connectsTop = false;
    public bool connectsRight = false;
    public bool connectsBottom = false;
    public bool connectsLeft = false;

    [Header("Constraints")]
    public bool mustConnect = true;
    public bool destroyable = false;


}