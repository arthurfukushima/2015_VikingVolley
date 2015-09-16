using UnityEngine;
using System.Collections;
using CoreUI;
using UnityEngine.UI;
using Prime31.MessageKit;


public class UIHUD : BaseHUD
{
    [Header("Score")]
    public Text m_txtScore;
    private int m_iScore;
    private LTDescr m_pScorePunchDescr;

    [Header("Power bar")]
    public Slider m_slrPowerBar;
    public float m_fPowerFactor = 5f;
    public RectTransform m_rectColorable;
    private float m_fPowerValue;
    private bool m_bIsPowerBarOn = false;
    private LTDescr m_pValueToDescr;
    private float m_fLastTweenColor = 1;

    public override void OnShow()
    {
        base.OnShow();
        SetScore(0);
        SetPower(1);
    }


    #region Score

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
    #endregion


    #region Power

    public void StartPower()
    {
        m_fLastTweenColor = 1;
        m_bIsPowerBarOn = true;
        if(m_pValueToDescr != null)
            m_pValueToDescr.cancel();
        m_pValueToDescr = null;
    }
    public void StopPower()
    {
        m_bIsPowerBarOn = false;
    }
    public void SetPower(float fPower)
    {
        m_fPowerValue = fPower;
        m_slrPowerBar.value = m_fPowerValue;
        //Debug.Log(fPower);
    }
    public void FillPower()
    {
        StopPower();
        if (m_pValueToDescr != null)
            m_pValueToDescr.cancel();
        m_pValueToDescr = LeanTween.value(GameObject, SetPower, m_fPowerValue, m_slrPowerBar.maxValue, TweeningDefaultTime / m_slrPowerBar.maxValue * (m_slrPowerBar.maxValue - m_fPowerValue));
        //m_pValueToDescr. onUpdateFloat = SetPower;
    }

    private void OnFinishPower()
    {
        StopPower();
        MessageKit.post(StateMessages.GP_OnPowerOver);
    }

    private void OnReachAlmost()
    {
        LeanTween.color(m_rectColorable, Color.yellow, TweeningDefaultTime * 2).setEase(LeanTweenType.punch);
    }
    private void OnReachAlmostAlmost()
    {
        LeanTween.color(m_rectColorable, Color.red, TweeningDefaultTime * 2).setEase(LeanTweenType.punch);
    }

    private void UpdatePower()
    {
        if (m_bIsPowerBarOn)
        {
            m_fPowerValue -= (Time.deltaTime / m_fPowerFactor);
            SetPower(m_fPowerValue);
            if(m_fPowerValue < 0.4f && m_fLastTweenColor >= 0.4f)
            {
                m_fLastTweenColor = m_fPowerValue;
                OnReachAlmost();
            }
            if (m_fPowerValue < 0.15f && m_fLastTweenColor >= 0.15f)
            {
                m_fLastTweenColor = m_fPowerValue;
                // missing creative names
                OnReachAlmostAlmost();
            }
            if (m_fPowerValue <= 0)
            {
                OnFinishPower();
            }
        }
    }

    #endregion


    void Update()
    {
        UpdatePower();
    }

    
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 300, 100), "add"))
        {
            //SetScore(Random.Range(0, 100));
            AddScore();
        }
        if (GUI.Button(new Rect(0, 100, 300, 100), "Start Power"))
        {
            //SetScore(Random.Range(0, 100));
            StartPower();
        }
        if (GUI.Button(new Rect(0, 200, 300, 100), "Stop Power"))
        {
            //SetScore(Random.Range(0, 100));
            StopPower();
        }
        if (GUI.Button(new Rect(0, 300, 300, 100), "Fill Power"))
        {
            //SetScore(Random.Range(0, 100));
            FillPower();
        }
    }
}