using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    //private Rigidbody2D rb;
    
    public int zOffSet = 5;
    public int xOffSet = 0;
    public int yOffSet = 0;
    //Transform currentView;
    //public int transitionSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //rb = player.gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x - xOffSet, player.position.y - yOffSet, player.position.z - zOffSet);
        /*if(rb.velocity.x >= 0)
            currentView.position = new Vector3(player.position.x - xOffSet, player.position.y - yOffSet, player.position.z - zOffSet);
        
        else
            currentView.position = new Vector3(player.position.x + xOffSet, player.position.y - yOffSet, player.position.z-zOffSet);

        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);*/
    }

    /*void LateUpdate()
    {

        //Lerp position
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
         Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        transform.eulerAngles = currentAngle;

    }*/
}
