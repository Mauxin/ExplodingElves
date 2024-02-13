using DG.Tweening;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private const float MOVE_SPEED = 1f;
        private const float ROTATION_DURATION = 0.15f;

        private Vector3 movementDirection;
        private Vector3 currentPosition;
        private float lastDirectionChangeTime;
        private float changeDirectionInterval = 1f;
        private bool isMoving = true;

        private void Start()
        {
            ChangeDirection();
        }
        
        private void Update()
        {
            if (!isMoving) return;

            ChangeDirection();   
            MoveCharacter();
        }
        
        private void ChangeDirection()
        {
            if (Time.time - lastDirectionChangeTime <= changeDirectionInterval) return;
            
            ChooseNewMovementDirection();
            lastDirectionChangeTime = Time.time;
            changeDirectionInterval = Random.Range(1f, 5f);
        }
        
        private void ChooseNewMovementDirection()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomZ = Random.Range(-1f, 1f);
            movementDirection = new Vector3(randomX, 0,  randomZ).normalized;
            
            var lookRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.DORotateQuaternion(lookRotation, ROTATION_DURATION);
        }
        
        private void MoveCharacter()
        {
            currentPosition = _rigidbody.position;
            var newPosition = currentPosition + movementDirection * (MOVE_SPEED * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        public void FaceEnemy(Transform enemy)
        {
            var lookRotation = Quaternion.LookRotation(enemy.position - transform.position, Vector3.up);
            transform.DORotateQuaternion(lookRotation, ROTATION_DURATION);
            
            isMoving = false;
        }
    }
}
