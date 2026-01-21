using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Game/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float speed = 12f;
    public float damage = 1f;
    public float lifeTime = 2f;
    public ProjectileType type;
}

public enum ProjectileType
{
    Normal,
    Shell,
    Laser,
    Explosive
}



