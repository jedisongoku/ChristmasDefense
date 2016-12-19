using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour {

    public static MiniGameManager miniGameManager;

    public Text snowFlakeCountText;
    public Image[] gameBoardSprites;
    public Sprite specialHero;
    public Sprite giftBox;
    public Sprite boost250;
    public Sprite boost500;



    /// <summary>
    /// 0 : Boost 250
    /// 1 : Boost 500
    /// 2 : Gift Box
    /// 3 : Special Hero
    /// </summary>
    private int[] gameBoard = 
        { 0, 0, 0, 1, 2, 2, 2, 3, 3 };

    void Start()
    {
        miniGameManager = this;
    }
    void OnEnable()
    {
        MiniGameBlock.SetTheBoard();
        SetTheBoard();
    }

    public void SetTheBoard()
    {
        for (int i = 0; i < gameBoard.Length; i++)
        {
            int temp = gameBoard[i];
            int r = Random.Range(i, gameBoard.Length);
            gameBoard[i] = gameBoard[r];
            gameBoard[r] = temp;
        }
        Debug.Log("MINI GAME BOARD IS SET");
    }

    public void OpenBlock(int selection)
    {
        switch(gameBoard[selection])
        {
            case 0:
                gameBoardSprites[selection].sprite = boost250;
                Player.boostPoints += 250;
                break;
            case 1:
                gameBoardSprites[selection].sprite = boost500;
                Player.boostPoints += 500;
                break;
            case 2:
                gameBoardSprites[selection].sprite = giftBox;
                Player.life++;
                break;
            case 3:
                gameBoardSprites[selection].sprite = specialHero;
                Player.specialHero++;
                break;
        }
        
        snowFlakeCountText.text = Player.snowFlakes.ToString();
        GameHUDManager.gameHudManager.MenuHudUpdate();
        DataStore.Save();
            
    }
}
