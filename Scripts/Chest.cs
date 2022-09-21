using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class Chest : Collectable
{
    public Sprite emptyChest;
    public Sprite openChest;
    public static float bonesAmount = 100;
    protected virtual void OpenChest()
    {
        
         if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = openChest;        
          
        }
    }
    
  protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;    
            GameManager.instance.ShowText("+" + bonesAmount + " Bones!",25,Color.yellow, transform.position, Vector3.up *25, 0.75f);
        }
       
    }
   
}
