//
//  GameHandler.cs
//  dinamITAM
//
//  Created by Alejandro O on 05/12/20.
//


// Perdonen si junto palabras en inglés y en español :/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{
    //Opciones de usuario
    public static CocheElegido cocheElegido;
    public static CocheElegido cocheElegido2;
    public static TipoJuego tipoJuego;

    //Mulijugador Leaderboards
    public Canvas winnerPlayer;
    public Canvas finalTimePlayer1;
    public Canvas finalTimePlayer2;

    //Multijugador Selección de coche
    public static bool player1Confirm;
    public static bool player2Confirm;

    private static int movingCar1;
    private static int breakCar1;

    private static int movingCar2;
    private static int breakCar2;

    public static bool terminoJugador1;
    public static bool terminoJugador2;

    private bool  savedJugador1Time;
    private bool  savedJugador2Time;

    //Music
    public AudioSource music;
    public static bool musicEnabled;

    // Completed Levels
    public static bool completedLvl1 = false;
    public static bool completedLvl2 = false;
    public static bool completedLvl3 = false;
    public static bool completedLvl4 = false;
    public static bool completedLvl5 = false;
    public static bool completedLvl6 = false;


    public Canvas NewRecordCanvas;

    //Seguimiento de cámara
    public Camera camara;

    public Camera camara1;
    public Camera camara2;

    public Rigidbody2D coche1;
    public Rigidbody2D coche2;
    public Rigidbody2D coche3;
    public Rigidbody2D coche4;

    public Rigidbody2D coche1_2;
    public Rigidbody2D coche2_2;
    public Rigidbody2D coche3_2;
    public Rigidbody2D coche4_2;
                             
    public SpriteRenderer fondo;

    public SpriteRenderer fondoMulti1;
    public SpriteRenderer fondoMulti2;

    public Rigidbody2D ayudas;

    // Elementos del juego (Estado de Coche Actual)
    private float posXCamara;
    private float posYCamara;
    private float movimiento;

    private float movimiento1;
    private float movimiento2;

    private float posXCamara2;
    private float posYCamara2;
    

    private float tiempo1EnAire;
    private bool _Coche1EnAire = false;
    private bool Coche1EnAire
    {
        get
        {
            return Coche1EnAire;
        }
        set
        {
            if (_Coche1EnAire == false && value)
            {
                tiempo1EnAire = Time.time;
            }

            _Coche1EnAire = value;

            
        }
    }

    private float tiempo2EnAire;
    private bool _Coche2EnAire = false;
    private bool Coche2EnAire
    {
        get
        {
            return Coche2EnAire;
        }
        set
        {
            if (_Coche2EnAire == false && value)
            {
                tiempo2EnAire = Time.time;
            }

            _Coche2EnAire = value;

            
        }
    }

    // Estado del juego
    private int levelActual;
    private StageJuego stageJuego;
    private Rigidbody2D cocheActualElegido;
    private Rigidbody2D cocheActualElegido2;


    private float tiempoJugador1;
    private float tiempoJugador2;

    // Canvas de Texto
    public Canvas textoTiempo1;

    public Canvas textoTiempoMulti1;
    public Canvas textoTiempoMulti2;

    public Canvas mensajeVictoria_Nivel;
    public Canvas mensajeVictoria_Tiempo;
    public Canvas pressToContinue;

    public SpriteRenderer mensajeVictoria_Fondo;

    public SpriteRenderer PorcentajeTrackSolitario;

    public SpriteRenderer PorcentajeTrack1;
    public SpriteRenderer PorcentajeTrack2;

    public SpriteRenderer PorcentajeTrack1_2;
    public SpriteRenderer PorcentajeTrack2_2;

    //Indicador de "velocidad" y aconsejamiento de reinicio
    public SpriteRenderer indicadorSpriteSolo;

    public SpriteRenderer indicadorSprite1;
    public SpriteRenderer indicadorSprite2;

    

    public SpriteRenderer hintReiniciar;

    // Start is called before the first frame update
    void Start()
    {
        tiempoJugador1 = 0;
        tiempoJugador2 = 0;

        movimiento1 = 0;
        movimiento2 = 0;

        musicEnabled = true;

        player1Confirm = false;
        player2Confirm = false;

        cocheElegido = CocheElegido.Uno;
        cocheElegido2 = CocheElegido.Uno;

        levelActual = 0;
        stageJuego = StageJuego.Menu;
        tipoJuego = TipoJuego.Single;

        
    }

    // Update is called once per frame
    void Update()
    {
        cocheActualElegido = CocheElegidoActual();
        cocheActualElegido2 = CocheElegidoActual(true);
        KeyHandler();
        if (stageJuego == StageJuego.Juego )
        {
            UpdateIndicationsUI();
        }
        
        CameraFollow();

        if (tipoJuego == TipoJuego.Single)
        {
            CheckIfUserWin();
            ShowWinMessage();
        } else
        {
            MultiplayerWinHandler();
        }
        
    }

// MARK: - Enums

    public enum CocheElegido {
        Uno,
        Dos,
        Tres,
        Cuatro,
    }

    enum StageJuego
    {
        // General
        Menu,

        // Solo
        Seleccion,
        SeleccionCoche,
        PreJuego, // Cargado
        Juego,
        PostJuego, // Victoria

        //Multijugador
        SeleccionMulti,
        SeleccionCocheMulti,
        PreJuegoMulti,
        JuegoMulti,
        PostJuegoMulti,
    }


    public enum TipoJuego
    {
        Single,
        Multijugador,
    }

