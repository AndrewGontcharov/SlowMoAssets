using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Texts")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text timePercentText;

    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;

    private PlayerMovement playerMovement;
    private Shooter shooter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        shooter = FindObjectOfType<Shooter>();

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        waveText.gameObject.SetActive(false);
        killsText.gameObject.SetActive(false);
        timePercentText.gameObject.SetActive(false);

        playerMovement?.SetCanMove(false);
        if (shooter != null) shooter.enabled = false;
    }

    private void StartGame()
    {
        startPanel.SetActive(false);

        playerMovement?.SetCanMove(true);
        if (shooter != null) shooter.enabled = true;

        waveText.gameObject.SetActive(true);
        killsText.gameObject.SetActive(true);
        timePercentText.gameObject.SetActive(true);

        GameManager.Instance.StartGame();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateWave(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    public void UpdateKills(int kills)
    {
        killsText.text = "Kills: " + kills;
    }

    public void UpdateTimePercent(float percent)
    {
        timePercentText.text = "Time: " + Mathf.RoundToInt(percent * 100) + "%";
    }

    public void ShowGameOver(int wave, int kills)
    {
        gameOverPanel.SetActive(true);
        waveText.text = "Wave: " + wave;
        killsText.text = "Kills: " + kills;
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }
}
