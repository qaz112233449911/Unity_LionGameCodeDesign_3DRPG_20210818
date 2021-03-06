using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LiangWei.Dialogue
{
    /// <summary>
    /// NPC 系統
    /// 偵測目標是否進入對話範圍
    /// 並且開啟對話系統
    /// </summary>
    public class NPC : MonoBehaviour
    {
        #region 欄位與屬性
        [Header("對話資料")]
        public DataDialogue dataDialogue;
        [Header("相關資訊")]
        [Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;

        private Transform target;
        private bool starDialogueKey { get => Input.GetKeyDown(KeyCode.E); }
        [Header("對話系統")]
        public DialogueSystem dialogueSystem;
        [Header("完成任務事件")]
        public UnityEvent onFinish;

        /// <summary>
        /// 目前任務數量
        /// </summary>
        private int countCurrent;
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            goTip.SetActive(CheckPlayer());
            LookAtPlayer();
            StartDialogue();
        }

        /// <summary>
        /// 初始設定
        /// 狀態恢復為任務前
        /// </summary>
        private void Initialize()
        {
            dataDialogue.stateNPCMission = StateNPCMission.BeforeMission;
        }

        /// <summary>
        /// 檢查玩家是否進入
        /// </summary>
        /// <returns>玩家進入 傳回 true 否則 false</returns>
        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);

            if (hits.Length > 0) target = hits[0].transform;

            return hits.Length > 0;
        }
        /// <summary>
        /// 面向玩家
        /// </summary>
        private void LookAtPlayer()
        {
            if (CheckPlayer())
            {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }
        /// <summary>
        /// 玩家進入範圍內 並且 按下指定按鍵 請對話系統執行 開始對話
        /// 玩家退出範圍外 停止對話
        /// 判斷狀態 : 任務前、任務中、任務後
        /// </summary>
        private void StartDialogue()
        {
            if (CheckPlayer() && starDialogueKey)
            {
                dialogueSystem.Dialogue(dataDialogue);

                if (dataDialogue.stateNPCMission == StateNPCMission.BeforeMission) dataDialogue.stateNPCMission = StateNPCMission.Missionning;
            }
            else if (!CheckPlayer()) dialogueSystem.StopDialogue();
        }

        /// <summary>
        /// 更新任務需求數量
        /// 任務目標物件得到或死亡後處理
        /// </summary>
        public void UpdateMissionCount()
        {
            countCurrent++;

            //目前數量 等於 需求數量 狀態 等於 完成任務
            if (countCurrent == dataDialogue.countNeed)
            {
                dataDialogue.stateNPCMission = StateNPCMission.AfterMission;
                onFinish.Invoke();
            }
        }
    }
}