using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public AudioSource audioSource;
    public AudioClip winSound;

    public void ShowWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        if (audioSource && winSound)
            audioSource.PlayOneShot(winSound);
    }
}
