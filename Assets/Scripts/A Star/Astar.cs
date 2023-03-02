using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public enum TileType { START, GOAL, WATER,GRASS,PATH}

public class Astar : MonoBehaviour
{
    private TileType tileType;

    [Header("Camera")]
    [SerializeField] private Camera camera;
    [Header("Laymask")]
    [SerializeField] private LayerMask layerMask;
    [Header("Tilmap")]
    [SerializeField] private Tilemap tilemap;
    [Header("Tile Array")]
    [SerializeField] private Tile[] tiles;
    [SerializeField] private Tile waterTiles;

    private Node current;

    private HashSet<Node> openList;
    private HashSet<Node> closedList;
    private HashSet<Vector3Int> changeTiles = new HashSet<Vector3Int>();

    private Stack<Vector3Int> path;


    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();

    private Vector3Int startPos, goalPos;

    private bool start, goal;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), 
                               Vector2.zero, Mathf.Infinity, layerMask);
            if(hit.collider != null)
            {
                Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPos = tilemap.WorldToCell(mouseWorldPos);

                ChangeTile(clickPos);
            }
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    // run my algorithm
        //    Algoithm();
        //}
    }

    private void Initialized()
    {
        current = GetNode(startPos);

        openList = new HashSet<Node>();        /// ahmmmmmm 
        closedList = new HashSet<Node>();

        // adding start to the open list
        openList.Add(current);                         // not sure if this code is running
    }

    public void Algoithm(bool step)
    {
        if(current == null)
        {
            Initialized();
        }

        while(openList.Count > 0 && path == null)
        {
            List<Node> neighbors = FindNeighbors(current.Position);

            ExaminNeighbors(neighbors, current);

            UpdateCurrentTile(ref current);

            path = GeneratePath(current);
            if(step)
            {
                break;
            }
        }

        if(path != null)
        {
            foreach(Vector3Int position in path)
            {
                if(position != goalPos)
                {
                    tilemap.SetTile(position, tiles[2]);
                }
            }
        }

        AStarDebugger.MyInstance.CreateTiles(openList, closedList, allNodes, startPos, goalPos, path);
    }

    private List<Node> FindNeighbors(Vector3Int parentPosition)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)       
        {            
            for (int y = -1; y <= 1; y++)        // found my mistake
            {
                Vector3Int neighborPos = new Vector3Int(parentPosition.x - x, parentPosition.y - y, parentPosition.z);

                if (y != 0 || x != 0)
                {
                    if(neighborPos != startPos && tilemap.GetTile(neighborPos))
                    {
                        Node neighbor = GetNode(neighborPos);
                        neighbors.Add(neighbor);
                    }
                }
            }
        }
        return neighbors;
    }

    private void ExaminNeighbors(List<Node> neighbors, Node current)
    {
        for(int i = 0; i < neighbors.Count; i++)
        {
            Node neighbor = neighbors[i];

            //if(!ConnectedDiagonally(current, neighbor))
            //{
            //    continue;
            //}

            int gScore = DetermineGScore(neighbors[i].Position, current.Position);

            if(openList.Contains(neighbor))
            {
                if (current.G + gScore < neighbor.G)
                {
                    CalValues(current, neighbor, gScore);
                }
            }
            else if(!closedList.Contains(neighbor))
            {
                CalValues(current, neighbor, gScore);

                openList.Add(neighbor);
            } 
        }
    }

    private void CalValues(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;

        neighbor.H = Mathf.Abs(neighbor.Position.x - goalPos.x) + Mathf.Abs(neighbor.Position.y - goalPos.y) * 10;     // if not work add parentsies

        neighbor.F = neighbor.G + neighbor.H;
    }

    private int DetermineGScore(Vector3Int neighbor, Vector3Int current)
    {
        int gScore = 0;
        int x = current.x - neighbor.x;
        int y = current.y - neighbor.y;

        if(MathF.Abs(x - y) % 2 == 1)
        {
            gScore = 10;
        }
        else
        {
            gScore = 14;
        }
        return gScore;
    }

    private void UpdateCurrentTile(ref Node current)
    {
        openList.Remove(current);

        closedList.Add(current);

        if(openList.Count > 0)
        {
            current = openList.OrderBy(x=> x.F).First();
        }

    }


    private Node GetNode(Vector3Int position)
    {
        if(allNodes.ContainsKey(position))
        {
            return allNodes[position];
        }
        else
        {
            Node node = new Node(position);
            allNodes.Add(position, node);
            return node;
        }
    }

    public void ChangeTileType(TileButton button)
    {
        tileType = button.MyTileType;
    }

    void ChangeTile(Vector3Int clickPos)
    {
        //if(tileType == TileType.WATER)
        //{
        //    tilemap.SetTile(clickPos, waterTiles);
        //    waterTiles.Add(clickPos);
        //}
        //else
        //{
        //    if (tileType == TileType.START)             // this goes in Else up there
        //    {
        //        startPos = clickPos;
        //    }
        //    else if (tileType == TileType.GOAL)
        //    {
        //        goalPos = clickPos;
        //    }

        //    tilemap.SetTile(clickPos, tiles[(int)tileType]);
        //}

        if(tileType == TileType.START)             // this goes in Else up there
        {
            if(start)
            {
                tilemap.SetTile(startPos, tiles[3]);
            }
            start= true;

            startPos = clickPos;
        }
        else if(tileType == TileType.GOAL)
        {
            if (goal)
            {
                tilemap.SetTile(goalPos, tiles[3]);
            }
            goal = true;

            goalPos = clickPos;
        }

        tilemap.SetTile(clickPos, tiles[(int)tileType]);

        changeTiles.Add(clickPos);
    }

    //private bool ConnectedDiagonally(Node currentNode, Node neighbor)   // miss spelled
    //{
    //    Vector3Int direct = currentNode.Position - neighbor.Position;
    //    Vector3Int first = new Vector3Int(current.Position.x + (direct.x * -1), current.Position.y, current.Position.z);
    //    Vector3Int second = new Vector3Int(current.Position.x, current.Position.y +(direct.y * -1), current.Position.z);

    //    if(waterTiles.Contains(first) || waterTiles.contains(second))
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    private Stack<Vector3Int> GeneratePath(Node current)
    {
        if(current.Position == goalPos)
        {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();

            while(current.Position != startPos)
            {
                finalPath.Push(current.Position);

                current = current.Parent;
            }
            return finalPath;
        }
        return null;
    }

    public void ResetAstar()
    {
        AStarDebugger.MyInstance.ResetButton(allNodes);

        foreach (Vector3Int position in changeTiles)
        {
            tilemap.SetTile(position, tiles[3]);
        }

        foreach (Vector3Int position in path)
        {
            tilemap.SetTile(position, tiles[3]);
        }

        tilemap.SetTile(startPos, tiles[3]);
        tilemap.SetTile(goalPos, tiles[3]);

        //waterTiles.Clear();                     if i figure out how to fix this 
        allNodes.Clear();

        start = false;
        goal = false;
        path = null;
        current = null;
        
    }
}
