using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class SoccerActorView : TrackedObjectView
    {
        private const float MaxSpeed = 5f;

        private readonly int _speedHash = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;

        public override void SetSpeed(float speed)
        {
            base.SetSpeed(speed);
            _animator.SetFloat(_speedHash, Speed / MaxSpeed);
        }

        public override void SetPosition(Vector3 position)
        {
            base.SetPosition(position);

            if (Direction != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(Direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 7f * Time.deltaTime);
            }
        }
    }
}