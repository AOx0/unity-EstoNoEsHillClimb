using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedView2 : MonoBehaviour
{
    public SpriteRenderer view;
    public Canvas text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.terminoJugador2)
        {
            view.enabled = true;
            text.enabled = true;
        } else
        {
            view.enabled = false;
            text.enabled = false;
        }
    }
}
