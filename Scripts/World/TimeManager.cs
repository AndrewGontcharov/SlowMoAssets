using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public float normalTime = 1f;
    public float slowTime = 0.15f;
    public float smoothSpeed = 5f;
    public float timeBeforeSlow = 1f;

    private float targetTime;
    private float lastActionTime;

    public float TimePercent => Mathf.InverseLerp(slowTime, normalTime, Time.timeScale);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        targetTime = normalTime;
        lastActionTime = Time.unscaledTime;
        Time.timeScale = normalTime;
    }

    private void Update()
    {
        if (Time.unscaledTime - lastActionTime > timeBeforeSlow)
            targetTime = slowTime;

        Time.timeScale = Mathf.Lerp(
            Time.timeScale,
            targetTime,
            Time.unscaledDeltaTime * smoothSpeed
        );

        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        UIManager.Instance?.UpdateTimePercent(TimePercent);
    }

    public void RegisterAction()
    {
        lastActionTime = Time.unscaledTime;
        targetTime = normalTime;
    }
}
