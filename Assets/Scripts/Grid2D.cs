using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    //public Vector3 gridWorldSize;
    //public float nodeRadius;
    //public Node2D[,] TestGrid;
    //public Tilemap obstaclemap;
    //public List<Node2D> path;
    //Vector3 worldBottomLeft;

    //float nodeDiameter;
    //public int gridSizeX, gridSizeY;
    
    
    //void Awake()
    //{
    //    nodeDiameter = nodeRadius * 2;
    //    gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
    //    gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
    //    CreateGrid();
    //}

    //void CreateGrid()
    //{
    //    TestGrid = new Node2D[gridSizeX, gridSizeY];
    //    //Collision check
    //    worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

    //    for (int x = 0; x < gridSizeX; x++)
    //    {
    //        for (int y = 0; y < gridSizeY; y++)
    //        {
    //            Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                
    //            TestGrid[x, y] = new Node2D(false, worldPoint, x, y);
    //            //Looking for the rocks
    //            if (obstaclemap.HasTile(obstaclemap.WorldToCell(TestGrid[x, y].worldPosition)))
    //                TestGrid[x, y].SetObstacle(true);
    //            else
    //                TestGrid[x, y].SetObstacle(false);
    //        }
    //    }
    //}
    
    //public List<Node2D> GetNeighbors(Node2D node)
    //{
    //    List<Node2D> neighbors = new List<Node2D>();

    //    //checks top
    //    if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < gridSizeY)
    //        neighbors.Add(TestGrid[node.GridX, node.GridY + 1]);

    //    //checks bottom
    //    if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < gridSizeY)
    //        neighbors.Add(TestGrid[node.GridX, node.GridY - 1]);

    //    //checks right
    //    if (node.GridX + 1 >= 0 && node.GridX + 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
    //        neighbors.Add(TestGrid[node.GridX + 1, node.GridY]);

    //    //checks left
    //    if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
    //        neighbors.Add(TestGrid[node.GridX - 1, node.GridY]);
        
    //    return neighbors;
    //}

    ////Finding the node from the world coordinates for the player
    //public Node2D NodeFromWorldPoint(Vector3 worldPosition)
    //{

    //    int x = Mathf.RoundToInt(worldPosition.x - 1 + (gridSizeX / 2));
    //    int y = Mathf.RoundToInt(worldPosition.y + (gridSizeY / 2));
    //    return TestGrid[x, y];
    //}
    ////Visual representation
    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

    //    if (TestGrid != null)
    //    {
    //        foreach (Node2D n in TestGrid)
    //        {
    //            if (n.obstacle)
    //                Gizmos.color = Color.red;
    //            else
    //                Gizmos.color = Color.white;

    //            if (path != null && path.Contains(n))
    //                Gizmos.color = Color.black;
    //            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeRadius));

    //        }
    //    }
    //}
}