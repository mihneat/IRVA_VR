using UnityEngine;

namespace L6_GoogleCardboard.Scripts
{
    /// <summary>
    /// Trigger which destroys balloons which touch it.
    /// </summary>
    public class BalloonPopper : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var potentialBalloon = other.GetComponent<BalloonController>();
            potentialBalloon?.DestroyBalloon(); // Null check with '?'
            
            // TODO 4.3 : Decrement score
        }
    }
}
