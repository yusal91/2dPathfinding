using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    private static AStarDebugger instance;

    [Header("Grid")]
    [SerializeField] public Grid grid;                // why does this not showing in instpector

    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    [Header("Tile")]
    [SerializeField] private Tile tile;

    [Header("Colors")]
    [SerializeField] private Color openColor, closedColor, pathColor, currentColor, startColor, goalColor;

    [Header("Canvas")]
    [SerializeField] private Canvas canvas;
    [Header("Prefab")]
    [SerializeField] private GameObject debugTextPrefab;

    private List<GameObject> debugObjects = new List<GameObject>();

    //-------------------------------
    private bool debugVisible = false;

    public static AStarDebugger MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance =FindObjectOfType<AStarDebugger>();
            }
            return instance;
        }
    }   

    public void CreateTiles(HashSet<Node> openList, HashSet<Node> closedList, 
                            Dictionary<Vector3Int, Node> allNodes ,Vector3Int start, 
                            Vector3Int goal, Stack<Vector3Int> path = null)
    {
        foreach(GameObject go in debugObjects)
        {
            Destroy(go);
        }

        foreach(Node node in openList)
        {
            ColorTiles(node.Position, openColor);
        }
        foreach (Node node in closedList)
        {
            ColorTiles(node.Position, closedColor);
        }

        if(path != null)
        {
            foreach(Vector3Int pos in path)
            {
                if(pos != start && pos != goal)
                {
                    ColorTiles(pos, pathColor);
                }
            }
        }


        //ColorTiles(start, startColor);
        ColorTiles(goal, goalColor);

        foreach(KeyValuePair<Vector3Int, Node> node in allNodes)
        {
            if(node.Value.Parent != null)
            {
                GameObject go = Instantiate(debugTextPrefab, canvas.transform);
                go.transform.position = grid.CellToWorld(node.Key);    // everything else works apart from this stupid shit
                debugObjects.Add(go);
                GenerateDebugText(node.Value, go.GetComponent<DebugText>());
            }
        }
    }

    private void GenerateDebugText(Node node, DebugText debugText)
    {
        debugText.F.text = $"F: {node.F}";
        debugText.G.text = $"G: {node.G}";
        debugText.H.text = $"H: {node.H}";
        debugText.P.text = $"P: {node.Position.x},{node.Position.y}";

        if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y == node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 135));
        }
        else if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 225));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y == node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, -45));
        }
        else if (node.Parent.Position.x == node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (node.Parent.Position.x == node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
    }

    public void ColorTiles(Vector3Int position, Color color)
    {
        tilemap.SetTile(position, tile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }

    public void ShowHide()          /// this wont work since i am using same canvas for debugging as well
    {
        canvas.gameObject.SetActive(!canvas.isActiveAndEnabled);               // if you are using 2 different canvas
        Color c = tilemap.color;
        c.a = c.a != 0 ? 0: 1;
        tilemap.color = c;
    }

    public void ResetButton(Dictionary<Vector3Int, Node> allNodes)
    {
        foreach(GameObject go in debugObjects)
        {
            Destroy(go);
        }
        debugObjects.Clear();

        foreach (Vector3Int position in allNodes.Keys)
        {
            tilemap.SetTile(position, null);
        }
    }

}
