using UnityEngine;

namespace Sports.Playback.View.Soccer.TrackedObjects
{
    public class SoccerBallView : TrackedObjectView
    {
        [SerializeField] private Transform _view;

        public override void SetSpeed(float speed)
        {
            base.SetSpeed(speed);

            if (Direction != Vector3.zero)
            {
                _view.Rotate(-Direction, Speed * Time.deltaTime);
            }
        }
    }
}