using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject girl;
    [SerializeField] public GameObject dog;
    [SerializeField] public GameObject ghost;

    [SerializeField] public TMP_Text canvas;


    void Start()
    {

    }


    void Update()
    {
        TextUpdate();
    }

    void TextUpdate()
    {
        canvas.text = girl.GetComponent<Player>().health.ToString();
    }

    public void OnRestart(InputValue inputValue)
    {
        float value = inputValue.Get<float>();

        if (value != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



}
