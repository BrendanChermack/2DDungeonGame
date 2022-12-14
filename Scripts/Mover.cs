using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
   protected Vector3 moveDelta;
   protected RaycastHit2D hit;
   protected float ySpeed = 0.75f;
   protected float xSpeed = 1.0f;  

   protected virtual void Start()//happens at the start of the game
   {
       boxCollider = GetComponent <BoxCollider2D>();
   }


   protected virtual void UpdateMotor(Vector3 input)
   {
     //reset movedelta
       moveDelta = new Vector3 (input.x * xSpeed, input.y * ySpeed, 0);

      //swap spirte direction

      if(moveDelta.x > 0)
        transform.localScale = new Vector3(-1,1,1); 
           
    else if (moveDelta.x < 0)
        transform.localScale = Vector3.one; 
    
    //add push vector 
    moveDelta += pushDirection;

    //reduce push every frame
    pushDirection = Vector3.Lerp(pushDirection,Vector3.zero, pushRecoverySpeed);


    //added hit boxes for walls and characters
    hit  = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
     //movement
        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
    hit  = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(moveDelta.x,0),Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
     //movement
        transform.Translate(moveDelta.x * Time.deltaTime,0, 0);
        }
    }
}
