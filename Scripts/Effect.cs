using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float lifeTime = 0.333f;
    void Start()
    {
        //destroy affter lifeTime
        Destroy(gameObject, lifeTime);
    }
}
