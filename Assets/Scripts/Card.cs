using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{
    [SerializeField] private Button btnCard;
    [SerializeField] private Image imgIcon;
    [SerializeField] private CanvasGroup cgCard;
    //[SerializeField] private GameController gameController;  //phải nằm chung với Hierachy mới kéo vào được


    private bool isOpened;

    ScoreController scoreController;

    /// <summary>
    /// Gán hàm vào biến Action 
    /// </summary>
    private Action<Card> onClickCard;
    private Action onClickTest;
    private Action<int> onClickTest2;

    public int id;

    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        btnCard.onClick.AddListener(OnClickBtnCard);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        onClickTest = PlaySound;
    }

    // Update is called once per frame

    private void OnClickBtnCard()
    {
        //gameController.OnClickCard(this);
        this.onClickCard.Invoke(this);
        
        //OnClickTest();
        this.onClickTest.Invoke();

        this.onClickTest2.Invoke(1);
        
    }

    public void InItData(int id, Sprite Sprite, Action<Card> onClickCard, Action<int> onClickTest2)
    {
        this.id = id;
        this.onClickCard = onClickCard;
        imgIcon.sprite = Sprite;
        this.onClickTest2 = onClickTest2;
    }

    public void FlipDown()
    {
        imgIcon.gameObject.SetActive(false);
        wrongCard();
    }
    public void FlipUp()
    {
        imgIcon.gameObject.SetActive(true);
    }

    public void HideCards()
    {
        cgCard.alpha = 0f;
        CardDownSound();
        //this.gameObject.SetActive(false);
    }


    //Add SFX
    private void PlaySound()
    {
        audioManager.PlaySFX(audioManager.clickDown);
    }

    private void CardDownSound()
    {
        audioManager.PlaySFX(audioManager.cardDown);
    }

    private void wrongCard()
    {
        audioManager.PlaySFX(audioManager.wrongCard);
    }
}
