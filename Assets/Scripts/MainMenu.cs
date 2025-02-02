using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string Scene;
    private AudioSource audioSourceButton;


    void Start()
    {
        audioSourceButton = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        audioSourceButton.Play();
        SceneManager.LoadScene(Scene);
    }

    public void ExitGame()
    {
        audioSourceButton.Play();
        Application.Quit();
    }
}
