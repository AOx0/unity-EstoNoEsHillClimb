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
    private TipoJuego tipoJuego;

    //Music
    public AudioSource music;
    public static bool musicEnabled;

    // Completed Levels
    public static bool completedLvl1 = false;
    public static bool completedLvl2 = false;
    public static bool completedLvl3 = false;
    public static bool completedLvl4 = false;
    public static bool completedLvl5 = false;

    public Canvas NewRecordCanvas;

    //Seguimiento de cámara
    public Camera camara;
    public Camera camara2;
    public Rigidbody2D coche1;
    public Rigidbody2D coche2;
    public Rigidbody2D coche3;
    public Rigidbody2D coche4;

    public SpriteRenderer fondo;
    public Rigidbody2D ayudas;

    // Elementos del juego (Estado de Coche Actual)
    private float posXCamara;
    private float posYCamara;
    private float movimiento;

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

    // Estado del juego
    private int levelActual;
    private StageJuego stageJuego;
    private Rigidbody2D cocheActualElegido;
    private Rigidbody2D cocheActualElegido2;


    private float tiempoJugador1;

    // Canvas de Texto
    public Canvas textoTiempo1;

    public Canvas mensajeVictoria_Nivel;
    public Canvas mensajeVictoria_Tiempo;
    public Canvas pressToContinue;

    public SpriteRenderer mensajeVictoria_Fondo;

    public SpriteRenderer PorcentajeTrackSolitario;

    //Indicador de "velocidad" y aconsejamiento de reinicio
    public SpriteRenderer indicadorSprite;
    public SpriteRenderer hintReiniciar;

    // Start is called before the first frame update
    void Start()
    {
        tiempoJugador1 = 0;
        musicEnabled = true;

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
        if (stageJuego == StageJuego.Juego)
        {
            UpdateIndicationsUI();
        }
        
        CameraFollow();
        CheckIfUserWin();
        ShowWinMessage();
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
        Menu,
        Seleccion,
        SeleccionCoche,
        PreJuego, // Cargado
        Juego,
        PostJuego, // Victoria
    }


    enum TipoJuego
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
                cocheActual = coche1;
                break;
            case CocheElegido.Dos:
                cocheActual = coche2;
                break;
            case CocheElegido.Tres:
                cocheActual = coche3;
                break;
            case CocheElegido.Cuatro:
                cocheActual = coche4;
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
            camara.transform.position = makeVector(posXCamara, posYCamara);
            fondo.transform.position = makeVector(posXCamara, posYCamara, true);
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
            
        }
        
    }


// MARK: - Update UI
    void UpdateIndicationsUI()
    {
        void changeSpeedUIState()
        {

            void rotateBody(double degrees)
            {
                indicadorSprite.transform.rotation = Quaternion.Euler(Vector3.forward * (float)degrees);
            }

        
            if (movimiento >= 0)
            {
                if (movimiento * -0.2 >= -200)
                {
                    rotateBody((movimiento * -0.2));
                } else
                {
                    rotateBody((movimiento * -0.0033) - (200));
                }
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

        void setCarPosition(double x, double y)
        {
            cocheActualElegido.transform.position = new Vector2 { x = (float)x, y = (float)y};
            cocheActualElegido.transform.rotation = new Quaternion { z = 0 };
            
        }

        void setCarLevelPosition(bool restart = false)
        {
            if (stageJuego == StageJuego.PreJuego || restart == true) {
                

                // Inicia el tiempo del usuarui
                tiempoJugador1 = Time.time;
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
                }
                stageJuego = StageJuego.Juego;
            }
        }

        // Handler de movimiento  
        void carMovement(Rigidbody2D coche)
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

            if (tipoJuego == GameHandler.TipoJuego.Single && stageJuego == GameHandler.StageJuego.Juego)
            {
                singleVerticalMovement();
                singleHorizontalMovement();
            }
            
        }

        void setSpeedTo0()
        {
            WheelJoint2D rueda1 = cocheActualElegido.gameObject.transform.GetChild(0).gameObject.GetComponent<WheelJoint2D>();
            WheelJoint2D rueda2 = cocheActualElegido.gameObject.transform.GetChild(1).gameObject.GetComponent<WheelJoint2D>();

            rueda2.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};
            rueda1.motor = new JointMotor2D { motorSpeed = 1, maxMotorTorque = 100};
        }

        void setSpecifiedCarPosition(Rigidbody2D coche, double x, double y)
            {
                coche.transform.position = new Vector2 { x = (float)x, y = (float)y};
                coche.transform.rotation = new Quaternion { z = 0 };
            }

           void parkAllCars()
           {
                setSpecifiedCarPosition(coche1,-31.7,51.5 );
                setSpecifiedCarPosition(coche2,-26.28,51.5 );
                setSpecifiedCarPosition(coche3,-26.28,51.5 );
                setSpecifiedCarPosition(coche4,-19.95,51.5 );
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
            if (tipoJuego == TipoJuego.Single)
            {
                carMovement(cocheActualElegido);
            }
            
            tipToRestart();
            if (pressed(KeyCode.R))
            {
                setSpeedTo0();
                setCarLevelPosition(true);
            }

            if (pressed(KeyCode.Q))
            {
                parkAllCars();
                stageJuego = StageJuego.Seleccion;
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
        }

        void selectionLvlKeys()
        {
            returnToMenu();

            if (pressed(KeyCode.Alpha1) || pressed(KeyCode.Keypad1))
            {
                levelActual = 1;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
            }

            if (pressed(KeyCode.Alpha2) || pressed(KeyCode.Keypad2))
            {
                levelActual = 2;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
            }

            if (pressed(KeyCode.Alpha3) || pressed(KeyCode.Keypad3))
            {
                levelActual = 3;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
            }

            if (pressed(KeyCode.Alpha4) || pressed(KeyCode.Keypad4))
            {
                levelActual = 4;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
            }

            if (pressed(KeyCode.Alpha5) || pressed(KeyCode.Keypad5))
            {
                levelActual = 5;
                stageJuego = StageJuego.PreJuego;
                setCarLevelPosition();
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
}
