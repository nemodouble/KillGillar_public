using System;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider.BaseMover
{
    public class RaiderMover : MonoBehaviour
    {
        public float walkSpeed = 3f;
        public bool maintainDistance = false;

        private float walkSpeedBuffTimes = 1f;
        private float walkSpeedBuffAmount = 0f;

        private RaiderController _controller;
        private Rigidbody2D _rigid2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private IMovingPattern _nowMovingPattern;
        private GoingNearPattern _basicMovingPattern;

        private void Start()
        {
            _rigid2D = GetComponent<Rigidbody2D>();
            _controller = GetComponent<RaiderController>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
            _nowMovingPattern = _basicMovingPattern =
                new GoingNearPattern(GetWalkSpeed(), _controller.attackRange, maintainDistance);
        }

        private void FixedUpdate()
        {
            if (_nowMovingPattern == null)
            {
                _nowMovingPattern = _basicMovingPattern;
            }
            _nowMovingPattern.Move(_rigid2D, _animator);
            // 보스방향으로 스프라이트 반전
            var boss = GameObject.FindWithTag("Boss");
            
            if (boss != null)
            {
                var bossPosition = boss.transform.position;
                var myPosition = transform.position;
                if (bossPosition.x < myPosition.x)
                {
                    _spriteRenderer.flipX = true;
                }
                else
                {
                    _spriteRenderer.flipX = false;
                }
            }
            
        }
        
        public void SetWalkSpeedBuff(float times, float amount)
        {
            walkSpeedBuffTimes += times;
            walkSpeedBuffAmount += amount;
            _nowMovingPattern.SetSpeed(GetWalkSpeed());
        }

        public float GetWalkSpeed()
        {
            return walkSpeed * (walkSpeedBuffTimes > 0 ? walkSpeedBuffTimes : 0) + walkSpeedBuffAmount;
        }
        
        public void SetMovingPattern(IMovingPattern movingPattern)
        {
            _nowMovingPattern = movingPattern ?? _basicMovingPattern;
        }
    }
}