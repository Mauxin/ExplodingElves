using TMPro;
using UnityEngine;

namespace SpawnSystem
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _timerText;

        private const string TIMER_TEMPLATE = "{0}s";
        private const string TIMER_FORMAT = "F1";
    
        public void UpdateTimer(float time)
        {
            _timerText.text = string.Format(TIMER_TEMPLATE, time.ToString(TIMER_FORMAT));
        }
    }
}