// MARK: - Functions

    // Mostrar texto en el Text de un Canvas

    void ShowInCanvas(Canvas canvas, string texto)
    {
        canvas.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = texto;
    }

    // Returns the current selected car by te user
    Rigidbody2D CocheElegidoActual(bool segundoJugador = false)
    {
        Rigidbody2D cocheActual = coche1;
        switch (segundoJugador ? cocheElegido2 : cocheElegido)
        {
            case CocheElegido.Uno:
                cocheActual = segundoJugador ? coche1_2 : coche1;
                break;
            case CocheElegido.Dos:
                cocheActual = segundoJugador ? coche2_2 : coche2;
                break;
            case CocheElegido.Tres:
                cocheActual = segundoJugador ? coche3_2 : coche3;
                break;
            case CocheElegido.Cuatro:
                cocheActual = segundoJugador ? coche4_2 : coche4;
                break;
        }
        return cocheActual;
    }

// MARK: - cameraFollow
    void CameraFollow()
    {
        Vector3 makeVector(double x, double y, bool fondo = false)
        {
            if (!fondo)
            {
                return new Vector3 { x = (float)x, y = (float)y, z = (float)-30.0};
            } else
            {
                return new Vector3 { x = (float)x, y = (float)y, z = (float)10.0};
            }
            
        }

        // Sets camera to follow the Car
        void gameView()
        {
            // Implementation to fix single level
            void IMP_fix2DCarPositionInLevels(int level, double fixedY)
            {
                if (levelActual == level)
                {
                    if (posYCamara < fixedY)
                    {
                        posYCamara = (float)fixedY;
                    }
                }
        
            }

            // Calls to fix all positions in levels that require it
            void fix2DCarPositionInLevels()
            {
                IMP_fix2DCarPositionInLevels(5, 0.9);
                IMP_fix2DCarPositionInLevels(3, -31);
            }

            void get2DCarPosition()
            {
                posXCamara = cocheActualElegido.transform.position.x;
                posYCamara = cocheActualElegido.transform.position.y;
            }

            get2DCarPosition();
            fix2DCarPositionInLevels();

            if (tipoJuego == TipoJuego.Multijugador)
            {
                camara1.transform.position = makeVector(posXCamara, posYCamara);
            } else
            {
                camara.transform.position = makeVector(posXCamara, posYCamara);
            }
            

            if (tipoJuego == TipoJuego.Multijugador)
            {
                
                fondoMulti1.transform.position = makeVector(posXCamara, posYCamara, true);
           
                
            } else
            {
                
                
                fondo.transform.position = makeVector(posXCamara, posYCamara, true);
                
                
            } 

            

            if (tipoJuego == TipoJuego.Multijugador)
            {
                void IMP_fix2DCarPositionInLevels2(int level, double fixedY)
                {
                    if (levelActual == level)
                    {
                        if (posYCamara2 < fixedY)
                        {
                            posYCamara2 = (float)fixedY;
                        }
                    }
        
                }

                // Calls to fix all positions in levels that require it
                void fix2DCarPositionInLevels2()
                {
                    IMP_fix2DCarPositionInLevels2(5, 0.9);
                    IMP_fix2DCarPositionInLevels2(3, -31);
                }

                void get2DCarPosition2()
                {
                    posXCamara2 = cocheActualElegido2.transform.position.x;
                    posYCamara2 = cocheActualElegido2.transform.position.y;
                }

                get2DCarPosition2();
                fix2DCarPositionInLevels2();
                camara2.transform.position = makeVector(posXCamara2, posYCamara2);
                fondoMulti2.transform.position = makeVector(posXCamara2, posYCamara2, true);
            }
        }

        // Sets the camera to view the menu
        void menuView()
        {
            camara.transform.position = makeVector(-28.97, -0.39);
        }

        void selectCarView()
        {

            camara.transform.position = makeVector(-51.76, -0.39);
        }

        // Sets the camera to view the selection level menu
        void selectView()
        {
            camara.transform.position = makeVector(-28.97, 11.33);
        }

        void multi_selectView()
        {
            camara.transform.position = makeVector(-52.17, -13.61);
        }

        void multiplayerLeaderBoard()
        {
            camara.transform.position = makeVector(-52, -29);
        }

        if (tipoJuego == TipoJuego.Multijugador && stageJuego == StageJuego.Juego)
        {
            camara.enabled = false;
        } else
        {
            camara.enabled = true;
        }

        switch (stageJuego)
        {
            case StageJuego.Menu:
                menuView();
                break;
            case StageJuego.SeleccionCoche:
                selectCarView();
                break;
            case StageJuego.Seleccion:
                selectView();
                break;
            case StageJuego.Juego:
                gameView();
                break;

            //Mulijugador
            case StageJuego.SeleccionCocheMulti:
                multi_selectView();
                break;
            case StageJuego.PostJuegoMulti:
                multiplayerLeaderBoard();
                break;
            
        }
        
    }


