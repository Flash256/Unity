using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whoseTurn; //0 = x and 1 = o
    public int turnCount; // counts and number of turn played
    public GameObject[] turnIcons; // displays whos turn it is
    public Sprite[] playerIcons; //0 = x icon and 1 = y icon
    public Button[] tictactoeSpaces; //playable space for our game
    public int[] markedSpaces; //ID's which space was marked by which player;
    public Text winnerText;// Holds the text componet of the winner text;
    public GameObject[] winningLine;// Holds all the different lines for show that there is a winner
    public GameObject winnerPanel;
    public int xPlayersScore;
    public int oPlayersScore;
    public Text xPlayersScoreText;
    public Text oPlayersScoreText;
    public Button xPlayersButton;
    public Button oPlayerButton;
    public GameObject catImage;
    public Button versusAI;
    public Button twoPlayer;
    public int gameType; // 0 = AI and 1 = Player

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }
    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameType == 0 && turnCount < 9 && winnerText.text == "")
        {
            while (whoseTurn != 0)
            {
                AITurn();
            }
        }
    }


    public void TicTacToeButton(int WhichNumber)
    {
        xPlayersButton.interactable = false;
        oPlayerButton.interactable = false;
        tictactoeSpaces[WhichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoseTurn + 1;
        turnCount++;
        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                Cat();
            }

        }
        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }
    bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoseTurn + 1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            xPlayersScore++;
            xPlayersScoreText.text = xPlayersScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if (whoseTurn == 1)
        {
            oPlayersScore++;
            oPlayersScoreText.text = oPlayersScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLine[indexIn].SetActive(true);


    }
    public void Rematch()
    {
        GameSetup();
        winnerText.text = "";
        for (int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        xPlayersButton.interactable = true;
        oPlayerButton.interactable = true;
        catImage.SetActive(false);
    }
    public void Restart()
    {
        Rematch();
        xPlayersScore = 0;
        oPlayersScore = 0;
        xPlayersScoreText.text = "0";
        oPlayersScoreText.text = "0";
    }

    public void SwitchPlayer(int whichPlayer)
    {
        if (whichPlayer == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whichPlayer == 1)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }
    void Cat()
    {
        winnerPanel.SetActive(true);
        catImage.SetActive(true);
        winnerText.text = "Draw";
    }

    public void PlayAgainstAI()
    {
        gameType = 0;
        winnerText.text = "";
    }

    public void PlayAgainstPlayer()
    {
        gameType = 1;
    }

    public void AITurn()
    {
        //System.Random randInt = new System.Random();
        //var aiSpace = randInt.Next(0, 9);
        var aiSpace = 0;
        if (markedSpaces[0] == -100)
        {
            aiSpace = 0;
        }
        else if (markedSpaces[1] == -100)
        {
            aiSpace = 1;
        }
        else if (markedSpaces[2] == -100)
        {
            aiSpace = 2;
        }
        else if (markedSpaces[3] == -100)
        {
            aiSpace = 3;
        }
        else if (markedSpaces[4] == -100)
        {
            aiSpace = 4;
        }
        else if (markedSpaces[5] == -100)
        {
            aiSpace = 5;
        }
        else if (markedSpaces[6] == -100)
        {
            aiSpace = 6;
        }
        else if (markedSpaces[7] == -100)
        {
            aiSpace = 7;
        }
        else if (markedSpaces[8] == -100)
        {
            aiSpace = 8;
        }

        TicTacToeButton(aiSpace);
    }
}
