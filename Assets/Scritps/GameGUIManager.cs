using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Text appleCountingText;
    public GameoverDialog gameoverDialog;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGameGUI(bool isshow)
    {
        if (gameGUI)
            gameGUI.SetActive(isshow);
        if (homeGUI)
            homeGUI.SetActive(!isshow);
    }
    public void UPdateApple(int apple)
    {
        if (appleCountingText)
            appleCountingText.text = apple.ToString();
    }
}
