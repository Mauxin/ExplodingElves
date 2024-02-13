using System;
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
        
        private Action _onDeath;

        private void Start()
        {
            _animator.SetBool(WALKING_PARAM, true);
        }
        
        public void AttackAndDie(Action onDeath)
        {
            _onDeath = onDeath;
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
            _onDeath?.Invoke();
            _onDeath = null;
        }
        
        private void JumpEnd()
        {
            _animator.ResetTrigger(FRIEND_PARAM);
            _animator.SetTrigger(JUMP_END_PARAM);
        }
    }
}
