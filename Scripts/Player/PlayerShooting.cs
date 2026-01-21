using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public ProjectileData projectileData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);

            // ---- œ–Œ¬≈– ¿ Õ¿ NULL ----
            if (proj == null)
            {
                Debug.LogError("Shooter: projectilePrefab is NULL!");
                return;
            }

            Projectile projectile = proj.GetComponent<Projectile>();

            if (projectile == null)
            {
                Debug.LogError("Shooter: Projectile component missing on projectilePrefab!");
                return;
            }

            projectile.data = projectileData;

            if (TimeManager.Instance == null)
            {
                Debug.LogError("Shooter: TimeManager.Instance is NULL!");
                return;
            }

            TimeManager.Instance.RegisterAction();
        }
    }
}
