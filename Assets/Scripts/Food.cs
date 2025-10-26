using UnityEngine;


public class Food : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Manager.foodItems.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} triggered by {other.name}");
        Manager.foodItems.Remove(this);
        Destroy(this.gameObject);
    }
}
