using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patron : MonoBehaviour
{
    public Transform dest;
    public Zone dineZone;
    public AIPath aiPath;
    public AIDestinationSetter destSetter;
    public SpriteRenderer srenderer;
    public Sounder sounder;
    public bool orderRequested = false;
    public bool orderTaken = false;

    public float minChillDuration;
    public float maxChillDuration;
    public float chillChance = 0.5f;
    public float orderChance = 0.5f;
    public bool isBuyer;
    public float duckChance = 1f;
    public SpriteSpriteShift sss;

    public float reward = 20f;

    public State state;

    public enum State
    {
        Waiting,
        Entering,
        Idle,
        Leaving
    }

    public GameManager gm;
    private void Start()
    {
        gm = GameManager.Get();
        dineZone = gm.dineZone;

        isBuyer = Random.Range(0f, 1f) < orderChance;
        if (!isBuyer)
        { 
            bool ducked = Random.Range(0f, 1f) < duckChance;
            if (ducked)
            {
                sss.duckForm = true;
                srenderer.color = Color.white;
            }
        }
        GoToRandomDineSpot();
    }

    // Update is called once per frame
    void Update()
    {
        // Flip sprite
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (reward > 1f) reward -= Time.deltaTime;

        if (state != State.Leaving && gm.gameOver) Leave();
    }

    void SetRandomDest(Zone zone)
    {
        Vector2 randPoint = zone.RandomPoint();
        dest.position = randPoint;
        destSetter.target = dest;
    }

    void GoToRandomDineSpot()
    {
        SetRandomDest(dineZone);
        state = State.Entering;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player collision
        if (collision.gameObject.CompareTag("Player") &&
            state == State.Waiting &&
            gm.player.Items().Count > 0 && 
            gm.player.Items().Last().GetComponent<SpriteRenderer>().color == srenderer.color)
        {
            gm.player.Pop();
            Pay();
            Leave();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On reaching destination
        if (collision.transform == dest)
        {
            if (state == State.Entering)
            {
                ChillABit();
            }
            else if (state == State.Leaving)
            {
                Destroy(transform.parent.parent.gameObject);
            }
        }
    }

    private void ChillABit()
    {
        state = State.Idle;

        // Roll to action
        // 
        float roll = Random.Range(0f, 1f);
        if (roll < chillChance)
        {
            float duration = Random.Range(minChillDuration, maxChillDuration);
            StartCoroutine(DelayedAction(GoToRandomDineSpot, duration));
        }
        else
        {
            if (isBuyer && gm.storage != null && gm.storage.orders.Count < gm.storage.orderLimit)
            {
                MakeOrder();
            }
            else
            {
                Leave();
            }
        }
        
    }

    IEnumerator DelayedAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    void MakeOrder()
    {
        print("making order!");
        Color c = srenderer.color;
        if (gm.sounder != null && gm.storage.orders.Count == 0) gm.sounder.PlayDing();
        gm.storage.MakeOrder(c);
        state = State.Waiting;
    }

    void Pay()
    {
        gm.sounder.PlayMoney();
        gm.money += (int)reward;
        if (gm.money > gm.highScore) gm.highScore = gm.money;
    }

    public void Leave()
    {
        SetRandomDest(gm.RandomSpawnZone());
        state = State.Leaving;
    }

    public void SetSpeed(int speed)
    {
        aiPath.maxSpeed = speed;
    }
}
