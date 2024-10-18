using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerOrderer : MonoBehaviour
{
    public bool isOnBossSprite;
    void Update()
    {
        if (isOnBossSprite)
            GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -100) + (isOnBossSprite ? 1000 : 0);
    }
}
