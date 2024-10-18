using System;
using Boss.Pattern;
using DefaultNamespace;
using Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Boss
{
    public class BossController : MonoBehaviour , IDamaged
    {
        // 성장 값
        public BossData bossData;
        
        // 인 레이드 값
        public float nowHealth;
        public float damageReduce = 0f;
        public bool isMoving;
        private bool canMoving;
        private Vector2 _movePosition;
        [HideInInspector]
        public GameObject moveTarget;
        private GameObject _castingPattern;
        
        
        // 인풋 관련
        private PlayerActions _playerActions;
        private InputAction _leftClick;
        private InputAction _rightClick;
        private InputAction[] _patternAction;
        
        // 컴포넌트
        public GameObject[] patternObjects;
        private BossPattern[] _bossPattern;
        public Rigidbody2D _rigidbody2D;
        public Animator _animator;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int NormalAttack = Animator.StringToHash("NormalAttack");

        void Awake()
        {
            _playerActions = new PlayerActions();
            _patternAction = new InputAction[5];
            _bossPattern = new BossPattern[5];
            for (int i = 0; i < _bossPattern.Length; i++)
            {
                _bossPattern[i] = patternObjects[i].GetComponent<BossPattern>();
            }
            
            _leftClick = _playerActions.gameplay.LeftClick;
            _rightClick = _playerActions.gameplay.RightClick;
            _patternAction[0] = _playerActions.gameplay.Pattern0;
            _patternAction[1] = _playerActions.gameplay.Pattern1;
            _patternAction[2] = _playerActions.gameplay.Pattern2;
            _patternAction[3] = _playerActions.gameplay.Pattern3;
            _patternAction[4] = _playerActions.gameplay.NormalAttack;
        }

        private void Start()
        {
            nowHealth = bossData.health;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = transform.Find("Animator").GetComponent<Animator>();
            canMoving = true;
        }

        private void OnEnable()
        {
            _leftClick.Enable();
            _rightClick.Enable();
            _leftClick.performed += LeftClick;
            _rightClick.performed += RightClick;
            
            foreach (var t in _patternAction)
            {
                t.Enable();
            }
            _patternAction[0].performed += DisplayPattern0;
            _patternAction[1].performed += DisplayPattern1;
            _patternAction[2].performed += DisplayPattern2;
            _patternAction[3].performed += DisplayPattern3;
            _patternAction[4].performed += DisplayNormalAttack;
        }


        private void OnDisable()
        {
            _rightClick.Disable();
            _leftClick.Disable();
            _leftClick.performed -= LeftClick;
            _rightClick.performed -= RightClick;
            
            foreach (var t in _patternAction)
            {
                t.Disable();
            }
            _patternAction[0].performed -= DisplayPattern0;
            _patternAction[1].performed -= DisplayPattern1;
            _patternAction[2].performed -= DisplayPattern2;
            _patternAction[3].performed -= DisplayPattern3;
            _patternAction[4].performed -= DisplayNormalAttack;
        }

        void FixedUpdate()
        {
            if (isMoving)
            {
                if(moveTarget != null)
                    _movePosition = moveTarget.transform.position;
                
                _rigidbody2D.velocity = (_movePosition - (Vector2) transform.position).normalized * bossData.moveSpeed;
                if (_rigidbody2D.velocity.x > 0)
                {
                    _animator.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    _animator.GetComponent<SpriteRenderer>().flipX = false;
                }
                if((_movePosition - (Vector2)transform.position).magnitude < 0.1f)
                {
                    isMoving = false;
                    _animator.SetBool(IsMoving, false);
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
        }
        private void RightClick(InputAction.CallbackContext obj)
        {
            // 스킬 취소
            if (_castingPattern != null)
            {
                if (_castingPattern.GetComponent<BossPattern>().patternState ==
                    BossPattern.PatternState.OnDelay) return;
                _castingPattern.GetComponent<BossPattern>().CancelPattern();
                _castingPattern = null;
                return;
            }
            
            // 기본공격 및 이동
            if (!canMoving) return;
            _animator.GetComponent<SpriteRenderer>().flipX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x > transform.position.x;
            // var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            // var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Raider"));
            // if (hit.collider != null)
            // {
            //     _bossPattern[4].UsePattern(hit.point, _rigidbody2D);
            // }
            // else
            // {
                isMoving = true;
                _animator.SetBool(IsMoving, true);
                _movePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // }
        }
        private void LeftClick(InputAction.CallbackContext obj)
        {
            if (!_castingPattern) return;
            _animator.GetComponent<SpriteRenderer>().flipX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x > transform.position.x;
            _castingPattern.GetComponent<BossPattern>().UsePattern(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), _rigidbody2D);
        }

        private void DisplayPattern0(InputAction.CallbackContext context)
        {
            DisplayPatternByIndex(0);
        }
        private void DisplayPattern1(InputAction.CallbackContext context)
        {
            DisplayPatternByIndex(1);
        }
        private void DisplayPattern2(InputAction.CallbackContext context)
        {
            DisplayPatternByIndex(2);
        }
        private void DisplayPattern3(InputAction.CallbackContext context)
        {
            DisplayPatternByIndex(3);
        }
        private void DisplayNormalAttack(InputAction.CallbackContext context)
        {
            DisplayPatternByIndex(4);
        }
        
        private void DisplayPatternByIndex(int index)
        {
            if (_castingPattern != null)
            {
                _castingPattern.GetComponent<BossPattern>().CancelPattern();
                _castingPattern = null;
            }
            _bossPattern[index].DisplayPattern();
            _castingPattern = patternObjects[index];
        }

        public void SetCanMove(bool canMove)
        {
            Debug.Log("SetCanMove" + canMove);
            canMoving = canMove;
            if(!canMove) StopMove();
        }
        
        public void StopMove()
        {
            isMoving = false;
            _animator.SetBool(IsMoving, false);
            moveTarget = null;
            if(_castingPattern != null)
                _castingPattern.GetComponent<BossPattern>().CancelPattern();
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void Damaged(float damageAmount, float damagePercentage = 0f)
        {
            var damage = bossData.health * damagePercentage + damageAmount;
            nowHealth -= damage * (1 - damageReduce > 0 ? 1 - damageReduce : 0);

            // HP UI
            FindObjectOfType<UiBossHP>().SetHp(nowHealth);
            
            if (nowHealth <= 0)
            {
                //TODO : 죽는 애니메이션
                // Destroy(gameObject);
            }
        }

        public void NormalAttackAnimationTrigger()
        {
            _animator.SetTrigger(NormalAttack);
        }
    }
}
