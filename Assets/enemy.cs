using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 100;
    public void damage(int damage ) {
        health -= damage;
        if (health<=0) { Die(); }
    }
    // Start is called before the first frame update
    void Die() { Destroy(gameObject); }
}
