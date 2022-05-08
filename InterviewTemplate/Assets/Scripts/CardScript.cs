using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoPoker;


public class CardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hold;
    public GameObject HoldUI;
    private UIManager UIManager;
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void HoldButton() {
        if(!UIManager.gameReset) {
            hold = !hold;
            HoldUI.SetActive(hold);
        }
    }
    public void ChangeSprite(int suit, int num) {
        if(!hold) {
            Debug.Log("My suit = " + suit + " My value = " + num);
        }
    }
    public void ResetCard() {
        hold = false;
        HoldUI.SetActive(hold);
        ChangeSprite(0,0);
    }
}

