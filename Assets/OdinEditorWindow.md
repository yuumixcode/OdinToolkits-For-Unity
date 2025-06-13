# OdinEditorWindow
> Sirenix.OdinInspector.Editor.OdinEditorWindow

## 构造 Constructors
| 构造函数 | 注释 Comment |
| :--- | :--- |
| public .ctor() | 无 |

## 静态字段 Static Fields
| 静态字段 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| MAX_DROPDOWN_HEIGHT | public static int MAX_DROPDOWN_HEIGHT; | 无 |
| MIN_DROPDOWN_HEIGHT | public static int MIN_DROPDOWN_HEIGHT; | 无 |
| EmptyObjectArray | private static Object[] EmptyObjectArray; | 无 |
| hasUpdatedOdinEditors | private static bool hasUpdatedOdinEditors; | 无 |
| inspectObjectWindowCount | private static int inspectObjectWindowCount; | 无 |
| materialForceVisibleProperty | private static PropertyInfo materialForceVisibleProperty; | 无 |

## 静态方法 Static Methods
| 静态方法成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| CreateOdinEditorWindowInstanceForObject | public static OdinEditorWindow CreateOdinEditorWindowInstanceForObject(Object obj) | 无 |
| InspectObject | public static OdinEditorWindow InspectObject(OdinEditorWindow window, Object obj) | 无 |
| InspectObject | public static OdinEditorWindow InspectObject(Object obj) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, Vector2 position, float windowWidth) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, float windowWidth) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, float width, float height) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, Vector2 position) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, Rect btnRect, float windowWidth) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj, Rect btnRect, Vector2 windowSize) | 无 |
| InspectObjectInDropDown | public static OdinEditorWindow InspectObjectInDropDown(Object obj) | 无 |
| CreateOdinEditorWindowInstanceForObject | internal static OdinEditorWindow CreateOdinEditorWindowInstanceForObject(Object obj, bool forceSerializeInspectedObject) | 无 |
| InspectObject | internal static OdinEditorWindow InspectObject(Object obj, bool forceSerializeInspectedObject) | 无 |

## 静态事件 Static Events
| 静态事件成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |

## 实例字段 Fields
| 实例字段成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| _onBeginGUI | private Action _onBeginGUI; | 无 |
| _onEndGUI | private Action _onEndGUI; | 无 |
| contenSize | private Vector2 contenSize; | 无 |
| currentTargets | private Object[] currentTargets; | 无 |
| currentTargetsImm | private ImmutableList<Object> currentTargetsImm; | 无 |
| defaultEditorPreviewHeight | private float defaultEditorPreviewHeight; | 无 |
| drawUnityEditorPreview | private bool drawUnityEditorPreview; | 无 |
| editors | private Editor[] editors; | 无 |
| inspectTargetObject | private Object inspectTargetObject; | 无 |
| inspectorTargetSerialized | private Object inspectorTargetSerialized; | 无 |
| isAutoHeightAdjustmentReady | private bool isAutoHeightAdjustmentReady; | 无 |
| isInOurImGUIContainer | private bool isInOurImGUIContainer; | 无 |
| isInitialized | private bool isInitialized; | 无 |
| isInsideOnGUI | private bool isInsideOnGUI; | 无 |
| labelWidth | private float labelWidth; | 无 |
| marginStyle | private GUIStyle marginStyle; | 无 |
| mouseDownId | private int mouseDownId; | 无 |
| mouseDownKeyboardControl | private int mouseDownKeyboardControl; | 无 |
| mouseDownWindow | private EditorWindow mouseDownWindow; | 无 |
| preventContentFromExpanding | private bool preventContentFromExpanding; | 无 |
| propertyTrees | private PropertyTree[] propertyTrees; | 无 |
| scrollPos | private Vector2 scrollPos; | 无 |
| serializationData | private SerializationData serializationData; | 无 |
| toasts | private List<Toast> toasts; | 无 |
| useScrollView | private bool useScrollView; | 无 |
| warmupRepaintCount | private int warmupRepaintCount; | 无 |
| windowPadding | private Vector4 windowPadding; | 无 |
| wrappedAreaMaxHeight | private int wrappedAreaMaxHeight; | 无 |
| m_FadeoutTime | internal float m_FadeoutTime; | 无 |
| m_IsPresented | internal bool m_IsPresented; | 无 |
| m_Notification | internal GUIContent m_Notification; | 无 |
| m_Parent | internal HostView m_Parent; | 无 |
| m_Pos | internal Rect m_Pos; | 无 |
| m_SerializedDataModeController | internal DataModeController m_SerializedDataModeController; | 无 |
| m_TitleContent | internal GUIContent m_TitleContent; | 无 |

