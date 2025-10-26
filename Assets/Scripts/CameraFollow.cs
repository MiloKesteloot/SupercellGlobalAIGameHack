using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    Vector3 pos = new Vector3(0, 25, 0);
    Vector3 vel = new Vector3(0, 0, 0);
    public float deadZoneRadius = 3;
    public float maxRadius = 5;
    public float maxCamSpeed = 20;


    public float baseHeight = 25;
    public float minZoom = -2;
    public float maxZoom = 2;
    public float zoomExp = 0.0f;
    public float zoomRate = 2f;   // per 

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
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
            
            float zoomDir = 0;
            if (Input.GetKey(KeyCode.Q)) zoomDir += 1;
            if (Input.GetKey(KeyCode.E)) zoomDir -= 1;
            zoomExp = Mathf.Clamp(zoomExp + zoomDir * zoomRate * Time.deltaTime, minZoom, maxZoom);
            pos.y = Mathf.Pow(2, zoomExp) * baseHeight;

        }
        pos += vel * Time.deltaTime;
        transform.position = pos;
    }
}