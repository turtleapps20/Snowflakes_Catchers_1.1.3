using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text snowText;

    private void Start()
    {
        int snow = PlayerPrefs.GetInt("Snowflake");
        snowText.text = snow.ToString();
    }

    public void NextLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
