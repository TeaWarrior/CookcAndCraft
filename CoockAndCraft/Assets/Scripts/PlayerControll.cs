using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerControll : MonoBehaviour
{

    public GameObject textEFX;
    public string itemName;
    public GameObject iKTarget;
    public Rig rig;
    bool isRig;
    bool isAnimation;
    public Animator playerAnimation;
    public float pickingTime;
    public Transform rightHand;
    public GameObject basket;
    public int itemsInBasket;
    public List<Transform> basketPlaces;
    
    float timeElapsed;
    float lerpDuration = 1;

    public GameObject tempItem;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!isRig)
            {
               
                PickItem();
            }
      
          
        }

        if (isRig)
        {
            rig.weight = Mathf.Lerp(0f, 1f, timeElapsed / lerpDuration);
            iKTarget.transform.position = tempItem.transform.position;
            timeElapsed += Time.deltaTime;
        }
        if (isAnimation)
        {
            rig.weight = Mathf.Lerp(1f, 0f, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        
    }

    private void Start()
    {
        itemsInBasket = 0;
    }
    IEnumerator StartAnimation(GameObject item)
    {
       
        yield return new WaitForSeconds(pickingTime);
        iKTarget.transform.position = item.transform.position;
        isRig = false;
        isAnimation = true;
        timeElapsed = 0f;
          item.gameObject.transform.parent = rightHand.transform;
          item.transform.position = rightHand.transform.position;
        yield return new WaitForSeconds(pickingTime);
        item.transform.parent = basketPlaces[itemsInBasket].transform;
        item.transform.position = basketPlaces[itemsInBasket].position;
        itemsInBasket++;
        Instantiate(textEFX, basket.transform.position, Quaternion.identity);
        isAnimation = false;
        
        timeElapsed = 0f;
      //  yield return new WaitForSeconds(pickingTime);
      
       // isAnimation = false;
        playerAnimation.SetBool("isPick", false);
        GameManager.instance.AddToBasket();


    }

    public void PickItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.CompareTag("Food"))
            {
                if (basketPlaces.Count == itemsInBasket)
                {
                    Debug.Log("no MOre Space");
                    return;
                }
                Item_clickable item = hit.transform.gameObject.GetComponent<Item_clickable>();
                if (item.item.itemName == itemName)
                {
                  
                   
                 
                    iKTarget.transform.position = item.transform.position;
                    playerAnimation.SetBool("isPick", true);

                    isRig = true;
                    item.GetComponent<Collider>().enabled = false;
                    item.GetComponent<Rigidbody>().useGravity = false;
                    tempItem = item.gameObject;

                    timeElapsed = 0f;
                  //  isRig = true;
                    StartCoroutine(StartAnimation(item.gameObject));

                   
                }

            }

        }
    }

  


}

