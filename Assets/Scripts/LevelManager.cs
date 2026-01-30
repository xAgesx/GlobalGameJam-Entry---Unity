using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LevelManager : MonoBehaviour {
    
    public static bool introHasPlayed = false;

    public PlayableDirector introTimeline;
    public PlayableDirector introTimeline1;
    public GameObject player;
    public Transform startPoint; 
    public GameObject introCanvas;

    void Start() {
        if (introHasPlayed) {
            SkipIntro();
        } else {
            
            introHasPlayed = true;
        }
    }

    public void SkipIntro() {
        if (introTimeline != null) {
            
            introTimeline.time = introTimeline.duration;
            introTimeline.Evaluate(); 
            introTimeline.Stop();
            introCanvas.SetActive(false);

            introTimeline1.time = introTimeline1.duration;
            introTimeline1.Evaluate(); 
            introTimeline1.Stop();

        }
        
        
        if (startPoint != null) {
            player.transform.position = startPoint.position;
        }
    }

    public void ReloadLevel() {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}