using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{
    public Image energyBar;
    //public float energy_percent = 100.0f;
    public Image fastBuff;
    public Image slowBuff;
    public Image tooClose;
    public Image airTunnel;


    // Start is called before the first frame update
    void Start()
    {
        fastBuff.enabled = false;
        slowBuff.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateEnergy(float energy_percent)//0-1
    {
        energyBar.fillAmount = energy_percent;
    }

    public void SetFastSpeed(bool val)
    {
        fastBuff.enabled = val;
        //slowBuff.enabled = !val;
    }

    public void SetSlowSpeed(bool val)
    {
        slowBuff.enabled = val;
        //fastBuff.enabled = !val;
    }

    
    public void SetTooClose(bool val)
    {
        tooClose.enabled = val;
    }
    public void SetTunnel(bool val)
    {
        airTunnel.enabled = val;
    }
}
