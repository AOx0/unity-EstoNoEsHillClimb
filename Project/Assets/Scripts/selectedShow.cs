using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedShow : MonoBehaviour
{
    public SpriteRenderer selected1;
    public SpriteRenderer selected2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuControl.cocheElegido == 1)
        {
            selected1.enabled = true;
            selected2.enabled = false;
        } else
        {
            selected1.enabled = false;
            selected2.enabled = true;
        }

        if (MenuControl.cocheElegido == 2)
        {
            selected1.enabled = true;
            selected2.enabled = false;
        } else
        {
            selected1.enabled = false;
            selected2.enabled = true;
        }
    }
}
