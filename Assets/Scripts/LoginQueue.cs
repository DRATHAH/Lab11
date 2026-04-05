using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LoginQueue : MonoBehaviour
{
    public string[] firstNames = new string[20];
    public char[] lastInitials =
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };
    Queue<string> loginQueue = new Queue<string>();
    [Range(4, 6)]
    public int queueSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < queueSize; i++)
        {
            loginQueue.Enqueue(GetRandomPlayerName());
        }

        List<string> debugList = (from name in loginQueue
                                  select name).ToList();
        string playerList = "Initial login queue created. There are " + debugList.Count + " players in the queue: " + debugList[0];
        for (int i = 1; i < debugList.Count; i++)
        {
            playerList += ", " + debugList[i];
        }
        Debug.Log(playerList);

        GetNamesTask();
        HonorCardGame();
    }

    string GetRandomPlayerName()
    {
        int rand = Random.Range(0, firstNames.Length);
        int lastRand = Random.Range(0, lastInitials.Length);
        return firstNames[rand] + " " + lastInitials[lastRand] + ".";
    }

    void GetNamesTask()
    {
        List<string> randomNames = new List<string>();
        string initialArray = "Created the name array: ";
        for (int i = 0; i < 15; i++)
        {
            randomNames.Add(firstNames[Random.Range(0, firstNames.Length)]);
            initialArray += randomNames[i] + ", ";
        }

        Debug.Log(initialArray.Trim(',', ' '));
        HashSet<string> seen = new HashSet<string>();
        HashSet<string> duplicates = new HashSet<string>();

        foreach(string name in randomNames)
        {
            if (seen.Add(name) == false)
            {
                duplicates.Add(name);
            }
        }
        
        string debugString = "The array has no duplicate names.";
        if (duplicates.Count > 0)
        {
            debugString = "The array has duplicate names: ";
            foreach (string duplicate in duplicates)
            {
                debugString += duplicate + ", ";
            }
        }
        
        Debug.Log(debugString.Trim(',', ' '));
    }

    void HonorCardGame()
    {
        string[] cardArray = new string[]
        {
            "J\u2660",
            "Q\u2660",
            "K\u2660",
            "A\u2660",
            "J\u2663",
            "Q\u2663",
            "K\u2663",
            "A\u2663",
            "J\u2665",
            "Q\u2665",
            "K\u2665",
            "A\u2665",
            "J\u2666",
            "Q\u2666",
            "K\u2666",
            "A\u2666",
        };
        var rng = new System.Random();
        cardArray = cardArray.OrderBy(x => rng.Next()).ToArray();
        
        Queue<string> cardQueue = new Queue<string>(cardArray);
        List<string> hand = new List<string>();
        while (hand.Count < 4)
        {
            DrawCard(hand, cardQueue);
        }
        Debug.Log("I made the initial deck and draw. My hand is: " + hand[0] + ", " + hand[1] + ", " + hand[2] + ", " + hand[3] + ".");

        bool won = false;

        while (won == false || cardQueue.Count == 0)
        {
            string debugMessage = "";
            debugMessage += DiscardCard(hand);
            debugMessage += DrawCard(hand, cardQueue);
            debugMessage += "My hand is: " + hand[0] + ", " + hand[1] + ", " + hand[2] + ", " + hand[3] + ". ";
            won = CheckWinningHand(hand);

            if (won == true)
            {
                debugMessage += "The game is WON";
            }
            else if (cardQueue.Count == 0)
            {

                Debug.Log("The deck is empty. The game is LOST.");
                break;
            }
            else
            {
                debugMessage += "This is not a winning hand. I will attempt to play another round.";
            }

            Debug.Log(debugMessage);
        }
    }

    string DrawCard(List<string> hand, Queue<string> cards)
    {
        string cardToDraw = cards.Dequeue();
        hand.Add(cardToDraw);
        return "and drew " + cardToDraw + ". ";

    }

    string DiscardCard(List<string> hand)
    {
        int rand = Random.Range(0, hand.Count);
        string cardToRemove = hand[rand];
        hand.Remove(cardToRemove);
        return "I discarded " + cardToRemove + " ";
    }

    bool CheckWinningHand(List<string> hand)
    {
        string[] suits = new string[]
        {
            "\u2660",
            "\u2663",
            "\u2665",
            "\u2666"
        };

        foreach (string suit in suits)
        {
            int matches = 0;
            foreach(string card in hand)
            {
                if (card.Contains(suit))
                {
                    matches++;
                }
            }

            if (matches >= 3)
            {
                return true;
            }
        }

        return false;
    }
}
