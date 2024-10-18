using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Raider.BaseMover;
using Raider.BaseRaider;
using UnityEngine;

namespace Boss.Pattern
{
    public class RoarRange : MonoBehaviour
    {
        private RoarPattern _roarPattern;

        private List<GameObject> _debuffedRaiders = new();
        private float damage = 10f;
        
        
        private void Start()
        {
            _roarPattern = transform.parent.GetComponent<RoarPattern>();
        }

        private void OnEnable()
        {
            // 1프레임 후 disable
            StartCoroutine(DisableAfterFrame());
        }

        IEnumerator DisableAfterFrame()
        {
            yield return new WaitForSeconds(0.1f);
            _debuffedRaiders.Clear();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Raider") && !_debuffedRaiders.Contains(col.gameObject))
            {
                _debuffedRaiders.Add(col.gameObject);
                _roarPattern.raiderMover = col.gameObject.GetComponent<RaiderMover>();
                col.gameObject.GetComponent<RaiderController>().Damaged(damage);
            }
        }
    }
}