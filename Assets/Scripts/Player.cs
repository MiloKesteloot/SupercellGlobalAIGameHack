using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    public Eyeball eyeball;
    public Flipper flipper;
    public Bias bias;

    [HideInInspector] public Rigidbody rb;

    public void Start() {
        rb = GetComponent<Rigidbody>();

        Organ eye1  = CreateOrgan(eyeball, 1, 1, 0); eye1.transform.Rotate(0,  15, 0);
        Organ eye2  = CreateOrgan(eyeball,-1, 1, 0); eye2.transform.Rotate(0, -15, 0);
        Organ flip1 = CreateOrgan(flipper, 2f,-1, 0); flip1.inupts.Add(eye1); flip1.transform.Rotate(0,-90 - 45, 0);
        Organ flip2 = CreateOrgan(flipper,-2f,-1, 0); flip2.inupts.Add(eye2); flip2.transform.Rotate(0, 90 + 45, 0);

        Organ bias1 = CreateOrgan(bias, 0, 0, 0);
        Organ flip3 = CreateOrgan(flipper,-1,-1, 0); flip3.inupts.Add(bias1);
        Organ flip4 = CreateOrgan(flipper, 0,-1, 0); flip4.inupts.Add(bias1);
        Organ flip5 = CreateOrgan(flipper, 1,-1, 0); flip5.inupts.Add(bias1);
    }

    public Organ CreateOrgan(Organ prefab, float x, float z, float r) {
        Organ component = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.Euler(0, r, 0), transform);
        component.player = this;
        return component;
    }

    public void FixedUpdate() {
        Organ.UpdateAll();
    }

    
}


