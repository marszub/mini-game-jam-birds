using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{
    public Image energyBar;

    //public float energy_percent = 100.0f;
   // public bool 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateEnergy(float energy_percent)//0-1
    {
        energyBar.fillAmount = energy_percent;
    }
}
