using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterWarehouse : MonoBehaviour
    {
        [SerializeField] private ModelsDataManager _characterPrefabs;
        
        public static CharacterWarehouse Instance { get; private set; }
        
        public int TotalPopulation => population.Sum(pair => pair.Value);

        private readonly Dictionary<CharacterType, int> population = new (){
            {CharacterType.Blue, 0},
            {CharacterType.Black, 0},
            {CharacterType.Red, 0},
            {CharacterType.White, 0}
        };

        private readonly Dictionary<CharacterType, int> spawnCount = new (){
            {CharacterType.Blue, 0},
            {CharacterType.Black, 0},
            {CharacterType.Red, 0},
            {CharacterType.White, 0}
        };
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
                Instance.Setup();
            } 
        }

        private int fps;
        
        private void Setup()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            getFPS();
        }

        private void getFPS()
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
        }
        
        public void CreateCharacter(Transform spawnPoint, CharacterType characterType)
        {
            CreateCharacter(spawnPoint.position, spawnPoint.rotation, characterType);
        }
        
        public void CreateCharacter(Vector3 spawnPoint, Quaternion spawnRotation, CharacterType characterType)
        {
            if (fps <= 20) return;
            if (!_characterPrefabs.TryGetModel(characterType, out var modelInfo)) return;
            
            var character = Instantiate(modelInfo.Value, transform);
            character.transform.position = spawnPoint;
            character.transform.rotation = spawnRotation;
            

            if (character.TryGetComponent<CharacterInteractionController>(out var interactionController))
            {
                interactionController.Id = spawnCount[characterType];
            }
            
            spawnCount[characterType]++;
            population[characterType]++;
        }

        public int GetPopulation(CharacterType type)
        {
            return population[type];
        }
        
        public void RemoveCharacter(CharacterType type)
        {
            population[type]--;
        }
    }
    
    public enum CharacterType
    {
        Blue,
        Black,
        Red,
        White
    }
    
    [Serializable]
    public class ModelInfo
    {
        public CharacterType Type;
        public GameObject Value;
    }

    [Serializable]
    public class ModelsDataManager
    {
        [SerializeField]
        private List<ModelInfo> Models;

        public bool TryGetModel(CharacterType type, out ModelInfo value)
        {
            value = Models.FirstOrDefault(info => info.Type == type);
            return value != null;
        }
    }
}