using System.Collections;
using UnityEngine;

namespace L6_GoogleCardboard.Scripts
{
    /// <summary>
    /// Used to control the spawn behavior of balloons.
    /// </summary>
    public class BalloonSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject balloonPrefab;
        [SerializeField] [Range(0.1f, 5f)] private float spawnRate = 1.5f;

        private BoxCollider _boxCollider;

        private void Awake() => _boxCollider = GetComponent<BoxCollider>();

        private void Start() => StartCoroutine(SpawnBalloonsRoutine());

        private IEnumerator SpawnBalloonsRoutine()
        {
            for (;;)
            {
                /* TODO 1.1 : Get the spawning position for the balloon.
                 *            Balloons should spawn at a random position inside the volume of the bounding box.
                 *            Hint: Look in the Utils functions for a helpful method & use the bounds attribute of the BoxCollider.
                 */
                Vector3 spawnPos = Vector3.zero;
                
                if(balloonPrefab != null)
                {
                    // TODO 1.2 : Spawn the balloon.
                    var inst = new GameObject();
                }
                else
                {
                    Debug.LogWarning("'balloonPrefab' is NULL!");
                }
                
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}
