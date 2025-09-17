using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0, 3)]
    private int corridorBrushSize = 0; // 0 = no widening, 1 = 3-wide, etc.
    [SerializeField, Range(0.1f, 1f)]
    private float roomPercentage = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomFloors = new HashSet<Vector2Int>();

        // Create corridors and get the raw corridor lists so we can widen them (video approach)
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        // First, union raw (thin) corridors so we can detect dead-ends on the thin layout
        HashSet<Vector2Int> rawCorridorFloor = new HashSet<Vector2Int>();
        foreach (var corridor in corridors)
        {
            rawCorridorFloor.UnionWith(corridor);
        }

        // Find dead ends on the raw corridors and create rooms there
        List<Vector2Int> deadEnds = FindAllDeadEnds(rawCorridorFloor);
        CreateRoomsAtDeadEnds(deadEnds, roomFloors);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Now decide corridor footprint: widen corridors if requested, otherwise use raw
        if (corridorBrushSize > 0)
        {
            HashSet<Vector2Int> widened = new HashSet<Vector2Int>();
            foreach (var corridor in corridors)
            {
                var expanded = IncreaseCorridorBrush3by3(corridor); // video helper
                widened.UnionWith(expanded);
            }
            floorPositions.UnionWith(widened);
        }
        else
        {
            floorPositions.UnionWith(rawCorridorFloor);
        }

        // Finally add rooms
        floorPositions.UnionWith(roomFloors);
        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighborsCount = 0;
            foreach (var direction in ProceduralGenerationAlgorithms.Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighborsCount++;
                }
            }
            if (neighborsCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercentage);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var corridors = new List<List<Vector2Int>>();
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            corridors.Add(corridor);
        }

        return corridors;
    }
    // Video-style 3x3 brush widening helper
    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        var result = new HashSet<Vector2Int>();
        foreach (var pos in corridor)
        {
            // 3x3 around the tile
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    result.Add(new Vector2Int(pos.x + x, pos.y + y));
                }
            }
        }
        return result.ToList();
    }
}