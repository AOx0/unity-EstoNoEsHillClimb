using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Camera camara;
    public Rigidbody2D coche;
    public Rigidbody2D coche2;
    public SpriteRenderer fondo;
    public Rigidbody2D hints;

    public float posX;
    public float posY;

    private const float posZ = -1;


    // Start is called before the first frame update
    void Start()
    {

    }
    // 14.8
    // Update is called once per frame
    void Update()
    {
        if (MenuControl.cocheElegido == 1)
        {
            posX = coche.position.x;
            posY = coche.position.y + (float)0.5;
        } else
        {
            posX = coche2.position.x;
            posY = coche2.position.y + (float)0.5;
        }
        

        if (MenuControl.level == 5)
        {
            if (posY < 0.9)
            {
                posY = (float)0.9;
            }
        }

        if (MenuControl.level == 3)
        {
            if (posY < -31)
            {
                posY = (float)-31;
            }
        }
    }

    private void FixedUpdate()
    {
        if (MenuControl.gameStage == "menu")
        {
            Vector3 vectorCamara = new Vector3
            {
                x = (float)-28.97,
                y = (float)-0.39,
                z = posZ
            };
            camara.transform.position = vectorCamara;
        }

        if (MenuControl.gameStage == "select")
        {
            Vector3 vectorCamara = new Vector3
            {
                x = (float)-28.97,
                y = (float)11.33,
                z = posZ
            };
            camara.transform.position = vectorCamara;
        }
        else if (MenuControl.gameInit == true)
        {

            Vector3 vectorCamara = new Vector3
            {

                x = posX,
                y = posY,
                z = posZ,
            };

            Vector3 vectorFondo = new Vector3
            {
                x = posX,
                y = posY,
                z = (float)14.8,
            };

            Vector2 vectorHints = new Vector2
            {
                x = (float)(posX + 0.78),
                y = (float)(posY + 2.50)
            };

            hints.transform.position = vectorHints;
            camara.transform.position = vectorCamara;
            fondo.transform.position = vectorFondo;
        }
    }
}

