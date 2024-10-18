using System;
using Boss;
using UnityEngine;

namespace DefaultNamespace
{
    public class BossSprite : SortingLayerOrderer
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.CompareTag($"Raider"))
                col.gameObject.transform.Find("Body").GetComponent<SortingLayerOrderer>().isOnBossSprite = true;
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if(col.gameObject.CompareTag($"Raider"))
                col.gameObject.transform.Find("Body").GetComponent<SortingLayerOrderer>().isOnBossSprite = false;
        }
    }
}