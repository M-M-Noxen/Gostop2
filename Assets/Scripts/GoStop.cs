using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoStop : MonoBehaviour
{
    private UserInput userinput;
    private Selectable selectable;

    public Sprite[] cardFaces;
    public GameObject cardPrefabs;
    public GameObject[] Player1Place;
    public GameObject[] Player2Place;
    public GameObject[] FloorPlace;
    //Scene Assets

    public static string[] Month = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
    public static string[] Value = new string[] { "a", "b", "c", "d", };
    public static string[] Blank = new string[] { "", "", "", "" };
    public List<string>[] Player1Cards;
    public List<string>[] Player2Cards;
    public List<string>[] FloorCards;
    public List<int> Player1CardsMonth;
    public List<int> Player2CardsMonth;
    public List<int> FloorCardsMonth;

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

    private List<string> FloorCard0 = new List<string>();
    private List<string> FloorCard1 = new List<string>();
    private List<string> FloorCard2 = new List<string>();
    private List<string> FloorCard3 = new List<string>();
    private List<string> FloorCard4 = new List<string>();
    private List<string> FloorCard5 = new List<string>();
    private List<string> FloorCard6 = new List<string>();
    private List<string> FloorCard7 = new List<string>();
    private List<string> FloorCard8 = new List<string>();
    private List<string> FloorCard9 = new List<string>();
    private List<string> FloorCard10 = new List<string>();
    private List<string> FloorCard11 = new List<string>();
    //Objects for generating deck

    public List<string> deck;
    public List<int> monthdeck;

    private void Awake()
    {
        userinput = FindObjectOfType<UserInput>();
        selectable = FindObjectOfType<Selectable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player1Cards = new List<string>[] { MyCard0, MyCard1, MyCard2, MyCard3, MyCard4, MyCard5, MyCard6, MyCard7, MyCard8, MyCard9};
        Player2Cards = new List<string>[] { YourCard0, YourCard1, YourCard2, YourCard3, YourCard4, YourCard5, YourCard6, YourCard7, YourCard8, YourCard9 };
        FloorCards = new List<string>[] { FloorCard0, FloorCard1, FloorCard2, FloorCard3, FloorCard4, FloorCard5, FloorCard6, FloorCard7, FloorCard8, FloorCard9, FloorCard10, FloorCard11 };
        //PlayerCards = new List<string>[] { "MyCard0", "MyCard1", "MyCard2", "MyCard3", "MyCard4", "MyCard5", "MyCard6", "MyCard7", "MyCard8", "MyCard9", "YourCard0", "YourCard1", "YourCard2", "YourCard3", "YourCard4", "YourCard5", "YourCard6", "YourCard7", "YourCard8", "YourCard9"};
        //->foreach 구문과 List<string>[] 의 관계, 왜 List 와 Array 로는 안되는지 생각해보자

        PlayCards();
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        monthdeck = GenerateMonthDeck();
        Shuffle(monthdeck);
        /*foreach (string card in deck)
        {
            print(card);
        }
        foreach (string card2 in monthdeck)
        {
            print(card2);
        }*/
        GoStopSort();
        GoStopMonthSort();
        GoStopDeal();
    }

    public static List<string> GenerateDeck()
    {

        List<string> newDeck = new List<string>();
        foreach (string m in Month)
        {
            foreach (string v in Value)
            {
                newDeck.Add(v + m);
            }
        }
        return newDeck;
    }

    public static List<int> GenerateMonthDeck()
    {
        List<string> newMonthDeck = new List<string>();
        List<int> TrueMonthDeck = new List<int>();
        foreach (string m in Month)
        {
            foreach (string b in Blank)
            {
                newMonthDeck.Add(b + m);
            }
        }
        foreach (string s in newMonthDeck)
        {
            int Month_int = int.Parse(s);
            TrueMonthDeck.Add(Month_int);
        }
        return TrueMonthDeck;

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

        for (int i = 0; i < 20; i++)
        {

            if (i < 10)
            {

                foreach (string card in Player1Cards[i])
                {
                    GameObject newCard = Instantiate(cardPrefabs, new Vector3(Player1Place[i].transform.position.x, Player1Place[i].transform.position.y, Player1Place[i].transform.position.z ), Quaternion.identity, Player1Place[i].transform);
                    newCard.name = card;
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

            }
            else if(i>=10 && i<20)
            {
                foreach (string card in Player2Cards[i-10])
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
            foreach (string card in FloorCards[i])
            {
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(FloorPlace[i].transform.position.x, FloorPlace[i].transform.position.y, FloorPlace[i].transform.position.z), Quaternion.identity, FloorPlace[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().faceUp = true;               
            }
        }

    }

    void GoStopSort()
    {
        for (int i = 0; i < 10; i++)
        {
            Player1Cards[i].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);

        }
        for (int i = 0; i < 10; i++)
        {
            Player2Cards[i].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);

        }

        for (int i = 0; i < 8; i++)
        {
            FloorCards[i].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);

        }
    }

    void GoStopMonthSort()
    {

        for (int i = 0; i < 10; i++)
        {            
            Player1CardsMonth.Add(monthdeck.Last<int>());
            monthdeck.RemoveAt(monthdeck.Count - 1);
        }
        for (int i = 0; i < 10; i++)
        {
            Player2CardsMonth.Add(monthdeck.Last<int>());
            monthdeck.RemoveAt(monthdeck.Count - 1);
        }

        for (int i = 0; i < 8; i++)
        {
            FloorCardsMonth.Add(monthdeck.Last<int>());
            monthdeck.RemoveAt(monthdeck.Count - 1);
        }
    }

    void Check()
    {
        foreach (int m in FloorCardsMonth)
        {
            print("Check: " + m);
        }
    }

}
