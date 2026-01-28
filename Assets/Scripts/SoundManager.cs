using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundManager : MonoBehaviour {

    public List<AudioSource> audioEffects;
    [SerializeField] List<PlayableDirector> timelines;
    void Update() {
        foreach(var i in timelines) {
            if (i.isActiveAndEnabled) {
                foreach(var sound in audioEffects) {
                    sound.volume = 0.3f;
                }
            }
        }
    }
}
