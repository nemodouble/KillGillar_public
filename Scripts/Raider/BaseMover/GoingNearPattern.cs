using UnityEngine;

namespace Raider.BaseMover
{
    public class GoingNearPattern : IMovingPattern
    {
        private float Speed { get; set; }
        private float Distance { get; set; }
        private bool MaintainDistance { get; set; }
        // 여유 간격
        private const float Margin = 1f;

        private Animator _animator;
        private static readonly int Walking = Animator.StringToHash("Walking");

        public GoingNearPattern(float speed, float distance, bool maintainDistance)
        {
            Speed = speed;
            Distance = distance;
            MaintainDistance = maintainDistance;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void Move(Rigidbody2D rigidbody2D, Animator animator)
        {
            var bossGameObject = GameObject.FindWithTag("Boss");
            // 거리 유지
            var distance = Vector2.Distance(rigidbody2D.position, bossGameObject.transform.position);

            animator.SetBool(Walking, true);
            float speed;
            if (distance > Distance)
                speed = Speed;
            else if (distance < Distance - Margin && MaintainDistance)
                speed = -Speed;
            else
            {
                speed = 0f;
                animator.SetBool(Walking, false);
            }

            rigidbody2D.MovePosition(Vector2.MoveTowards(rigidbody2D.position, bossGameObject.transform.position,
                speed * Time.fixedDeltaTime));
        }
    }
}