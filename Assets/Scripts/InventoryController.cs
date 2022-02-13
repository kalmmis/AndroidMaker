using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryUI;
    private List<MyInventory> items;
    private List<Dictionary<string,object>> itemInfo;
    private int pageNumber = 2;
    private Text itemSlotText;

    void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        items = DataController.Instance.gameData.myInventoryList;
        itemInfo = CSVReader.Read ("ItemInfo");
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        Debug.Log ("How many kind items you have : " + items.Count);
        //인벤토리에 존재하는 아이템 종류 수
        int counter = 0;
        int rowIndex = 0;
        int slotIndex = 0;

        foreach (MyInventory item in items)
        {
            int id = item.id;
            int amount = item.amount;
            string itemName;

            int pageIndex = (pageNumber - 1) * 12;
            if(pageIndex <= counter && counter < pageIndex + 12)
            {
                itemName = (string)itemInfo[id]["itemName"];
                
                itemSlotText = inventoryUI.transform.Find("Panel").transform.Find("ItemScrollView").transform.Find("Viewport").transform.Find("Content").transform.Find("ItemPanelRow" + rowIndex.ToString()).transform.Find("ItemSlot" + slotIndex.ToString()).transform.Find("Text").GetComponent<Text>();
                //미친 짓... 단순화할 방법을 찾아보자

                itemSlotText.text = amount.ToString();
                Debug.Log("you are having " + itemName);
                Debug.Log("id is " + id + "/ amount is " + amount);
                Debug.Log("slotIndex is " + slotIndex);
                slotIndex++;
            }
            if (slotIndex == 4)
            {
                slotIndex = 0;
                rowIndex++;
            }
            counter++;
        }
    }

    public void TestingAddInventory()
    {
        AddItemToInventory(1,1);
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
