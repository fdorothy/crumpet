using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCamera : MonoBehaviour
{
    public Camera mainCamera;
    protected Camera particleCamera;

    private void Start()
    {
        particleCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        particleCamera.orthographicSize = mainCamera.orthographicSize;
    }
}
