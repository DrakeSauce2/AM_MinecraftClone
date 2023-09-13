using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameCounter;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        frameCounter.text = $"FPS : {Time.frameCount / Time.time}";
    }
}
