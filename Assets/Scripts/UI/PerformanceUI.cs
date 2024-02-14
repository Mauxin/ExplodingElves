using Extensions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PerformanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _performanceText;

        private const string BASE_TEXT = "FPS: {0}" +
                                         "\nTotal Sys Memory: {1} MB" +
                                         "\nAllocated Memory: {2} MB" +
                                         "\nGraphics Memory: {3} MB";

        private void Update()
        {
            RefreshText();
        }

        private void RefreshText()
        {
            _performanceText.text = string.Format(BASE_TEXT,
                PerformanceManager.GetFPS(),
                PerformanceManager.GetTotalMemory(),
                PerformanceManager.GetAllocatedMemory(),
                PerformanceManager.GetGraphicsMemory());
        }
    }
}
