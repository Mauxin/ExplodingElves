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
        private const float POSITION_CHECK_INTERVAL = 2f;
        
        public int Id { get; set; }
        
        private float lastSpawnTime;

        private void Start()
        {
            lastSpawnTime = Time.time;
            InvokeRepeating(nameof(CheckValidPosition), POSITION_CHECK_INTERVAL, POSITION_CHECK_INTERVAL);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(CHARACTER_TAG)) return;
            
            var otherCharacter = other.gameObject.GetComponent<CharacterInteractionController>();

            if (otherCharacter._characterType == _characterType)
            {
                _characterAnimator.Jump();
                SpawnFriend(otherCharacter.Id);
            }
            else
            {
                _movementController.FaceEnemy(otherCharacter.transform);
                _characterAnimator.AttackAndDie(OnDeath);
            }
        }
        
        private void CheckValidPosition()
        {
            if (transform.position.y >= -5f) return;
                
            CharacterWarehouse.Instance.RemoveCharacter(_characterType);
            Destroy(gameObject);
        }

        private void SpawnFriend(int otherId)
        {
            if (Time.time - lastSpawnTime < MIN_SPAWN_INTERVAL) return;
            
            lastSpawnTime = Time.time;
            
            if (Id >= otherId) return;
            
            CharacterWarehouse.Instance.CreateCharacter(transform.position + Vector3.up, transform.rotation, _characterType);
        }
        
        private void OnDeath()
        {
            CharacterWarehouse.Instance.RemoveCharacter(_characterType);
            Destroy(gameObject, .5f);
        }
    }
}