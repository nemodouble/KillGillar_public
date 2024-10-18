using System.Collections;
using Raider;
using Raider.BaseMover;
using Raider.BaseRaider;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boss.Pattern
{
    public class RoarPattern : BossPattern
    {
        private Coroutine _coroutine;

        public float attackRange = 5f;
        public float slowTime = 5f;
        public float slowTimes = 0.5f;
        public RaiderMover raiderMover;
        private static readonly int Roar1 = Animator.StringToHash("Roar");

        public override void CancelPattern()
        {
            base.CancelPattern();
            if (_coroutine != null)
            {
                _BossController.SetCanMove(true);
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        public override void UsePattern(Vector2 position, Rigidbody2D rb2D)
        {
            base.UsePattern(position, rb2D);
            if (!IsCoolDown()) return;
            _BossController.StopMove();
            _coroutine = StartCoroutine(Roar());
            RaidMaster.Instance.FarAwayFromBoss(attackRange, delay, 1);
        }

        IEnumerator Roar()
        {
            _BossController._animator.SetTrigger(Roar1);
            _nowCooldown = cooldown;
            transform.Find("Range1").gameObject.SetActive(true);
            _BossController.SetCanMove(false);
            patternState = PatternState.OnDelay;
            yield return new WaitForSeconds(delay);
            transform.Find("Range2").gameObject.SetActive(true);
            _BossController.SetCanMove(true);
            patternState = PatternState.Wait;
        }

        public void StartSlow()
        {
            StartCoroutine(Slow());
        }
        
        private IEnumerator Slow()
        {
            var rm = raiderMover;
            rm.SetWalkSpeedBuff(-slowTimes,0f);
            yield return new WaitForSeconds(slowTime);
            rm.SetWalkSpeedBuff(slowTimes,0f);
        }
    }
}