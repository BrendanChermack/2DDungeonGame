using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage structure
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //upgrades
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swings
    private Animator anim;
    private float cooldown = .3f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }
    protected override void Update ()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void onCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
            
                return;

            //create a new dmg obj then sent it to the fighter weve hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

                coll.SendMessage("ReceiveDamage", dmg);
            
              
        }
      
    }
    private void Swing()
    {
       anim.SetTrigger("Stab");
    }

}
