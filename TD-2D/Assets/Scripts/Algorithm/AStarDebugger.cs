using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour {
	private TileScript start;
	private TileScript goal;
	[SerializeField]
	private Sprite blankTile;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject debugTilePrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//void Update () {
	//	ClickTile ();

	//	if (Input.GetKeyDown (KeyCode.Space)) {
	//		AStarAlgorithm.GetPath (start.GridPosition, goal.GridPosition);
	//	}
	//}

	private void ClickTile() {
		// Right-click to set start and goal
		if (Input.GetMouseButtonDown (1)) {
			
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 

			if (hit.collider != null) {
				TileScript tmp = hit.collider.GetComponent<TileScript> ();

				if (tmp != null) {
					if (start == null) {
						start = tmp;
                        CreateDebugTile(start.worldPos, new Color32(255, 135, 0, 255));
					}
					else if (goal == null) {
						goal = tmp;
                        CreateDebugTile(goal.worldPos, new Color32(255, 0, 0, 255));
                    }
				}
			}
		}
	}

	public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> path) {
	
		foreach (Node node in openList) {
            if (node.TileRef != start && node.TileRef != goal)
            {
                CreateDebugTile(node.TileRef.worldPos, Color.cyan, node);
            }

            PointToParent(node, node.TileRef.worldPos);
		}

        foreach (Node node in closedList)
        {
            if (node.TileRef != start && node.TileRef != goal && !path.Contains(node))
            {
                CreateDebugTile(node.TileRef.worldPos, Color.blue, node);
            }

            PointToParent(node, node.TileRef.worldPos);
        }

        foreach(Node node in path)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {
                CreateDebugTile(node.TileRef.worldPos, Color.green, node);
            }
        }
    }



    private void PointToParent(Node node, Vector2 position)
    {
        if(node.Parent != null)
        {
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, position, Quaternion.identity);

            arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;

            //right
            if ((node.GridPosition.X < node.Parent.GridPosition.X)
                && (node.GridPosition.Y == node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            //top right
            else if ((node.GridPosition.X < node.Parent.GridPosition.X)
                && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 45);
            }

            //up
            else if ((node.GridPosition.X == node.Parent.GridPosition.X)
                && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 90);
            }

            //top left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X)
                && (node.GridPosition.Y > node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 135);
            }

            //left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X)
                && (node.GridPosition.Y == node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 180);
            }

            //bottom left
            else if ((node.GridPosition.X > node.Parent.GridPosition.X)
                && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 225);
            }

            //bottom
            else if ((node.GridPosition.X == node.Parent.GridPosition.X)
                && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 270);
            }

            //bottom right
            else if ((node.GridPosition.X < node.Parent.GridPosition.X)
                && (node.GridPosition.Y < node.Parent.GridPosition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 315);
            }
        }       
    }

    private void CreateDebugTile(Vector3 worldPos, Color32 color, Node node = null)
    {
        GameObject debugTile = (GameObject)Instantiate(debugTilePrefab, worldPos, Quaternion.identity);


        // if-u' asta nu merge pt ca nu am facut un script din 6.7
        // tre facut
        //if(node != null)
        //{
        //    DebugTile tmp = debugTile.GetComponent<DebugTile>();

        //    tmp.G.text += node.G;
        //    tmp.H.text += node.H;
        //    tmp.F.text += node.F;
        //}

        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}
