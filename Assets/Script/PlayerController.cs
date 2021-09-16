using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigi;
    public Animator anime;
    public LayerMask ground;
    public LayerMask enemy;
    public Collider2D collide;
    public Transform Firepoint;
    public enum DragonState { idle,walk,jump,fall,crouch, AttackFireball, FlyingStrike, CrouchKick,Dizzy, Death, Win}
    DragonState CurrentState = DragonState.idle;
 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello and Welcome to my first Game");
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");
        float VerticalDirection = Input.GetAxis("Vertical");
        float AttackDirection = Input.GetAxis("Fire1");
        bool death = collide.IsTouchingLayers(enemy);
        IdleMove(HorizontalDirection,VerticalDirection, AttackDirection);
        HorizontalMove(HorizontalDirection);
        VerticalMove(VerticalDirection,AttackDirection);
        AttackMove(AttackDirection,VerticalDirection,HorizontalDirection);
        DeathMove(death,AttackDirection);


        anime.SetInteger("CurrentState", (int)CurrentState);
    }



    void HorizontalMove(float HorizontalDirection) {
        if (HorizontalDirection < 0)
        {

            rigi.velocity = new Vector2(-3, rigi.velocity.y);
            transform.localScale = new Vector3(-1,1,10);
            Firepoint.localRotation = Quaternion.Euler(0,180,0);
            if (collide.IsTouchingLayers(ground) && rigi.velocity.y == 0)
            { CurrentState = DragonState.walk; }
           
        }
        else if (HorizontalDirection > 0 )
        {
            rigi.velocity = new Vector2(3, rigi.velocity.y);
            transform.localScale = new Vector3(1, 1, 10);
            Firepoint.localRotation = Quaternion.Euler(0, 0, 0);
            if (collide.IsTouchingLayers(ground)&&rigi.velocity.y==0)
            { CurrentState = DragonState.walk; }
        }
    }
    void VerticalMove(float VerticalDirection,float AttackMove)
    {
        if (VerticalDirection > 0 && collide.IsTouchingLayers(ground)&&rigi.velocity.y==0)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, 6);
            CurrentState = DragonState.jump;
        

        }
        if (rigi.velocity.y < 0&& collide.IsTouchingLayers(ground)==false&&AttackMove==0)
        {
            CurrentState = DragonState.fall;
        }
        else if (VerticalDirection < 0)
        {
            CurrentState = DragonState.crouch;
        }



    }
    void IdleMove(float VerticalMove, float HorizontalMove, float AttackDirection) {
        if (VerticalMove == 0 && HorizontalMove == 0 && rigi.velocity.y == 0 && collide.IsTouchingLayers(ground) && AttackDirection == 0) { CurrentState = DragonState.idle;rigi.velocity = new Vector2(0,0); }
        else if (VerticalMove == 0 && HorizontalMove == 0 && rigi.velocity.y == 0 && collide.IsTouchingLayers(ground) && AttackDirection != 0) { CurrentState = DragonState.FlyingStrike; }
   
    }
    void AttackMove(float Attackmove,float VerticalDirection,float HorizontalMove) {

        if (Attackmove > 0 && collide.IsTouchingLayers(ground) && VerticalDirection == 0 && HorizontalMove == 0&&rigi.velocity.y==0&&rigi.velocity.x==0)
        {
            CurrentState = DragonState.AttackFireball;

        }
        else if (Attackmove > 0 && collide.IsTouchingLayers(ground) && VerticalDirection == 0 && HorizontalMove != 0) {
            transform.localScale = new Vector3(transform.localScale.x, 1, 10);
            rigi.velocity = new Vector2(transform.localScale.x * 5, rigi.velocity.y); 
            CurrentState = DragonState.CrouchKick; }

        else if (Attackmove > 0 && collide.IsTouchingLayers(ground) == false&&rigi.velocity.y!=0) {

            transform.localScale = new Vector3(transform.localScale.x, 1, 10);
            rigi.velocity = new Vector2(transform.localScale.x * 8, rigi.velocity.y);
            CurrentState = DragonState.FlyingStrike; }
    
    
    }
    void DeathMove(bool death,float AttackDirection) {

        if (death&& AttackDirection == 0) {
            CurrentState = DragonState.Death;
            Invoke("Respawn", .5f);
        } }
    void Respawn() { transform.localPosition = new Vector3(0, 5, 0); }
}
