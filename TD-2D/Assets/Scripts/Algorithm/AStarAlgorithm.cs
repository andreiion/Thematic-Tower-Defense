using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStarAlgorithm {

//	A dictionary for all the nodes in the game
	private static Dictionary<Point, Node> nodes;


//	Creates a Node for each tile int the game
	private static void CreateNodes() {
//		Instantiate the dictionary
		nodes = new Dictionary<Point, Node> ();

//		Run through all the tiles in the game
		foreach (TileScript tile in LevelManager.Instance.Tiles.Values) {
//			Adds the node to the node dictionary
			nodes.Add (tile.GridPosition, new Node (tile));
		}
	}

	public static void GetPath(Point start, Point goal) {
		
		if (nodes == null) {
			CreateNodes ();  
		} 

//		Creates an open list to be used with A* algorithm
		HashSet<Node> openList = new HashSet<Node> ();

        //Creates a closed list to be used with A* algorithm
        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();

        //		Finds the start node and creates a reference to it called current node
        Node currentNode = nodes [start];
//		Adds the start node to the OpenList
		openList.Add (currentNode);

        while(openList.Count > 0)
        {
            //		Look in through neighbors
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                    if (LevelManager.Instance.InBounds(neighbourPos) &&
                        LevelManager.Instance.Tiles[neighbourPos].Walkable &&
                        LevelManager.Instance.InBounds(neighbourPos) &&
                        neighbourPos != currentNode.GridPosition)
                    {

                        int gCost = 0;

                        if (Mathf.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if(!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        Node neighbour = nodes[neighbourPos];

                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
                        }
                        

                        //ONLY FOR DEBUGGING
                        //neighbour.TileRef.SpriteRenderer.color = Color.black;
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if(currentNode == nodes[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }

        }


        //*****THIS IS ONLY FOR DEBUGGING
        GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList,finalPath);   
	}

    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = neighbour.GridPosition - currentNode.GridPosition;

        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);

        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable)
        {
            return false;
        }

        if(LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }
}
