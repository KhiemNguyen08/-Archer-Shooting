using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    public Text bestScoreText;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText)
            bestScoreText.text = Prefs.BESTSCORE.ToString();
        AudioController.Ins.PlaySound(AudioController.Ins.gameover);

    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnApplicationQuit()
    {
        
    }
}
