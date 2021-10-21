using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Dialogue
{
    /// <summary>
    /// ��ܨt�Ϊ����
    /// NPC �n��ܪ��T�Ӷ��q���e
    /// �����ȫe�B���ȶi�椤�B��������
    /// </summary>
    // ScriptableObject �~�Ӧ����O�|�ܦ��}���ƪ���
    // �i�N���}����Ʒ�����O�s�b�M�� Project��
    // CreatAssetMenu ���O�ݩ� : �������O�إ߱M�פ����
    // menuName ���W�� : �i�� / ���h
    // fileName �ɮצW��
    [CreateAssetMenu(menuName ="Wei/��ܸ��", fileName = "NPC ��ܸ��")]
    public class DataDialogue : ScriptableObject
    {
        //�}�C:�O�s�ۦP������������c
        //TextArea �r����ݩʡA�i�]�w���
        [Header("���ȫe��ܤ��e"), TextArea(2, 7)]
        public string[] beforeMission;
        [Header("���ȶi�椤��ܤ��e"), TextArea(2, 7)]
        public string[] Missionning;
        [Header("���ȧ�����ܤ��e"), TextArea(2, 7)]
        public string[] afterMission;
        [Header("���ȻݨD�ƶq"), Range(0, 100)]
        public int countNeed;
        //�ϥΦC�| : 
        //�y�k : �׹��� �C�|�W�� �۩w�q���W��;
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;
    }
}
