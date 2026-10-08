using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SimpleFPScounter : MonoBehaviour
{
    [SerializeField] string format = "FPS: {0}";
    [SerializeField] float fpsUpdateRate = 5;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("UpdateFPS", 0, 1 / fpsUpdateRate);
    }

    void UpdateFPS()
    {
        text.text = string.Format(format, Mathf.Round(1f / Time.deltaTime));
    }
}
