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
            vec = tileMap.WorldToCell(vec) + tileMap.tileAnchor;
            
            currentPos.position = vec;
            if (Input.GetMouseButton(0))
            {
                if (Check())
                {
                    //tileMap.SetTiles();
                }
            }
        }
    }
    public void SpawnShip(GameObject obj)
    {
        Vector3 worldCooridnates= main.ScreenToWorldPoint(Input.mousePosition);
        current = Instantiate(obj, worldCooridnates, Quaternion.identity);
        currentPos = current.GetComponent<Transform>();
    }
    private bool Check()
    {
        for (int i=0; i<current.GetComponent<SpriteRenderer>().size.x;i++)
        {
            Vector3Int coor = new Vector3Int((int)currentPos.position.x, (int)currentPos.position.y, (int)currentPos.position.z) + new Vector3Int(1+i,0,0);
            if (tileMap.GetTile(coor)!=null) return false;
        }
        return true; 
    }
    public void PlaceShip()
    {

    }
}
