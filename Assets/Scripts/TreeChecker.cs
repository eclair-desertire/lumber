using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChecker : MonoBehaviour
{
    public AudioSource audioSource;
  
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ENTERED");
        audioSource.Play();
    }
}
