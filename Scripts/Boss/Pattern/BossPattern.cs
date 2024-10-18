using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boss.Pattern
{
    public abstract class BossPattern : MonoBehaviour
    {
        public enum PatternState
        {
            Wait,
            Display,
            OnDelay
        }
        
        [FormerlySerializedAs("damage")] public float attackDamage;
        public float delay;
        [FormerlySerializedAs("cost")] public int equipCost;
        public float cooldown;
        [FormerlySerializedAs("patternIcon")] public Sprite icon;
        public Sprite patternIconLocked;
        public float _nowCooldown;
        public PatternState patternState;
        [FormerlySerializedAs("patternExplanation")] [TextArea] public string explanation;
        
        protected BossController _BossController;
        
        private void Start()
        {
            //Boss Dependency
            _BossController = GameObject.FindWithTag("Boss").GetComponent<BossController>();
            transform.parent = _BossController.transform;
            _nowCooldown = 0;
            patternState = PatternState.Wait;
        }

        private void Update()
        {
            if(_nowCooldown > 0)
                _nowCooldown -= Time.deltaTime;
        }

        public virtual void DisplayPattern()
        {
            patternState = PatternState.Display;
            transform.Find("RangeDisplay").gameObject.SetActive(true);
        }
        public virtual void CancelPattern()
        {
            transform.Find("RangeDisplay").gameObject.SetActive(false);
            _BossController.SetCanMove(true);
        }

        public virtual void UsePattern(Vector2 position, Rigidbody2D rb2D)
        {
            if(!IsCoolDown()) return;
            
            // fmod 사운드 재생
            FMODUnity.RuntimeManager.PlayOneShot("event:/boss/growl", transform.position);

            var rangeDisplay = transform.Find("RangeDisplay").gameObject;
            if(rangeDisplay != null) rangeDisplay.SetActive(false);
        }
        
        public bool IsCoolDown()
        {
            return _nowCooldown <= 0;
        }
        
        public void ResetCoolDown(float resetPercent)
        {
            _nowCooldown = cooldown - cooldown * resetPercent;
        }
    }
}