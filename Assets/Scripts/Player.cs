using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gm;
    Transform head;
    public int headLimit = 1;

    public float interactRange;
    private void Start()
    {
        gm = GameManager.Get();
        head = transform.Find("Head");
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Interact();
        }
    }

    public void Equip(Color color)
    {
        // Spawn item with respective color on player head
        GameObject itemGo = Instantiate(gm.itemPf, transform.position, Quaternion.identity);
        SpriteRenderer itemRenderer = itemGo.GetComponent<SpriteRenderer>();
        itemRenderer.color = color;
        itemGo.transform.SetParent(head);
    }

    public void Interact()
    {
        // Get all interactables in radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
        List<GameObject> objects = new List<GameObject>();
        foreach (Collider2D c in colliders)
        {
            if (c.GetComponent<Interactable>() != null) objects.Add(c.gameObject);
        }
        if (objects.Count > 0)
        {
            // Interact with the nearest
            GameObject nearest = Helper.Nearest(transform.position, objects);
            nearest.GetComponent<Interactable>().OnInteract();
        }
    }

    public void Pop()
    {
        Destroy(Items().Last().gameObject);
    }

    public void PrintHead()
    {
        
        string items = "";
        foreach (Transform item in head)
        {
            items += item.gameObject.name + ", ";
        }
        print(items);
    }

    public List<Transform> Items()
    {
        List<Transform> items = new List<Transform>();
        foreach (Transform item in head)
        {
            items.Add(item);
        }
        return items;
    }
}
