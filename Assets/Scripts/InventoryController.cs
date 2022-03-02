using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryUI;
    private List<MyInventory> items;
    private List<Dictionary<string,object>> itemInfo;
    private int pageNumber;
    
    private GameObject itemSlot;
    private Button itemSlotButton;
    private Image itemSlotImage;
    private Text itemSlotText;

    public Text itemDescPanelNameText;
    public Text itemDescPanelDescText;
    public Text itemDescPanelCreditText;
    public Text itemDescPanelCoreText;
    public Image itemDescPanelImage;
    public Text itemDescPanelButtonText;

    void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        itemSlot = GameObject.FindWithTag("ItemSlotContent");

        items = DataController.Instance.gameData.myInventoryList;
        itemInfo = CSVReader.Read ("ItemInfo");
        pageNumber = 1;
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

            if(slotIndex == 0 && rowIndex == 0)
            {
                SelectItem(id);
            }

            int pageIndex = (pageNumber - 1) * 12;
            if(pageIndex <= counter && counter < pageIndex + 12)
            {
                itemSlotButton = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Button>();
                itemSlotImage = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Image>();
                itemSlotText = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).transform.Find("Text").GetComponent<Text>();
                        
                itemSlotButton.interactable = true;
                itemSlotImage.sprite = Resources.Load<Sprite>("Image/ItemIcon/ItemIcon_" + id);
                itemSlotText.text = amount.ToString();
                
                itemSlotButton.onClick.AddListener(delegate() { SelectItem(id); });

                itemName = (string)itemInfo[id]["itemName"];
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
    public void SelectItem(int item)
    {
        Debug.Log("item id is " + item);
        
        itemDescPanelNameText.text = (string)itemInfo[item]["itemName"];
        itemDescPanelDescText.text = (string)itemInfo[item]["itemDesc"];
        itemDescPanelImage.sprite = Resources.Load<Sprite>("Image/ItemIcon/ItemIcon_" + item);
        itemDescPanelCreditText.text = "";
        itemDescPanelCoreText.text = "";
        itemDescPanelButtonText.text = "Use";
    }

    public void InventoryPageUp()
    {
        float count = items.Count / 12f;
        Debug.Log(count);
        if(pageNumber < count)
        {
            pageNumber++;
            SetEmptyInventoryUI();
            RefreshInventoryUI();
        }
        Debug.Log("pageNumber is " + pageNumber);
    }

    public void InventoryPageDown()
    {
        if(pageNumber >= 2)
        {
            pageNumber--;
            SetEmptyInventoryUI();
            RefreshInventoryUI();
        
        Debug.Log("pageNumber is " + pageNumber);}
    }
    
    public void SetEmptyInventoryUI()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                itemSlotButton = itemSlot.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>();
                itemSlotImage = itemSlot.transform.GetChild(i).transform.GetChild(j).GetComponent<Image>();
                itemSlotText = itemSlot.transform.GetChild(i).transform.GetChild(j).transform.Find("Text").GetComponent<Text>();
                
                itemSlotButton.interactable = false;
                itemSlotImage.sprite = null;
                itemSlotText.text = "";
            }
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
