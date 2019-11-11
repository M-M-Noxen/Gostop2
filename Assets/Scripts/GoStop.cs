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
    public List<string> Player1Cards;
    public List<string> Player2Cards;
    public List<string> FloorCards;
    public List<int> Player1CardsMonth;
    public List<int> Player2CardsMonth;
    public List<int> FloorCardsMonth;
    //Scene Variables

    public Stack<string> deck;
    public Stack<int> monthdeck;

    private void Awake()
    {
        userinput = FindObjectOfType<UserInput>();
        selectable = FindObjectOfType<Selectable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayCards();
        Check();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCards()
    {
        List<string> deck1 = GenerateDeck();
        Shuffle(deck1);
        deck = new Stack<string>(deck1);

        List<int> monthdeck1 = GenerateMonthDeck();
        Shuffle(monthdeck1);
        monthdeck = new Stack<int>(monthdeck1);

        GoStopSort();
        GoStopMonthSort();
        StartCoroutine(GoStopDeal());
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
    IEnumerator GoStopDeal()
    {
        float zOffset = 0.03f;

        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefabs, new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset), Quaternion.identity);
            newCard.name = card;

            zOffset = zOffset + 0.03f;
        }

        for (int i = 0; i < 28; i++)
        {
            if (i < 10)
            {
                yield return new WaitForSeconds(0.03f);
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(Player1Place[i].transform.position.x, Player1Place[i].transform.position.y, Player1Place[i].transform.position.z), Quaternion.identity, Player1Place[i].transform);
                newCard.name = Player1Cards[i];
                newCard.GetComponent<Selectable>().faceUp = true;
            }
            else if (i >= 10 && i < 20)
            {
                yield return new WaitForSeconds(0.03f);
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(Player2Place[i - 10].transform.position.x, Player2Place[i - 10].transform.position.y, Player2Place[i - 10].transform.position.z), Quaternion.identity, Player2Place[i - 10].transform);
                newCard.name = Player2Cards[i - 10];
                newCard.GetComponent<Selectable>().faceUp = true;
            }

            if (i >= 20 && i < 28)
            {
                yield return new WaitForSeconds(0.03f);
                GameObject newCard = Instantiate(cardPrefabs, new Vector3(FloorPlace[i - 20].transform.position.x, FloorPlace[i - 20].transform.position.y, FloorPlace[i - 20].transform.position.z), Quaternion.identity, FloorPlace[i - 20].transform);
                newCard.name = FloorCards[i - 20];
                newCard.GetComponent<Selectable>().faceUp = true;
            }
            //Debug.Log("i=" + i);
        }

    }

    void GoStopSort()
    {
        List<string> templist = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            Player1Cards.Add(deck.Pop());
        }
        for (int i = 0; i < 10; i++)
        {
            Player2Cards.Add(deck.Pop());
        }
        for (int i = 0; i < 8; i++)
        {
            FloorCards.Add(deck.Pop());
        }
        //FloorCards = new Stack<string>(templist);
    }

    void GoStopMonthSort()
    { 
        for (int i = 0; i < 28; i++)
        {
            if (i < 10)
            {
                Player1CardsMonth.Add(monthdeck.Pop());
            }
            if (i >= 10 && i < 20)
            {
                Player2CardsMonth.Add(monthdeck.Pop());
            }
            if (i >= 20)
            {
                FloorCardsMonth.Add(monthdeck.Pop());
            }
        }
    }

    void Check()
    {
        Stack<int> PopPop = new Stack<int>();
        for (int i = 0; i < 10; i++)
        {
            PopPop.Push(i);
        }

        List<int> Check = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            Check.Add(i);
        }

        if (PopPop.Peek() == Check[9])
        {
            print("Check is " + Check[9]);
            print("Will Pop in if delete the number?" + PopPop.Pop());
            print("Will Pop in print delete the number?" + PopPop.Pop());
        }
    }

    /*List Save
     *private List<string> MyCard0 = new List<string>();
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
    
    //Player1Cards = new List<string>[] { MyCard0, MyCard1, MyCard2, MyCard3, MyCard4, MyCard5, MyCard6, MyCard7, MyCard8, MyCard9 };
        Player2Cards = new List<string>[] { YourCard0, YourCard1, YourCard2, YourCard3, YourCard4, YourCard5, YourCard6, YourCard7, YourCard8, YourCard9 };
        FloorCards = new List<string>[] { FloorCard0, FloorCard1, FloorCard2, FloorCard3, FloorCard4, FloorCard5, FloorCard6, FloorCard7, FloorCard8, FloorCard9, FloorCard10, FloorCard11 };
        //Player1Cards = new string[] { "MyCard0", "MyCard1", "MyCard2", "MyCard3", "MyCard4", "MyCard5", "MyCard6", "MyCard7", "MyCard8", "MyCard9"};
        */

}