// MARK: - Update UI
    void UpdateIndicationsUI()
    {
        void changeSpeedUIState()
        {

            void rotateBody(double degrees)
            {
                indicadorSpriteSolo.transform.rotation = Quaternion.Euler(Vector3.forward * (float)degrees);
            }


            print(movimiento);

            if (movimiento >= 0)
            {
                rotateBody((movimiento * -0.1333333333));
            }

            if (movimiento < 0)
            {
                rotateBody((int)(movimiento * -0.04));
            }
        }

        void updateTrackMinimap()
        {
            void setTrackerPosition(double x)
            {
                
                PorcentajeTrackSolitario.transform.localPosition = new Vector3 { x = (float)x, y = (float)0.5, z = PorcentajeTrackSolitario.transform.localPosition.z};
            }

            float carPosition = cocheActualElegido.transform.position.x ;
            setTrackerPosition((-10 + (carPosition*9.85)/100));
        }

        void updateTimeText()
        {
            float time = Time.time - tiempoJugador1;
            ShowInCanvas(textoTiempo1, ("Time : " + (string.Format("{0:N}", time))));

            string mensaje2 = "Time : " + (string.Format("{0:N4}", time));
            ShowInCanvas(mensajeVictoria_Tiempo, mensaje2);

        }

        // Multijugador

        void changeSpeedUIState1()
        {

            void rotateBody(double degrees)
            {
                indicadorSprite1.transform.rotation = Quaternion.Euler(Vector3.forward * (float)degrees);
            }

        
            if (movimiento1 >= 0)
            {
                rotateBody((movimiento1 * -0.1333333333));
            }

            if (movimiento1 < 0)
            {
                rotateBody((int)(movimiento1 * -0.04));
            }
        }
        void updateTimeText1()
        {
            if (!terminoJugador1  && stageJuego == StageJuego.Juego)
            {
                float time = Time.time - tiempoJugador1;
                ShowInCanvas(textoTiempoMulti1, "Time : " + string.Format("{0:N2}", time));

                ShowInCanvas(finalTimePlayer1, "Player 1                " + "Time : " + string.Format("{0:N4}", time));
            }
            

        }

        void changeSpeedUIState2()
        {

            void rotateBody(double degrees)
            {
                indicadorSprite2.transform.rotation = Quaternion.Euler(Vector3.forward * (float)degrees);
            }

            
            if (movimiento2 >= 0)
            {
                rotateBody((movimiento2 * -0.1333333333));
            }

            if (movimiento2 < 0)
            {
                rotateBody((int)(movimiento2 * -0.04));
            }
        }
        void updateTimeText2()
        {
            if (!terminoJugador2 && stageJuego == StageJuego.Juego)
            {
                float time = Time.time - tiempoJugador2;
                ShowInCanvas(textoTiempoMulti2, ("Time : " + (string.Format("{0:N2}", time))));

                ShowInCanvas(finalTimePlayer2, "Player 2               " + "Time : " + string.Format("{0:N4}", time));
            }
            

        }

       void updateTrackMinimapMulti()
       {
            void setTracker1Position(double x)
            {
                
                PorcentajeTrack1.transform.localPosition = new Vector3 { x = (float)x, y = 1, z = PorcentajeTrack1.transform.localPosition.z};
                PorcentajeTrack1_2.transform.localPosition = new Vector3 { x = (float)x, y = 1, z = PorcentajeTrack1_2.transform.localPosition.z};
            }

            void setTracker2Position(double x)
            {
                
                PorcentajeTrack2.transform.localPosition = new Vector3 { x = (float)x, y = 1, z = PorcentajeTrack2.transform.localPosition.z};
                PorcentajeTrack2_2.transform.localPosition = new Vector3 { x = (float)x, y = 1, z = PorcentajeTrack2_2.transform.localPosition.z};
            }

            if (!terminoJugador1)
            {
                PorcentajeTrack1.enabled = true;
                PorcentajeTrack1_2.enabled = true;
                float car1Position = cocheActualElegido.transform.position.x ;
                setTracker1Position((-10 + (car1Position*9.85)/100));
            } else
            {
                PorcentajeTrack1.enabled = false;
                PorcentajeTrack1_2.enabled = false;
            }

            if (!terminoJugador2)
            {
                PorcentajeTrack2.enabled = true;
                PorcentajeTrack2_2.enabled = true;
                float car2Position = cocheActualElegido2.transform.position.x ;
                setTracker2Position((-10 + (car2Position*9.85)/100));
            }
            else
            {
                PorcentajeTrack2.enabled = false;
                PorcentajeTrack2_2.enabled = false;
            } 
            
            
       }
       

        if (tipoJuego == TipoJuego.Single)
        {
            PorcentajeTrackSolitario.enabled = true;
            changeSpeedUIState();
            updateTrackMinimap();
            updateTimeText();
        }
        else
        {
            PorcentajeTrackSolitario.enabled = false;
            changeSpeedUIState1();
            updateTimeText1();

            changeSpeedUIState2();
            updateTimeText2();

            updateTrackMinimapMulti();
        }

    }

