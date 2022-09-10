using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    private GameProfile _gameProfile;
    
    [SerializeField] private GameObject[] _candy;
    
    private void Start()
    {
        _gameProfile = GameObject.Find ("GameProfile").GetComponent<GameProfile>();
        var _totalDropsCount = _gameProfile._totalDropsCount;
        if (_totalDropsCount > 500)
        {
            _totalDropsCount = 500;
        }
        
        for (int i = 0; i < _totalDropsCount; i++)
        {
            var x = Random.Range(-8f,8f);
            var y = Random.Range(6f, 15f);
            var vecZ = Random.Range(-180f,180f);
            var pos = new Vector3(x, y, 0);
            Instantiate(_candy[i%_candy.Length], pos, Quaternion.identity).transform.Rotate(0,0,vecZ);
        }
    }
    
}
