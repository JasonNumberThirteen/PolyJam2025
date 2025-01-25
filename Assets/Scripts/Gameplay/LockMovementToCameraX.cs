using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMovementToCameraX : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private void Update()
    {
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, transform.position.z);
    }
}
