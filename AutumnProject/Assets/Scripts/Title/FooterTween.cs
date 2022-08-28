using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ğg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ğ“ü‚ê‚é


public class FooterTween : MonoBehaviour
{
    Tweener tweener;    //‚±‚±‚ÉDoTween‚Ìî•ñ‚ğ“ü‚ê‚é


    // Start is called before the first frame update
    void Start()
    {
        tweener = this.transform.DOLocalRotate(new Vector3(0, 0, 10), 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

}
