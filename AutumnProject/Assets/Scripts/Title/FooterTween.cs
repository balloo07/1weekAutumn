using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class FooterTween : MonoBehaviour
{
    Tweener tweener;    //ここにDoTweenの情報を入れる


    // Start is called before the first frame update
    void Start()
    {
        tweener = this.transform.DOLocalRotate(new Vector3(0, 0, 10), 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

}
