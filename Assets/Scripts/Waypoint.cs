using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Color exploredColor;
    bool changedColor = false;
    public bool isExplored = false;
    public Waypoint exploredFrom;

    const int gridSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int
            (
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public void SetTopColour(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isExplored && !changedColor)
        {
            changedColor = true;
            SetTopColour(exploredColor);
        }
    }
}
