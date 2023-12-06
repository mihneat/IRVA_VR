using System;
using UnityEngine;

namespace L7_HTCVive.Scripts
{
    public class ProjectileCannonController : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnTransform;
        [SerializeField] [Range(0.1f, 20)] private float projectileLaunchForce = 2f;

        private void Awake()
        {
            if(projectilePrefab == null || projectileSpawnTransform == null)
            {
                Debug.LogError("[ProjectileCannonController] Some inspector values have not been assigned!");
                throw new NullReferenceException();
            }
        }

        public void LaunchProjectile()
        {
            var projInst = Instantiate(projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
            var projRb = projInst.GetComponent<Rigidbody>();
            var projDir = projectileSpawnTransform.transform.up;
            projRb.AddForce(projDir * projectileLaunchForce, ForceMode.Impulse);
        }
    }
}