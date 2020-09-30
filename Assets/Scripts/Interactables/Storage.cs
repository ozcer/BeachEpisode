using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : Interactable
{
    GameManager gm;
    public List<Color> orders = new List<Color>();
    public int orderLimit = 3;
    //public TextMesh text;
    public Text readyText;

    private void Start()
    {
        gm = GameManager.Get();
    }

    void Update()
    {
        if (orders.Count > 0) readyText.text = "New Order!\nPress E to Pick Up";
        else readyText.text = "";
    }

    public override void OnInteract()
    {
        if (orders.Count > 0 && gm.player.Items().Count < gm.player.headLimit)
        {
            gm.player.Equip(GetOrder());
        }
        else
        {
            print("No orders");
        }
    }

    public void MakeOrder(Color color)
    {
        orders.Add(color);
    }

    public Color GetOrder()
    {
        Color c = orders[0];
        orders.RemoveAt(0);
        return c;
    }
}
