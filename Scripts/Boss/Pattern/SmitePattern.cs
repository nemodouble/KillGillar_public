using System.Collections;
using Raider;
using Raider.BaseRaider;
using UnityEngine;

namespace Boss.Pattern
{
    public class SmitePattern : TargetAttackPattern
    {
        public float smiteDamagePercentage = 0.4f;
        RaiderController raiderController;
        private static readonly int Smite1 = Animator.StringToHash("Smite");

        protected override void ApplyAttack(RaiderController raiderController)
        {
            this.raiderController = raiderController;
            StartCoroutine(Smite());
        }

        IEnumerator Smite()
        {
            _BossController.SetCanMove(false);
            _BossController._animator.SetTrigger(Smite1);
            RaidMaster.Instance.FarAwayFromBoss( raiderController, attackRange, delay, 4);
            _nowCooldown = cooldown;
            patternState = PatternState.OnDelay;
            yield return new WaitForSeconds(delay);
            if(Vector2.Distance(raiderController.transform.position, transform.position) < attackRange)
            {
                raiderController.Damaged(0, smiteDamagePercentage);
            }
            else
            {
                ResetCoolDown(0.5f);
            }
            _BossController.SetCanMove(true);
            patternState = PatternState.Wait;
        }
    }
}