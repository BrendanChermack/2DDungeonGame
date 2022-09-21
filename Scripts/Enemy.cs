using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //xp
    public int xpValue = 1;

    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // in range? 
        if(Vector3.Distance(playerTransform.position, startPosition) < chaseLength)
        {
            if( Vector3.Distance(playerTransform.position, startPosition) < triggerLength)
            {
                chasing = true;
            }

            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startPosition - transform.position);
            chasing = false;
        }

        //overlap check
        collidingWithPlayer = false;
         boxCollider.OverlapCollider(filter, hits);
       for (int i = 0; i < hits.Length; i++)
       {
           if(hits[i] == null)
             continue;

           if(hits[i].tag == "Fighter" && hits[i].name == "Player")
           {
               collidingWithPlayer = true;
           }

            //clean array up
            hits[i] = null;
       }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+" + xpValue + " xp", 25, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
