
using System.Dynamic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterInteractionController : MonoBehaviour
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterMovementController _movementController;
        
        private const string CHARACTER_TAG = "Character";
        
        public int Id { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(CHARACTER_TAG)) return;
            
            var otherCharacter = other.gameObject.GetComponent<CharacterInteractionController>();
            
            if (otherCharacter._characterType == _characterType)
            {
                _characterAnimator.Jump();
                
                if (Id < otherCharacter.Id) Invoke(nameof(SpawnFriend), 1f);
            }
            else
            {
                _movementController.Stop();
                _characterAnimator.AttackAndDie();
            }
        }
        
        private void SpawnFriend()
        {
            CharacterWarehouse.Instance.CreateCharacter(transform, _characterType);
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