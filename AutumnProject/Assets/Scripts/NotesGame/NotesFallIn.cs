using System.Collections;
using UnityEngine;

public class NotesFallIn : MonoBehaviour
{
    private GameProfile _gameProfile;
    private StageState _stageState;

    public int lineNum;
    private NotesGameController _gameController;
    private bool _isInLine = false;
    private KeyCode _lineKey;

    [SerializeField] private GameObject _itemGetEffect;

    void Start () {
        _gameProfile = GameObject.Find("GameProfile").GetComponent<GameProfile>();
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();

        // _gameController = GameObject.Find ("GameMNG").GetComponent<NotesGameController> ();
        _lineKey = GameUtil.GetKeyCodeByLineNum(lineNum);
    }

    void Update () {
        this.transform.position += Vector3.down * 10f * Time.deltaTime;
        this.transform.Rotate (0f, 0f, 180f*Time.deltaTime);

        //画面外に行けば消える
        if (this.transform.position.y < -5.0f) {
            Destroy(this.gameObject);
        }

        //判定個所に入っている
        if(_isInLine){
            CheckInput(_lineKey);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        _isInLine = true;
    }

    void OnTriggerExit2D (Collider2D other) {
        _isInLine = false;
    }

    void CheckInput (KeyCode key) {
        if (Input.GetKeyDown (key))
        {
            if (this.tag == "Candy")
            {
                _stageState._score++;
                _gameProfile._totalDropsCount ++;
            }
            else
            {
                //キャンディ以外なら原点
                _stageState._score--;
            }
            Instantiate(_itemGetEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}