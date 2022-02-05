using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIRB_ENERGY : MonoBehaviour
{
    public Bars bird_info;
    public float energy_percent = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       // bird_info = GameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        bird_info.updateEnergy(energy_percent);
    }
}
