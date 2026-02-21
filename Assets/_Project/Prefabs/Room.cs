using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject roomPrefab;
    public Vector2 size = new Vector2(10, 10);

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
        GameObject newRoom = Instantiate(roomPrefab, 
                    transform.position + offsets[doorIdx], 
                    Quaternion.identity, transform
        );

        int oppositeDoorIdx = (doorIdx + 2) % 4;
        string oppositeDoorName = "Door" + (oppositeDoorIdx + 1);
        newRoom.transform.Find(oppositeDoorName).GetComponent<Door>().active = false;
        
    }
}