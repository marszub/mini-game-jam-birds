using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIRB_ENERGY : MonoBehaviour
{
    public Bars bird_info;
    public float energy_percent = 1.0f;


    public bool fastBuff = false;
    public bool slowBuff=false;
    public bool tooClose=false;
    public bool airTunnel=false;
    // Start is called before the first frame update
    void Start()
    {
       // bird_info = GameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        bird_info.updateEnergy(energy_percent);
        bird_info.SetFastSpeed(fastBuff);
        bird_info.SetSlowSpeed(slowBuff);
        bird_info.SetTooClose(tooClose);
        bird_info.SetTunnel(airTunnel);
    }
}
