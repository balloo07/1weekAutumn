using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject popupMenu;
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void MenuActive()
    {
        audio.Play();
        Debug.Log("Menuが押されたよ");
        popupMenu.SetActive(true);
    }
    
    public void MenuHide()
    {
        audio.Play();
        popupMenu.SetActive(false);
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}
