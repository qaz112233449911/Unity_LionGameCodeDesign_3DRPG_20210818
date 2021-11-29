using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei
{
    /// <summary>
    /// ���Ĩt��
    /// ���ѵ��f���n���񭵮Ī�����
    /// </summary>
    /// �M�Τ���ɷ|�n�D���� : �|�۰ʲK�[���w������
    /// [�n�D����(����(����1), ����(����2), ...)]
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystem : MonoBehaviour
    {
        #region ���
        private AudioSource aud;
        #endregion

        #region �ƥ�
        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }
        #endregion

        #region ��k : ���}
        /// <summary>
        /// �H���`���q���񭵮�
        /// </summary>
        /// <param name="sound"></param>
        public void PlaySound(AudioClip sound)
        {
            aud.PlayOneShot(sound);
        }

        public void PlaySoundRandimVolume(AudioClip sound)
        {
            float volume = Random.Range(0.7f, 1.2f);
            aud.PlayOneShot(sound, volume);
        }
        #endregion
    }
}