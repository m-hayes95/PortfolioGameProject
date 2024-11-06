using System;
using UnityEngine;

public class SphereRayDebug : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private float collisionRadius;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask playerLayerMask;
    
    private void OnDrawGizmos()
    {
        Collider[] hitColliders = Physics.OverlapSphere(origin.position + offset, collisionRadius, playerLayerMask);

        if (hitColliders.Length == 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(origin.position + offset, collisionRadius);
            return;
        }

        Gizmos.color = hitColliders[0].GetComponent<PlayerTag>() != null ? Color.green : Color.red;
        Gizmos.DrawWireSphere(origin.position + offset, collisionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"Game object: {hitCollider.name} is within the radius"); 
        }
    }
}
