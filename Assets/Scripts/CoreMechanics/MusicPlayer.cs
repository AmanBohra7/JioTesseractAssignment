using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicPlayer : MonoBehaviour{
    
    public void SetName(string name){
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
    }
    
    public Image progress;
    public AudioSource audioSource;

    void Update(){
        if(audioSource.isPlaying){
            progress.fillAmount = audioSource.time / 100;
        }
    }


    public void PauseMusic(){
        audioSource.Pause();
        PlayerManager.instance.MusicPlayerPauseMusic();
    }

    public void PlayMusic(){
        audioSource.Play();
        PlayerManager.instance.MusicPlayerPlayMusic();
    }

}
