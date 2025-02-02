using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    public string Scene;


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si el trigger detecta al player, guarda las monedas recogidas hasta el momento y carga otra escena
        if(other.tag == "Player"){
            LevelManager.instance.SaveCoinsAtSceneStart();
            SceneManager.LoadScene(Scene);
        }

    }
}
