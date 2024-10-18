using Raider.BaseRaider;
using UnityEngine;
using UnityEngine.Serialization;

namespace Raider
{
    public abstract class ProjectileRaiderController : RaiderController
    {
        public GameObject projectilePrefab;
        protected override void Attack()
        {
            //TODO: Attack animation
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().SetTarget(BossGameObject);
            projectile.GetComponent<Projectile>().SetDamage(GetCalculatedAttackDamage());
            base.Attack();
        }
    }
}