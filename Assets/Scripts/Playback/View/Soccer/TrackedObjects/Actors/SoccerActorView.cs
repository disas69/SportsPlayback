using Framework.Utils.Math;
using UnityEngine;

namespace Sports.Playback.View.Soccer.TrackedObjects.Actors
{
    public class SoccerActorView : TrackedObjectView
    {
        private const float MaxSpeed = 5f;

        private readonly int _speedHash = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void SetSpeed(float speed)
        {
            base.SetSpeed(speed);
            _animator.SetFloat(_speedHash, Speed / MaxSpeed);
        }

        public override void SetPosition(Vector3 position)
        {
            base.SetPosition(position);

            Quaternion rotation;

            if (Direction != Vector3.zero && Speed / MaxSpeed > 0.3f)
            {
                rotation = Quaternion.LookRotation(Direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 7.5f * Time.deltaTime);
            }
            else
            {
                var direction = (transform.position - _target.transform.position.WithY(transform.position.y)).normalized;
                if (direction != Vector3.zero)
                {
                    rotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
                }
            }
        }
    }
}