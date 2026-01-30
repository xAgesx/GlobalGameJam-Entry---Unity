using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundManager : MonoBehaviour {

    public List<AudioSource> audioEffects;
    [SerializeField] List<PlayableDirector> timelines;


    public void PlaySound(int index) {
        audioEffects[index].Play();
    }
}
