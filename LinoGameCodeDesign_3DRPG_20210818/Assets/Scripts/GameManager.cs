using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiangWei
{
    /// <summary>
    /// �C���޲z��
    /// �����B�z
    /// 1. ���ȧ���
    /// 2. ���a���`
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region ���
        [Header("�s�ժ���")]
        public CanvasGroup groupFinal;
        [Header("�����e�����D")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titleLose = "You Failed..";
        #endregion

        #region ��k : ���}
        /// <summary>
        /// �}�l�H�J�̫ᤶ��
        /// </summary>
        /// <param name="win">�O�_���</param>
        public void StatFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI(win ? titleWin : titleLose));
        }
        #endregion

        #region ��k : �p�H
        /// <summary>
        /// �H�J�����e��
        /// </summary>
        /// <param name="title">���D</param>
        /// <returns></returns>
        private IEnumerator FadeFinalUI(string title)
        {
            textTitle.text = title;
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;

            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
        }
        #endregion
    }
}