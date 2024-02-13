using System.Collections;
using CharacterSystem;
using UnityEngine;
using UnityEngine.UI;

namespace SpawnSystem
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private TimerView _timerView;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private Button _plusButton;
        [SerializeField] private Button _minusButton;
        
        private const float SPAWN_INTERVAL_DELTA = 0.5f;

        private float _spawnInterval;

        private void Awake()
        {
            _spawnInterval = 0f;
            _plusButton.onClick.AddListener(IncreaseSpawnTimer);
            _minusButton.onClick.AddListener(DecreaseSpawnTimer);
            
            UpdateView();
        }

        private void Start()
        {
            StartCoroutine(SpawnCharacter());
        }
        
        private IEnumerator SpawnCharacter()
        {
            while (_spawnInterval <= 0) yield return null;
            
            yield return new WaitForSeconds(_spawnInterval);
            CreateCharacter();

            yield return SpawnCharacter();
        }
        
        private void CreateCharacter()
        {
            CharacterWarehouse.Instance.CreateCharacter(_spawnPoint, _characterType);
        }

        private void IncreaseSpawnTimer()
        {
            _spawnInterval += SPAWN_INTERVAL_DELTA;
            UpdateView();
        }
        
        private void DecreaseSpawnTimer()
        {
            _spawnInterval -= + SPAWN_INTERVAL_DELTA;
            UpdateView();
        }
        
        private void UpdateView()
        {
            _timerView.UpdateTimer(_spawnInterval);
        }
    }
}
