using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
    int health;
    public int startHealth = 2;

    void Start()
    {
        health = startHealth;
    }
}
