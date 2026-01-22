using UnityEngine;

public enum RoomType
{
    Combat,
    Event,
    Secret,
    Shop,
    Boss
}

public class Room : MonoBehaviour
{
    public RoomType roomType;

    public void Enter()
    {
        Debug.Log("Entered room: " + roomType);

        if (roomType == RoomType.Combat)
        {
            TimeManager.Instance.StartSlowMotion();
        }
    }

    public void ClearRoom()
    {
        if (roomType == RoomType.Combat)
        {
            TimeManager.Instance.StopSlowMotion();
        }
    }
}
