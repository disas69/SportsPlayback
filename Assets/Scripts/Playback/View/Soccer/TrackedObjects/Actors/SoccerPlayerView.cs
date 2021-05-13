using TMPro;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class SoccerPlayerView : SoccerActorView
    {
        [SerializeField] private TextMeshPro _number;

        public void SetNumber(int number)
        {
            _number.text = number.ToString();
        }
    }
}