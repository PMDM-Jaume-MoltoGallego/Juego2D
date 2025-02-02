using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{

    public static UIHealthController instance;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartEmpty;
    public Sprite heartHalf;

    public Text coinText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoinCount();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthDisplay();
        UpdateCoinCount();
    }

    public void UpdateHealthDisplay()
    {
        //Modifica la UI de vida del player
        switch (CharacterHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateCoinCount()
    {
        //Modifica la UI de monedas
        coinText.text = LevelManager.instance.coinsCollected.ToString();
    }
}
