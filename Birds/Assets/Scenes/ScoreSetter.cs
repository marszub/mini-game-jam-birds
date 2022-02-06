using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSetter : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Your score: " + ((int)BirdsManager.Score).ToString() + " m";
    }
}
