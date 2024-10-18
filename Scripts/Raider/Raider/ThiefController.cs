using System;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    public class ThiefController : TargetingRaiderController
    {
        [Header("Thief stats")]
        public float debuffDuration = 7f;
        
        protected override void Skill()
        {
            //TODO
            base.Skill();
        }
    }
}