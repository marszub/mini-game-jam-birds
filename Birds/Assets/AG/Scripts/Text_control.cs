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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Distance(float distance)
    {
        dst.text = "Distance: " + distance;
    }
}
