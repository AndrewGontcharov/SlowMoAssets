using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Time Scale")]
    public float normalTime = 1f;
    public float slowTime = 0.15f;

    [Header("Smooth")]
    public float smoothSpeed = 5f;

    [Header("Delay")]
    public float timeBeforeSlow = 1f; // время, через которое замедляется

    private float targetTime;
    private float lastActionTime;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        targetTime = slowTime;
        lastActionTime = Time.unscaledTime;
    }

    void Update()
    {
        // Если прошло время без действия — замедляемся
        if (Time.unscaledTime - lastActionTime > timeBeforeSlow)
        {
            targetTime = slowTime;
        }

        // Плавное изменение timeScale
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTime, Time.unscaledDeltaTime * smoothSpeed);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // Вызывать, когда игрок делает действие
    public void RegisterAction()
    {
        lastActionTime = Time.unscaledTime;
        targetTime = normalTime;
    }
}
