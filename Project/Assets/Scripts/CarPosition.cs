using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPosition : MonoBehaviour
{

    public Rigidbody2D coche;
    public Rigidbody2D coche2;
    public float rotation;
    public float posX;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            startLevel(true);
        }
        startLevel();

        if (MenuControl.cocheElegido == 1)
        {
            posX = coche.transform.position.x;
            rotation = coche.transform.rotation.z;
        } else
        {
            posX = coche2.transform.position.x;
            rotation = coche2.transform.rotation.z;
        }
    
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
            coche.transform.rotation = new Quaternion
            {
                z = 0
            };
        
            coche.transform.position = position;
        } else
        {
            coche2.transform.rotation = new Quaternion
            {
                z = 0
            };
        
            coche2.transform.position = position;
        }
        
    }

    void startLevel(bool forced = false)
    {
        if (MenuControl.gameStage == "pre-game" || forced == true) {
            switch (MenuControl.level)
            {
                case 1:
                    setPosition((float)3.05, (float)32.71);
                    break;
                case 5:
                    setPosition((float)7.5, (float)1);
                    break;
                case 3:
                    setPosition((float)-5.79, (float)-14.53);
                    break;
                case 4:
                    setPosition((float)-6.74, (float)-50);
                    break;
                case 2:
                    setPosition((float)-8.9, (float)-78.6);
                    break;

            }
            MenuControl.gameStage = "game";
        }
    }
}
