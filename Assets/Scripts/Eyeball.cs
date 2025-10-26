using UnityEngine;

public class Eyeball : Organ {
    [Header("Eyeball Settings")]
    public float fieldOfView = 90f;   // degrees (e.g. 90 means 45° to each side)
    public float viewDistance = 4f;   // range the eye can see

    public override float ExternalSignal() {
        bool active = checkAll();
        Vector3 origin = transform.position; // re-getting these is crappy, quick fix
        Vector3 forward = transform.forward;
        DrawFOVDebugLines(origin, forward, active ? Color.green : Color.red);
        return active ? 1 : 0;
    }

    bool checkAll() {
        if (IsInFieldOfView(GetMouseWorldPosition())) return true;
        foreach (Food food in Manager.foodItems) {
            if (IsInFieldOfView(food.transform.position)) return true;
        }
        return false;
    }

    Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Y = 0 plane for top-down view

        if (plane.Raycast(ray, out float distance)) {
            return ray.GetPoint(distance);
        }

        return Vector3.zero; // fallback if ray doesn't hit
    }

    bool IsInFieldOfView(Vector3 target) {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        Vector3 toTarget = target - origin;
        if (toTarget.magnitude > viewDistance) return false;
        Vector3 directionToTarget = toTarget.normalized;
        float angle = Vector3.Angle(forward, directionToTarget);

        bool inFOV = angle <= fieldOfView / 2f;


        return inFOV;
    }

    void DrawFOVDebugLines(Vector3 origin, Vector3 forward, Color color) {
        // Half the FOV in radians
        float halfFOV = fieldOfView / 2f;

        // Left and right directions
        Vector3 leftDir = Quaternion.Euler(0, -halfFOV, 0) * forward;
        Vector3 rightDir = Quaternion.Euler(0, halfFOV, 0) * forward;

        // Draw lines in Scene view
        float viewDistance = 100;
        Debug.DrawRay(origin, leftDir * viewDistance, color, duration: 0, depthTest: false);
        Debug.DrawRay(origin, rightDir * viewDistance, color, duration: 0, depthTest: false);
    }
}

