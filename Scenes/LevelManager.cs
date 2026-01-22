using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Room startRoom;
    public Room currentRoom;

    public void StartLevel()
    {
        // пока просто стартовая комната
        EnterRoom(startRoom);
    }

    public void EnterRoom(Room room)
    {
        currentRoom = room;
        currentRoom.Enter();
    }
}
