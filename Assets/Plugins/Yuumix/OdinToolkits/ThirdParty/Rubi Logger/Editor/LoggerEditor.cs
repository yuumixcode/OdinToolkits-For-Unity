using System;
using UnityEditor;
using UnityEngine;


namespace Rubickanov.Logger.Editor
{
    [CustomEditor(typeof(RubiLogger))]
    public class LoggerEditor : UnityEditor.Editor
    {
        private SerializedProperty showLogsProperty;
        private SerializedProperty categoryNameProperty;
        private SerializedProperty categoryColorProperty;
        private SerializedProperty logLevelFilterProperty;

        private SerializedProperty screenLogsEnabledProperty;
        private SerializedProperty showErrorWhenDisabledScreenLogsProperty;
        private SerializedProperty fileLogsEnabledProperty;
        private SerializedProperty showErrorWhenDisabledFileLogsProperty;
        
        private const string putLoggerDisplayWarn =
            "Settings for Screen Logs are in LoggerDisplay component.\n Please be sure you added LoggerDisplay in the scene.";
        private const string screenLogPreviewInLogDisplayWant =
            "Screen Logs will be the same like Editor Console logs, but you can add index prefix in LoggerDisplay. Please be sure you added LoggerDisplay in the scene.";

        private SerializedProperty logFilePathProperty;

        // Editor Variables
        private bool showEditorLogPreview = true;
        private bool showFileLogPreview = true;
        private bool showScreenLogPreview = true;

        private GUIStyle headerStyle;
        private DateTime logTime;

        private void OnEnable()
        {
            logTime = DateTime.Now;
            headerStyle = CreateHeaderStyle();
            
            showLogsProperty = serializedObject.FindProperty("showLogs");
            categoryNameProperty = serializedObject.FindProperty("categoryName");
            categoryColorProperty = serializedObject.FindProperty("categoryColor");
            logLevelFilterProperty = serializedObject.FindProperty("logLevelFilter");

            screenLogsEnabledProperty = serializedObject.FindProperty("screenLogsEnabled");
            showErrorWhenDisabledScreenLogsProperty = serializedObject.FindProperty("showErrorWhenDisabledScreenLogs");
            fileLogsEnabledProperty = serializedObject.FindProperty("fileLogsEnabled");
            showErrorWhenDisabledFileLogsProperty = serializedObject.FindProperty("showErrorWhenDisabledFileLogs");

            logFilePathProperty = serializedObject.FindProperty("logFilePath");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            UpdateHeaderStyleColor();

            DrawHeader();

            DrawMainSettings();

            DrawScreenSettings();

            DrawFileSettings();

            EditorGUILayout.LabelField("Logs Preview", new GUIStyle()
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(0, 0, 10, 10),
                normal = { textColor = new Color(0.56f, 0.56f, 0.56f) }
            });

            DrawLogsPreview();


