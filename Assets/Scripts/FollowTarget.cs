using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
