using UnityEngine;

namespace CharacterSystem
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        
        private const string WALKING_PARAM = "IsWalking";
        private const string ENEMY_PARAM = "EnemyFound";
        private const string FRIEND_PARAM = "FriendFound";
        private const string DEAD_PARAM = "Dead";
        private const string JUMP_END_PARAM = "JumpEnd";
        
        void Start()
        {
            _animator.SetBool(WALKING_PARAM, true);
        }
        
        public void AttackAndDie()
        {
            _animator.SetBool(WALKING_PARAM, false);
            _animator.SetTrigger(ENEMY_PARAM);
        }
        
        public void Jump()
        {
            _animator.SetTrigger(FRIEND_PARAM);
        }
        
        private void PunchEnd()
        {
            _animator.SetTrigger(DEAD_PARAM);
            Destroy(gameObject, 1f);
        }
        
        private void JumpEnd()
        {
            _animator.ResetTrigger(FRIEND_PARAM);
            _animator.SetTrigger(JUMP_END_PARAM);
        }
    }
}
