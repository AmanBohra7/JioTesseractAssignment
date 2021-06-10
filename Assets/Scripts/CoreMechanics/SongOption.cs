using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongOption : MonoBehaviour{
    
    private bool isOptionSelected;

    public float speed;

    private Vector3 savePose;
    void Start(){
        isOptionSelected = false;
        speed = 0.05f;
        gameObject.GetComponentInParent<Button>().onClick.AddListener(delegate {BtnPressed();});
    }

    void Update(){
        if(!isOptionSelected){
            gameObject.transform.forward = Camera.main.transform.forward;
        }
        if(isOptionSelected){
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,0.001f,0) , Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale , new Vector3(0.02f,0.02f,0.02f),Time.deltaTime);
        }
    }

    
    private void BtnPressed(){

        if(PlayerManager.instance.isSongSelected) return;

        isOptionSelected = true;
        savePose = transform.position;
        // after song is selected call for the animation for moving song canvas to the center pose 
        PlayerManager.instance.SelectSong(gameObject);

        // hide song name below disk
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

        StartCoroutine(AnimateIt());
    }

    public void Deselect(){
        isOptionSelected = false;
        gameObject.transform.position = savePose;
        gameObject.transform.localScale = new Vector3(0.01f,0.01f,0.01f);

        //  show song name below disk
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }


    IEnumerator AnimateIt(){
        yield return new WaitForSeconds(0);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
    }

}
