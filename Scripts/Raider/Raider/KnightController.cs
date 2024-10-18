using System.Collections;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    public class KnightController : TargetingRaiderController
    {
        [Header("Knight Stats")]
        public float buffDuration = 6f;
        
        protected override void Skill()
        {
            StartCoroutine(Breath());
            base.Skill();
        }

        IEnumerator Breath()
        {
            criticalChance = 1;
            damageReduce += 0.5f;
            yield return new WaitForSeconds(buffDuration);
            damageReduce -= 0.5f;
        }
    }
}