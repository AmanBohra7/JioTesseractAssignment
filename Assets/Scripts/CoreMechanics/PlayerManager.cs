using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour{

    // character animation controller
    public Animator characterAnimator;

    // singlton instance of the PlayerManager class
    public static PlayerManager instance; 

    // current selected song name from the menu bar
    private string selectedSongName;
    private GameObject selectedSongObject;

    [HideInInspector]
    public bool isSongSelected ;

    // music player having controllers for music 
    public GameObject musicPlayer;
    
    // single audio source for playing songs from the list
    public AudioSource audioSource;

    // all songs options parent
    public GameObject menu;

    // list of all songs loaded from json file dynamicaly
    // data is filled from JSONFileLoader file
    public Dictionary<string,AudioClip> mp3List;

    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        mp3List = new Dictionary<string, AudioClip>();
    }

    void Start(){
        //  setting up initial state
        isSongSelected = false;
        menu.SetActive(true);
        musicPlayer.SetActive(false);
        menu.GetComponent<MenuGenerator>().GeneterateMenu();
    }  

    public void SelectSong(GameObject songObj){

        // diable momement of song list pose in optionController script
        menu.GetComponent<OptionController>().setSelectionState(false);


        selectedSongName = songObj.gameObject.name;
        selectedSongObject = songObj;
        isSongSelected = true;
        
        StartCoroutine(
            SelectSongCo()
        );
    }

    IEnumerator SelectSongCo(){
        yield return new WaitForSeconds(.4f);

        // make character dance
        characterAnimator.SetBool("isJump",true);
        characterAnimator.SetBool("isIdle",false);
        characterAnimator.SetBool("isPause",false);


        yield return new WaitForSeconds(1.6f);

        // disable non selected song options
        for (int i = 0; i < menu.transform.childCount; i++){
            if(menu.transform.GetChild(i).gameObject.name != selectedSongObject.name) 
                menu.transform.GetChild(i).gameObject.SetActive(false);
        }


        // set name of the song in music player
        musicPlayer.GetComponent<MusicPlayer>().SetName(selectedSongName);

        // set active song name
        audioSource.clip = mp3List[selectedSongObject.name];
        
        // play audio
        audioSource.Play();

        // enable music player
        musicPlayer.SetActive(true);


        // make character dance
        // since no dance animation in character, running forword and backword will be played at random
    }


    public void CloseMusicPlayer(){

        // make character go to state state
        characterAnimator.SetBool("isJump",false);
        characterAnimator.SetBool("isIdle",true);
        characterAnimator.SetBool("isPause",false);

        //  deselect the song option
        selectedSongObject.GetComponent<SongOption>().Deselect();

        // close music player
        musicPlayer.SetActive(false);

        // stop song
        audioSource.Stop();

        // set bool
        isSongSelected = false;

        // show all other options
        for (int i = 0; i < menu.transform.childCount; i++){
            menu.transform.GetChild(i).gameObject.SetActive(true);
        }

        // enab;e momement of song list pose in optionController script
        menu.GetComponent<OptionController>().setSelectionState(true);
    }


    public void MusicPlayerPauseMusic(){
        // go to idle state
        characterAnimator.SetBool("isPause",true);
        characterAnimator.SetBool("isJump",false);
        characterAnimator.SetBool("isIdle",false);
    }

    public void MusicPlayerPlayMusic(){
        // cont. with dance
        characterAnimator.SetBool("isJump",true);
        characterAnimator.SetBool("isIdle",false);
        characterAnimator.SetBool("isPause",false);
    }

}

//  which song is selected
//  set selected song in the audio player
//  close btn functionality 
//  
