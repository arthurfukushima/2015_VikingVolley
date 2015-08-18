using UnityEngine;
using System.Collections;
using CoreUI;
using UnityEngine.UI;

public class UIHUD : BaseHUD
{

    public Text m_txtScore;
    private int m_iScore;
    private LTDescr m_pScorePunchDescr;


    public override void OnShow()
    {
        base.OnShow();
        SetScore(0);
    }

    public void AddScore()
    {
        SetScore(++m_iScore);
    }

    public void SetScore(int iScore)
    {
        m_txtScore.text = iScore.ToString();
        if (m_pScorePunchDescr != null)
        {
            m_pScorePunchDescr.cancel();
        }
        m_pScorePunchDescr = LeanTween.moveLocalY(m_txtScore.gameObject, 7, 0.3f).setEase(LeanTweenType.punch);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 300, 100), "add"))
        {
            //SetScore(Random.Range(0, 100));
            AddScore();
        }
    }
}