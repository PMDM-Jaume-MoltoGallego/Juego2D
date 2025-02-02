using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public int coinsCollected;
    private int coinsAtSceneStart;

    private void Awake()
    {
        //Comprueba si ya existe una instancia del LevelManager
        if (instance == null)
        {
            //Si no hay una instancia, esta se convierte en la única instancia
            instance = this;
            //No se destruye al cambiar de escena, permitiendo que las monedas y otros datos se conserven
            DontDestroyOnLoad(gameObject);
           
        }
        else
        {
            //Si ya existe una instancia de LevelManager, destruye la nueva para evitar duplicados
            Destroy(gameObject);
        }
    }

    public void SaveCoinsAtSceneStart()
    {
        //Guarda cuántas monedas tenía al entrar a la escena
        coinsAtSceneStart = coinsCollected; 
    }

    public void RestoreCoinsAtSceneStart()
    {
        //Restaura las monedas si muere en la segunda escena
        coinsCollected = coinsAtSceneStart; 
    }
}
