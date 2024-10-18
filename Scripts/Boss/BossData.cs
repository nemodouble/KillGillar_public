using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boss
{
    public class BossData : MonoBehaviour
    {
        // Singleton
        public static BossData Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple BossData instances detected!");
                Destroy(this);
            }
        }

        public float health = 2000f;
        /// <summary>
        /// 패턴의 피해량에 배수로 적용됨 
        /// </summary>
        public float strength = 1.0f;
        [FormerlySerializedAs("speed")] public float moveSpeed;
    
        public GameObject[] patterns;
    }
}
