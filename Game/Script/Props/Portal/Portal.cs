using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
    [SerializeField] private int moveScene;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out PlayerEntity _)) {
            SceneManager.LoadSceneAsync(moveScene);
        }
    }
}