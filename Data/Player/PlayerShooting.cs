using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Fire settings")]
    public Transform firePoint;

    [Header("Weapon settings")]
    public WeaponData currentWeapon;
    public GameObject projectilePrefab;
    public ProjectileData projectileData;

    private float lastShotTime;
    public bool canShoot = false;

    void Update()
    {
        if (!canShoot) return;

        if (Input.GetMouseButton(0))   // <-- теперь можно зажимать
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("Shooter: firePoint is NULL!");
            return;
        }

        if (currentWeapon != null)
        {
            if (Time.time < lastShotTime + 1f / currentWeapon.fireRate)
                return;

            ShootWithWeapon();
            lastShotTime = Time.time;
        }
        else
        {
            ShootWithPrefab();
        }

        TimeManager.Instance?.RegisterAction();
    }

    private void ShootWithWeapon()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2 baseDirection = (mouseWorldPos - firePoint.position).normalized;

        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            float angleOffset = currentWeapon.bulletsPerShot > 1
                ? Random.Range(-currentWeapon.spreadAngle, currentWeapon.spreadAngle)
                : 0f;

            Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * baseDirection;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

            GameObject proj = Instantiate(
                currentWeapon.projectilePrefab,
                firePoint.position,
                rotation
            );

            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.data = currentWeapon.projectileData;
        }
    }

    private void ShootWithPrefab()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Shooter: projectilePrefab is NULL!");
            return;
        }

        if (projectileData == null)
        {
            Debug.LogError("Shooter: projectileData is NULL!");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);

        Projectile projectile = proj.GetComponent<Projectile>();
        if (projectile == null)
        {
            Debug.LogError("Shooter: Projectile component missing on projectilePrefab!");
            return;
        }

        projectile.data = projectileData;
    }
}
