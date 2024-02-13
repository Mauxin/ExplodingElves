using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

namespace UI
{
    public class PerformanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _performanceText;

        private const int MB_CONVERTER = 1024 * 1024;

        private const string BASE_TEXT = "FPS: {0}" +
                                         "\nTotal Sys Memory: {1} MB" +
                                         "\nAllocated Memory: {2} MB" +
                                         "\nGraphics Memory: {3} MB";
        
        private void Start () {
           //InvokeRepeating(nameof(RefreshText), 0f, 5f);
        }
        
        private void RefreshText()
        {
            var fps = 1.0f / Time.deltaTime;
            var totalMemory = Profiler.GetMonoHeapSizeLong() / MB_CONVERTER;
            var allocatedMemory = Profiler.GetMonoUsedSizeLong() / MB_CONVERTER;
            var graphicsMemory = Profiler.GetTotalReservedMemoryLong() / MB_CONVERTER;

            // Display performance information
            _performanceText.text = string.Format(BASE_TEXT,
                fps.ToString("F1"), totalMemory,
                allocatedMemory, graphicsMemory);
        }
    }
}
