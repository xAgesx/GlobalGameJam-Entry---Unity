using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string firstLevelName = "MainScene";

    public void PlayGame() {

        LevelManager.introHasPlayed = false;

        SceneManager.LoadScene(firstLevelName);
    }

    public void QuitGame() {

        Application.Quit();
    }
}
