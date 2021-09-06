using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour {
    //Serializable classes implements
    public bool isTest;
    public bool skipDialog;

    private GameObject UI;
    private GameObject backgroundImage;
    private Boolean clicked = false;

    public int storyButtonID;

/*
    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("DialogueUI");
        UI.SetActive(false);
        if (!isTest) StartCoroutine( StartLevel() );
        Debug.Log("Started!");
    }
    public IEnumerator StartLevel()
    {
        int i = 1;
        while(true)
        {
            if (i == 999) break;
            bool isFinished = false;
            StartCoroutine(StringParser(i, ExcelParser.GetResource("level", i), ExcelParser.GetResource("dialog", i), (bool val, int nextStage)=> {
                isFinished = val; i = nextStage;
                Debug.Log("isFin : " + isFinished);
                Debug.Log("nextStage : " + i);
            }));


            yield return new WaitUntil(() => {
                return isFinished;
             });
        }

    }
*/

//여기부터 (id 로드해서 스토리 부를 수 있도록 개조함)
    private void Awake()
    {
        backgroundImage = GameObject.FindGameObjectWithTag("StoryBg");
        backgroundImage.SetActive(false);
        UI = GameObject.FindGameObjectWithTag("DialogueUI");
        UI.SetActive(false);
    }
    private void Start()
    {
    }
    public void DoStory(int storyID)
    {
        //UI = GameObject.FindGameObjectWithTag("DialogueUI");
        //UI.SetActive(false);
        if (!isTest) StartCoroutine( StartLevel(storyID) );
        Debug.Log("Started!");
    }

    public IEnumerator StartLevel(int storyID)
    {
        while(true)
        {
            if (storyID == 999) break;
            bool isFinished = false;
            StartCoroutine(StringParser(storyID, ExcelParser.GetResource("level", storyID), ExcelParser.GetResource("dialog", storyID), (bool val, int nextStage)=> {
                isFinished = val; storyID = nextStage;
                Debug.Log("isFin : " + isFinished);
                Debug.Log("nextStage : " + storyID);
            }));

            yield return new WaitUntil(() => {
                return isFinished;
             });
        }
    }


    WaitForSeconds _delayBetweenCharactersYieldInstruction;

    public void StartTypeWriterOnText(Text textComponent, string stringToDisplay, float delayBetweenCharacters = 0.02f)
    {
        StartCoroutine(TypeWriterCoroutine(textComponent, stringToDisplay, delayBetweenCharacters));
    }
    
    IEnumerator TypeWriterCoroutine(Text textComponent, string stringToDisplay, float delayBetweenCharacters)
    {
        // Cache the yield instruction for GC optimization
        _delayBetweenCharactersYieldInstruction = new WaitForSeconds(delayBetweenCharacters);

        
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        AudioClip clip = (AudioClip) Resources.Load("Sound/Menu_Navigate_02");
        //AudioClip clip = (AudioClip) Resources.Load("Sound/Talk");
        //AudioClip clip = (AudioClip) Resources.Load("Sound/SE/FX1");

        // Iterating(looping) through the string's characters
        for(int i = 0; i < stringToDisplay.Length + 1; i++)
        {
            // Retrieves part of the text from string[0] to string[i]
            textComponent.text = stringToDisplay.Substring(0, i);
            // We wait x seconds between characters before displaying them
            if (i > 0 && i % 3 == 0)
            {
                if( clip != null)
                {
                    audio.PlayOneShot(clip);
                }
            }
            yield return _delayBetweenCharactersYieldInstruction;
        }
    }
//여기까지

    IEnumerator StringParser(int currentInt,string levelStr, string dialogs, System.Action<bool, int> callback) {

        char[] splitter = { '#' };
        string[] levelRows = levelStr.Split(splitter);
        string[] dialogRows = dialogs.Split(splitter);
        int nextStage = 999;
        IEnumerable<string> dialogRowsEnum = dialogRows.Cast<string>();
        IEnumerable<string[]> dialogRowArrays = dialogRowsEnum.Select(row => row.Split(','));
        string[][] dialogRowsDual = dialogRowArrays.ToArray<string[]>();
        Dictionary<string, int> dialogBook = new Dictionary<string, int>();
        for (int i = 0; i < dialogRowsDual.Length; i++)
        {
            string[] row = dialogRowsDual[i];
            if (row.Length == 1) continue;
            if (!"".Equals(row[0]))
            {
                dialogBook.Add(row[0], i);
            }
            else if ("event".Equals(row[1]) && "route".Equals(row[2]))
            {
                dialogBook.Add(row[3], i);
            }
            else if (row[1].Contains("select") && "end".Equals(row[2]))
            {
                dialogBook.Add(row[1], i);
            }
        }


        IEnumerable<string> levelRowsEnum = levelRows.Cast<string>();
        IEnumerable<string[]> levelRowArrays = levelRowsEnum.Select(row => row.Split(','));
        string[][] levelRowsDual = levelRowArrays.ToArray<string[]>();
        //levelRows
        for (int j = levelRowsDual.Length - 1; j > -1; j--)
        {
            string[] fd = levelRowsDual[j];
            if (fd[0].Equals("")) fd[0] = "1";
            float duration = float.Parse(fd[0]);
            //yield return new WaitForSeconds(duration + 2);
            yield return new WaitForSeconds(duration);
            for (int i = fd.Length - 1; i > 0; i -= 2) {
                if (fd[i - 1].Equals("")) continue;
                if (fd[1].Contains("load"))
                {
                    if (skipDialog) continue;
                    string[] loadRow = fd[1].Split('=');
                    string dialogId = loadRow[1];
                    Debug.Log(dialogId);
                    int index = dialogBook[dialogId];
                    while (true)
                    {
                        string[] row = dialogRowsDual[index];
                        Debug.Log(row[1]);
                        if ("event".Equals(row[1]))
                        {
                            if (row[2].Contains("end"))
                            {
                                //대화끝
                                UI.SetActive(false);
                                DataController.Instance.gameData.storyProgress[currentInt] = 1;
                                StoryController.DoStorySet();
                                break;
                            }
                            else if (row[2].Contains("show"))
                            {
                                //표시하기
                                string[] showParam = row[2].Split(' ');
                                string fileName = showParam[1];
                                string position = row[3];

                                //getFile
                                // Debug.Log("Position : " + position);
                                //Resources.Load<Sprite>("Assets/Resources/" + fileName);
                                //setFileToPositoin
                                Transform leftTf = UI.transform.Find("Portrait").Find("Left");
                                Transform rightTf = UI.transform.Find("Portrait").Find("Right");
                                Image left = leftTf.GetComponent<Image>();
                                Image right = rightTf.GetComponent<Image>();
                                if ("left".Equals(position))
                                {
                                    leftTf.SetAsFirstSibling();
                                    left.sprite = Resources.Load<Sprite>("Image/" + fileName) as Sprite;
                                    left.color = new Color(255f, 255f, 255, 1f);
                                    if (right.sprite != null)
                                        right.color = new Color(100f, 100f, 100, 0.7f);
                                }
                                else
                                {
                                    rightTf.SetAsFirstSibling();
                                    right.sprite = Resources.Load<Sprite>("Image/" + fileName) as Sprite;
                                    right.color = new Color(255f, 255f, 255, 1f);
                                    if (left.sprite != null)
                                        left.color = new Color(100f, 100f, 100, 0.7f);

                                }
                            }
                            else if (row[2].Contains("play_se"))
                            {
                                //음악플레이
                                string fileName = row[3];
                                AudioSource audio = gameObject.AddComponent<AudioSource>();
                                AudioClip clip = (AudioClip) Resources.Load("sound/" + fileName);
                                if( clip != null)
                                {
                                    audio.PlayOneShot(clip);
                                }
                            }
                            else if (row[2].Contains("move"))
                            {
                                //해당 루트로 이동
                                index = dialogBook[row[3]];
                                continue;
                            }
                            else if (row[2].Contains("route"))
                            {
                                //다음행으로 갈것
                                //암것도 안해도 됨
                            }
                            else if (row[2].Contains("nextStage"))
                            {
                                string nextStageStr = row[3];
                                nextStage = int.Parse(nextStageStr);
                            }
                            else if (row[2].Contains("set_bg"))
                            {
                                if(backgroundImage.activeInHierarchy == false)
                                {
                                    backgroundImage.SetActive(true);
                                }                        
        
                                string fileName = row[3];
                                Transform bg = UI.transform.Find("BackgroundImage");
                                Image bg_image = bg.GetComponent<Image>();
                                bg_image.sprite = Resources.Load<Sprite>("Image/" + fileName) as Sprite;
                            }
                        }
                        else if ("l".Equals(row[1]))
                        {
                            //대사표시
                            UI.SetActive(true);
                            UI.transform.Find("Name").Find("Text").GetComponent<Text>().text = row[2];
                            

                            //UI.transform.Find("MainDialogue").Find("Text").GetComponent<Text>().text = row[3].Replace('$', ',').Replace(';', '\n');
                            //텍스트를 그냥 넣어주던 것을 StartTypeWriterOnText 함수로 넣어주도록 수정

                            Text temp = UI.transform.Find("MainDialogue").Find("Text").GetComponent<Text>();
                            string tempText = row[3].Replace('$', ',').Replace(';', '\n');

                            StartTypeWriterOnText(temp, tempText);

                            yield return new WaitForSeconds(1);
                            yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject());
                        }
                        else if (row[1].Contains("select"))
                        {
                            if (!"end".Equals(row[2]))
                            {
                                GameObject buttons = UI.transform.Find("Buttons").gameObject;
                                GameObject button1 = buttons.transform.Find("Button1").gameObject;
                                GameObject button2 = buttons.transform.Find("Button2").gameObject;
                                GameObject button3 = buttons.transform.Find("Button3").gameObject;


                                GameObject[] btnArr = new GameObject[] { button1, button2, button3 };

                                button1.SetActive(false);
                                button2.SetActive(false);
                                button3.SetActive(false);
                                buttons.SetActive(true);

                                int endIndex = dialogBook[row[1]];
                                for (int h = index; h <= endIndex; h++)
                                {
                                    string[] searching = dialogRowsDual[h];
                                    if (row[1].Equals(searching[1]) && searching[2].Contains("choice"))
                                    {
                                        int btnIndex = Int32.Parse("" + searching[2][searching[2].Length - 1]);

                                        int nextLine = h;
                                        btnArr[btnIndex - 1].transform.Find("Text").GetComponent<Text>().text = searching[3];
                                        btnArr[btnIndex - 1].GetComponent<Button>().onClick.AddListener(() =>
                                        {
                                            clicked = true;
                                            index = nextLine;
                                        });
                                        btnArr[btnIndex - 1].SetActive(true);
                                    }
                                }

                                yield return new WaitForSeconds(1);
                                yield return new WaitUntil(() =>
                                {
                                    if (clicked)
                                    {
                                        foreach (GameObject go in btnArr)
                                            go.GetComponent<Button>().onClick.RemoveAllListeners();
                                        clicked = false;
                                        return true;
                                    }
                                    else
                                        return false;
                                });
                                buttons.SetActive(false);
                            }
                        }
                        else
                        {
                            yield return new WaitForSeconds(1);
                        }
                        index++;
                    }
                }
                Debug.Log(levelRows[j]);
            }
        }
        callback(true, nextStage);
    }

}    
