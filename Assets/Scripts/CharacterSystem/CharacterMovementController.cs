using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private const float MOVE_SPEED = 2f;
        private const float CHANGE_DIRECTION_INTERVAL = 1f;
        
        private Vector3 movementDirection;
        private Vector3 currentPosition;
        private float lastDirectionChangeTime;

        private void Start()
        {
            ChangeDirection();
        }
        
        private void Update()
        {
            ChangeDirection();   
            MoveCharacter();
        }
        
        private void ChangeDirection()
        {
            if (Time.time - lastDirectionChangeTime <= CHANGE_DIRECTION_INTERVAL) return;
            
            ChooseNewMovementDirection();
            lastDirectionChangeTime = Time.time;
        }
        
        private void ChooseNewMovementDirection()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomZ = Random.Range(-1f, 1f);
            movementDirection = new Vector3(randomX, 0,  randomZ).normalized;
        }
        
        private void MoveCharacter()
        {
            currentPosition = _rigidbody.position;
            var newPosition = currentPosition + movementDirection * (MOVE_SPEED * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }
}
