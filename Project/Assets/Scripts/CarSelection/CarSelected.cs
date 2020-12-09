using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelected : MonoBehaviour
{
    public SpriteRenderer selected1;
    public SpriteRenderer selected2;
    public SpriteRenderer selected3;
    public SpriteRenderer selected4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Uno)
        {
            selected1.enabled = true;
            selected2.enabled = false;
            selected3.enabled = false;
            selected4.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Dos)
        {
            selected1.enabled = false;
            selected2.enabled = true;
            selected3.enabled = false;
            selected4.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Tres)
        {
            selected1.enabled = false;
            selected2.enabled = false;
            selected3.enabled = true;
            selected4.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Cuatro)
        {
            selected1.enabled = false;
            selected2.enabled = false;
            selected3.enabled = false;
            selected4.enabled = true;
        }
    }
}
