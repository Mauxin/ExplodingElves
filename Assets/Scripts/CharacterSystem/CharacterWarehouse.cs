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

        private Dictionary<CharacterType, int> population = new (){
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

        private void Setup()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void CreateCharacter(Transform spawnPoint, CharacterType characterType)
        {
            if (!_characterPrefabs.TryGetModel(characterType, out var modelInfo)) return;
            
            var character = Instantiate(modelInfo.Value, transform);
            character.transform.position = spawnPoint.position;
            character.transform.rotation = spawnPoint.rotation;
            

            if (character.TryGetComponent<CharacterInteractionController>(out var interactionController))
            {
                interactionController.Id = population[characterType];
            }
            
            population[characterType]++;
        }
        
        public int GetPopulation(CharacterType type)
        {
            return population[type];
        }
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