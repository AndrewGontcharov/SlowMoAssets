using UnityEngine;
using TMPro;

public class TimeIndicator : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = "TIME: " + Mathf.Round(Time.timeScale * 100) + "%";
    }
}
