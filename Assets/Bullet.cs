using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=10f;
    public Rigidbody2D rigi;
    public int damage = 50;
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        rigi.velocity = transform.right * speed;
       
    }

    // Update is called once per frame
    void Update()
    {
    
    }


     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.name!="Player") {
            rigi.velocity = new Vector2(0, 0);
            anime.SetBool("Hit", true);
           
            enemy enemy = other.GetComponent<enemy>();
            if (enemy != null) {
              
                enemy.damage(damage);
            }
            Invoke("Destroyer", .5f);

        }
    }
    void Destroyer() {
        Destroy(gameObject);
    }
}
