using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public AudioSource music;
    public Rigidbody2D hints;
    public static int cocheElegido = 1;

    public Rigidbody2D coche;
    public Rigidbody2D coche2;

    public static bool gameInit = false;
    public static string gameStage = "menu";
    public static int level = 0;
    public static bool hintsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (MenuControl.level == 1)
        {
            MenuControl.hintsEnabled = false;
        }

        if (MenuControl.gameStage == "menu")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (MenuControl.cocheElegido == 1)
                {
                    
                    MenuControl.cocheElegido = 2;
                    setPosition((float)-73.96,(float)134.07);
                    

                    
                    
                } else
                {
                    
                    MenuControl.cocheElegido = 1;
                    setPosition((float)-82.24,(float)134.07);
                    
                }
                
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                MenuControl.gameStage = "select";
            }
        }
        else if (MenuControl.gameStage == "select")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                startLevel(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                startLevel(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                startLevel(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                startLevel(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                startLevel(5);
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (music.mute == true)
            {
                music.mute = false;
            }
            else
            {
                music.mute = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (MenuControl.hintsEnabled == true)
            {
                MenuControl.hintsEnabled = false;
            }
            else if (MenuControl.hintsEnabled == false)
            {
                MenuControl.hintsEnabled = true;
            }
        }

        if (MenuControl.gameStage == "game")
        {
            if (Input.GetKeyDown(KeyCode.Q) == true)
            {
                MenuControl.gameInit = false;
                MenuControl.gameStage = "select";
                MenuControl.level = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            MenuControl.gameStage = "menu";
            MenuControl.gameInit = false;
            MenuControl.level = 0;
        }
        
    }

    public void startLevel(int level)
    {
        
        MenuControl.gameInit = true;
        MenuControl.gameStage = "pre-game";
        MenuControl.level = level;
    }

    void setPosition(float x, float y)
    {
        Vector2 position = new Vector2
        {
            x = x,
            y = y,
        };

        if (MenuControl.cocheElegido == 1)
        {
            coche2.transform.rotation = new Quaternion
            {
                z = 0
            };
        
            coche2.transform.position = position;
        } else
        {
            coche.transform.rotation = new Quaternion
            {
                z = 0
            };
        
            coche.transform.position = position;
        }
        
    }

}

