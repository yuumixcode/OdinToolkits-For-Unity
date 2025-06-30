using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubickanov.Logger
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class LoggerDisplay : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        protected int fontSize = 14;
        [SerializeField]
        protected int maxLines = 10;
        [SerializeField]
        protected float logLifetime = 3.0f;
        [SerializeField]
        private bool addIndexPrefix = true;

        private List<RubiLogger> loggers = new List<RubiLogger>();
        protected Queue<RectTransform> logLines = new Queue<RectTransform>();
        protected StringBuilder logText = new StringBuilder();

        private int index = 1;

        private void OnEnable()
        {
            SubscribeToLoggers();
        }

        private void OnDisable()
        {
            UnsubscribeFromLoggers();
        }

        private void UpdateConsole(string message)
        {
            RectTransform logLine = CreateLogLine(message);
            AddLogLineToQueue(logLine);
            RemoveOldLogLines();
            StartRemoveLogAfterDelayCoroutine(logLine);
        }

        private IEnumerator RemoveLogAfterDelay(float delay, RectTransform logLine)
        {
            yield return new WaitForSeconds(delay);
            RemoveLogLineIfItExists(logLine);
        }

        private void SubscribeToLoggers()
        {
            foreach (var logger in FindObjectsOfType<RubiLogger>())
            {
                logger.LogAdded += UpdateConsole;
                loggers.Add(logger);
            }

            RubiLoggerStatic.LogAdded += UpdateConsole;
        }

        private void UnsubscribeFromLoggers()
        {
            foreach (var logger in loggers)
            {
                logger.LogAdded -= UpdateConsole;
            }

            loggers.Clear();

            RubiLoggerStatic.LogAdded -= UpdateConsole;
        }

        private RectTransform CreateLogLine(string message)
        {
            TextMeshProUGUI logLine =
                new GameObject("LogLine", typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
            logLine.transform.SetParent(transform);
            logLine.fontSize = fontSize;
            logLine.enableWordWrapping = false;
            if (addIndexPrefix)
            {
                logLine.text = $"{index}." + message;
            }
            else
            {
                logLine.text = message;
            }

            index++;
            return logLine.rectTransform;
        }

        private void AddLogLineToQueue(RectTransform logLine)
        {
            logLines.Enqueue(logLine);
        }

        private void RemoveOldLogLines()
        {
            if (logLines.Count > maxLines)
            {
                Destroy(logLines.Dequeue().gameObject);
            }
        }

        private void StartRemoveLogAfterDelayCoroutine(RectTransform logLine)
        {
            if (logLifetime > 0)
            {
                StartCoroutine(RemoveLogAfterDelay(logLifetime, logLine));
            }
        }

        private void RemoveLogLineIfItExists(RectTransform logLine)
        {
            if (logLine != null && logLines.Contains(logLine))
            {
                logLines.Dequeue();
                Destroy(logLine.gameObject);
            }
        }
    }
}