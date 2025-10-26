using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Manager.foodItems.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
