using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < 15; i++)
        {
            randomNames.Add(firstNames[Random.Range(0, firstNames.Length)]);
        }

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
            debugString.Substring(0, debugString.Length - 2);
        }
        Debug.Log(debugString);
    }
}
