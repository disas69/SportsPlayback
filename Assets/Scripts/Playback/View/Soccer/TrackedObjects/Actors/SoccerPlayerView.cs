using Sports.Playback.Data.Soccer;
using UnityEngine;
using TMPro;

namespace Sports.Playback.View.Soccer
{
    public class SoccerPlayerView : SoccerActorView
    {
        [SerializeField] private TextMeshPro _number;

        public override void Setup(TrackedObject trackedObject)
        {
            base.Setup(trackedObject);
            SetNumber(trackedObject.ShirtNumber);
        }

        private void SetNumber(int number)
        {
            _number.text = number.ToString();
        }
    }
}