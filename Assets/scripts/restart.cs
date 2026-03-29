using UnityEngine;

public class restart : MonoBehaviour
{
    public void LoadCurrentscene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("gamescene");
        Time.timeScale = 1;
    }
}
