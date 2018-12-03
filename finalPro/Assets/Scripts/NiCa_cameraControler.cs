using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiCa_cameraControler : MonoBehaviour {

    public GameObject player;

    public Vector3 offset;
	
	void Start ()
    {
        //offset = transform.position - player.transform.position.y;	
	}
	
	void LateUpdate ()
    {
        //transform.position = player.transform.position.x + offset;
        transform.position = new Vector3(0, player.transform.position.y, -10f) + offset;
    }
}