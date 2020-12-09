﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Complete : MonoBehaviour
{
    public SpriteRenderer lvlText;
    public SpriteRenderer lvlThick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.completedLvl4 == true)
        {
            lvlText.enabled = true;
            lvlThick.enabled = true;
        } else
        {
            lvlText.enabled = false;
            lvlThick.enabled = false;
        }
    }
}
