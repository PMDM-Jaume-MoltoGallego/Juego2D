using UnityEngine;

public class PickUp : MonoBehaviour
{

    public bool isCoin;
    public bool isHeal;
    private bool isCollected;
    public static PickUp instance;
    public AudioSource audioSourceCoin;
    public AudioSource audioSourceHearth;

    public void Awake()
    {
        instance = this;
        //audioSourceCoin = GetComponent<AudioSource>(); 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Controla los recolección de items
        if (other.CompareTag("Player") && !isCollected)
        {
            //Si es una moneda, siempre actualizamos el contador y la UI de monedas, reproducimos su sonido  y la destruimos
            if (isCoin)
            {

                LevelManager.instance.coinsCollected++;
                
                UIHealthController.instance.UpdateCoinCount();
                isCollected = true;

                AudioSource.PlayClipAtPoint(audioSourceCoin.clip, transform.position);
                

                Destroy(gameObject);
            }

            //Si es un corazón y la vida no está a tope, curamos al jugador y destruimos el corazón
            if (isHeal) {
                if (CharacterHealthController.instance.currentHealth != CharacterHealthController.instance.maxHealth) 
                {
                    CharacterHealthController.instance.HealPlayer();

                    isCollected = true;
                    AudioSource.PlayClipAtPoint(audioSourceHearth.clip, transform.position);
                    Destroy(gameObject);
                }
            }
        }
    }
}
