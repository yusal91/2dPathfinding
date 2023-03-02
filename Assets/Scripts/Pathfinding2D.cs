using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding2D : MonoBehaviour
{

    //public Transform seeker, target;
    //Grid2D grid;
    //Node2D seekerNode, targetNode;
    //public List<Node2D> pathSet = new List<Node2D>();
    //public GameObject GridOwner;


    //void Awake()
    //{
        
    //    grid = GridOwner.GetComponent<Grid2D>();
    //}


    //private void Update()
    //{
    //    FindPath(seeker.position, target.position);
    //}


    //public void FindPath(Vector3 startPos, Vector3 targetPos)
    //{
    //    //get player and end position in grid coordinates
    //    seekerNode = grid.NodeFromWorldPoint(startPos);
    //    targetNode = grid.NodeFromWorldPoint(targetPos);

    //    List<Node2D> openSet = new List<Node2D>();
    //    List<Node2D> closedSet = new List<Node2D>();
    //    pathSet = new List<Node2D>();
    //    openSet.Add(seekerNode);
        
    //    //calculates the path for pathfinding
    //    while (openSet.Count > 0)
    //    {
    //        //Go through the open set and finds the lowest fCost
    //        Node2D node = openSet[0];
    //        for (int i = 1; i < openSet.Count; i++)
    //        { 
    //            if (openSet[i].fCost <= node.fCost)
    //            {
    //                if (openSet[i].hCost < node.hCost)
    //                    node = openSet[i];
    //            }
    //        }
            
    //        openSet.Remove(node);
    //        closedSet.Add(node);
    //        pathSet.Add(node);
    //        //Debug.Log("adding in closed set: " + closedSet);

    //        //If target found, retrace path
    //        if (node == targetNode)
    //        {
    //            RetracePath(seekerNode, targetNode);
    //            return;
    //        }
            
    //        //adds neighbor nodes to openSet
    //        foreach (Node2D neighbour in grid.GetNeighbors(node))
    //        {
    //            if (neighbour.obstacle || closedSet.Contains(neighbour))
    //            {
    //                continue;
    //            }

    //            int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
    //            if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
    //            {
    //                neighbour.gCost = newCostToNeighbour;
    //                neighbour.hCost = GetDistance(neighbour, targetNode);
    //                neighbour.parent = node;

    //                if (!openSet.Contains(neighbour))
    //                    openSet.Add(neighbour);
    //            }
    //        }
    //    }
        
    //}

    //public void GoToNode(Node2D node)
    //{
    //    pathSet.Remove(node);
    //}

    ////reversing the calculated path so the first node is closest to player
    //public void RetracePath(Node2D startNode, Node2D endNode)
    //{
    //    List<Node2D> path = new List<Node2D>();
    //    Node2D currentNode = endNode;

    //    while (currentNode != startNode)
    //    {
    //        path.Add(currentNode);
    //        currentNode = currentNode.parent;
    //    }
    //    path.Reverse();

    //    grid.path = path;

    //}

    ////gets distance between 2 nodes for calculating cost
    //int GetDistance(Node2D nodeA, Node2D nodeB)
    //{
    //    int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
    //    int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

    //    if (dstX > dstY)
    //        return 14 * dstY + 10 * (dstX - dstY);
    //    return 14 * dstX + 10 * (dstY - dstX);
    //}
}