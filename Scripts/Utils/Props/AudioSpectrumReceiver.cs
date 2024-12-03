using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/AudioSpectrumReceiver")]
    public class AudioSpectrumReceiver : MonoBehaviour
    {

        [SerializeField] AudioSource source;
        [Tooltip("毎フレーム取得する場合はこちら")]
        [SerializeField] bool alwaysGetSpectrum = true;
        [Tooltip("スペクトラムを取得するレート（秒）です")]
        [SerializeField] float spectrumReceiveRateSeconds = 0.2f;
        [SerializeField] int samplesCount;
        [SerializeField] int channel;

        public delegate void SpectrumReceiveDelegate(float[] data);
        public SpectrumReceiveDelegate onSpectrumDataUpdated;
        [SerializeField] public UnityEvent onSpectrumDataUpdatedU;

        private float[] _samples;

        /*  how to use
        public AudioSpectrumReceiver audioSpec;

        private void Start(){        audioSpec.onSpectrumDataUpdated += Tset;    }
        public void Tset(float[] data){        Debug.Log("hi" + data[128]);    }
        */

        private void Awake()
        {
            _samples = new float[samplesCount];
        }

        private void Start()
        {
            if (!alwaysGetSpectrum) StartCoroutine(GetSpectrumData());
        }

        private void Update()
        {
            if (alwaysGetSpectrum)
            {
                source.GetSpectrumData(_samples, channel, FFTWindow.Rectangular);
                if (onSpectrumDataUpdated != null) onSpectrumDataUpdated.Invoke(_samples);
                if (onSpectrumDataUpdatedU != null) onSpectrumDataUpdatedU.Invoke();
            }
        }

        public float GetSpectrum(int freq)
        {
            return _samples[freq];
        }
        public float[] GetSpectrum()
        {
            return _samples;
        }

        IEnumerator GetSpectrumData()
        {
            while (!alwaysGetSpectrum)
            {
                source.GetSpectrumData(_samples, channel, FFTWindow.Rectangular);
                if (onSpectrumDataUpdated != null) onSpectrumDataUpdated.Invoke(_samples);
                if (onSpectrumDataUpdatedU != null) onSpectrumDataUpdatedU.Invoke();
                yield return new WaitForSeconds(spectrumReceiveRateSeconds);
            }
        }

    }
}