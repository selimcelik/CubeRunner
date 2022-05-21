// I want to use cinemachine but I decided to write this script because of the risk of not being accepted.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    private GameManager _gameManager;

    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
