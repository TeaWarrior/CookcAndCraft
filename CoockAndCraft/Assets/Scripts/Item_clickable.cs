using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class Item_clickable : MonoBehaviour
{
    public Item item;
    public Transform itemGraphyx;

    private void Start()
    {

        CreateItem();

    }

    void CreateItem()
    {
        GameObject currentItem = Instantiate(item.itemPrefab, itemGraphyx.transform.position, Quaternion.identity);
        currentItem.transform.parent = itemGraphyx.transform;
        currentItem.transform.localScale = Vector3.one;
    }

}
