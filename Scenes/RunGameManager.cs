using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager Instance;

    public bool IsRunActive { get; private set; } = false;

    public LevelManager levelManager;
    public UIManager uiManager;
    public Transform playerSpawn;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartRun()
    {
        IsRunActive = true;

        // Сброс UI
        uiManager.HideGameOver();
        uiManager.UpdateWave(0);
        uiManager.UpdateKills(0);

        // Старт уровня
        levelManager.StartLevel();
    }

    public void GameOver()
    {
        IsRunActive = false;
        uiManager.ShowGameOver();
    }

    public void WinRun()
    {
        IsRunActive = false;
        uiManager.ShowWin();
    }
}
