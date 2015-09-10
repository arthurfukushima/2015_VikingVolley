using UnityEngine;
using System.Collections;
using CoreUI;
using UnityEngine.UI;
using Prime31.MessageKit;

/// <summary>
/// main menu panel
/// </summary>
public class UIMainMenu : BasePanel
{
    public Button[] m_pButtons;

    public Rigidbody2D m_pGAMBBall;
    Vector2 m_pGambBallPosition;

    


    protected override void Awake()
    {
        base.Awake();
        if (m_pGAMBBall)
        {
            Debug.Log("Brow, fiz uma gambi aqui so p simular o menu. flw");
            m_pGambBallPosition = m_pGAMBBall.position;
            m_pGAMBBall.Sleep();
        }

        MessageKit.addObserver(StateMessages.GP_OnGameOver, OnGameOver);
    }

    void OnGameOver()
    {
        if(!IsVisible())
            Show(null);
    }

    #region click callbacks

    public void OnClickPlay()
    {
        Close(null);
        if (m_pGAMBBall)
        {
            m_pGAMBBall.position = m_pGambBallPosition;
            // kkkkkk
            m_pGAMBBall.angularVelocity = 1350;
            m_pGAMBBall.WakeUp();
            m_pGAMBBall.velocity = new Vector2(0, 3);
            // best way to do it is listening the UI_MainMenu_OnClose event with MessagKit
            // then start all game stuffs
        }

        MessageKit.post(StateMessages.UI_MainMenu_OnPlayClick);

        UnityEngine.Analytics.Analytics.CustomEvent("PlayGame", null);
    }
    public void OnClickSettings()
    {
    }
    public void OnClickAchievements()
    {
    }
    public void OnClickLeaderboards()
    {
    }
    public void OnClickChangeScene()
    {
    }
    public void OnClickChangeBall()
    {
    }
    public void OnClickChangeSkin()
    {
    }
    #endregion


    #region UI overrides

    public override void Show(OnUICompleteEvent pOnShow = null)
    {
        base.Show(pOnShow);
        foreach (Button but in m_pButtons)
        {
            but.gameObject.SetActive(false);
        }

        if (m_pGAMBBall)
        {
            Debug.Log("Brow, fiz uma gambi aqui so p simular o menu. flw");
            m_pGAMBBall.Sleep();
        }

        if (UnityEngine.Advertisements.Advertisement.IsReady())
        {
            UnityEngine.Advertisements.Advertisement.Show();
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        int iIdx = 0;
        foreach (Button but in m_pButtons)
        {
            Vector3 vStartPos = but.rectTransform().anchoredPosition3D;
            Vector3 vFinishPos = but.rectTransform().anchoredPosition3D;
            vStartPos.y = -150;
            vStartPos.x = vStartPos.x + (Screen.width / 2);
            but.rectTransform().position = vStartPos;
            // tweening positions of buttons
            LeanTween.move(but.rectTransform(), vFinishPos, TweeningDefaultTime)
                .setEase(m_eOpenTweenType)
                .setDelay(iIdx * 0.01f);
            // tweening alpha of buttons
            LeanTween.alpha(but.rectTransform(), 1, TweeningDefaultTime *2)
                .setEase(m_eOpenTweenType)
                .setDelay(iIdx * 0.01f);
            but.gameObject.SetActive(true);
            iIdx++;
        }
    }

    public override void Close(OnUICompleteEvent pOnClose = null)
    {
        base.Close(pOnClose);

        int iIdx = 0;
        //Debug.Log(m_pButtons.Length);
        foreach (Button but in m_pButtons)
        {
            // tweening alpha of buttons
            LeanTween.alpha(but.rectTransform(), 0, TweeningDefaultTime)
                .setEase(m_eOpenTweenType)
                .setDelay(iIdx * 0.01f);
            iIdx++;
        }
    }

    public override void OnClose()
    {
        base.OnClose();
        // test in loop
        //Show(null);

        MessageKit.post(StateMessages.UI_MainMenu_OnClose);
    }

    #endregion


}