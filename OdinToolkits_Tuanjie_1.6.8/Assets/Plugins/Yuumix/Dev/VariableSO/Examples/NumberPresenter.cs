using System;
using TMPro;
using UnityEngine;

namespace Dev.VariableSO.Examples
{
    public class PureMonitor
    {
        public void LogMonitor(int value)
        {
            Debug.Log("监测到的值为：" + value);
        }
    }

    public class NumberPresenter : MonoBehaviour
    {
        // Model
        [SerializeField]
        IntVariableSO intVariable;

        // View
        [SerializeField]
        TextMeshProUGUI textMeshPro;

        PureMonitor _pureMonitor;

        void Awake()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            _pureMonitor = new PureMonitor();
        }

        void Start()
        {
            if (intVariable == null)
            {
                Debug.LogError("NumberPresenter: IntVariableSO is not assigned!");
                return;
            }

            textMeshPro.text = intVariable.Value.ToString();
            intVariable.OnValueChanged += _pureMonitor.LogMonitor;
        }

        void OnEnable()
        {
            intVariable.OnValueChanged += IntVariable_OnValueChanged;
        }

        void OnDisable()
        {
            intVariable.OnValueChanged -= IntVariable_OnValueChanged;
        }

        void OnDestroy()
        {
            intVariable.OnValueChanged -= _pureMonitor.LogMonitor;
        }

        void IntVariable_OnValueChanged(int obj)
        {
            textMeshPro.text = obj.ToString();
        }

        public void IncrementCommand()
        {
            intVariable.SetValue(intVariable.Value + 1);
        }

        public void DecrementCommand()
        {
            intVariable.SetValue(intVariable.Value - 1);
        }
    }
}
