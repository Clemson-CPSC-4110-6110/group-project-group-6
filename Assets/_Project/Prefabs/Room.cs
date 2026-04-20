using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    private static HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    public GameObject roomPrefab;
    public Vector2 size = new Vector2(10, 10);

    void Start() {
        occupiedPositions.Add(transform.position);
    }

    public void DoorTriggered(string door, GameObject player)
    {
        Debug.Log(door + " triggered by " + player.name);
        
        char lastChar = door[door.Length - 1];
        int doorIdx = lastChar - '0';
        doorIdx--;

        Vector3[] offsets = new Vector3[] {
            new Vector3(0, 0, -size.y),
            new Vector3(-size.x, 0, 0),
            new Vector3(0, 0, size.y),
            new Vector3(size.x, 0, 0)
        };
        Vector3 newPosition = transform.position + offsets[doorIdx];

        if (occupiedPositions.Contains(newPosition)){
            Debug.Log("Room already exists at " + newPosition);
            return;
        }
        
        GetComponent<AudioSource>().Play();
        occupiedPositions.Add(newPosition);

        GameObject newRoom = Instantiate(roomPrefab, 
                    newPosition, 
                    Quaternion.identity
        );

        int oppositeDoorIdx = (doorIdx + 2) % 4;
        string oppositeDoorName = "Door" + (oppositeDoorIdx + 1);
        // newRoom.transform.Find(oppositeDoorName).GetComponent<Door>().active = false;
        
    }
}