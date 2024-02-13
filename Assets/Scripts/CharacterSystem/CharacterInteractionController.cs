using System;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterInteractionController : MonoBehaviour
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterMovementController _movementController;

        private const string CHARACTER_TAG = "Character";
        private const float MIN_SPAWN_INTERVAL = 2f;
        
        public int Id { get; set; }
        
        private float lastSpawnTime;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(CHARACTER_TAG)) return;
            
            var otherCharacter = other.gameObject.GetComponent<CharacterInteractionController>();

            if (otherCharacter._characterType == _characterType)
            {
                _characterAnimator.Jump();
                
                if (Id < otherCharacter.Id) SpawnFriend();
            }
            else
            {
                _movementController.FaceEnemy(otherCharacter.transform);
                _characterAnimator.AttackAndDie(OnDeath);
            }
        }

        private void SpawnFriend()
        {
            if (!(Time.time - lastSpawnTime >= MIN_SPAWN_INTERVAL)) return;
            if (CharacterWarehouse.Instance.GetPopulation(_characterType) >= 256) return;

            lastSpawnTime = Time.time;
            CharacterWarehouse.Instance.CreateCharacter(transform.position + Vector3.up, transform.rotation, _characterType);
        }
        
        private void OnDeath()
        {
            CharacterWarehouse.Instance.RemoveCharacter(_characterType);
            Destroy(gameObject, .5f);
        }
    }
    
    public enum CharacterType
    {
        Blue,
        Black,
        Red,
        White
    }
}