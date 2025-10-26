using System.Collections.Generic;
using UnityEngine;

public abstract class Organ : MonoBehaviour {

    [Header("Organ Settings")]
    public float weight;

    [HideInInspector]
    public static List<Organ> _instances = new();
    [HideInInspector]
    public List<Organ> inupts;
    [HideInInspector]
    public Player player;

    // public static float decayRate = 0.9f;

    // [HideInInspector]
    public float value = 0f;
    [HideInInspector]
    public float _next_value = 0f;

    // Sensors override this method
    public virtual float ExternalSignal() {
        // senses the enviroment to obtain the signal (ex: distance to nearest object)
        return 0;
    }
    
    // Actuators override this method
    public virtual void Action() {
        // Do something with the signal
    }

    // static method to update all organs
    public static void UpdateAll() {
        foreach (var instance in _instances) {
            instance._next_value = instance.ExternalSignal();
            foreach (var input in instance.inupts) instance._next_value += input.value * input.weight;
        }
        foreach (Organ instance in _instances) {
            instance.value = instance._next_value;
            foreach (Organ input in instance.inupts) {
                Debug.DrawRay(instance.transform.position, input.transform.position - instance.transform.position, instance.value == 0 ? Color.red : Color.green, duration: 0, depthTest: false);
            }
            instance.Action();
        }
    }


    // on creatiion add instance to list of all organs
    public void Start() {
        _instances.Add(this);
    }
}