// MARK: - Key Handler

    void KeyHandler()
    {

    // MARK: - Car Methods

        void tipToRestart()
        {
            if (cocheActualElegido.transform.rotation.z < -0.75 || cocheActualElegido.transform.rotation.z > 0.75)
            {
                    hintReiniciar.enabled = true;
            } else
            {
                    hintReiniciar.enabled = false;
            }   
        }

        void setCarPosition(double x, double y, bool procesoCoche2 = false)
        {
            if (!procesoCoche2)
            {
                cocheActualElegido.transform.position = new Vector2 { x = (float)x, y = (float)y};
                cocheActualElegido.transform.rotation = new Quaternion { z = 0 };
            } else
            {
                cocheActualElegido2.transform.position = new Vector2 { x = (float)x, y = (float)y};
                cocheActualElegido2.transform.rotation = new Quaternion { z = 0 };
            } 
            
            
        }

        void setCarLevelPosition(bool restart = false, bool proceso2Coche = false)
        {
            if (stageJuego == StageJuego.PreJuego || restart == true) {
                

                // Inicia el tiempo del usuarui
                if (!restart && tipoJuego == TipoJuego.Multijugador)
                {
                    tiempoJugador1 = Time.time;
                    tiempoJugador2 = Time.time;
                }

                if (tipoJuego == TipoJuego.Single)
                {
                    tiempoJugador1 = Time.time;
                }
                
                switch (levelActual)
                {
                    case 1:
                        setCarPosition((float)0, (float)32.7, proceso2Coche);
                        break;
                    case 2:
                        setCarPosition((float)0, (float)-86.07, proceso2Coche);
                        break;
                    case 3:
                        setCarPosition((float)0, (float)-28.5, proceso2Coche);
                        break;
                    case 5:
                        setCarPosition((float)0, (float)1, proceso2Coche);
                        break;
                    case 4:
                        setCarPosition((float)0, (float)-55.3, proceso2Coche);
                        break;
                    case 6:
                        setCarPosition((float)0, (float)-118, proceso2Coche);
                        break;
                }
                stageJuego = StageJuego.Juego;
            }
        }

        // Handler de movimiento  
        void carMovement(Rigidbody2D coche, int modo = 1)
        {
            WheelJoint2D rueda1 = coche.gameObject.transform.GetChild(0).gameObject.GetComponent<WheelJoint2D>();
            WheelJoint2D rueda2 = coche.gameObject.transform.GetChild(1).gameObject.GetComponent<WheelJoint2D>();

            void singleVerticalMovement()
            {
                float movimientoAntes = movimiento;
                float speed = Input.GetAxisRaw("Horizontal");

                Coche1EnAire = (coche.gameObject.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.transform.GetChild(1).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers() == false);
                
                if (speed == 1)
                {
                    if ((movimiento + speed * (10 * ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 2 : 1))) <= ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 1500 : 1000))
                    {
                        movimiento += speed * (10 * ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 2 : 1));
                    }
                    if (movimiento < 0)
                    {
                        movimiento += speed * 20;
                    }
 
                    // Rotates in the air
                    if (_Coche1EnAire && (Time.time - tiempo1EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ + (PreviousZ > 1 ? (PreviousZ*0.5) : (PreviousZ*-0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ + 2 });
                    }

                }

                

                if (speed == -1)
                {
                    if ((movimiento + speed * 15) >= -500)
                    {
                        movimiento += speed * 15;
                    }

                    if (_Coche1EnAire && (Time.time - tiempo1EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ - (PreviousZ > 1 ? (PreviousZ*-0.5) : (PreviousZ*0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ - 2 });
                    }
                }
                        

                if (movimiento >= 1 && movimientoAntes == movimiento)
                {
                    movimiento -= 2;
                }

                if (movimiento < 0 && movimientoAntes == movimiento)
                {
                    movimiento += 10;
                }

                rueda2.motor = new JointMotor2D { motorSpeed = movimiento , maxMotorTorque = 10000 };
                rueda1.motor = new JointMotor2D { motorSpeed = movimiento , maxMotorTorque = 10000 };
                
            }
            void singleHorizontalMovement()
            {
                float speed = Input.GetAxisRaw("Vertical");

                if (speed == -1)
                {

                    if ((movimiento + (speed * 35)) >= 0)
                    {
                        movimiento += speed * 35;
                    }
                    if (movimiento < 0)
                    {
                        if (movimiento % 2 != 0)
                        {
                            movimiento -= 1;
                        }
                        movimiento += 20;
                    }

                    rueda2.motor = new JointMotor2D { motorSpeed = movimiento , maxMotorTorque = 10000 };
                    rueda1.motor = new JointMotor2D { motorSpeed = movimiento , maxMotorTorque = 10000 };


                    
                }
            }

            void mutliVerticalMovement1()
            {

                float movimientoAntes = movimiento1;
                float speed = movingCar1;

                Coche1EnAire = (coche.gameObject.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.transform.GetChild(1).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers() == false);
                
                if (speed == 1)
                {
                    if ((movimiento1 + speed * (10 * ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 2 : 1))) <= ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 1500 : 1000))
                    {
                        movimiento1 += speed * (10 * ((cocheElegido == CocheElegido.Tres || cocheElegido == CocheElegido.Cuatro) ? 2 : 1));
                    }
                    if (movimiento1 < 0)
                    {
                        movimiento1 += speed * 20;
                    }
 
                    // Rotates in the air
                    if (_Coche1EnAire && (Time.time - tiempo1EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ + (PreviousZ > 1 ? (PreviousZ*0.5) : (PreviousZ*-0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ + 2 });
                    }

                }

                

                if (speed == -1)
                {
                    if ((movimiento1 + speed * 15) >= -500)
                    {
                        movimiento1 += speed * 15;
                    }

                    if (_Coche1EnAire && (Time.time - tiempo1EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ - (PreviousZ > 1 ? (PreviousZ*-0.5) : (PreviousZ*0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ - 2 });
                    }
                }
                        

                if (movimiento1 >= 1 && movimientoAntes == movimiento1)
                {
                    movimiento1 -= 2;
                }

                if (movimiento1 < 0 && movimientoAntes == movimiento1)
                {
                    movimiento1 += 10;
                }

                rueda2.motor = new JointMotor2D { motorSpeed = movimiento1 , maxMotorTorque = 10000 };
                rueda1.motor = new JointMotor2D { motorSpeed = movimiento1 , maxMotorTorque = 10000 };
                
            }
            void mutliHorizontalMovement1()
            {
                float speed = breakCar1;

                if (speed == -1)
                {

                    if ((movimiento1 + (speed * 35)) >= 0)
                    {
                        movimiento1 += speed * 35;
                    }
                    if (movimiento1 < 0)
                    {
                        if (movimiento1 % 2 != 0)
                        {
                            movimiento1 -= 1;
                        }
                        movimiento1 += 20;
                    }

                    rueda2.motor = new JointMotor2D { motorSpeed = movimiento1 , maxMotorTorque = 10000 };
                    rueda1.motor = new JointMotor2D { motorSpeed = movimiento1 , maxMotorTorque = 10000 };


                    
                }
            }

            void mutliVerticalMovement2()
            {

                float movimientoAntes = movimiento2;
                float speed = movingCar2;

                Coche2EnAire = (coche.gameObject.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.transform.GetChild(1).gameObject.GetComponent<CircleCollider2D>().IsTouchingLayers() == false && coche.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers() == false);
                
                if (speed == 1)
                {
                    if ((movimiento2 + speed * (10 * ((cocheElegido2 == CocheElegido.Tres || cocheElegido2 == CocheElegido.Cuatro) ? 2 : 1))) <= ((cocheElegido2 == CocheElegido.Tres || cocheElegido2 == CocheElegido.Cuatro) ? 1500 : 1000))
                    {
                        movimiento2 += speed * (10 * ((cocheElegido2 == CocheElegido.Tres || cocheElegido2 == CocheElegido.Cuatro) ? 2 : 1));
                    }
                    if (movimiento2 < 0)
                    {
                        movimiento2 += speed * 20;
                    }
 
                    // Rotates in the air
                    if (_Coche2EnAire && (Time.time - tiempo2EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ + (PreviousZ > 1 ? (PreviousZ*0.5) : (PreviousZ*-0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ + 2 });
                    }

                }

                

                if (speed == -1)
                {
                    if ((movimiento2 + speed * 15) >= -500)
                    {
                        movimiento2 += speed * 15;
                    }

                    if (_Coche2EnAire && (Time.time - tiempo2EnAire) > 0.7)
                    {
                        float PreviousZ = coche.transform.rotation.z;
                        PreviousZ = (float)(PreviousZ - (PreviousZ > 1 ? (PreviousZ*-0.5) : (PreviousZ*0.5)));
                        coche.transform.Rotate(new Vector3 { z = PreviousZ - 2 });
                    }
                }
                        

                if (movimiento2 >= 1 && movimientoAntes == movimiento2)
                {
                    movimiento2 -= 2;
                }

                if (movimiento2 < 0 && movimientoAntes == movimiento2)
                {
                    movimiento2 += 10;
                }

                rueda2.motor = new JointMotor2D { motorSpeed = movimiento2 , maxMotorTorque = 10000 };
                rueda1.motor = new JointMotor2D { motorSpeed = movimiento2 , maxMotorTorque = 10000 };
                
            }
            void mutliHorizontalMovement2()
            {
                float speed = breakCar2;

                if (speed == -1)
                {

                    if ((movimiento2 + (speed * 35)) >= 0)
                    {
                        movimiento2 += speed * 35;
                    }
                    if (movimiento2 < 0)
                    {
                        if (movimiento2 % 2 != 0)
                        {
                            movimiento2 -= 1;
                        }
                        movimiento2 += 20;
                    }

                    rueda2.motor = new JointMotor2D { motorSpeed = movimiento2 , maxMotorTorque = 10000 };
                    rueda1.motor = new JointMotor2D { motorSpeed = movimiento2 , maxMotorTorque = 10000 };


                    
                }
            }

            switch (modo)
            {
                case 1:
                    singleVerticalMovement();
                    singleHorizontalMovement();
                    break;
                case 2:
                    mutliVerticalMovement1();
                    mutliHorizontalMovement1();
                    break;
                case 3:
                    mutliVerticalMovement2();
                    mutliHorizontalMovement2();
                    break;

            }
            
        }

        void setSpeedTo0(bool jugador1 = true)
        {
            if (jugador1)
            {
                WheelJoint2D rueda1 = cocheActualElegido.gameObject.transform.GetChild(0).gameObject.GetComponent<WheelJoint2D>();
                WheelJoint2D rueda2 = cocheActualElegido.gameObject.transform.GetChild(1).gameObject.GetComponent<WheelJoint2D>();

                rueda2.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};
                rueda1.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};

                movimiento1 = 0;
                movimiento = 0;
            } else
            {
                WheelJoint2D rueda1 = cocheActualElegido2.gameObject.transform.GetChild(0).gameObject.GetComponent<WheelJoint2D>();
                WheelJoint2D rueda2 = cocheActualElegido2.gameObject.transform.GetChild(1).gameObject.GetComponent<WheelJoint2D>();

                rueda2.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};
                rueda1.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};

                movimiento2 = 0;
            }
            

            
        }

        void setSpecifiedCarPosition(Rigidbody2D coche, double x, double y, bool acciones2 = false)
        {
            if (!acciones2)
            {
                coche.transform.position = new Vector3 { x = (float)x, y = (float)y, z= 0};
                coche.transform.rotation = new Quaternion { z = 0 };
            } else
            {
                coche.transform.position = new Vector3 { x = (float)x, y = (float)y, z = -10};
                coche.transform.rotation = new Quaternion { z = 0 };
            }
            
        }

        void parkAllCars()
        {
            setSpecifiedCarPosition(coche1,-31.7,51.5 );
            setSpecifiedCarPosition(coche2,-26.28,51.5 );
            setSpecifiedCarPosition(coche3,-26.28,51.5 );
            setSpecifiedCarPosition(coche4,-19.95,51.5 );

            setSpecifiedCarPosition(coche1_2,-31.7,51.5, true);
            setSpecifiedCarPosition(coche2_2,-26.28,51.5, true);
            setSpecifiedCarPosition(coche3_2,-26.28,51.5, true);
            setSpecifiedCarPosition(coche4_2,-19.95,51.5, true);
        }

        bool pressed(KeyCode key)
        {
           
            if (Input.GetKeyDown(key))
            {
                return true;
            } else
            {
                return false;
            }
        }

        void returnToMenu()
        {
            if (pressed(KeyCode.R))
            {
                stageJuego = StageJuego.Menu;
            }
        }

        void gameKeys()
        {
            if (tipoJuego == TipoJuego.Single) { carMovement(cocheActualElegido, 1); }
            else
            {
                if (!terminoJugador2)
                {
                    if (pressed(KeyCode.RightArrow))
                    {
                        movingCar2 = 1;
                    }

                    if (Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        movingCar2 = 0;
                    }

                    if (pressed(KeyCode.LeftArrow))
                    {
                        movingCar2 = -1;
                    }

                    if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        movingCar2 = 0;
                    }

                    if (pressed(KeyCode.DownArrow))
                    {
                        breakCar2 = -1;
                    }

                    if (Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        breakCar2 = 0;
                    }
                }
                

                
                if (!terminoJugador1)
                {
                    if (pressed(KeyCode.A))
                    {
                        movingCar1 = -1;
                    }

                    if (Input.GetKeyUp(KeyCode.A))
                    {
                        movingCar1 = 0;
                    }

                    if (pressed(KeyCode.D))
                    {
                        movingCar1 = 1;
                    }

                    if (Input.GetKeyUp(KeyCode.D))
                    {
                        movingCar1 = 0;
                    }

                    if (pressed(KeyCode.S))
                    {
                        breakCar1 = -1;
                    }

                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        breakCar1 = 0;
                    }
                    
                }

                carMovement(cocheActualElegido2, 3);
                carMovement(cocheActualElegido, 2);

                if (pressed(KeyCode.UpArrow) &&  !terminoJugador2)
                {
                    setSpeedTo0(false);
                    setCarLevelPosition(true, true);
                }
                
            }

            if (tipoJuego == TipoJuego.Single) { tipToRestart(); }
            
            if (pressed(KeyCode.R))
            {
                if (tipoJuego == TipoJuego.Single)
                {
                    setSpeedTo0();
                    setCarLevelPosition(true, false);
                } else
                {
                    if (!terminoJugador1)
                    {
                        setSpeedTo0();
                        setCarLevelPosition(true, false);
                    }
                }
                
            }

            

            if (pressed(KeyCode.Q))
            {
                parkAllCars();
                stageJuego = StageJuego.Seleccion;

                if (tipoJuego == TipoJuego.Multijugador)
                {
                    terminoJugador2 = false;
                    terminoJugador1 = false;

                    savedJugador1Time = false;
                    savedJugador2Time = false;
               
                    tiempoJugador2 = 0;
                    tiempoJugador1 = 0;
                }
            }
        }

        void menuKeys()
        {
            if (pressed(KeyCode.C))
            {
                stageJuego = StageJuego.SeleccionCoche;
            }

            if (pressed(KeyCode.P))
            {
                stageJuego = StageJuego.Seleccion;
            }

            if (pressed(KeyCode.F))
            {
                stageJuego = StageJuego.SeleccionCocheMulti;
            }
        }

        void selectionLvlKeys()
        {
            if (pressed(KeyCode.R))
            {
                if (tipoJuego == TipoJuego.Multijugador)
                {
                    tipoJuego = TipoJuego.Single;
                    stageJuego = StageJuego.SeleccionCocheMulti;
                    player1Confirm = false;
                    player2Confirm = false;

                    terminoJugador2 = false;
                    terminoJugador1 = false;

                    savedJugador1Time = false;
                    savedJugador2Time = false;
               
                    tiempoJugador2 = 0;
                    tiempoJugador1 = 0;

                
                } else
                {
                    stageJuego = StageJuego.Menu;
                }
                
            }

            void setCar2PositionLevelInit()
            {
                if (tipoJuego == TipoJuego.Multijugador)
                {
                    setCarLevelPosition(true, true);
                }
            }

            if (pressed(KeyCode.Alpha1) || pressed(KeyCode.Keypad1))
            {
                levelActual = 1;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }

            if (pressed(KeyCode.Alpha2) || pressed(KeyCode.Keypad2))
            {
                levelActual = 2;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }

            if (pressed(KeyCode.Alpha3) || pressed(KeyCode.Keypad3))
            {
                levelActual = 3;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }

            if (pressed(KeyCode.Alpha4) || pressed(KeyCode.Keypad4))
            {
                levelActual = 4;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }

            if (pressed(KeyCode.Alpha5) || pressed(KeyCode.Keypad5))
            {
                levelActual = 5;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }

            if (pressed(KeyCode.Alpha6) || pressed(KeyCode.Keypad6))
            {
                levelActual = 6;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
                setCar2PositionLevelInit();
            }
        }

        void selectionCarKeys()
        {

            // Methods

            

            // Calls
            returnToMenu();
            if (pressed(KeyCode.Alpha1) || pressed(KeyCode.Keypad1))
            {
                parkAllCars();
                cocheElegido = CocheElegido.Uno;
            }

            if (pressed(KeyCode.Alpha2) || pressed(KeyCode.Keypad2))
            {
                parkAllCars();
                cocheElegido = CocheElegido.Dos;
            }

            if (pressed(KeyCode.Alpha3) || pressed(KeyCode.Keypad3))
            {
                parkAllCars();
                cocheElegido = CocheElegido.Tres;
            }

            if (pressed(KeyCode.Alpha4) || pressed(KeyCode.Keypad4))
            {
                parkAllCars();
                cocheElegido = CocheElegido.Cuatro;
            }
            
        }

        void globalKeys()
        {
            if (pressed(KeyCode.M))
            {
                if (music.mute == true)
                {
                    music.mute = false;
                    musicEnabled = true;
                }
                else
                {
                    music.mute = true;
                    musicEnabled = false;
                }
            }
        }

