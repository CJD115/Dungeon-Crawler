using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap; // Reference to the tilemap for floor tiles
    [SerializeField]
    private TileBase floorTile, wallTop; // The tile asset used for floor tiles



    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        if (floorTile == null)
        {
            return;
        }
        if (floorPositions == null)
        {
            return;
        }
        var floorList = floorPositions.ToList();
        PaintTiles(floorList, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
        if (tilemap != null)
        {
            tilemap.RefreshAllTiles();
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        if (tilemap == null)
        {
            Debug.LogWarning($"Tilemap is null when trying to paint at {position}");
            return;
        }
        if (tile == null)
        {
            Debug.LogWarning($"Tile is null when trying to paint at {position}");
            return;
        }
        // The incoming positions are grid coordinates (cell coords). Use Vector3Int directly.
        var tilePosition = new Vector3Int(position.x, position.y, 0);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        if (floorTilemap != null)
        {
            floorTilemap.ClearAllTiles();
        }
        // Also clear walls if present
        if (wallTilemap != null)
        {
            wallTilemap.ClearAllTiles();
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        if (wallTilemap == null)
        {
            Debug.LogWarning($"wallTilemap is null, cannot paint wall at {position}");
            return;
        }
        if (wallTop == null)
        {
            Debug.LogWarning($"wallTop tile is null. Cannot paint wall at {position}");
            return;
        }
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    public void ClearWalls()
    {
        if (wallTilemap != null)
        {
            wallTilemap.ClearAllTiles();
        }
    }
}
