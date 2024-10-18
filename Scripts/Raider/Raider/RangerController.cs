using System.Collections;
using UnityEngine;

namespace Raider
{
    public class RangerController : ProjectileRaiderController
    {
        public GameObject skillArrowPrefab;
        protected override void Skill()
        {
            StartCoroutine(TripleArrow());
            base.Skill();
        }

        private IEnumerator TripleArrow()
        {
            for (int i = 0; i < 3; i++)
            {
                var arrow = Instantiate(skillArrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Projectile>().SetTarget(BossGameObject);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}