using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class MenuGenerator : MonoBehaviour{
    
    public GameObject optionPrefab;

    BoxCollider menuCollider;
    PlayerManager playerInstance;

    void Start(){

        menuCollider = gameObject.GetComponent<BoxCollider>();

        playerInstance = PlayerManager.instance;
    
    }

    public void GeneterateMenu(){
        StartCoroutine(InstantiateOptions(1f));
    }

    ///<summary>
    ///
    ///</summary>
    IEnumerator InstantiateOptions(float waitTime){

        yield return new WaitForSeconds(waitTime);

        float radius = (menuCollider.size.x / 2);
        // int count = songsList.Count;
        int count = playerInstance.mp3List.Count;


        // trging to positioning first one in the fron of camera    

        for(int i = 0 ; i < count ; ++i){
            float x =  radius * Mathf.Cos(2 * Mathf.PI * i / count); 
            float z =  radius * Mathf.Sin(2 * Mathf.PI * i / count);
            GameObject option = Instantiate(
                optionPrefab,
                new Vector3(x,menuCollider.gameObject.transform.position.y,z),
                Quaternion.identity,
                menuCollider.gameObject.transform);

            // set option name and text
            string name = playerInstance.mp3List.Keys.ToList()[i].Split('.')[0];
            option.GetComponentInChildren<TextMeshProUGUI>().text = name;
            option.name = name;
                ;

            option.GetComponent<Canvas>().worldCamera = Camera.main;
        
        }
        
    }

}
