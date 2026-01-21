using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 3f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveRunning = false;

    private void OnEnable()
    {
        Health.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        Health.OnEnemyKilled -= OnEnemyKilled;
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            StartWave();
            waveRunning = true;

            // ждём пока враги не закончатся
            while (enemiesAlive > 0)
                yield return null;

            waveRunning = false;

            Debug.Log($"Wave {currentWave} completed");
            // тут потом будет магазин
        }
    }

    private void StartWave()
    {
        currentWave++;

        int enemyCount = 3 + currentWave * 2;

        enemiesAlive = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawner.SpawnEnemy(spawn.position);
        }

        Debug.Log($"Wave {currentWave} started");
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("WaveManager: No spawn points assigned!");
            return;
        }

    }

    private void OnEnemyKilled(GameObject enemy)
    {
        enemiesAlive--;
    }



}
