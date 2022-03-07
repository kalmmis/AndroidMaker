using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI;

    private List<MyInventory> items;
    private List<Dictionary<string,object>> itemInfo;
    private List<Dictionary<string,object>> craftInfo;
    private static int pageNumberInventory;
    private static int pageNumberCraft;
    
    public GameObject itemSlot;

    private Button itemSlotButton;
    private Image itemSlotImage;
    private Text itemSlotText;

    public Button toggleInventoryBtn;
    public Button toggleCraftBtn;

    public Text itemDescPanelNameText;
    public Text itemDescPanelDescText;
    public Text itemDescPanelCreditText;
    public Text itemDescPanelCoreText;
    public Image itemDescPanelImage;
    public Text itemDescPanelButtonText;

    public Text pageDescText;

    public Button useBtn;

    private static bool isCraft;

    void Start()
    {
        items = DataController.Instance.gameData.myInventoryList;
        itemInfo = CSVReader.Read ("ItemInfo");
        craftInfo = CSVReader.Read ("CraftInfo");
        pageNumberInventory = 1;
        pageNumberCraft = 1;
        isCraft = false;
        
        RefreshInventoryUI();
    }

    public void SetPageNumber()
    {
        int maxPageNumberInventory = items.Count / 12;
        if(items.Count % 12 > 0) maxPageNumberInventory++;

        int maxPageNumberCraft = craftInfo.Count / 12;
        if(craftInfo.Count % 12 > 0) maxPageNumberCraft++;

        if(isCraft)
        {
            pageDescText.text = pageNumberCraft.ToString() + " / " + maxPageNumberInventory.ToString();
            Debug.Log("set page number : pageNumberCraft is" + pageNumberCraft);
        }
        else
        {
            pageDescText.text = pageNumberInventory.ToString() + " / " + maxPageNumberCraft.ToString();
            Debug.Log("set page number : pageNumberInventory is" + pageNumberInventory);
        } 
    }

    public void ToggleInventoryUI()
    {
        isCraft = false;
        pageNumberInventory = 1;
        SetEmptyInventoryUI();
        RefreshInventoryUI();
        
        toggleInventoryBtn.interactable = false;
        toggleCraftBtn.interactable = true;
    }

    public void ToggleCraftUI()
    {
        isCraft = true;
        pageNumberCraft = 1;
        SetEmptyInventoryUI();
        RefreshCraftUI();
        
        toggleInventoryBtn.interactable = true;
        toggleCraftBtn.interactable = false;
    }


    public void RefreshCraftUI()
    {
        int counter = (pageNumberCraft - 1) * 12;
        int craftInfoCount = craftInfo.Count;
        SetPageNumber();
        
        foreach (var item in craftInfo)
        {
            Debug.Log ("foreach for craftInfo ");
            Debug.Log (item["condHasItem"]);
            Debug.Log (item["condStoryId"]);
            Debug.Log (item["condSchedule1Id"]);
            Debug.Log (item["condSchedule1Lv"]);
            
            /*
            //if(cond)
            {
                itemSlotButton = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Button>();
                itemSlotImage = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Image>();
                itemSlotText = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).transform.Find("Text").GetComponent<Text>();
                      
                itemSlotButton.interactable = true;
                itemSlotImage.sprite = Resources.Load<Sprite>("Image/ItemIcon/ItemIcon_" + id);
                itemSlotText.text = amount.ToString();
                
                itemSlotButton.onClick.AddListener(delegate() { SelectCraftRecipe(id); });

                slotIndex++;
            }
            if (slotIndex == 4)
            {
                slotIndex = 0;
                rowIndex++;
            }
            */

            //SetPageNumber 도 활성화되는 아이템 개수에 따라서 재구현해야 함
        }

        for(int rowIndex = 0; rowIndex < 3; rowIndex++)
        {
            for(int slotIndex = 0; slotIndex < 4; slotIndex++)
            {
                int id = (int)craftInfo[counter]["resultItemId"];
                int amount = (int)craftInfo[counter]["resultItemCount"];
                string itemName = (string)itemInfo[id]["itemName"];
                string itemDesc = (string)itemInfo[id]["itemDesc"];

                if(rowIndex == 0 && slotIndex == 0) SelectCraftRecipe(id);

                itemSlotButton = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Button>();
                itemSlotImage = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).GetComponent<Image>();
                itemSlotText = itemSlot.transform.GetChild(rowIndex).transform.GetChild(slotIndex).transform.Find("Text").GetComponent<Text>();
                        
                itemSlotButton.interactable = true;
                itemSlotImage.sprite = Resources.Load<Sprite>("Image/ItemIcon/ItemIcon_" + id);
                itemSlotText.text = amount.ToString();
                
                itemSlotButton.onClick.AddListener(delegate() { SelectCraftRecipe(id); });

                counter++;
                //Debug.Log("counter is " + counter);
                //Debug.Log("craftInfo.Count is " + craftInfo.Count);

                if(counter == craftInfoCount) break;
            }
            if(counter == craftInfoCount) break;
        }
    }

    public void RefreshInventoryUI()
    {
        Debug.Log ("How many kind items you have : " + items.Count);
        //인벤토리에 존재하는 아이템 종류 수
        int counter = 0;
        int rowIndex = 0;
        int slotIndex = 0;
        SetPageNumber();

        foreach (MyInventory item in items)
        {
            int id = item.id;
            int amount = item.amount;
            string itemName;

            // 인벤토리 첫번째 아이템을 선택해 좌측 정보를 갱신한다.
            if(slotIndex == 0 && rowIndex == 0)
            {
                SelectItem(id);
            }

            // 인벤토리 한 페이지에 아이템을 12개까지만 보여주므로 페이지 수에 따라서 표시
            // 1페이지면 0~11까지 2페이지면 12~23까지
            int pageIndex = (pageNumberInventory - 1) * 12;
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
        
        useBtn.interactable = true;
    }

    public void SelectCraftRecipe(int item)
    {
        Debug.Log("Recipe item id is " + item);
        
        itemDescPanelNameText.text = (string)itemInfo[item]["itemName"];
        itemDescPanelDescText.text = (string)itemInfo[item]["itemDesc"];
        itemDescPanelImage.sprite = Resources.Load<Sprite>("Image/ItemIcon/ItemIcon_" + item);
        
        int reqCredit = (int)craftInfo[item]["reqCredit"];
        int reqItem = (int)craftInfo[item]["reqItem1Count"];
        
        itemDescPanelCreditText.text = reqCredit.ToString();
        itemDescPanelCoreText.text = reqItem.ToString();
        itemDescPanelButtonText.text = "Craft";

        //if(Cond)
        //{
            useBtn.interactable = false;
        //}
    }

    public void InventoryPageUp()
    {
        if(isCraft)
        {
            float count = craftInfo.Count / 12f;
            Debug.Log(count);
            if(pageNumberCraft < count)
            {
                pageNumberCraft++;
                SetEmptyInventoryUI();
                RefreshCraftUI();
            }
            Debug.Log("pageNumberCraft is " + pageNumberCraft);
        }
        else
        {
            float count = items.Count / 12f;
            Debug.Log(count);
            if(pageNumberInventory < count)
            {
                pageNumberInventory++;
                SetEmptyInventoryUI();
                RefreshInventoryUI();
            }
            Debug.Log("pageNumberInventory is " + pageNumberInventory);
        }
    }

    public void InventoryPageDown()
    {
        if(isCraft)
        {
            if(pageNumberCraft >= 2)
            {
                pageNumberCraft--;
                SetEmptyInventoryUI();
                RefreshCraftUI();
            
            Debug.Log("pageNumberCraft is " + pageNumberCraft);}
        }
        else
        {
            if(pageNumberInventory >= 2)
            {
                pageNumberInventory--;
                SetEmptyInventoryUI();
                RefreshInventoryUI();
            
            Debug.Log("pageNumberInventory is " + pageNumberInventory);}
        }
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

    // 아이템 획득 및 제거 코드

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
}
