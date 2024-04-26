using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 1. Doi vi tri
/// 2. 
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = new Sprite[100];
    [SerializeField] private List<Card> cards = new List<Card>();
    [SerializeField] private int totalCards;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform tfHolder;

    private List<int> shuffledIds = new List<int>();

    private Card card1 = null;
    private Card card2 = null;

    private int matchedPairs = 0; //Track the number of matched paris

    public ScoreController scoreController;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Icon");
        InItData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void InItData()
    //{

    //    for(int i=0; i < totalCards; i++)
    //    {
    //        var card = Instantiate(cardPrefab, tfHolder);
    //        int id = i / 2;
    //        card.InItData(id, sprites[id], OnClickCard, OnClickTest2); //
    //        cards.Add(card);
    //    }

    //}

    private void InItData()
    {
        // Create shuffled IDs for pairs
        for (int i = 0; i < totalCards; i++)
        {
            shuffledIds.Add(i);
            shuffledIds.Add(i);
        }

        //Shuffle card id before instatiate the cards
        ShuffleIds();

        // Instantiate cards with shuffled IDs
        for (int i = 0; i < totalCards; i++)
        {
            var card = Instantiate(cardPrefab, tfHolder);
            int id = shuffledIds[i];
            card.InItData(id, sprites[id], OnClickCard, OnClickTest2); //
            cards.Add(card);
        }
    }

    private void ShuffleIds()
    {
        // Fisher-Yates shuffle algorithm for IDs
        for (int i = 0; i < shuffledIds.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledIds.Count);
            int temp = shuffledIds[randomIndex];
            shuffledIds[randomIndex] = shuffledIds[i];
            shuffledIds[i] = temp;
        }
    }



    public void OnClickCard(Card card)
    {
        StopAllCoroutines();
        StartCoroutine(CheckMatching(card));
    }

    private IEnumerator CheckMatching(Card card) //them tu khoa start CoROUTINE
    {
        if (card1 == null)
        {
            card1 = card;
            card1.FlipUp();
            yield break;
        }
        if (card2 == null)
        {
            card2 = card;
            card2.FlipUp();

        }

        yield return new WaitForSeconds(.5f);

        if (card1.id == card2.id)
        {
            card1.HideCards();
            card2.HideCards();
            scoreController.IncrementScore(10);
            matchedPairs++; //Increment matched Pairs
            CheckGameOver();
        }
        else
        {
            card1.FlipDown();
            card2.FlipDown();
            scoreController.DecrementScore(5);
        }
            card1 = null;
            card2 = null;

    }

    private void OnClickTest2(int one)
    {
        Debug.Log("Test Action 2");
    }
    //Add logic to check if all pair are matched to end the game 
    private void CheckGameOver()
    {
        if (matchedPairs == totalCards / 2) // Check if all pairs are matched
        {
            // All cards are matched, trigger game over
            scoreController.GameOver();
        }
    }
}
