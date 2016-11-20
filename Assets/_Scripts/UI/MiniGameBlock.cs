using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGameBlock : MonoBehaviour {

    public delegate void MiniGameAction();
    public static event MiniGameAction SetBlocks;

    public int id;
    public Image blockImage;
    public Sprite snowFlakeSprite;

    private bool openBlock = false;

    void Start()
    {
        SetBlocks += SetTheBlocks;
        blockImage.sprite = snowFlakeSprite;
        //Player.boostPoints += 5000;
    }

    void SetTheBlocks()
    {
        blockImage.sprite = snowFlakeSprite;

        if (Player.snowFlakes > 0)
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
            Invoke("DelayOpenBlock", Random.Range(3, 5));
        }  
    }

    public void OpenBlock()
    {
        if(openBlock)
        {
            openBlock = false;
            MiniGameManager.miniGameManager.OpenBlock(id);
        }
        
        //GetComponent<Animator>().SetBool("Open", false);
    }

    void DelayOpenBlock()
    {
        //GetComponent<Animator>().SetBool("Open", false);
        GetComponent<Animator>().SetTrigger("End");
        openBlock = true;
    }
}
