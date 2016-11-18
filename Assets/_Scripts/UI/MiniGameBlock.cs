using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGameBlock : MonoBehaviour {

    public delegate void MiniGameAction();
    public static event MiniGameAction SetBlocks;

    public int id;
    public Image blockImage;
    public Sprite snowFlakeSprite;

    void Start()
    {
        SetBlocks += SetTheBlocks;
    }

    void SetTheBlocks()
    {
        blockImage.sprite = snowFlakeSprite;
        if(Player.snowFlakes > 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public static void SetTheBoard()
    {
        if(SetBlocks != null)
        {
            SetBlocks();
        }
        
    }

    public void Clicked()
    {
        if(Player.snowFlakes > 0)
        {
            GetComponent<Animator>().SetTrigger("Open");
            GetComponent<Button>().interactable = false;
        }  
    }

    public void OpenBlock()
    {
        MiniGameManager.miniGameManager.OpenBlock(id);
        //GetComponent<Animator>().SetBool("Open", false);
    }
}
