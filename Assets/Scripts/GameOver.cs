using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text coinText;
    public string Scene;
    private AudioSource audioSourceButton;

    void Start()
    {
        audioSourceButton = GetComponent<AudioSource>();
        //Muestra el número de monedas recogidas
        coinText.text = LevelManager.instance.coinsCollected.ToString();
    }

    public void StartGame()
    {
        audioSourceButton.Play();
        //Pone las monedas a 0 y vuelve a cargar la primera escena
        LevelManager.instance.coinsCollected = 0;
        SceneManager.LoadScene(Scene);
    }

    public void ExitGame()
    {
        audioSourceButton.Play();
        //Sale de la aplicación
        Application.Quit();
    }
}