//MARK: - Multijugador KEYS
        void selectCarsMulti()
        {
            void previousCar1()
            {
                switch (cocheElegido)
                {
                    case CocheElegido.Uno:
                        cocheElegido = CocheElegido.Cuatro;
                        break;
                    case CocheElegido.Dos:
                        cocheElegido = CocheElegido.Uno;
                        break;
                    case CocheElegido.Tres:
                        cocheElegido = CocheElegido.Dos;
                        break;
                    case CocheElegido.Cuatro:
                        cocheElegido = CocheElegido.Tres;
                        break;
                }  
            }

            void nextCar1()
            {
                switch (cocheElegido)
                {
                    case CocheElegido.Uno:
                        cocheElegido = CocheElegido.Dos;
                        break;
                    case CocheElegido.Dos:
                        cocheElegido = CocheElegido.Tres;
                        break;
                    case CocheElegido.Tres:
                        cocheElegido = CocheElegido.Cuatro;
                        break;
                    case CocheElegido.Cuatro:
                        cocheElegido = CocheElegido.Uno;
                        break;
                }
            }

            void previousCar2()
            {
                switch (cocheElegido2)
                {
                    case CocheElegido.Uno:
                        cocheElegido2 = CocheElegido.Cuatro;
                        break;
                    case CocheElegido.Dos:
                        cocheElegido2 = CocheElegido.Uno;
                        break;
                    case CocheElegido.Tres:
                        cocheElegido2 = CocheElegido.Dos;
                        break;
                    case CocheElegido.Cuatro:
                        cocheElegido2 = CocheElegido.Tres;
                        break;
                }  
            }

            void nextCar2()
            {
                switch (cocheElegido2)
                {
                    case CocheElegido.Uno:
                        cocheElegido2 = CocheElegido.Dos;
                        break;
                    case CocheElegido.Dos:
                        cocheElegido2 = CocheElegido.Tres;
                        break;
                    case CocheElegido.Tres:
                        cocheElegido2 = CocheElegido.Cuatro;
                        break;
                    case CocheElegido.Cuatro:
                        cocheElegido2 = CocheElegido.Uno;
                        break;
                }
            }

            if (pressed(KeyCode.R))
            {
                player1Confirm = false;
                player2Confirm = false;
                stageJuego = StageJuego.Menu;
                tipoJuego = TipoJuego.Single;
            }

            if (pressed(KeyCode.Q) && player1Confirm && player2Confirm)
            {
                tipoJuego = TipoJuego.Multijugador;
                stageJuego = StageJuego.Seleccion;
            }

            if (pressed(KeyCode.S))
            {
                if (player1Confirm)
                {
                    player1Confirm = false;
                } else
                {
                    player1Confirm = true;
                }
            }

            if (pressed(KeyCode.DownArrow))
            {
                if (player2Confirm)
                {
                    player2Confirm = false;
                } else
                {
                    player2Confirm = true;
                }
            }

            if (pressed(KeyCode.A) && !player1Confirm)
            {
                previousCar1();
            }

            if (pressed(KeyCode.D) && !player1Confirm)
            {
                nextCar1();
            }

            if (pressed(KeyCode.LeftArrow) && !player2Confirm)
            {
                previousCar2();
            }

            if (pressed(KeyCode.RightArrow) && !player2Confirm)
            {
                nextCar2();
            }
        }

        void leaderboard()
        {
            if (pressed(KeyCode.C))
            {
                setSpeedTo0(true);
                setSpeedTo0(false);
                terminoJugador2 = false;
                terminoJugador1 = false;

                savedJugador1Time = false;
                savedJugador2Time = false;
               
                tiempoJugador2 = 0;
                tiempoJugador1 = 0;

                stageJuego = StageJuego.Seleccion;
            }
        }

        globalKeys();
        switch (stageJuego)
        {
            case StageJuego.Juego:
                gameKeys();
                break;
            case StageJuego.Menu:
                menuKeys();
                break;
            case StageJuego.Seleccion:
                selectionLvlKeys();
                break;
            case StageJuego.SeleccionCoche:
                selectionCarKeys();
                break;
            case StageJuego.PostJuegoMulti:
                leaderboard();
                break;

            //Multijugador
            case StageJuego.SeleccionCocheMulti:
                selectCarsMulti();
                break;
        }

        
    }

