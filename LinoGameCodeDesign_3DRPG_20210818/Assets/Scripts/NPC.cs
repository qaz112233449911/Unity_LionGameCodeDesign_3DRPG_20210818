using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Dialogue
{
    /// <summary>
    /// NPC �t��
    /// �����ؼЬO�_�i�J��ܽd��
    /// �åB�}�ҹ�ܨt��
    /// </summary>
    public class NPC : MonoBehaviour
    {
        #region ���P�ݩ�
        [Header("��ܸ��")]
        public DataDialogue dataDialogue;
        [Header("������T")]
        [Range(0, 10)]
        public float checkPlayerRadius = 3f;
        public GameObject goTip;
        [Range(0, 10)]
        public float speedLookAt = 3;

        private Transform target;
        private bool starDialogueKey { get => Input.GetKeyDown(KeyCode.E); }
        [Header("��ܨt��")]
        public DialogueSystem dialogueSystem;
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private void Update()
        {
            goTip.SetActive(CheckPlayer());
            LookAtPlayer();
            StartDialogue();
        }

        /// <summary>
        /// �ˬd���a�O�_�i�J
        /// </summary>
        /// <returns>���a�i�J �Ǧ^ true �_�h false</returns>
        private bool CheckPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkPlayerRadius, 1 << 6);

            if (hits.Length > 0) target = hits[0].transform;

            return hits.Length > 0;
        }
        /// <summary>
        /// ���V���a
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
        /// ���a�i�J�d�� �åB ���U���w���� �й�ܨt�ΰ��� �}�l���
        /// ���a�h�X�d��~ ������
        /// </summary>
        private void StartDialogue()
        {
            if (CheckPlayer() && starDialogueKey)
            {
                dialogueSystem.Dialogue(dataDialogue);
            }
            else if (!CheckPlayer()) dialogueSystem.StopDialogue();
        }
    }
}