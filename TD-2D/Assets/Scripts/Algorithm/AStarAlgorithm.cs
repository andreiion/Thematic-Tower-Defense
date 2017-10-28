using System.Collections;
using System.Collections.Generic;
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

	public static void GetPath(Point start) {
		
		if (nodes == null) {
			CreateNodes ();  
		} 

//		Creates an open list to be used with A* algorithm
		HashSet<Node> openList = new HashSet<Node> ();

//		Finds the start node and creates a reference to it called current node
		Node currentNode = nodes [start];
//		Adds the start node to the OpenList
		openList.Add (currentNode);

//		Look in through neighbors
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				Point neighbourPos = new Point (currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
				if (LevelManager.Instance.Tiles[neighbourPos].Walkable &&
					LevelManager.Instance.InBounds(neighbourPos) &&
					neighbourPos != currentNode.GridPosition) {

					Node neighbour = nodes [neighbourPos];
					neighbour.TileRef.SpriteRenderer.color = Color.black;
				}
			}
		}

		//*****THIS IS ONLY FOR DEBUGGING
		GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList);   
	}
}
