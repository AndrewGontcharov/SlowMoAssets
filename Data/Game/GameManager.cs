using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Wave")]
    public int wave = 0;
    public int baseEnemiesPerWave = 5;
    public int enemiesPerWaveGrowth = 2;
    public float spawnRadius = 8f;

    [Header("Counters")]
    public int enemiesAlive = 0;
    public int enemiesKilled = 0;

    [Header("Prefabs")]
    public GameObject[] enemyPrefabs;
    public Transform player;

    [Header("UI")]
    public UIManager ui;

    private bool isGameRunning = false;
    public bool IsGameRunning => isGameRunning;

    private Coroutine waveCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        Health.OnEnemyKilled += EnemyKilled;
        Health.OnPlayerDead += GameOver;
    }

    private void OnDisable()
    {
        Health.OnEnemyKilled -= EnemyKilled;
        Health.OnPlayerDead -= GameOver;
    }

    public void StartGame()
    {
        isGameRunning = true;

        wave = 0;
        enemiesKilled = 0;

        ui.HideGameOver();
        ui.UpdateKills(enemiesKilled);
        ui.UpdateWave(wave);

        waveCoroutine = StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(1f);

            StartNextWave();
            yield return new WaitUntil(() => enemiesAlive <= 0);
        }
    }

    private void StartNextWave()
    {
        wave++;

        int enemiesThisWave =
            baseEnemiesPerWave + wave * enemiesPerWaveGrowth;

        enemiesAlive = enemiesThisWave;
        ui.UpdateWave(wave);

        for (int i = 0; i < enemiesThisWave; i++)
        {
            Vector2 spawnPos =
                (Vector2)player.position +
                Random.insideUnitCircle * spawnRadius;

            GameObject enemyPrefab =
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void EnemyKilled(GameObject enemy)
    {
        if (!isGameRunning) return;

        enemiesAlive--;
        enemiesKilled++;

        ui.UpdateKills(enemiesKilled);
    }

    public void GameOver()
    {
        isGameRunning = false;

        if (waveCoroutine != null)
            StopCoroutine(waveCoroutine);

        ui.ShowGameOver(wave, enemiesKilled);

        PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        Shooter shooter = FindObjectOfType<Shooter>();

        if (pm != null) pm.SetCanMove(false);
        if (shooter != null) shooter.canShoot = false;
    }
}
