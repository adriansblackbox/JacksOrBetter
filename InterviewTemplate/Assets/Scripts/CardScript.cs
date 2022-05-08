using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VideoPoker;


public class CardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hold;
    public GameObject HoldUI;
    public UnityEngine.UI.Image CardImage;
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
    public void ChangeSprite(int suit, int value) {
        if(!hold) {
            string suitString = suit.ToString();
            string valueString = value.ToString();
            Debug.Log("Art/Cards/" + suitString + valueString);
            CardImage.sprite = Resources.Load<Sprite>("Art/Cards/" + suitString + valueString);
        }
    }
    public void ResetCard() {
        hold = false;
        HoldUI.SetActive(hold);
        ChangeSprite(0,0);
    }
}

