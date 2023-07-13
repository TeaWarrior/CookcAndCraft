using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Camera mainCamera;
    public GameObject gamePlayCamera;
    public GameObject winCamera;
    public GameObject winCameraFinall;
   
    public PlayerControll playerControll;

    public static GameManager instance;
    public List<Item> items;
    public TextMeshProUGUI itemForQuestTEXT;
    public TextMeshProUGUI itemForQuestAMOUNT_TEXT;
    public GameObject levelPassed;
    public Item currentQuestItem;
    public int currentQuestItemAmount;
    public int itemAmount;

    public GameObject button_NextLevel;

    public GameObject conveyour;

    public Transform basketPlaceWin;

    #region Singleton



    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one QuestGeneratpr were found");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GenerateQuest();
        itemForQuestAMOUNT_TEXT.text = itemAmount.ToString();
      
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateQuest()
    {
        int randomItem = Random.Range(0, items.Count);

        int randomAmount = Random.Range(1, 6);
        Debug.Log(randomItem);
        currentQuestItemAmount = randomAmount;
     
        currentQuestItem = items[randomItem];
        itemForQuestTEXT.text ="Collect " + randomAmount.ToString()+" "+ currentQuestItem.itemName +"s";
        playerControll.itemName = currentQuestItem.itemName;
    }


    public void AddToBasket()
    {
        itemAmount++;
        itemForQuestAMOUNT_TEXT.text = itemAmount.ToString();
        itemForQuestAMOUNT_TEXT.gameObject.transform.DOShakeScale(1f, 10, 5);
        if (itemAmount == currentQuestItemAmount)
        {
            FinishGame();
        }
    }

    IEnumerator CameraMove()
    {
        yield return new WaitForSeconds(2f);
        winCamera.SetActive(false);

    }
    public void FinishGame()
    {
        button_NextLevel.SetActive(true);
        levelPassed.SetActive(true);
        conveyour.SetActive(false);
        playerControll.playerAnimation.SetBool("isDance", true);
        playerControll.basket.transform.parent = null;
        playerControll.basket.transform.position = basketPlaceWin.position;
        playerControll.enabled = false;
        gamePlayCamera.SetActive(false);
        StartCoroutine(CameraMove());
    }
    public void StartGame()
    {
        button_NextLevel.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
