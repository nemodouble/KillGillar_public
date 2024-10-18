using System.Collections;
using Boss;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    public class ScouterController : ProjectileRaiderController
    {
        [Header("Scouter stats")] 
        public float increaseRate = 0.1f;
        public float increaseTime = 10f;
        
        protected override void Skill()
        {
            StartCoroutine(WeaknessCapture());
            base.Skill();
        }

        IEnumerator WeaknessCapture()
        {
            BossGameObject.GetComponent<BossController>().damageReduce -= increaseRate;
            yield return new WaitForSeconds(increaseTime);
            BossGameObject.GetComponent<BossController>().damageReduce += increaseRate;
        }
    }
}