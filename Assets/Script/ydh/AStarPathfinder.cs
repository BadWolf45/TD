using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;

public class AStarPathfinder : MonoBehaviour
{
    private readonly TileBehaviour[,] tiles;
    private readonly int width,height;
    public AStarPathfinder(TileBehaviour[,] thiles, int width, int height)
    {
        this.tiles = thiles;
        this.width = width;
        this.height = height;
    }
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        HashSet<Vector2Int> closedSet = new();
        priorityQueue<Node> openset = new();
        Dictionary<Vector2Int, Node> allNodes =new();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
