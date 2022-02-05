using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float optimalSpeed;
    [SerializeField] private float energyBuffFromSpeed;
    [SerializeField] private float energyNerfFromSpeed;
    [SerializeField] private float speedNeutralZone;
    private float energy;
    private float speed;

    private SpriteRenderer spriteRenderer;
    private List<Buff> activeBuffs = new List<Buff>();

    public float OptimalSpeed { get => optimalSpeed; }
    public float Speed {set => speed = value; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        energy = maxEnergy;
    }

    private void Update()
    {
        energy -= CalculateEnergyBuffs(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Buff buff)
            && !activeBuffs.Contains(buff)
            && buff.transform.parent.gameObject != gameObject)
        {
            activeBuffs.Add(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Buff buff)
            && activeBuffs.Contains(buff))
        {
            activeBuffs.Remove(buff);
        }
    }

    private float CalculateEnergyBuffs(float baseLoss)
    {
        float energyLoss = 0;
        foreach (Buff buff in activeBuffs)
        {
            energyLoss += buff.CorrectEnergyLoss(baseLoss);
        }
        energyLoss += GetSpeedBuff(baseLoss);

        return baseLoss + energyLoss;
    }

    private float GetSpeedBuff(float baseLoss)
    {
        if (optimalSpeed < speed)
            return baseLoss * energyNerfFromSpeed * (speed / optimalSpeed - 1);

        if ((1 - speedNeutralZone) * optimalSpeed > speed)
            return baseLoss * energyBuffFromSpeed * (speed / optimalSpeed / (1 - speedNeutralZone) - 1);

        return 0f;
    }

    public bool IsDead()
    {
        return energy < 0;
    }
}
