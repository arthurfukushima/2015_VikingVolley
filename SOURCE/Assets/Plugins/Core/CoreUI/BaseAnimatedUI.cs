///////////////////////////////////////////////////////////
//  BaseAnimatedUI.cs
//  Implementation of the Class BaseAnimatedUI
//  Generated by Enterprise Architect
//  Created on:      19-dez-2014 14:59:44
//  Original author: Hugo
///////////////////////////////////////////////////////////



using UnityEngine;
using CoreUI;
namespace CoreUI
{
    /// <summary>
    /// A animated UI extended from BaseUI
    /// </summary>
    public class BaseAnimatedUI : BaseUI
    {
        // open name
        private const string m_sOpenTransitionName = "Open";
        private const string m_sOpenStateName = "Open";
        private const string m_sClosedStateName = "Closed";

        [Header("Animator Animation Based (if null will use Tween)")]
        [SerializeField]
        private Animator m_FlowAnimator;

        [Header("Tween Animation Based")]
        [SerializeField]
        private bool m_bUseTweenAnimation = true;
        [SerializeField]
        protected LeanTweenType m_eOpenTweenType = LeanTweenType.easeOutBounce;
        [SerializeField]
        protected LeanTweenType m_eCloseTweenType = LeanTweenType.easeInBounce;

        // animation parameter hash id
        private int m_OpenParameterId, m_OpenStateId, m_ClosedStateId;
        private bool m_bIsAnimating = false;
        private bool m_bIsShowing = false;
        private bool m_bIsClosing = false;
        private bool m_bSkipFrame = false;

        public const float TweeningDefaultTime = 0.6f;


        #region Accessors

        public Animator UIAnimator
        {
            get { return m_FlowAnimator; }
            set { m_FlowAnimator = value; }
        }
        public bool IsAnimating()
        {
            return m_bIsAnimating;
        }
        public bool IsShowing()
        {
            return m_bIsShowing;
        }
        public bool IsClosing()
        {
            return m_bIsClosing;
        }

        #endregion


        #region overrides BaseUI

        protected override void Reset()
        {
            base.Reset();
            UIAnimator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            m_OpenParameterId = Animator.StringToHash(m_sOpenTransitionName);
            m_OpenStateId = Animator.StringToHash(m_sOpenStateName);
            m_ClosedStateId = Animator.StringToHash(m_sClosedStateName);
        }

        public override void Show(OnUICompleteEvent pOnShow = null)
        {
            SetVisible(true);
            m_pOnShowEvent = pOnShow;
            NotifyOpen();
            DoShowAnimation();
        }
        public override void OnShow()
        {
            //Debug.Log("OnShowAnim");
            m_bIsAnimating = false;
            m_bIsShowing = false;
            base.OnShow();
        }
        public override void Close(OnUICompleteEvent pOnClose = null)
        {
            m_pOnCloseEvent = pOnClose;
            NotifyClose();
            DoCloseAnimation();
        }
        public override void OnClose()
        {
            //Debug.Log("OnCloseAnim");
            m_bIsAnimating = false;
            m_bIsClosing = false;
            SetVisible(false);
            base.OnClose();
        }

        public override bool IsWorking()
        {
            return base.IsVisible() && !m_bIsAnimating;
        }

        #endregion


        #region loop checking for animation for animator

        protected virtual void Update()
        {
            if(m_FlowAnimator != null)
                CheckTransition();
        }
        /*void OnGUI()
        {
            GUI.Label(new Rect(0, 100, 550, 150),
                "m_bIsAnimating[" + m_bIsAnimating + "] "
                + "m_bIsShowing[" + m_bIsShowing + "] "
                + "m_bIsClosing[" + m_bIsClosing + "] "
                + "IsInTransition[" + UIAnimator.IsInTransition(0) + "] "
                + "UIAnimator.GetNextAnimatorStateInfo[" + UIAnimator.GetNextAnimatorStateInfo(0).nameHash + "] "
                + "m_OpenStateId[" + m_OpenStateId + "] "
                + "m_ClosedStateId[" + m_ClosedStateId + "] "
                //+ "OPEN?[" + UIAnimator.GetNextAnimatorStateInfo(0).IsName("Open") + "]"
                //+ "Closed?[" + UIAnimator.GetNextAnimatorStateInfo(0).IsName("Closed") + "]"
                //+ "DidTransitionFinish[" + DidTransitionFinish(m_bIsShowing ? m_sClosedStateName : m_sOpenStateName) + "]"
                );
        }*/
        /// <summary>
        /// checks if transition has finished
        /// </summary>
        protected void CheckTransition()
        {
            if (m_bSkipFrame)
            {
                m_bSkipFrame = false;
                return;
            }
            if (m_bIsAnimating && DidTransitionFinish(m_bIsShowing ? m_sOpenStateName : m_sClosedStateName))
            {
                if (m_bIsShowing)
                {
                    OnShow();
                }
                else if (m_bIsClosing)
                {
                    OnClose();
                }
            }
        }
        #endregion


