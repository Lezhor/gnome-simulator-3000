﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic.world.generators
{
    [CreateAssetMenu(fileName = "New World With Blocks Generator", menuName = "World Generator/World With Blocks Generator")]
    public class WorldWithBlocksGenerator : WorldGenerator
    {

        [SerializeField]
        [Range(0, 100)] 
        private int blockedTilesPercentage = 15;

        [SerializeField] 
        [Range(1, 9)] 
        private int maxBlockedTilesIn3x3Box = 2;
        [SerializeField] 
        [Range(1, 25)] 
        private int maxBlockedTilesIn5x5Box = 4;

        private Vector2Int _startPos = new(0, 0);

        private Node[,] _graph;

        protected override Node[,] GenerateWorldGraph(Vector2Int size)
        {
            _startPos = new Vector2Int(Mathf.FloorToInt(size.x / 2f), Mathf.FloorToInt(size.y / 2f));

            _graph = CreateBasicGraph(size);

            _graph[_startPos.x, _startPos.y].SetConnections(true, true, true, true);
            _graph[_startPos.x, _startPos.y].Value = 2;

            int blockedTilesCount = (int)(size.x * size.y * blockedTilesPercentage / 100f);

            for (int i = 0; i < blockedTilesCount; i++)
            {
                Vector2Int pos;
                int counter = 0;
                do
                {
                    pos = new Vector2Int(Random.Range(0, _graph.GetLength(0)), Random.Range(0, _graph.GetLength(1)));
                    Debug.Log("Trying to place ");
                    if (counter++ >= 100)
                    {
                        Debug.Log("Too many attempts to place Blocked Tile!!!");
                        break;
                    }
                } while (!CanBePlaced(pos));

                if (counter <= 99)
                {
                    _graph[pos.x, pos.y].Value = 1;
                }
                else
                {
                    Debug.Log("Only placed " + (i + 1) + " blocked Tiles out of " + blockedTilesCount);
                    break;
                }
            }

            return _graph;
        }

        private bool CanBePlaced(Vector2Int pos)
        {
            if (_graph[pos.x, pos.y].Value != 0)
            {
                Debug.Log("Tile has not value 0");
                return false;
            }

            Node tempBlockedNode = new Node(pos.x, pos.y)
            {
                Value = 1
            };
            if (GetNeighbours(_graph, pos).Where(node => node.Value != 0).Any(node => !Node.ConnectionsMatch(node, tempBlockedNode)))
            {
                Debug.Log("There are neighbours which don't match");
                return false;
            }

            return GetNodesIn3X3Box(pos).Count(node => node.Value == 1) < maxBlockedTilesIn3x3Box
                && GetNodesIn5X5Box(pos).Count(node => node.Value == 1) < maxBlockedTilesIn5x5Box;
        }

        private List<Node> GetNodesIn3X3Box(Vector2Int pos)
        {
            return GetNodesInBox(pos, 1);
        }

        private List<Node> GetNodesIn5X5Box(Vector2Int pos)
        {
            return GetNodesInBox(pos, 2);
        }

        private List<Node> GetNodesInBox(Vector2Int pos, int radius)
        {
            List<Node> list = new List<Node>();

            for (int y = pos.y - radius; y <= pos.y + radius; y++)
            {
                for (int x = pos.x - radius; x <= pos.x + radius; x++)
                {
                    if (InBounds(x, y) && (x != pos.x || y != pos.y))
                    {
                        list.Add(_graph[x, y]);
                    }
                }
            }

            return list;
        }

        protected override TileData GetTile(Node node)
        {
            return node.Value switch
            {
                1 => Registry.blockedTile,
                2 => Registry.nwse,
                _ => Registry.emptyTile
            };
        }

        protected override Vector2Int StartPos(Vector2Int size)
        {
            return _startPos;
        }
        
        private bool InBounds(Vector2Int v)
        {
            return InBounds(v.x, v.y);
        }

        private bool InBounds(int xCell, int yCell)
        {
            return xCell >= 0 && xCell < _graph.GetLength(0) && yCell >= 0 && yCell < _graph.GetLength(1);
        }
    }
}