using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Eyeball eyeball;
    public Flipper flipper;

    [HideInInspector] public Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        Organ eye1  = CreateOrgan(eyeball, 1, 1, 0);
        Organ eye2  = CreateOrgan(eyeball,-1, 1, 0);
        Organ flip1 = CreateOrgan(flipper, 1,-1, 0);
        Organ flip2 = CreateOrgan(flipper, -1, -1, 0);

        flip1.inupts.Add(eye2);
        flip2.inupts.Add(eye1);
    }

    public Organ CreateOrgan(Organ prefab, float x, float z, float r)
    {
        Organ component = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.Euler(0, r, 0), this.transform);
        // component.pla
        return component;
    }

    public void FixedUpdate()
    {
        Organ.UpdateAll();
    }
}


