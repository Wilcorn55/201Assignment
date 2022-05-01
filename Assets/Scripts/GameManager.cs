using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int gold;
    public Text goldDisplay;


    private Room roomtoplace;
    public GameObject grid;

    public CustomCursor customCursor;

    [SerializeField]
    public Tile[] tiles;

    void Update()
    {
        goldDisplay.text = gold.ToString();

        if (Input.GetMouseButtonDown(0) && roomtoplace != null)
        {
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach (Tile tile in tiles)
            {
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                }
            }
            if (nearestTile.isOccupied == false)
            {
                Instantiate(roomtoplace, nearestTile.transform.position, Quaternion.identity);
                roomtoplace = null;
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }


    }
    
    private void BuyRoom(Room room) 
    {
        if(gold >= room.cost) {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = room.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            gold -= room.cost;
            roomtoplace = room;
            grid.SetActive(true);
        }
    }
        
}
