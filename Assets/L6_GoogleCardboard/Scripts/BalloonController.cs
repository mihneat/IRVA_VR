using L6_GoogleCardboard.Scripts.Helpers;
using UnityEngine;

namespace L6_GoogleCardboard.Scripts
{
    /// <summary>
    /// Script which controls the behavior of balloon game objects.
    /// </summary>
    public class BalloonController : MonoBehaviour
    {
        /*
         * Extra info!
         *  --> The [SerializeField] attribute enables you to keep variables private whilst exposing their value in the editor's inspector.
         *  --> The [Range(...)] attribute is a nice way to set limits on the value of a variable & it also exposes it as a slider in the editor's inspector.
         */
        [SerializeField] [Range(0.25f, 5f)] private float speed = 1f;
        [SerializeField] [Range(0.25f, 2f)] private float popTime = 0.75f;
        [SerializeField] private AudioClip popSound;
        [SerializeField] private Gradient colors;
        
        private float _balloonPopTimeInternal = 0f;
        private bool _isBalloonTargeted = false;
        private MeshRenderer _balloonMeshRenderer;

        private void Awake() => _balloonMeshRenderer = GetComponent<MeshRenderer>();
        
        /// <summary>
        /// This is called from 'CardboardReticlePointer' when the reticle (crosshair) raycasts the collider of this object
        /// </summary>
        public void OnPointerEnter()
        {
            // TODO 3.1 : Set the bool value which indicates this game object is targeted.
        }
        
        /// <summary>
        /// This is called from 'CardboardReticlePointer' when the reticle (crosshair) raycasts the collider of this object
        /// </summary>
        public void OnPointerExit()
        {
            // TODO 3.2 : Set the bool value which indicates this game object is not targeted anymore.
            
            _balloonPopTimeInternal = 0f;
        }
        
        private void Update()
        {
            MoveBalloon();
            UpdateBalloonTimer();
            ColorBalloon();
        }

        private void MoveBalloon()
        {
            // TODO 2.1 : Make the balloons move upwards. Don't forget you have a speed variable already defined.
        }

        private void UpdateBalloonTimer()
        {
            /* TODO 3.3 : Increment the internal timer (_balloonPopTimeInternal) when the balloon is targeted (gazed at using the reticle).
             *            Pop the balloon if the internal timer exceeds the maximum timer ('popTime' variable).
             *            Hints: - OnPointerEnter() & OnPointerExit() already set a bool value which indicates if the balloon is gazed at (or not).
             *                   - Use the already provided method to pop the balloon.
             */
        }
        
        private void ColorBalloon() => _balloonMeshRenderer.material.color = colors.Evaluate(_balloonPopTimeInternal.Remap(0f, popTime, 0f, 1f));

        private void PopBalloon()
        {
            if (popSound != null)
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
            }
            ScoreController.Instance.IncrementScore();
            DestroyBalloon();
        }

        public void DestroyBalloon()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
