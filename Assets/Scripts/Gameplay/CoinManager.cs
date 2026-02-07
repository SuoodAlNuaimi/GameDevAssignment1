using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TMP_Text coinText; 
    public GameObject Door;
    private bool doorDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount.ToString() + " :";

        if(coinCount >= 2 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(Door);
            Debug.Log("Door");
        }
    }
}
