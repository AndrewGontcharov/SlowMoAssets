using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    [Header("Fire")]
    public float fireRate = 5f; // выстрелов в секунду
    public int bulletsPerShot = 1;
    public float spreadAngle = 0f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public ProjectileData projectileData;

    [Header("Recoil / Feel")]
    public float recoil = 0f;
}
