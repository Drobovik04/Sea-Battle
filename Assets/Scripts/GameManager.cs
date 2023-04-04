using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField]Tilemap tileMap;
    [SerializeField]Tilemap backTileMap;
    [SerializeField]Tile shipTile;
    [SerializeField]int shipLength;
    [SerializeField] Tile waterTile;
    int[,] field1 = new int[10, 10];
    int[,] field2 = new int[10, 10];
    GameObject current;
    Transform currentPos;
    SpriteRenderer rendererPrefab;

    Vector3 vec;
    Camera main;
    // Start is called before the first frame update
    void Awake()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (current!=null)
        {
            vec = main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 0);
            vec = tileMap.WorldToCell(vec);
            currentPos.position = vec + new Vector3(shipLength%2==1?0.5f:0f, transform.rotation.z%90==0?0.5f:0f, 0);
            Debug.Log(vec);
            if (Input.GetMouseButton(0))
            {
                if (Check())
                {
                    PlaceShip();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentPos.rotation *= Quaternion.Euler(0,0,90);
            }
        }
    }
    public void SpawnShip(GameObject obj)
    {
        Vector3 worldCooridnates= main.ScreenToWorldPoint(Input.mousePosition);
        if (current != null) Destroy(current);
        current = Instantiate(obj, worldCooridnates, Quaternion.identity);
        currentPos = current.GetComponent<Transform>();
        shipLength = (int)obj.GetComponent<SpriteRenderer>().size.x;
    }
    private bool Check()
    {
        for (int i=0; i<current.GetComponent<SpriteRenderer>().size.x;i++)
        {
            Vector3Int coor = new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z) + new Vector3Int(i-shipLength/2,0,0);
            if (tileMap.GetTile(tileMap.WorldToCell(coor))!=null|| backTileMap.GetTile(backTileMap.WorldToCell(coor))!=waterTile) return false;
        }
        return true; 
    }
    public void PlaceShip()
    {
        Vector3Int pos = new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
        pos = tileMap.WorldToCell(pos);
        for (int i = 0; i < shipLength; i++)
        {
            tileMap.SetTile(pos + new Vector3Int(i-shipLength / 2, 0,0), shipTile);
            if (pos.x < -6)
            {
                field1[Math.Abs(pos.y - 6), Math.Abs(pos.x + 18)] = shipLength;

            }
            else 
            {
                field2[Math.Abs(pos.y - 6), Math.Abs(pos.x + 4)] = shipLength;
            }
        }
    }
}
