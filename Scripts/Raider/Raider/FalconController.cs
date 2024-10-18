using System.Collections;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    public class FalconController : TargetingRaiderController
    {
        protected new void Start()
        {
            base.Start();
            StartCoroutine(DeadAfter60s());
        }

        private IEnumerator DeadAfter60s()
        {
            yield return new WaitForSeconds(60);
            Destroy(gameObject);
        }

        protected override void Skill()
        {
            base.Skill();
            // None
        }
    }
}