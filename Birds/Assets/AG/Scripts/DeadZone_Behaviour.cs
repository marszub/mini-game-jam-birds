using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Behaviour : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("OOOK");
        if (other.tag == "Decoration")
        {
            Debug.Log("Kaboom");
            Destroy(other.gameObject);
            //other.DestroySelf();
        }
    }
}
