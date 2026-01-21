using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SpawnEnemy(Vector2 position)
    {
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