// MARK: - Check if solo win
    void CheckIfUserWin()
    {       

        if (stageJuego == StageJuego.Juego)
        {
            if (cocheActualElegido.transform.position.x >= 200)
            {
                switch (levelActual)
                {
                    case 1:
                        completedLvl1 = true;
                        
                        break;
                    case 2:
                        completedLvl2 = true;
                        break;
                    case 3:
                        completedLvl3 = true;
                        break;
                    case 4:
                        completedLvl4 = true;
                        break;
                    case 5:
                        completedLvl5 = true;
                        break;
                    case 6:
                        completedLvl6 = true;
                        break;

                }
                stageJuego = StageJuego.PostJuego;
                
            }
        }
    }

    void ShowWinMessage()
    {

        void setCarPosition(double x, double y)
            {
                cocheActualElegido.transform.position = new Vector2 { x = (float)x, y = (float)y};
                cocheActualElegido.transform.rotation = new Quaternion { z = 0 };
            }

        void setCarLevelPosition()
        {  
            switch (levelActual)
            {
                case 1:
                    setCarPosition((float)0, (float)32.71);
                    break;
                case 2:
                    setCarPosition((float)0, (float)-86.07);
                    break;
                case 3:
                    setCarPosition((float)0, (float)-28.5);
                    break;
                case 5:
                    setCarPosition((float)0, (float)1);
                    break;
                case 4:
                    setCarPosition((float)0, (float)-55.3);
                    break;
                case 6:
                    setCarPosition((float)0, (float)-118);
                    break;
            }
                
        }

        if (stageJuego == StageJuego.PostJuego)
        {
            setCarLevelPosition();

            string mensaje1 =  "Nivel " + levelActual + " Completado";
            
            ShowInCanvas(mensajeVictoria_Nivel, mensaje1);
            
            
            mensajeVictoria_Nivel.enabled = true;
            mensajeVictoria_Tiempo.enabled = true;
            pressToContinue.enabled = true;
            mensajeVictoria_Fondo.enabled = true;

            string time = mensajeVictoria_Tiempo.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            time = time.Replace("Time : ", "");
            float timeFloat = float.Parse(time);

            switch (levelActual)
                {
                    case 1:
                        
                        if (timeFloat < showBestTimes.bestLevel1)
                        {
                            showBestTimes.bestLevel1 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;
                    case 2:
                        
                        if (timeFloat < showBestTimes.bestLevel2)
                        {
                            showBestTimes.bestLevel2 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;
                    case 3:
                        
                        if (timeFloat < showBestTimes.bestLevel3)
                        {
                            showBestTimes.bestLevel3 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;
                    case 4:
                        
                        if (timeFloat < showBestTimes.bestLevel4)
                        {
                            showBestTimes.bestLevel4 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;
                    case 5:

                        if (timeFloat < showBestTimes.bestLevel5)
                        {
                            showBestTimes.bestLevel5 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;
                    case 6:

                        if (timeFloat < showBestTimes.bestLevel6)
                        {
                            showBestTimes.bestLevel6 = timeFloat;
                            NewRecordCanvas.enabled = true;
                        }
                        break;

                }

            if (Input.GetKeyDown(KeyCode.C))
            {
                stageJuego = StageJuego.Seleccion;
                levelActual = 0;
                
            }

        } else
        {
            mensajeVictoria_Nivel.enabled = false;
            mensajeVictoria_Tiempo.enabled = false;
            pressToContinue.enabled = false;
            mensajeVictoria_Fondo.enabled = false;
            NewRecordCanvas.enabled = false;
        }
    }

    void MultiplayerWinHandler()
    {
        void setSpecifiedCarPosition(Rigidbody2D coche, double x, double y)
        {
            coche.transform.position = new Vector2 { x = (float)x, y = (float)y};
            coche.transform.rotation = new Quaternion { z = 0 };
        }

        if (stageJuego == StageJuego.Juego)
        {
            if (cocheActualElegido.transform.position.x >= 200)
            {
                terminoJugador1 = true;

                if (!savedJugador1Time)
                {
                    
                    savedJugador1Time = false;
                    movimiento1 = 0;
                    movingCar1 = 0;
                    breakCar1 = 0;
                    setSpecifiedCarPosition(cocheActualElegido,-19.95,51.5 );
                }

                if (!terminoJugador2)
                {
                    ShowInCanvas(winnerPlayer, "Player 1");
                }
            }

            if (cocheActualElegido2.transform.position.x >= 200)
            {
                terminoJugador2 = true;
                if (!savedJugador2Time)
                {
                    savedJugador2Time = false;
                    movimiento2 = 0;
                    movingCar2 = 0;
                    breakCar2 = 0;
                    setSpecifiedCarPosition(cocheActualElegido2,-19.95,51.5 );
                }

                if (!terminoJugador1)
                {
                    ShowInCanvas(winnerPlayer, "Player 2");
                }
            }

            if (terminoJugador1 && terminoJugador2)
            {
                stageJuego = StageJuego.PostJuegoMulti;
            }
        }
    }
}
