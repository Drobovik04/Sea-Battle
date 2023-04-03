using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    GameObject current;
    Transform currentPos;
    SpriteRenderer rendererPrefab;
    [SerializeField]Tile shipTile;
    [SerializeField]int shipLength;
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
            Vector3 vec = main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, main.nearClipPlane);
            vec = tileMap.WorldToCell(vec);
            currentPos.position = vec;
            if (Input.GetMouseButton(0))
            {
                if (Check())
                {
                    PlaceShip();
                }
            }
        }
    }
    public void SpawnShip(GameObject obj)
    {
        Vector3 worldCooridnates= main.ScreenToWorldPoint(Input.mousePosition);
        current = Instantiate(obj, worldCooridnates, Quaternion.identity);
        currentPos = current.GetComponent<Transform>();
        shipLength = (int)obj.GetComponent<SpriteRenderer>().size.x;
        Debug.Log(shipLength);
    }
    private bool Check()
    {
        for (int i=0; i<current.GetComponent<SpriteRenderer>().size.x;i++)
        {
            Vector3Int coor = new Vector3Int((int)currentPos.position.x, (int)currentPos.position.y, (int)currentPos.position.z) + new Vector3Int(i-shipLength/2,0,0);
            if (tileMap.GetTile(tileMap.WorldToCell(coor))!=null) return false;
        }
        return true; 
    }
    public void PlaceShip()
    {
        Vector3Int pos = new Vector3Int((int)currentPos.position.x, (int)currentPos.position.y, (int)currentPos.position.z);
        pos = tileMap.WorldToCell(pos);
        for (int i = 0; i < shipLength; i++)
        {
            tileMap.SetTile(pos + new Vector3Int(i-(int)shipLength / 2, 0,0), shipTile);
        }
    }
}
