using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{

    Camera cam;

    public float floatingTime=1f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.instance.mainCamera;
        float y = transform.position.y;
        y = y + 0.75f;
        transform.DOMoveY(y, floatingTime).OnComplete
            (() => transform.DOShakePosition(0.75f, 0.1f, 1, 1f, false, false, ShakeRandomnessMode.Harmonic).OnComplete
            (() => transform.DOScale(0f,0.75f).OnComplete
            (() => Destroy(gameObject)
            )));
    }

    // Update is called once per frame
    void Update()
    {
        if (cam!=null)
        {
            transform.LookAt(cam.transform.position);
        }
      
    }

   
}
