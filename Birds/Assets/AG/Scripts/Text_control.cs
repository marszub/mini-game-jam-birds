using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_control : MonoBehaviour
{
    Text dst;
    // Start is called before the first frame update
    void Start()
    {
        dst = GetComponent<Text>();
    }

    public void Set_Distance(float distance)
    {
        dst.text = "Distance: " + (int)distance + " m";
    }
}
