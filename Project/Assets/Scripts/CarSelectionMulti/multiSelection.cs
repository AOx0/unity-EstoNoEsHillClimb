using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiSelection : MonoBehaviour
{

    public SpriteRenderer jugador1_Coche1;
    public SpriteRenderer jugador1_Coche2;
    public SpriteRenderer jugador1_Coche3;
    public SpriteRenderer jugador1_Coche4;

    public SpriteRenderer jugador2_Coche1;
    public SpriteRenderer jugador2_Coche2;
    public SpriteRenderer jugador2_Coche3;
    public SpriteRenderer jugador2_Coche4;

    public SpriteRenderer  jugador1_Speed_Hint;
    public SpriteRenderer  jugador2_Speed_Hint;

    public SpriteRenderer  player1ConfirmedSprite;
    public SpriteRenderer  player2ConfirmedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       // Jugador 1
        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Uno)
        {
            jugador1_Coche1.enabled = true;
        } else
        {
            jugador1_Coche1.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Dos)
        {
            jugador1_Coche2.enabled = true;
        } else
        {
            jugador1_Coche2.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Tres)
        {
            jugador1_Coche3.enabled = true;
            
        } else
        {
            jugador1_Coche3.enabled = false;
            
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Cuatro)
        {
            jugador1_Coche4.enabled = true;
            
        } else
        {
            jugador1_Coche4.enabled = false;
            
        }

       // Jugador 2

        if (GameHandler.cocheElegido2 == GameHandler.CocheElegido.Uno)
        {
            jugador2_Coche1.enabled = true;
        } else
        {
            jugador2_Coche1.enabled = false;
        }

        if (GameHandler.cocheElegido2 == GameHandler.CocheElegido.Dos)
        {
            jugador2_Coche2.enabled = true;
        } else
        {
            jugador2_Coche2.enabled = false;
        }

        if (GameHandler.cocheElegido2 == GameHandler.CocheElegido.Tres)
        {
            jugador2_Coche3.enabled = true;
            
        } else
        {
            jugador2_Coche3.enabled = false;
            
        }

        if (GameHandler.cocheElegido2 == GameHandler.CocheElegido.Cuatro)
        {
            jugador2_Coche4.enabled = true;
        } else
        {
            jugador2_Coche4.enabled = false;
            
        }

        if (GameHandler.cocheElegido2 == GameHandler.CocheElegido.Cuatro || GameHandler.cocheElegido2 == GameHandler.CocheElegido.Tres)
        {
            jugador2_Speed_Hint.enabled = true;
        } else
        {
            jugador2_Speed_Hint.enabled = false;
        }

        if (GameHandler.cocheElegido == GameHandler.CocheElegido.Cuatro || GameHandler.cocheElegido == GameHandler.CocheElegido.Tres)
        {
            jugador1_Speed_Hint.enabled = true;
        } else
        {
            jugador1_Speed_Hint.enabled = false;
        }

        if (GameHandler.player1Confirm)
        {
            player1ConfirmedSprite.enabled = true;
        } else
        {
            player1ConfirmedSprite.enabled = false;
        }

        if (GameHandler.player2Confirm)
        {
            player2ConfirmedSprite.enabled = true;
        } else
        {
            player2ConfirmedSprite.enabled = false;
        }
    }
}
