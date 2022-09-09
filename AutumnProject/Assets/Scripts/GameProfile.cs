using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProfile : MonoBehaviour
{
    public static GameProfile _instance;
    public int _totalDropsCount = 0;
    public float _BGMvolume = 1f;
    public float _SEvolume = 1f;

    private void Awake()
    {
        CheckInstance();
    }
    
    private void CheckInstance()
    {
        //シングルトン
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
