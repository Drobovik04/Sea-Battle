using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    Transform current;
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
            current.position = main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,main.nearClipPlane);
            Debug.Log(main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public void SpawnShip(GameObject obj)
    {
        Vector3 worldCooridnates= main.ScreenToWorldPoint(Input.mousePosition);
        current = Instantiate(obj, worldCooridnates, Quaternion.identity).GetComponent<Transform>();
    }
}
