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
    }
}