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

    public void TestingAddInventory()
    {
        AddItemToInventory(0,1);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
