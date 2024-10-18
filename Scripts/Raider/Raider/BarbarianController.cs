using System.Collections;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    public class BarbarianController : RangedRaiderController
    {
        [Header("Barbarian Stats")]
        [Tooltip("최대 체력 퍼센트")]
        public float costHealth = 0.1f;
        public float attackTime = 3f;
        public float buffDuration = 10f;
        
        protected override void Skill()
        {
            StartCoroutine(BattleCry());
            base.Skill();
        }

        IEnumerator BattleCry()
        {
            maxHealth -= maxHealth * costHealth;
            attackBuffTimes += 2f;
            yield return new WaitForSeconds(buffDuration);
            attackBuffTimes -= 2f;
        }
    }
}