        #region animator states

        /// <summary>
        /// resets all animation info to avoid confusions
        /// </summary>
        private void ResetAnimVars()
        {
            m_bIsClosing = false;
            m_bIsAnimating = false;
            m_bIsShowing = false;
        }

        /// <summary>
        /// starts the showing animation 
        /// </summary>
        protected virtual void DoShowAnimation()
        {
            m_bSkipFrame = true;
            ResetAnimVars();
            m_bIsAnimating = true;
            m_bIsShowing = true;
            if (UIAnimator)
                UIAnimator.SetBool(m_sOpenTransitionName, true);
            else if (m_bUseTweenAnimation)
            {
                //Debug.Log("AwakePosition x[" + AwakePosition.x + "] y[" + AwakePosition.y + "]");
                // not sure why I have to get the parent position.. may be a bug with local and global positioning
                Vector2 vParentPos = UIManager.GetInstance().transform.position;
                Vector2 vStartPos = new Vector2(
                    //RectTransform.anchoredPosition.x + (Screen.width / 2),
                    //RectTransform.anchoredPosition.y + Screen.height);
                    AwakePosition.x + (Screen.width / 2),
                    vParentPos.y + AwakePosition.y + (Screen.height));
                Vector2 vFinishPos = new Vector2(AwakePosition.x, AwakePosition.y);
                RectTransform.position = vStartPos;
                // do it
                LeanTween.move(RectTransform, vFinishPos, TweeningDefaultTime)
                    .setOnComplete(OnShow)
                    .setEase(m_eOpenTweenType);

                //Debug.Log("vStartPos[" + vStartPos.x + "," + vStartPos.y + "]");
            }
            else
                OnShow();
            //Debug.Log("DoShowAnimation", gameObject);
        }
        /// <summary>
        /// starts the closing animation
        /// </summary>
        protected virtual void DoCloseAnimation()
        {
            m_bSkipFrame = true;
            ResetAnimVars();
            m_bIsAnimating = true;
            m_bIsClosing = true;
            if (UIAnimator)
                UIAnimator.SetBool(m_sOpenTransitionName, false);
            else if (m_bUseTweenAnimation)
            {
                // not sure why I have to get the parent position.. may be a bug with local and global positioning
                Vector2 vParentPos = UIManager.GetInstance().transform.position;
                Vector2 vFinishPos = new Vector2(
                    RectTransform.position.x - (Screen.width / 2),
                    vParentPos.y - AwakePosition.y - (Screen.width));
                // do it
                LeanTween.move(RectTransform, vFinishPos, TweeningDefaultTime)
                    .setOnComplete(OnClose)
                    .setEase(m_eCloseTweenType);
                //Debug.Log("vStartPos[" + vFinishPos.x + "," + vFinishPos.y + "]");
            }
            else
                OnClose();
            //Debug.Log("DoCloseAnimation", gameObject);
        }

        /// <summary>
        /// checks if the last animation is still happening
        /// </summary>
        /// <returns></returns>
        private bool DidTransitionFinish(string sName)
        //private bool DidTransitionFinish(int iHashId)
        {
            if (!UIAnimator.IsInTransition(0) && UIAnimator.GetCurrentAnimatorStateInfo(0).IsName(sName))
            {
                //m_bIsAnimating = false;
                return true;
            }
            //else
            //    Debug.Log("UIAnimator.IsInTransition[" + UIAnimator.IsInTransition(0) + "] isName(" + sName + ")[" + UIAnimator.GetNextAnimatorStateInfo(0).IsName(sName) + "]");
            return false;
        }
        #endregion

    }//end BaseAnimatedUI

}//end namespace CoreUI