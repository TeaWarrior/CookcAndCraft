using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ConveyourControll : MonoBehaviour
{

    public Transform spawnPoint, endPoint;
    public float spawnTime = 1f;
    public GameObject itemPrefab;
    public float moveSpeed;
    bool isSpawned;
    GameManager questGenerator;
    // Start is called before the first frame update
    void Start()
    {
        questGenerator = GameManager.instance;
       
    }
    public void SpawnObject()
    {

        GameObject temp = Instantiate(itemPrefab, spawnPoint.transform.position, Quaternion.identity);
        temp.transform.parent = this.transform;

        int randomItem = Random.Range(0, questGenerator.items.Count);

        Item tempScriptable = questGenerator.items[randomItem];
            temp.GetComponentInChildren<Item_clickable>().item=tempScriptable;
        temp.transform.DOMove(endPoint.position, moveSpeed).OnComplete(() => Destroy(temp));
    }

  

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            StartCoroutine(SpawnCorotine());
        }
    }

    IEnumerator SpawnCorotine()
    {
        isSpawned = true;
        SpawnObject();
        yield return new WaitForSeconds(spawnTime);
        isSpawned = false;
    }
}
