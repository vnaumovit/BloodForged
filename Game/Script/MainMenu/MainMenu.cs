using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("Forest");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame() {
        Application.Quit();
    }
}