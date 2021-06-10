using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JSONFileLoader : MonoBehaviour
{

    public static JSONFileLoader instance;

    // [HideInInspector]
    // public Dictionary<string,AudioClip> mp3List;

    public AudioSource test;

    PlayerManager playerInstance;

    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        #if UNITY_ANDROID
            StartCoroutine(ReadJsonFromStreaming());   
        #endif  
    }

    void Start(){
        // mp3List = new Dictionary<string, AudioClip>();
        playerInstance = PlayerManager.instance;
        ReadJSON();
    }

    [Serializable]
    class Song{
        public string Title;
        public string Path;
    }

    [Serializable]
    class SongsList{
        public List<Song> Songs;
    }

    public void ReadJSON(){

        #if UNITY_ANDROID
            string filePath = Application.persistentDataPath + "/SongList.json";
        #else
            string filePath = Path.Combine(Application.streamingAssetsPath + "/TessMusicPlayer/", "SongList.json");
        #endif

        string jsonData = File.ReadAllText(filePath);
        print(jsonData);
        SongsList songJson = JsonUtility.FromJson<SongsList>(jsonData);

        FillDictWithMp3(songJson);
    }


    private void FillDictWithMp3(SongsList songJson){
        for(int i = 0 ; i < songJson.Songs.Count ; ++i){
            AudioClip _audio = Resources.Load<AudioClip>("Songs/"+songJson.Songs[i].Path.Split('.')[0]);
            playerInstance.mp3List.Add(songJson.Songs[i].Path.Split('.')[0] , _audio);
        }
        // test.clip = PlayerManager.instance.mp3List[songJson.Songs[1].Path];
        // test.Play();
    }

    // function to load json file from streamingassets file and save it to persistant path
    // called in case of android mode
    IEnumerator ReadJsonFromStreaming(){

        string filePath = Path.Combine(Application.streamingAssetsPath+"/TessMusicPlayer/","SongList.json");
        string result;
        if (filePath.Contains(": //") || filePath.Contains (":///")) 
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            result = www.downloadHandler.text;
        } 
    
        else {
            result = File.ReadAllText(filePath);
        }
    
        File.WriteAllText(Application.persistentDataPath + "/SongList.json", result);
    } 

}
