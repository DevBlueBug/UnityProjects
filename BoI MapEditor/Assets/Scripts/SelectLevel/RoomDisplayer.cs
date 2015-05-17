using UnityEngine;
using System.Collections;
using Data;

public class RoomDisplayer : MonoBehaviour
{
	public GameEditor.EditorTile P_Tile;
	public GameEditor.EditorTile[,] tiles;
	public GameEditor.EditorTile[] doors;

	int id;
	void Awake(){
		id = Random.Range (1, 1000);
	}
	// Use this for initialization
	public void Init (float ratio)
	{

		tiles = new GameEditor.EditorTile[13,7];
		doors = new GameEditor.EditorTile[4];

		var parent = this.transform.parent;
		float length = 1.0f / 13.0f * ratio;

		for (int x= 0; x< 13; x++)
			for (int y = 0; y < 7; y++) {
			var tile = Instantiate(P_Tile);
			tiles[x,y] = tile;
			tile.transform.SetParent(this.transform);
			tile.transform.localPosition = new Vector3(-6*length,-3*length,0);
			tile.transform.localPosition += new Vector3(length*x,length*y);
			tile.transform.localScale = new Vector3(length,length,1);
			}
		for (int i = 0; i < 4; i++) {
			var door = Instantiate(P_Tile);
			door.transform.SetParent(this.transform);
			door.transform.localPosition = new Vector3 (-1.5f * length + length*i , 4*length, 0);
			door.transform.localScale = new Vector3(length,length,1);
		}

	}
	public void Init(DRoomLayout layout){
		for (int x= 0; x< 13; x++)
		for (int y = 0; y < 7; y++) {
			tiles[x,y].SetType(layout.tiles[x,y]);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

