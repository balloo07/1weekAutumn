using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class BlinkObject : MonoBehaviour
{
    Tweener tweener;
    public float time = 5f;
    public bool destory = false;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<CanvasGroup>().DOFade(endValue: 0f, duration: 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        if (destory)
        {
            Destroy(this.gameObject.transform.parent.gameObject, time);
        }
    }
}