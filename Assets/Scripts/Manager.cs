using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public static class Manager {
    public static List<Food> foodItems = new();
}