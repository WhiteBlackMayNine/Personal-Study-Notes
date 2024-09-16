using AnimatorParameters;
using General;
using Manage.Input_System;
using UnityEngine;


namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private Animator _animator;
        private PhysicsCheck _physicsCheck;
        private PlayerControl _playerControl;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _physicsCheck = GetComponent<PhysicsCheck>();
            _playerControl = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            SetAnimation();
        }

        private void SetAnimation()
        {
            _animator.SetFloat(AnimationID.VelocityXid, Mathf.Abs(_rb2d.velocity.x));
            _animator.SetFloat(AnimationID.VelocityYid, _rb2d.velocity.y);
            _animator.SetBool(AnimationID.IsGroundID,_physicsCheck.isGround);
            _animator.SetBool(AnimationID.IsSquatID,_physicsCheck.isGround && GameInputManage.MainInstance.Squat);
            _animator.SetBool(AnimationID.IsWalkID, GameInputManage.MainInstance.Walk);
            _animator.SetBool(AnimationID.IsDeadID, _playerControl.isDead);
            _animator.SetBool(AnimationID.IsAttackID, _playerControl.isAttack);
        }

        public void PlayHurt()
        {
            _animator.SetTrigger(AnimationID.HurtID);
        }
    }
}
