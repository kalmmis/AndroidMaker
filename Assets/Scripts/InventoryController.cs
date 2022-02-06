using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryUI;
    private List<MyInventory> items;

    void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        items = DataController.Instance.gameData.myInventoryList;
    }


    public void TestingAddInventory()
    {
        AddItemToInventory(0,1);
    }

    public void TestingUseInventory()
    {
        RemoveFromInventory(0,1);
    }

    public void TestingInventory()
    {
        //DataController.Instance.gameData.myInventoryList.Add (createSubObject (1,100));
        
        Debug.Log("objectList val is " + items);
        foreach (MyInventory item in items)
        {
            int i = item.id;
            Debug.Log(i);
        }
    }
    
    public void AddItemToInventory(int findId, int amount)
    {
        if(items.Exists(x => x.id == findId))
        {
            addSubObject(findId,amount);    
        }
        else
        {
            items.Add(createSubObject(findId,amount));
        }
    }
    
    public void RemoveFromInventory(int findId, int amount)
    {
        if(items.Exists(x => x.id == findId) && items.Exists(x => x.amount > amount))
        {
            subSubObject(findId,amount);    
        }
        else if(items.Exists(x => x.amount == amount))
        {
            items.Remove(removeSubObject(findId,amount));
        }
        else
        {
            Debug.Log ("Errror: Item amount is zero. you cant use it anymore.");
        }
    }

    public MyInventory addSubObject(int findId, int amount)
    {
        MyInventory myInnerObject = items.Find(x => x.id == findId);
        myInnerObject.amount += amount;
        return myInnerObject;
    }

    public MyInventory createSubObject(int id, int amount)
    {
        MyInventory myInnerObject = new MyInventory();
        myInnerObject.id = id;
        myInnerObject.amount = amount;
        return myInnerObject;
    }

    public MyInventory subSubObject(int findId, int amount)
    {
        MyInventory myInnerObject = items.Find(x => x.id == findId);
        myInnerObject.amount -= amount;
        return myInnerObject;
    }

    public MyInventory removeSubObject(int id, int amount)
    {
        MyInventory myInnerObject = items.Find(x => x.id == id);
        return myInnerObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
