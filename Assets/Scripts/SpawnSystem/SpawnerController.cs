using System.Collections;
using CharacterSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpawnSystem
{
    public class SpawnerController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TimerView _timerView;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CharacterType _characterType;
        
        private const float MAX_SPAWN_INTERVAL = 10f;

        private float _spawnInterval;

        private void Awake()
        {
            _spawnInterval = 0f;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            IncreaseSpawnTimer();
        }

        private void IncreaseSpawnTimer()
        {
            _spawnInterval = _spawnInterval < MAX_SPAWN_INTERVAL ? _spawnInterval + 0.5f : 0f;
            UpdateView();
        }
        
        private void UpdateView()
        {
            _timerView.UpdateTimer(_spawnInterval);
        }
    }
}
