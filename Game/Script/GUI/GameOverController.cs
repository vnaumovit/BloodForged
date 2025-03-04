using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
    private enum Scene {
        Forest,
        Dungeon
    }

    public void StartNewGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name == Scene.Forest.ToString()
            ? Scene.Forest.ToString()
            : Scene.Dungeon.ToString());
    }

    public void ExitGame() {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void GameOver() {
        gameObject.SetActive(true);
    }
}