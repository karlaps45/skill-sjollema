using UnityEngine;

public class FPS : MonoBehaviour
{
    private GUIStyle style;
    private GUIStyle fpsStyle;

    void Start()
    {
      
        style = new GUIStyle();
        style.fontSize = 40;
        style.normal.textColor = Color.white;

        
        fpsStyle = new GUIStyle();
        fpsStyle.fontSize = 20;
        fpsStyle.normal.textColor = Color.green;
    }

    void OnGUI()
    {
        
        GUI.Label(new Rect(10, 10, 300, 100), "Hallo!", style);

        float fps = 1.0f / Time.deltaTime;

       
        GUI.Label(new Rect(10, 60, 200, 40), "FPS: " + Mathf.Round(fps), fpsStyle);
    }
}
