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
    [SerializeField] private Bars bars;
    [SerializeField] private float destroyDistance;
    private float energy;
    private float speed;
    public enum State
    {
        Join,
        Fly,
        Dead
    }
    public State state;

    private SpriteRenderer spriteRenderer;
    private List<Buff> activeBuffs = new List<Buff>();

    public float OptimalSpeed { get => optimalSpeed; }
    public float Speed {set => speed = value; }

    private void Start()
    {
        state = State.Join;
        spriteRenderer = GetComponent<SpriteRenderer>();
        energy = maxEnergy;
        bars.updateEnergy(1);
        bars.SetTooClose(false);
        bars.SetTunnel(false);
        bars.SetFastSpeed(false);
        bars.SetSlowSpeed(false);
    }

    private void Update()
    {
        if (state == State.Dead)
            return;

        if(state == State.Join)
        {
            return;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        energy -= CalculateEnergyBuffs(Time.deltaTime);
        bars.updateEnergy(energy/maxEnergy);
        if(GetSpeedBuff(1) < 0)
        {
            bars.SetSlowSpeed(true);
            bars.SetFastSpeed(false);
        }else if(GetSpeedBuff(1) == 0)
        {
            bars.SetSlowSpeed(false);
            bars.SetFastSpeed(false);
        }else
        {
            bars.SetSlowSpeed(false);
            bars.SetFastSpeed(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Buff buff)
            && !activeBuffs.Contains(buff)
            && buff.transform.parent.gameObject != gameObject)
        {
            activeBuffs.Add(buff);
            UpdateAreaIcons();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Buff buff)
            && activeBuffs.Contains(buff))
        {
            activeBuffs.Remove(buff);
            UpdateAreaIcons();
        }
    }

    private void UpdateAreaIcons()
    {
        bars.SetTunnel(activeBuffs.Exists(buff => buff.CorrectEnergyLoss(1) < 0));
        bars.SetTooClose(activeBuffs.Exists(buff => buff.CorrectEnergyLoss(1) > 0));
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

    public IEnumerator Die()
    {
        state = State.Dead;
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        bars.gameObject.SetActive(false);
        while(transform.position.x > destroyDistance)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed / 2;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(gameObject);
    }

    public IEnumerator Join(float velocity, float endPoint)
    {
        while((velocity > 0 && transform.position.x < endPoint)
            || (velocity < 0 && transform.position.x > endPoint))
        {
            if (state != State.Join)
                break;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocity;
            yield return null;
        }

        if (state != State.Dead)
            state = State.Fly;
    }
}
