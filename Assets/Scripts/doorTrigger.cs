
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class doorTrigger : MonoBehaviour {

    public UnityEvent doorTriggered;
    public PlayableDirector pd;
    public GameObject player;

    public void playTimeline() {
        pd.Play();
    }
}
