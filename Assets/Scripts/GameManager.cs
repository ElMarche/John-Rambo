using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; } // used with awake to return singleton instance
    private Estados estados; // Enum class defined at the bottom of the file
    [SerializeField] private GameObject johnWinsPanel;
    [SerializeField] private GameObject johnLosesPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private int cantGrunt;

    // using awake to return a Singleton
    private void Awake()
    {
        if (Instancia != null) Destroy(gameObject);
        else Instancia = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To start the game pressing f
        if(this.estados == Estados.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ActualizarEstados(Estados.Playing);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                /*
                 * C++ offers conditional compilation through the inclusion of certain directives, whom control the behaviour
                 * of the preprocessor, in a way in witch it can ignore or compile some code lines evaluated during preprocessing.
                 */
                #if UNITY_EDITOR
                    EditorApplication.isPlaying= false;
                #else
                    Application.Quit();
                #endif

            }
        }
        // If you win or lose and you want to return to main menu
        if(this.estados == Estados.JohnWin || this.estados == Estados.JohnDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ActualizarEstados(Estados.MainMenu);
            }
        }
    }

    public void ActualizarEstados(Estados nuevoEstado)
    {
        this.estados = nuevoEstado;
        switch (estados)
        {
            case Estados.MainMenu:
                johnLosesPanel.SetActive(false);
                johnWinsPanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case Estados.Playing:
                mainMenuPanel.SetActive(false);
                break;
            case Estados.JohnWin:
                johnWinsPanel.SetActive(true);
                break;
            case Estados.JohnDead:
                johnLosesPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public Estados GetEstados()
    {
        return estados;
    }

    public void decreaseGrunt()
    {
        cantGrunt--;
        if (cantGrunt == 0)
        {
            ActualizarEstados(Estados.JohnWin);
        }
    }
}

// enum class to manage the game status
public enum Estados
{
    MainMenu,
    Playing,
    JohnDead,
    JohnWin
}
