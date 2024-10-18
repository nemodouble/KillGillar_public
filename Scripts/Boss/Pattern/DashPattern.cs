using System.Collections;
using Raider;
using UnityEngine;

namespace Boss.Pattern
{
    public class DashPattern : BossPattern
    {
        private Coroutine _coroutine;
        private Vector2 _position;
        private Rigidbody2D _rigidbody2D;
        
        public override void CancelPattern()
        {
            base.CancelPattern();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        public override void UsePattern(Vector2 position, Rigidbody2D rb2D)
        {
            base.UsePattern(position, rb2D);
            if (!IsCoolDown()) return;
            _BossController.StopMove();
            _position = position;
            _coroutine = StartCoroutine(Dash());
            _rigidbody2D = rb2D;
        }

        IEnumerator Dash()
        {
            _BossController.SetCanMove(false);
            _BossController._animator.SetTrigger("Dash");
            yield return new WaitForSeconds(delay);
            RaidMaster.Instance.FarAwayFromBoss(3f, 1f, 4f);
            //쿨 돌리기
            _nowCooldown = cooldown;
            // 위치 도달시 까지 이동
            while (Vector2.Distance(_BossController.transform.position, _position) > 0.1f)
            {
                // 속력으로 이동
                _rigidbody2D.velocity = (_position - (Vector2) _BossController.transform.position).normalized * 7f;
                yield return null;
            }
            _BossController.SetCanMove(true);
        }
    }
}