using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    List<Waypoint> path = new List<Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    Waypoint wp;
    bool isRunning = true;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColourStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        Waypoint currentWaypoint = endWaypoint;
        while (currentWaypoint.exploredFrom != null)
        {
            path.Add(currentWaypoint);
            currentWaypoint = currentWaypoint.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            wp = queue.Dequeue();
            wp.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {
        if (wp == endWaypoint)
        {
            isRunning = false;
            print("End node found");
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) return;
        Vector2Int start = wp.GetGridPos();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int explorationCoords = start + direction;
            if (grid.ContainsKey(explorationCoords))
            {
                QueueNewNeighbours(explorationCoords);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int explorationCoords)
    {
        Waypoint value = grid[explorationCoords];
        if (!queue.Contains(value) && !value.isExplored)
        {
            queue.Enqueue(value);
            value.exploredFrom = wp;
        }
    }

    private void ColourStartAndEnd()
    {
        startWaypoint.SetTopColour(Color.green);
        endWaypoint.SetTopColour(Color.red);
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
            // overlapping blocks?
            // add to dictionary?
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
