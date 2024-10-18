using Boss;

namespace Raider.BaseRaider
{
    public abstract class TargetingRaiderController : RaiderController
    {
        protected new void Start()
        {
            base.Start();
        }
        protected override void Attack()
        {
            //TODO: Add attack animation
            BossGameObject.GetComponent<BossController>().Damaged(GetCalculatedAttackDamage());
            base.Attack();
        }
    }
}