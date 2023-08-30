using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //public GameObject cam;
    private Transform camPosition;

    private void Start()
    {
        camPosition = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        //transform.LookAt(camPosition.forward);
        transform.rotation = Quaternion.LookRotation(transform.position - camPosition.transform.position);
    }
}
