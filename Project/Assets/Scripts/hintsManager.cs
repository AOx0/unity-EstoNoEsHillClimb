using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintsManager : MonoBehaviour
{
    public SpriteRenderer hintQuit;
    public SpriteRenderer hintMusic;
    public SpriteRenderer hintRestart;
    public SpriteRenderer hintHide;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MenuControl.hintsEnabled == false)
        {
            hintQuit.enabled = false;
            hintMusic.enabled = false;
            hintRestart.enabled = false;
            hintHide.enabled = false;
            
        }
        else if (MenuControl.hintsEnabled == true)
        {
            hintQuit.enabled = true;
            hintMusic.enabled = true;
            hintRestart.enabled = true;
            hintHide.enabled = true;
            
        }
    }
}
