using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoStop : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefabs;
    public GameObject[] Player1Place;
    public GameObject[] Player2Place;
    public GameObject[] OtherPlace;

    public static string[] month = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
    public static string[] value = new string[] { "a", "b", "c", "d", };
    public List<string>[] PlayerCards;
    public List<string>[] OtherCards;

    private List<string> MyCard0 = new List<string>();
    private List<string> MyCard1 = new List<string>();
    private List<string> MyCard2 = new List<string>();
    private List<string> MyCard3 = new List<string>();
    private List<string> MyCard4 = new List<string>();
    private List<string> MyCard5 = new List<string>();
    private List<string> MyCard6 = new List<string>();
    private List<string> MyCard7 = new List<string>();
    private List<string> MyCard8 = new List<string>();
    private List<string> MyCard9 = new List<string>();

    private List<string> YourCard0 = new List<string>();
    private List<string> YourCard1 = new List<string>();
    private List<string> YourCard2 = new List<string>();
    private List<string> YourCard3 = new List<string>();
    private List<string> YourCard4 = new List<string>();
    private List<string> YourCard5 = new List<string>();
    private List<string> YourCard6 = new List<string>();
    private List<string> YourCard7 = new List<string>();
    private List<string> YourCard8 = new List<string>();
    private List<string> YourCard9 = new List<string>();

    private List<string> OtherCard0 = new List<string>();
    private List<string> OtherCard1 = new List<string>();
    private List<string> OtherCard2 = new List<string>();
    private List<string> OtherCard3 = new List<string>();
    private List<string> OtherCard4 = new List<string>();
    private List<string> OtherCard5 = new List<string>();
    private List<string> OtherCard6 = new List<string>();
    private List<string> OtherCard7 = new List<string>();
    private List<string> OtherCard8 = new List<string>();
    private List<string> OtherCard9 = new List<string>();
    private List<string> OtherCard10 = new List<string>();
    private List<string> OtherCard11 = new List<string>();





    public List<string> deck;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCards = new List<string>[] { MyCard0, MyCard1, MyCard2, MyCard3, MyCard4, MyCard5, MyCard6, MyCard7, MyCard8, MyCard9, YourCard0, YourCard1, YourCard2, YourCard3, YourCard4, YourCard5, YourCard6, YourCard7, YourCard8, YourCard9};
        OtherCards = new List<string>[] { OtherCard0, OtherCard1, OtherCard2, OtherCard3, OtherCard4, OtherCard5, OtherCard6, OtherCard7, OtherCard8, OtherCard9, OtherCard10, OtherCard11 };

        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        foreach (string card in deck)
        {
            print(card);
        }
        GoStopSort();
        GoStopDeal();
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string m in month)
        {
            foreach (string v in value)
            {
                newDeck.Add(v + m);
            }
        }
        return newDeck;

    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            //random.Next(100) = 100보다 작은 무작위 숫자 
            n--;
            //k 가 정해졌으니 리스트 숫자 중 하나를 제외 
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
            //
            //Debug.Log("n="+n);
            //Debug.Log("k=" + k);

        }
        
    }

    void GoStopDeal()
    {
        float zOffset = 0.03f;

        foreach(string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefabs, new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset), Quaternion.identity);
            newCard.name = card;

            zOffset = zOffset + 0.03f;
        }

        for (int i = 0; i < 28; i++)
        {

            if (i < 10)
            {

                foreach (string card in PlayerCards[i])
                {
                    GameObject newCard = Instantiate(cardPrefabs, new Vector3(Player1Place[i].transform.position.x, Player1Place[i].transform.position.y, Player1Place[i].transform.position.z ), Quaternion.identity, Player1Place[i].transform);
                    newCard.name = card;
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

            }
            else if(i>=10 && i<20)
            {
                foreach (string card in PlayerCards[i])
                {
                    GameObject newCard = Instantiate(cardPrefabs, new Vector3(Player2Place[i-10].transform.position.x, Player2Place[i-10].transform.position.y, Player2Place[i-10].transform.position.z ), Quaternion.identity, Player2Place[i-10].transform);
                    newCard.name = card;
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
            }
        //Debug.Log("i=" + i);
        }
        for(int i=0; i<8; i++)
        {
            foreach (string card in OtherCards[i])
            {
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(OtherPlace[i].transform.position.x, OtherPlace[i].transform.position.y, OtherPlace[i].transform.position.z), Quaternion.identity, OtherPlace[i - 20].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().faceUp = true;
            }
        }
    }

    void GoStopSort()
    {
        for(int i =0; i<20; i++)
        {
                PlayerCards[i].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);

        }

        for(int i=0; i<8; i++)
        {
            OtherCards[i].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);
        }
    }
}
