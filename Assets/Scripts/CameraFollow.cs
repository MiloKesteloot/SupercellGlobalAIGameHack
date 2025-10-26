using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    Vector3 pos = new Vector3(0, 10, 0);
    Vector3 vel = new Vector3(0, 0, 0);
    public float deadZoneRadius = 3;
    public float maxRadius = 5;
    public float maxCamSpeed = 20;
    public float zoomOutspeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacment = objectToFollow.transform.position - transform.position;
        displacment.y = 0;
        
        float distance = (float) Math.Sqrt(displacment.x * displacment.x + displacment.z * displacment.z);
        vel *= 0;
        if (distance > deadZoneRadius) {
            Vector3 unitDisplacment = displacment / distance;
            distance -= deadZoneRadius;
            float speedFactor = distance / maxRadius;
            speedFactor = speedFactor * speedFactor;
            float cameraSpeed = maxCamSpeed * speedFactor;
            vel = unitDisplacment * cameraSpeed;
            float zoomRatio = cameraSpeed / zoomOutspeed;
            //camera.orthographicSize = 5 * (float)Math.Sqrt(1 + zoomRatio * zoomRatio);

        }
        pos += vel * Time.deltaTime;
        transform.position = pos;
    }
}