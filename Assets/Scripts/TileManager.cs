using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnZ = 0.0f;
	private float tileLength = 50.0f;
	private int amountOfTilesOnScreen = 3;
	
	// Use this for initialization
	private void Start ()
	{
		var playerObject = GameObject.Find("Player");
		playerTransform = playerObject.transform;
		for (int i = 0; i < amountOfTilesOnScreen; i++)
		{
			SpawnTile(); //spawn init tiles
		}
	}
	
	// Update is called once per frame
	private void Update () {
		if (playerTransform.position.z > (spawnZ - amountOfTilesOnScreen * tileLength))
		{
			SpawnTile();
		}
	}

	private void SpawnTile(int prefabIndex = -1)
	{
		print("Spawning Tile");
		GameObject go;
		go = Instantiate(tilePrefabs[0]) as GameObject;
		go.transform.SetParent(transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
	}
}
