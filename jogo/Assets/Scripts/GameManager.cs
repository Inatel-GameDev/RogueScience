using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Flags")]
    public bool isPaused = false;
    
    [Header("Canas")]
    public Canvas menuPause;

    
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        menuPause.gameObject.SetActive(true);
    }
    
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menuPause.gameObject.SetActive(false);
    }
    
    
}
