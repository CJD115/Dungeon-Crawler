using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

/// <summary>
/// Handles visualization of tilemaps, specifically painting floor tiles at given positions.
/// </summary>
public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap; // Reference to the tilemap for floor tiles
    [SerializeField]
    private TileBase floorTile; // The tile asset used for floor tiles

    /// <summary>
    /// Paints floor tiles at the specified positions.
    /// </summary>
    /// <param name="floorPositions">Positions to paint floor tiles at.</param>
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        if (floorTile == null)
        {
            Debug.LogWarning("TilemapVisualizer: floorTile is not assigned! No tiles will be painted.");
            return;
        }
        if (floorPositions == null)
        {
            Debug.LogWarning("TilemapVisualizer: floorPositions is null!");
            return;
        }
        var floorList = floorPositions.ToList();
        Debug.Log($"Painting {floorList.Count} floor tiles.");
        PaintTiles(floorList, floorTilemap, floorTile);
    }

    /// <summary>
    /// Paints tiles at the given positions on the specified tilemap.
    /// </summary>
    /// <param name="positions">Positions to paint tiles at.</param>
    /// <param name="tilemap">Tilemap to paint on.</param>
    /// <param name="tile">Tile asset to use.</param>
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            Debug.Log($"Painting tile at {position}");
            PaintSingleTile(tilemap, tile, position);
        }
    }

    /// <summary>
    /// Paints a single tile at the given position on the tilemap.
    /// </summary>
    /// <param name="tilemap">Tilemap to paint on.</param>
    /// <param name="tile">Tile asset to use.</param>
    /// <param name="position">Position to paint the tile at.</param>
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }
}
