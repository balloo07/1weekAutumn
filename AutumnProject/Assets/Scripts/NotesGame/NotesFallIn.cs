using System.Collections;
using UnityEngine;

public class NotesFallIn : MonoBehaviour {

    public int lineNum;
    private NotesGameController _gameController;
    private bool isInLine = false;
    private KeyCode _lineKey;

    void Start () {
        _gameController = GameObject.Find ("GameMNG").GetComponent<NotesGameController> ();
        _lineKey = GameUtil.GetKeyCodeByLineNum(lineNum);
    }

    void Update () {
        this.transform.position += Vector3.down * 10 * Time.deltaTime;
        this.transform.Rotate (0f, 0f, 180f*Time.deltaTime);

        if (this.transform.position.y < -5.0f) {
            Destroy(this.gameObject);
        }

        //判定個所に入っている
        if(isInLine){
            CheckInput(_lineKey);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        isInLine = true;
    }

    void OnTriggerExit2D (Collider2D other) {
        isInLine = false;
    }

    void CheckInput (KeyCode key) {

        if (Input.GetKeyDown (key)) {
            _gameController.GoodTimingFunc (lineNum);
            Destroy(this.gameObject);
        }
    }
}