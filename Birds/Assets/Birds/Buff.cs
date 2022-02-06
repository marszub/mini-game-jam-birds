using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private float energyLossMultiplier;
    public float CorrectEnergyLoss(float energy)
    {
        return energy * (energyLossMultiplier - 1);
    }
}
