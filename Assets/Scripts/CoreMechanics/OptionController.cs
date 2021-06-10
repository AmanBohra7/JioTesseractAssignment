using UnityEngine;

public class OptionController : MonoBehaviour{
   
    Vector3 cameraPreviousPose = Vector3.zero;
    Vector3 cameraPoseoffset;

    float rotationAngle;

    public bool isSelectionEnabled;

    void Start(){
        isSelectionEnabled = true;
    }

    public void setSelectionState(bool state){
        isSelectionEnabled = state;
    }

    void Update(){

        if (Input.GetMouseButton(0) && isSelectionEnabled){

            cameraPoseoffset = Input.mousePosition - cameraPreviousPose;
            cameraPoseoffset = new Vector3(
                cameraPoseoffset.x,
                cameraPoseoffset.y,
                cameraPoseoffset.z);
            
            // right
            if (Input.mousePosition.x > cameraPreviousPose.x){

                gameObject.transform.Rotate(
                new Vector3(0, - rotationAngle * .5f, 0),
                Space.World);

            }else 
            // left
            if (Input.mousePosition.x < cameraPreviousPose.x){
                
                gameObject.transform.Rotate(
                new Vector3(0, + rotationAngle * .5f, 0),
                Space.World);

            }
            else{
                return;
            }

            rotationAngle = Mathf.Abs(Vector3.Dot(cameraPoseoffset, Camera.main.transform.right));

        }
        cameraPreviousPose = Input.mousePosition;


    }


}
