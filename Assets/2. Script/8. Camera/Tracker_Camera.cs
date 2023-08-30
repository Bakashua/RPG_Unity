using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tracker_Camera : MonoBehaviour
{
    Cinemachine.CinemachineDollyCart cart;

    public Cinemachine.CinemachineSmoothPath startPath;
    public Cinemachine.CinemachineSmoothPath[] alternativePath;

    private void Awake()
    {
        cart = GetComponent<Cinemachine.CinemachineDollyCart>();

        Reset();
    }

    public void Reset()
    {
        StopAllCoroutines();
        
        cart.m_Path = startPath;

        StartCoroutine(ChangeTrack());
    }

    IEnumerator ChangeTrack()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = alternativePath[Random.Range(0, alternativePath.Length)];
        cart.m_Path = path;

        StartCoroutine(ChangeTrack());
    }

    //switch to normal when player do action avec call Reset()
    
}
