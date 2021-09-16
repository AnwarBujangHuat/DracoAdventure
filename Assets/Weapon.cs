using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Firepoint;
    public GameObject bullet;
    public Rigidbody2D rigi;
   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&&Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical") == 0 && rigi.velocity.y == 0 && rigi.velocity.x == 0 && Time.timeScale==1) {
            Shoot();
        }
        void Shoot() {
            Instantiate(bullet,Firepoint.position,Firepoint.rotation);
        }
    }
}
