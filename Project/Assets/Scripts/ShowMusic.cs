using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMusic : MonoBehaviour
{

    public SpriteRenderer textEnabled;
    public SpriteRenderer textDisabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.musicEnabled)
        {
            textEnabled.enabled = true;
            textDisabled.enabled = false;
        } else
        {
            textEnabled.enabled = false;
            textDisabled.enabled = true;
        }
    }
}
