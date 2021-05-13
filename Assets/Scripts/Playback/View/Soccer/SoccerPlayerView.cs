using TMPro;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class SoccerPlayerView : TrackedObjectView
    {
        private const float MaxSpeed = 5f;

        private readonly int _speedHash = Animator.StringToHash("Speed");

        private Vector3 _lastPosition = Vector3.zero;

        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshPro _number;

        public override void SetSpeed(float speed)
        {
            base.SetSpeed(speed);
            _animator.SetFloat(_speedHash, Speed / MaxSpeed);
        }

        public void SetNumber(int number)
        {
            _number.text = number.ToString();
        }

        public override void SetPosition(Vector3 position)
        {
            base.SetPosition(position);

            var direction = (_lastPosition - transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 7.5f * Time.deltaTime);

            _lastPosition = transform.position;
        }
    }
}