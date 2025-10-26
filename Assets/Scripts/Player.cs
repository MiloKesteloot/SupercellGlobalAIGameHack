using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    public Eyeball eyeball;
    public Flipper flipper;
    public Bias bias;
    public GameObject neuron;

    public static GameObject neuronStatic;
    [HideInInspector] public Rigidbody rb;
    private Organ dragging;

    public void Start() {
        rb = GetComponent<Rigidbody>();

        neuronStatic = neuron;

        Organ eye1  = CreateOrgan(eyeball, 1, 1, 0,  transform); eye1.transform.Rotate(0,  15, 0);
        Organ eye2  = CreateOrgan(eyeball,-1, 1, 0,  transform); eye2.transform.Rotate(0, -15, 0);
        Organ flip1 = CreateOrgan(flipper, 2f,-1, 0, transform); flip1.inupts.Add(eye1); flip1.transform.Rotate(0,-90 - 45, 0);
        Organ flip2 = CreateOrgan(flipper,-2f,-1, 0, transform); flip2.inupts.Add(eye2); flip2.transform.Rotate(0, 90 + 45, 0);

        Organ bias1 = CreateOrgan(bias, 0, 0, 0, transform);
        Organ flip3 = CreateOrgan(flipper,-1,-1, 0, transform); flip3.inupts.Add(bias1);
        Organ flip4 = CreateOrgan(flipper, 0,-1, 0, transform); flip4.inupts.Add(bias1);
        Organ flip5 = CreateOrgan(flipper, 1,-1, 0, transform); flip5.inupts.Add(bias1);
    }

    public Organ CreateOrgan(Organ prefab, float x, float z, float r, Transform t) {
        Organ component = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.Euler(0, r, 0), t);
        component.player = this;
        return component;
    }

    public void FixedUpdate() {
        Organ.UpdateAll();
    }

    public void BuildEyeball() {
        BuildThing(eyeball);
    }

    public void BuildFlipper() {
        BuildThing(flipper);
    }

    public void BuildBias() {
        BuildThing(bias);
    }

    public void BuildThing(Organ organ) {
        CancelDragging();
        Vector3 mousePos = GetMouseWorldPosition();
        dragging = CreateOrgan(organ, mousePos.x, mousePos.z, 0, null);
    }

    void CancelDragging() {
        if (!dragging) return;
        dragging.Destroy();
        dragging = null;
    }

    void Update() {
        if (dragging) DraggingUpdate();
        else NonDraggingUpdate();
    }
    
    void NonDraggingUpdate() {
        if (Input.GetMouseButtonDown(1)) {
            // Cast a ray from the camera to where the mouse is
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                // Check if the clicked object has a component of type Organ (or subclass)
                Organ organ = hit.collider.GetComponent<Organ>();
                if (organ != null) {
                    organ.Destroy();
                }
            }
            return;
        }
    }

    void DraggingUpdate() {
        dragging.transform.position = GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0)) {
            dragging.transform.SetParent(this.transform);
            dragging = null;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
            CancelDragging();
        }
    }
    
    Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Y = 0 plane for top-down view

        if (plane.Raycast(ray, out float distance)) {
            return ray.GetPoint(distance);
        }

        return Vector3.zero; // fallback if ray doesn't hit
    }
}


