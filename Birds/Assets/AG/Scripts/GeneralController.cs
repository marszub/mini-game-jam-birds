using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{

    public float tmp_bg_velocity = 0;
    public BGController back;
    public Text_control text;
    public float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        back.Set_Velocity(tmp_bg_velocity);
        text.Set_Distance(distance);
    }
}
