using System;
using System.Collections;
using Raider.BaseRaider;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boss.Pattern
{
    public class TargetAttackPattern : BossPattern
    {
        public float attackRange = 2f;
        public GameObject attackTarget;
        public Coroutine WaitAttackCoroutine;

        public override void CancelPattern()
        {
            base.CancelPattern();
            WaitAttackCoroutine = null;
        }
        
        public override void UsePattern(Vector2 position, Rigidbody2D rb2D)
        {
            base.UsePattern(position, rb2D);
            if(!IsCoolDown()) return;
            if(GetObjectByMousePosition() == null)
            {
                ResetCoolDown(1.0f);
                return;
            }
            var raiderGameObject = GetObjectByMousePosition();
            var raiderController = raiderGameObject.GetComponent<RaiderController>();
            
            if (Vector2.Distance(position, transform.position) < attackRange)
            {
                _BossController.StopMove();
                ApplyAttack(raiderController);
            }
            else
            {
                attackTarget = _BossController.moveTarget = raiderGameObject;
                _BossController.isMoving = true;
                WaitAttackCoroutine = StartCoroutine(WaitToAttack());
            }
        }

        IEnumerator WaitToAttack()
        {
            while (Vector2.Distance(attackTarget.transform.position, transform.position) >= attackRange)
            {
                yield return null;
            }
            _BossController.StopMove();
            var raiderController = attackTarget.GetComponent<RaiderController>();
            ApplyAttack(raiderController);
        }

        protected virtual void ApplyAttack(RaiderController raiderController)
        {
            raiderController.Damaged(attackDamage);
            _BossController.NormalAttackAnimationTrigger();
            _nowCooldown = cooldown;
        }
        
        // 마우스 위치의 오브젝트 찾기
        private GameObject GetObjectByMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Raider"));
            if (hit.collider == null) return null;
            return hit.collider.gameObject;
        }
    }
}