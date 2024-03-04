using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;


public class Shield : MonoBehaviour
{
    [SerializeField] float duration = 5;
    private bool isPicked = false;
    private bool isActive = false;
    

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // All players can call this function; only the StateAuthority receives the call.

   

    public bool GetIsActive()
    {
        return isActive;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield") && !isPicked)
        {

            Debug.Log("Shield Picked Up");
            
          
            isActive = true;
            isPicked = true;

            StartCoroutine(ShieldTemporarily());
            Destroy(other.gameObject);
        }
    }


    private IEnumerator ShieldTemporarily()
    {   // co-routines
        for (float i = duration; i > 0; i--)
        {
           
            yield return new WaitForSeconds(1);       // co-routines
            // await Task.Delay(1000);                // async-await
        }
       
       Debug.Log("Shield Deactivated");
      
        isActive = false;
        isPicked = false;

    }
}
