using UnityEngine;

namespace Raider.BaseMover
{
    public interface IMovingPattern
    {
        void Move(Rigidbody2D rigidbody2D, Animator animator);
        
        void SetSpeed(float speed);
    }
}