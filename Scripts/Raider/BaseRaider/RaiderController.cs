using System;
using System.Collections;
using DefaultNamespace;
using Raider.BaseMover;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Raider.BaseRaider
{
    public abstract class RaiderController : MonoBehaviour , IDamaged
    {
        // 레이더 스텟
        [FormerlySerializedAs("health")] [Header("Raider Stats")]
        public float maxHealth;
        //TODO : add min max
        public float attackDamage;
        /// <summary>
        /// 초당 공격 횟수
        /// </summary>
        public float attackCool;
        public float attackRange;
        public float attackBuffTimes = 1f;
        public float attackBuffAmount = 0;
        public float skillCost;
        public float skillCool;
        public bool skillHasRange;
        public float skillRange;
        public float criticalChance = 0.1f;
        public float criticalTime = 2f;
        public float damageReduce = 0f;
        public string skillName;
        public int aiLevel = 1;

        // 레이더 상태
        protected float _nowHealth;
        protected float _nowAttackCool;
        protected float _nowSkillCool;
        protected float _nowMana;
        protected bool isDead;

        // 레이더 컴포넌트
        private Animator _animator;
        
        // 참조 컴포넌트
        protected GameObject BossGameObject;
        
        // HP바
        public GameObject hpBarPrefab;
        public GameObject hpBarGameObject;
        public Canvas raiderHpBarCanvas;
        public Slider raiderHpBarSlider;
        public Slider raiderMpBarSlider;
        
        // Start is called before the first frame update
        protected void Start()
        {
            BossGameObject = GameObject.Find("Boss");
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            SetHpBar();
            //체력 슬라이더 활성화
            raiderHpBarSlider.gameObject.SetActive(true);
            //체력 슬라이더의 최댓값을 기본 체력값으로 변경
            raiderHpBarSlider.maxValue = maxHealth;
            //체력 슬라이더의 값을 현재 체력값으로 변경
            raiderHpBarSlider.value = maxHealth;
            
            raiderMpBarSlider.maxValue = skillCost;
            raiderMpBarSlider.value = _nowMana;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;
            
            // 기본 공격
            if (_nowAttackCool < attackCool)
            {
                _nowAttackCool += Time.deltaTime;
            }
            else
            {
                if(CanAttack())
                    Attack();
            }
            // 스킬 공격
            if (_nowSkillCool < skillCool)
            {
                _nowSkillCool += Time.deltaTime;
            }
            else
            {
                if(CanUseSkill())
                    Skill();
            }
            
        }

        private void OnDestroy()
        {
            RaidMaster.Instance.DeleteRaider(this);
            Destroy(hpBarGameObject);
        }


        private bool CanAttack()
        {
            // 보스 까지 거리가 사정범위 이내라면
            if (Vector3.Distance(transform.position, BossGameObject.transform.position) <= attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected virtual void Attack()
        {
            _animator.SetTrigger("Attack");
            _nowMana += 1;
            raiderMpBarSlider.value = _nowMana;
            _nowAttackCool = 0;
        }
        
        protected float GetCalculatedAttackDamage()
        {
            float damage;
            if (Random.Range(0f, 1f) < criticalChance)
            {
                damage = attackDamage * criticalChance;
            }
            else
            {
                damage = attackDamage;
            }
            damage *= attackBuffTimes;
            damage += attackBuffAmount;
            return damage;
        }
        
        private bool CanUseSkill()
        {
            // 스킬 사거리 확인
            var skillRangeCheck = !skillHasRange || Vector3.Distance(transform.position, BossGameObject.transform.position) <= skillRange;
            // 스킬 쿨타임 확인
            var skillCoolCheck = _nowSkillCool >= skillCool;
            // 스킬 마나 확인
            var skillManaCheck = _nowMana >= skillCost;
            
            return skillRangeCheck && skillCoolCheck && skillManaCheck;
        }

        protected virtual void Skill()
        {
            _nowMana = 0;
            raiderMpBarSlider.value = _nowMana;
            _nowSkillCool = 0;
        }
        
        public void Damaged(float damageAmount, float damagePercentage = 0f)
        {
            var damage = maxHealth * damagePercentage + damageAmount;
            maxHealth -= damage * (1 - damageReduce > 0 ? 1 - damageReduce : 0);

            raiderHpBarSlider.value = maxHealth;
            
            if (maxHealth <= 0)
            {
                Dead();
            }
        }

        protected void Dead()
        {
            isDead = true;
            Destroy(GetComponent<RaiderMover>());
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(hpBarGameObject);
            _animator.SetTrigger("Dead");
            StartCoroutine(DestroyAfterSeconds());
        }
        
        IEnumerator DestroyAfterSeconds()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        
        void SetHpBar()
        {
            raiderHpBarCanvas = GameObject.Find("Enemy Hp Bar Canvas").GetComponent<Canvas>();
            GameObject hpBar = Instantiate(hpBarPrefab, raiderHpBarCanvas.transform);
            hpBarGameObject = hpBar;
            raiderHpBarSlider = hpBar.transform.Find("HP").GetComponent<Slider>();
            raiderMpBarSlider = hpBar.transform.Find("MP").GetComponent<Slider>();
            var _hpbar = hpBar.GetComponent<RaiderHpBar>();
            _hpbar.enemyTr = this.gameObject.transform;
        }
        
    }
}
