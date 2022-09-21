using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   private void Awake()
   {
       if(GameManager.instance != null)
       {
           Destroy(gameObject);
                return;
       }
       instance = this;
       SceneManager.sceneLoaded += LoadState;
       DontDestroyOnLoad(gameObject);

   }

   //resources for game 
   public List<Sprite> playerSprites;
   public List<Sprite> weaponSprites;
   public List<int> weaponPrices;
   public List<int> xpTables;
   
   //references
   public Player player;
   
   //public weapons
   public FloatingTextManager floatingTextManager;

   //logic
   public int bones;
   public int experience;

//floating text
   public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
   {
       floatingTextManager.Show(msg,fontSize,color,position,motion,duration);
   }

    //savestate
    /*
    *int preferedSkin
    *int amount of bones
    *int experience
    int weoponlevel
    */
   public void SaveState()
   {
       string s = "";

        s += "0" + "|";
        s += bones.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

       PlayerPrefs.SetString("SaveState", s);
   }

   public void LoadState(Scene s, LoadSceneMode mode)
   {
       if(!PlayerPrefs.HasKey("SaveState"))
       return;
        string[] data = PlayerPrefs.GetString("SaveState").Split ('|');
       //Change player skin and experience
        bones = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        //change weapon level



       Debug.Log("LoadState");
   }

}