## 实例属性 Properties
| 实例属性成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| DefaultEditorPreviewHeight | public float DefaultEditorPreviewHeight { public get; public set; } | 无 |
| DefaultLabelWidth | public void DefaultLabelWidth { public get; public set; } | 无 |
| DrawUnityEditorPreview | public void DrawUnityEditorPreview { public get; public set; } | 无 |
| UseScrollView | public void UseScrollView { public get; public set; } | 无 |
| WindowPadding | public Vector4 WindowPadding { public get; public set; } | 无 |
| PropertyTree | public PropertyTree PropertyTree { public get;  set; } | 无 |
| CurrentDrawingTargets | protected ImmutableList<Object> CurrentDrawingTargets { protected get;  set; } | 无 |
| antiAlias | public int antiAlias { public get; public set; } | 无 |
| autoRepaintOnSceneChange | public bool autoRepaintOnSceneChange { public get; public set; } | 无 |
| dataModeController | public IDataModeController dataModeController { public get;  set; } | 无 |
| depthBufferBits | public int depthBufferBits { public get; public set; } | 无 |
| docked | public bool docked { public get;  set; } | 无 |
| hasFocus | public bool hasFocus { public get;  set; } | 无 |
| hasUnsavedChanges | public bool hasUnsavedChanges { public get; protected set; } | 无 |
| hideFlags | public HideFlags hideFlags { public get; public set; } | 无 |
| maxSize | public Vector2 maxSize { public get; public set; } | 无 |
| maximized | public void maximized { public get; public set; } | 无 |
| minSize | public void minSize { public get; public set; } | 无 |
| name | public string name { public get; public set; } | 无 |
| overlayCanvas | public OverlayCanvas overlayCanvas { public get;  set; } | 无 |
| position | public Rect position { public get; public set; } | 无 |
| rootVisualElement | public VisualElement rootVisualElement { public get;  set; } | 无 |
| saveChangesMessage | public string saveChangesMessage { public get; protected set; } | 无 |
| title | public string title { public get; public set; } | 无 |
| titleContent | public GUIContent titleContent { public get; public set; } | 无 |
| wantsLessLayoutEvents | public bool wantsLessLayoutEvents { public get; public set; } | 无 |
| wantsMouseEnterLeaveWindow | public void wantsMouseEnterLeaveWindow { public get; public set; } | 无 |
| wantsMouseMove | public bool wantsMouseMove { public get; public set; } | 无 |
| liveReloadPreferenceDefault | internal bool liveReloadPreferenceDefault { internal get;  set; } | 无 |
| antiAliasing | internal void antiAliasing { internal get; internal set; } | 无 |
| baseRootVisualElement | internal VisualElement baseRootVisualElement { internal get;  set; } | 无 |
| disableInputEvents | internal bool disableInputEvents { internal get; internal set; } | 无 |
| isUIToolkitWindow | internal bool isUIToolkitWindow { internal get;  set; } | 无 |
| resetPanelRenderingOnAssetChange | internal void resetPanelRenderingOnAssetChange { internal get; internal set; } | 无 |
| viewDataDictionary | internal SerializableJsonDictionary viewDataDictionary { internal get;  set; } | 无 |

