using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Completed : MonoBehaviour
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
        if (GameHandler.tipoJuego == GameHandler.TipoJuego.Single)
        {
            if (GameHandler.completedLvl6 == true)
            {
                lvlText.enabled = true;
                lvlThick.enabled = true;
            } else
            {
                lvlText.enabled = false;
                lvlThick.enabled = false;
            }
        } else
        {
            lvlText.enabled = false;
            lvlThick.enabled = false;
        }
    }
}
