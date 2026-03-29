using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject winui;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winui.SetActive(true);
            Debug.Log("You win!");
        }
    }

}
