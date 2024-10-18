using System;
using System.Collections;
using System.Collections.Generic;
using Boss;
using Boss.Pattern;
using Raider.BaseMover;
using Raider.BaseRaider;
using UnityEngine;

namespace Raider
{
    
    public class RaidMaster : MonoBehaviour
    {
        private BossController bossController;
        private List<RaiderMover> raiderMovers = new();
        
        private BossPattern previousPattern;
        private List<float> timerReterunToBasicPattern = new();

        public int aiLevel = 10;
        
        // Singleton
        private static RaidMaster _instance;
        public static RaidMaster Instance => _instance;
        
        public void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this; 
            }
            bossController = FindObjectOfType<BossController>();
            raiderMovers.AddRange(FindObjectsOfType<RaiderMover>());
            timerReterunToBasicPattern = new List<float>();
            for (int i = 0; i < raiderMovers.Count; i++)
            {
                timerReterunToBasicPattern.Add(0f);
            }
        }

        private void Update()
        {
            for (int i = 0; i < timerReterunToBasicPattern.Count; i++)
            {
                if (timerReterunToBasicPattern[i] >= 0)
                {
                    timerReterunToBasicPattern[i] -= Time.deltaTime;
                    if (timerReterunToBasicPattern[i] < 0)
                    {
                        raiderMovers[i].SetMovingPattern(null);
                    }
                }
            }
        }

        /// <summary>
        /// Entire raiders try to Far Away from the boss
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="duration"></param>
        /// <param name="aiLevel"></param>
        public void FarAwayFromBoss(float distance, float duration, float aiLevel)
        {
            for (var index = 0; index < raiderMovers.Count; index++)
            {
                timerReterunToBasicPattern[index] = duration;
                var rm = raiderMovers[index];
                var raidController = rm.GetComponent<RaiderController>();
                var randomDistance = UnityEngine.Random.Range(distance + 1, distance + 3);
                if (raidController.attackRange < distance && raidController.aiLevel >= aiLevel)
                    rm.SetMovingPattern(new GoingNearPattern(rm.GetWalkSpeed(), randomDistance, true));
            }
        }
        /// <summary>
        /// Specific raider try to Far Away from the boss
        /// </summary>
        /// <param name="raiderController"></param>
        /// <param name="distance"></param>
        /// <param name="duration"></param>
        /// <param name="aiLevel"></param>
        public void FarAwayFromBoss(RaiderController raiderController, float distance, float duration, float aiLevel)
        {
            var rm = raiderController.GetComponent<RaiderMover>();
            var findIndex = raiderMovers.FindIndex(r => r.Equals(rm));
            timerReterunToBasicPattern[findIndex] = duration;
            var randomDistance = UnityEngine.Random.Range(distance + 1, distance + 3);
            if(raiderController.attackRange < distance && raiderController.aiLevel >= aiLevel)
                rm.SetMovingPattern(new GoingNearPattern(rm.GetWalkSpeed(), randomDistance, true));
        }

        public void DeleteRaider(RaiderController raiderController)
        {
            var rm = raiderController.GetComponent<RaiderMover>();
            var findIndex = raiderMovers.FindIndex(r => r.Equals(rm));
            timerReterunToBasicPattern.RemoveAt(findIndex);
            raiderMovers.RemoveAt(findIndex);
        }
    }
}