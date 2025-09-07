using System.Collections.Generic;
using UnityEngine;

/// Contains algorithms for procedural generation, such as random walk for dungeon/path creation.
public static class ProceduralGenerationAlgorithms
{
    /// Performs a simple random walk starting from a given position.
    /// Returns a set of unique positions visited during the walk.
    /// <param name="startPosition">The starting position of the walk.</param>
    /// <param name="walkLength">The number of steps to take in the walk.</param>
    /// <returns>A HashSet of Vector2Int positions visited during the walk.</returns>
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        // Stores all unique positions visited during the walk
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Add the starting position to the path
        path.Add(startPosition);
        var previousPosition = startPosition;

        // Perform the walk for the specified number of steps
        for (int i = 0; i < walkLength; i++)
        {
            // Choose a random cardinal direction (up, down, left, right)
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            // Add the new position to the path (HashSet ensures uniqueness)
            path.Add(newPosition);
            // Update the previous position for the next iteration
            previousPosition = newPosition;
        }

        // Return all unique positions visited
        return path;
    }
}

/// Provides utility methods and data for working with 2D cardinal directions.
public static class Direction2D
{
    /// List of cardinal directions represented as Vector2Int:
    /// Up (0,1), Right (1,0), Down (0,-1), Left (-1,0)
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // Up
        new Vector2Int(1,0), // Right
        new Vector2Int(0,-1), // Down
        new Vector2Int(-1,0), // Left
    };
    /// Returns a random cardinal direction from the list.
    /// <returns>A Vector2Int representing a random cardinal direction.</returns>
    public static Vector2Int GetRandomCardinalDirection()
    {
        // Random.Range with integers is exclusive on the upper bound
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}