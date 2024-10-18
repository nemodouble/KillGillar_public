using UnityEngine;

namespace Raider.BaseRaider
{
    public abstract class RangedRaiderController : RaiderController
    {
        public GameObject AttackRange;

        protected new void Start()
        {
            base.Start();
            AttackRange = transform.Find("AttackRange").gameObject;
        }
        protected override void Attack()
        {
            AttackRange.SetActive(true);
            base.Attack();
        }
    }
}