## 实例方法 Methods
| 实例方法成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| ShowToast | public void ShowToast(ToastPosition toastPosition, SdfIconType icon, string text, Color color, float duration) | 无 |
| UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize | private virtual void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() | 无 |
| UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize | private virtual void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() | 无 |
| Cleanup | private void Cleanup() | 无 |
| CreateGUI | private void CreateGUI() | 无 |
| InitializeIfNeeded | private void InitializeIfNeeded() | 无 |
| SelectionChanged | private void SelectionChanged() | 无 |
| DrawEditor | protected virtual void DrawEditor(int index) | 无 |
| DrawEditorPreview | protected virtual void DrawEditorPreview(int index, float height) | 无 |
| DrawEditors | protected virtual void DrawEditors() | 无 |
| GetTarget | protected virtual Object GetTarget() | 无 |
| GetTargets | protected virtual IEnumerable<Object> GetTargets() | 无 |
| Initialize | protected virtual void Initialize() | 无 |
| OnAfterDeserialize | protected virtual void OnAfterDeserialize() | 无 |
| OnBeforeSerialize | protected virtual void OnBeforeSerialize() | 无 |
| OnBeginDrawEditors | protected virtual void OnBeginDrawEditors() | 无 |
| OnDestroy | protected virtual void OnDestroy() | 无 |
| OnDisable | protected virtual void OnDisable() | 无 |
| OnEnable | protected virtual void OnEnable() | 无 |
| OnEndDrawEditors | protected virtual void OnEndDrawEditors() | 无 |
| OnImGUI | protected virtual void OnImGUI() | 无 |
| EnableAutomaticHeightAdjustment | protected void EnableAutomaticHeightAdjustment(int maxHeight, bool retainInitialWindowPosition) | 无 |
| EnsureEditorsAreReady | protected void EnsureEditorsAreReady() | 无 |
| UpdateEditors | protected void UpdateEditors() | 无 |
| DiscardChanges | public virtual void DiscardChanges() | 无 |
| Equals | public virtual bool Equals(Object other) | 无 |
| GetExtraPaneTypes | public virtual IEnumerable<Type> GetExtraPaneTypes() | 无 |
| GetHashCode | public virtual int GetHashCode() | 无 |
| Repaint | public virtual void Repaint() | 无 |
| SaveChanges | public virtual void SaveChanges() | 无 |
| ToString | public virtual string ToString() | 无 |
| BeginWindows | public void BeginWindows() | 无 |
| Close | public void Close() | 无 |
| EndWindows | public void EndWindows() | 无 |
| Focus | public void Focus() | 无 |
| GetInstanceID | public int GetInstanceID() | 无 |
| GetType | public Type GetType() | 无 |
| RemoveNotification | public void RemoveNotification() | 无 |
| SendEvent | public bool SendEvent(Event e) | 无 |
| Show | public void Show(bool immediateDisplay) | 无 |
| Show | public void Show() | 无 |
| ShowAsDropDown | public void ShowAsDropDown(Rect buttonRect, Vector2 windowSize) | 无 |
| ShowAuxWindow | public void ShowAuxWindow() | 无 |
| ShowModal | public void ShowModal() | 无 |
| ShowModalUtility | public void ShowModalUtility() | 无 |
| ShowNotification | public void ShowNotification(GUIContent notification) | 无 |
| ShowNotification | public void ShowNotification(GUIContent notification, Double fadeoutWait) | 无 |
| ShowPopup | public void ShowPopup() | 无 |
| ShowTab | public void ShowTab() | 无 |
| ShowUtility | public void ShowUtility() | 无 |
| TryGetOverlay | public bool TryGetOverlay(string id, Overlay& match) | 无 |
| Finalize | protected virtual void Finalize() | 无 |
| OnBackingScaleFactorChanged | protected virtual void OnBackingScaleFactorChanged() | 无 |
| MemberwiseClone | protected Object MemberwiseClone() | 无 |
| CanMaximize | internal virtual bool CanMaximize() | 无 |
| OnBackgroundViewResized | internal virtual void OnBackgroundViewResized(Rect pos) | 无 |
| OnMaximized | internal virtual void OnMaximized() | 无 |
| OnResized | internal virtual void OnResized() | 无 |
| AddGameTab | internal void AddGameTab() | 无 |
| AddSceneTab | internal void AddSceneTab() | 无 |
| CheckForWindowRepaint | internal void CheckForWindowRepaint() | 无 |
| ClearPersistentViewData | internal void ClearPersistentViewData() | 无 |
| DisableViewDataPersistence | internal void DisableViewDataPersistence() | 无 |
| DrawNotification | internal void DrawNotification() | 无 |
| GetDataModeController_Internal | internal DataModeController GetDataModeController_Internal() | 无 |
| GetDisplayViewSize | internal Vector2 GetDisplayViewSize(int displayId) | 无 |
| GetLocalizedTitleContent | internal GUIContent GetLocalizedTitleContent() | 无 |
| GetNumTabs | internal int GetNumTabs() | 无 |
| GetViewDataDictionary | internal ISerializableJsonDictionary GetViewDataDictionary() | 无 |
| IsSelectedTab | internal bool IsSelectedTab() | 无 |
| MakeParentsSettingsMatchMe | internal void MakeParentsSettingsMatchMe() | 无 |
| MarkDirty | internal void MarkDirty() | 无 |
| OnBackingScaleFactorChangedInternal | internal void OnBackingScaleFactorChangedInternal() | 无 |
| ReleaseViewData | internal void ReleaseViewData() | 无 |
| RemoveFromDockArea | internal void RemoveFromDockArea() | 无 |
| RepaintImmediately | internal void RepaintImmediately() | 无 |
| SaveViewData | internal void SaveViewData() | 无 |
| SetDisplayViewSize | internal void SetDisplayViewSize(int displayId, Vector2 targetSize) | 无 |
| SetMainPlayModeViewSize | internal void SetMainPlayModeViewSize(Vector2 targetSize) | 无 |
| SetParentGameViewDimensions | internal void SetParentGameViewDimensions(Rect rect, Rect clippedRect, Vector2 targetSize) | 无 |
| SetPlayModeView | internal void SetPlayModeView(bool value) | 无 |
| SetPlayModeViewSize | internal void SetPlayModeViewSize(Vector2 targetSize) | 无 |
| ShowAsDropDown | internal void ShowAsDropDown(Rect buttonRect, Vector2 windowSize, PopupLocation[] locationPriorityOrder) | 无 |
| ShowAsDropDown | internal void ShowAsDropDown(Rect buttonRect, Vector2 windowSize, PopupLocation[] locationPriorityOrder, ShowMode mode) | 无 |
| ShowAsDropDown | internal void ShowAsDropDown(Rect buttonRect, Vector2 windowSize, PopupLocation[] locationPriorityOrder, ShowMode mode, bool giveFocus) | 无 |
| ShowAsDropDownFitToScreen | internal Rect ShowAsDropDownFitToScreen(Rect buttonRect, Vector2 windowSize, PopupLocation[] locationPriorityOrder) | 无 |
| ShowNextTabIfPossible | internal bool ShowNextTabIfPossible() | 无 |
| ShowPopupWithMode | internal void ShowPopupWithMode(ShowMode mode, bool giveFocus) | 无 |
| ShowTooltip | internal void ShowTooltip() | 无 |
| ShowWithMode | internal void ShowWithMode(ShowMode mode) | 无 |
| WaitUntilPresented | internal CustomYieldInstruction WaitUntilPresented() | 无 |

### 弃用 Obsolete
| 静态方法成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| OnGUI | protected virtual void OnGUI() | 无 |
| SetDirty | public void SetDirty() | 无 |

## 实例事件 Events
| 实例事件成员 | 完整签名 | 注释 Comment |
| :--- | :--- | :--- |
| OnBeginGUI | public event Action OnBeginGUI; | 无 |
| OnClose | public event Action OnClose; | 无 |
| OnEndGUI | public event Action OnEndGUI; | 无 |


