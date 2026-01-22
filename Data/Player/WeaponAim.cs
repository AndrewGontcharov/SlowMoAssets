using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;

    void Update()
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        Vector2 dir =
            mouseWorld - transform.position;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angle);

        // Флип спрайта
        if (dir.x < 0)
            weaponSprite.flipY = true;
        else
            weaponSprite.flipY = false;
    }
}