            EditorGUILayout.Space(10);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawMainSettings()
        {
            EditorGUILayout.LabelField("Main Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(showLogsProperty, new GUIContent("Show Logs"));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(categoryNameProperty, new GUIContent("Category"), GUILayout.ExpandWidth(true));
            if (GUILayout.Button(new GUIContent("Auto Prefix", "Click to automatically name the Logger prefix"),
                    GUILayout.Width(100)))
            {
                categoryNameProperty.stringValue = serializedObject.targetObject.name;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(categoryColorProperty, new GUIContent("Category Color"));
            EditorGUILayout.PropertyField(logLevelFilterProperty, new GUIContent("Log Level Filter"));
            EditorGUILayout.Space(15);
        }

        private void DrawScreenSettings()
        {
            string screenLogsLabel =
                screenLogsEnabledProperty.boolValue ? "Screen Logs Enabled" : "Screen Logs Disabled";
            EditorGUILayout.PropertyField(screenLogsEnabledProperty, new GUIContent(screenLogsLabel));
            if (screenLogsEnabledProperty.boolValue)
            {
                EditorGUILayout.LabelField(
                    putLoggerDisplayWarn,
                    new GUIStyle()
                    {
                        fontSize = 14,
                        fontStyle = FontStyle.Normal,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 10, 10),
                        normal = { textColor = new Color(0.56f, 0.56f, 0.56f) },
                        wordWrap = true
                    },
                    GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
            }
            else
            {
                EditorGUILayout.PropertyField(showErrorWhenDisabledScreenLogsProperty, new GUIContent("Show Error When Disabled"));
            }

            EditorGUILayout.Space(15);
        }

        private void DrawFileSettings()
        {
            string fileLogsLabel = fileLogsEnabledProperty.boolValue ? "File Logs Enabled" : "File Logs Disabled";
            EditorGUILayout.PropertyField(fileLogsEnabledProperty, new GUIContent(fileLogsLabel));
            if (fileLogsEnabledProperty.boolValue)
            {
                EditorGUILayout.LabelField("File Settings", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(logFilePathProperty, new GUIContent("Log File Path"));
                if (GUILayout.Button(new GUIContent("Set Default Path",
                        "Click to set the default path for the log file.\nYou can change default path in the Logger script.")))
                {
                    logFilePathProperty.stringValue = RubiConstants.DEFAULT_PATH;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.PropertyField(showErrorWhenDisabledFileLogsProperty, new GUIContent("Show Error When Disabled"));
            }

            EditorGUILayout.Space(15);
        }

        private void DrawLogsPreview()
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(categoryColorProperty.colorValue);
            string prefix = categoryNameProperty.stringValue;

            showEditorLogPreview = EditorGUILayout.Foldout(showEditorLogPreview, "Editor Log Preview");
            if (showEditorLogPreview)
            {
                foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel)))
                {
                    string logTypeColor = RubiConstants.GetLogLevelColor(logLevel);
                    EditorGUILayout.LabelField(
                        $"<color={logTypeColor}>[{logLevel}]</color> <color=#{hexColor}>[{prefix}] </color> [SenderName]: This is a {logLevel} message",
                        new GUIStyle()
                        {
                            fontSize = 14,
                            fontStyle = FontStyle.Normal,
                            alignment = TextAnchor.MiddleLeft,
                            margin = new RectOffset(0, 0, 10, 10),
                            normal = { textColor = new Color(0.56f, 0.56f, 0.56f) },
                            richText = true
                        });
                }
            }

            EditorGUILayout.Space(10);
            if (screenLogsEnabledProperty.boolValue)
            {
                showScreenLogPreview = EditorGUILayout.Foldout(showScreenLogPreview, "Screen Log Preview");
                if (showScreenLogPreview)
                {
                    EditorGUILayout.LabelField(
                        screenLogPreviewInLogDisplayWant,
                        new GUIStyle()
                        {
                            fontSize = 14,
                            fontStyle = FontStyle.Normal,
                            alignment = TextAnchor.MiddleLeft,
                            margin = new RectOffset(0, 0, 10, 10),
                            normal = { textColor = new Color(0.56f, 0.56f, 0.56f) },
                            wordWrap = true
                        },
                        GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
                }
            }


            EditorGUILayout.Space(10);
            if (fileLogsEnabledProperty.boolValue)
            {
                showFileLogPreview = EditorGUILayout.Foldout(showFileLogPreview, "File Log Preview");
                if (showFileLogPreview)
                {
                    foreach (LogLevel logType in Enum.GetValues(typeof(LogLevel)))
                    {
                        EditorGUILayout.LabelField(
                            $"{logTime} [{logType}] [{prefix}] [SenderName]: This is a {logType} message",
                            new GUIStyle()
                            {
                                fontSize = 14,
                                fontStyle = FontStyle.Normal,
                                alignment = TextAnchor.MiddleLeft,
                                margin = new RectOffset(0, 0, 10, 10),
                                normal = { textColor = new Color(0.56f, 0.56f, 0.56f) }
                            });
                    }
                }
            }
        }


        private new void DrawHeader()
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 30);
            EditorGUI.DrawRect(rect, categoryColorProperty.colorValue);

            string name = categoryNameProperty.stringValue;

            EditorGUI.LabelField(rect, $"{name} Settings", headerStyle);
        }

        private GUIStyle CreateHeaderStyle()
        {
            return new GUIStyle()
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(0, 0, 10, 10),
                normal = { textColor = new Color(0.56f, 0.56f, 0.56f) }
            };
        }

        private void UpdateHeaderStyleColor()
        {
            Color prefixColor = categoryColorProperty.colorValue;

            float brightness = 0.299f * prefixColor.r + 0.587f * prefixColor.g + 0.114f * prefixColor.b;
            if (brightness > 0.5f)
            {
                headerStyle.normal.textColor = Color.black;
            }
            else
            {
                headerStyle.normal.textColor = Color.white;
            }
        }
    }
}