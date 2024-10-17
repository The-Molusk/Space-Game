using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTracker : MonoBehaviour
{
    GameObject currentPlayer;
    public GameObject startingPlayer;

    private void Start()
    {
        currentPlayer = startingPlayer;
    }

    // Sets the current player to the specified character
    public void setPlayer(GameObject character)
    {
        currentPlayer = character;
    }

    // Returns the current player
    public GameObject getPlayer()
    {
        return currentPlayer;
    }
}
