using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Raider
{
    public class HawkEyeController : ProjectileRaiderController
    {
        [Header("HawkEye Stats")]
        public GameObject falconPrefab;
        protected override void Skill()
        {
            Instantiate(falconPrefab, transform.position, Quaternion.identity);
            base.Skill();
        }
    }
}