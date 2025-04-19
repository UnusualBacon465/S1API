#if (IL2CPP)
using S1Quests = Il2CppScheduleOne.Quests;
using S1Dev = Il2CppScheduleOne.DevUtilities;
using S1Map = Il2CppScheduleOne.Map;
using S1Data = Il2CppScheduleOne.Persistence.Datas;
using S1Contacts = Il2CppScheduleOne.UI.Phone.ContactsApp;
using Il2CppInterop.Runtime;
#elif (MONO)
using S1Quests = ScheduleOne.Quests;
using S1Dev = ScheduleOne.DevUtilities;
using S1Map = ScheduleOne.Map;
using S1Data = ScheduleOne.Persistence.Datas;
using S1Contacts = ScheduleOne.UI.Phone.ContactsApp;
#endif

using System;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using S1API.Internal.Utils;
using S1API.Saveables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace S1API.Quests
{
    /// <summary>
    /// An abstract class intended to be derived from for creating custom quests in the game.
    /// </summary>
    public abstract class Quest : Saveable
    {
        /// <summary>
        /// The title of the quest to display for the player.
        /// </summary>
        protected abstract string Title { get; }
        
        /// <summary>
        /// The description provided to the player.
        /// </summary>
        protected abstract string Description { get; }
        
        /// <summary>
        /// Whether to automatically begin the quest once instanced.
        /// NOTE: If this is false, you must manually `.Begin()` this quest.
        /// </summary>
        protected virtual bool AutoBegin => true;
        
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// A list of all quest entries added to this quest.
        /// </summary>
        protected readonly QuestEntry[] QuestEntries = Array.Empty<QuestEntry>();
        
        [SaveableField("SOEQuest")] 
        private QuestData _questData = new QuestData();
        
        internal string? SaveFolder => _s1Quest?.SaveFolderName;
        
        internal S1Quests.Quest S1Quest => _s1Quest ?? throw new InvalidOperationException("S1Quest not initialized");
        private S1Quests.Quest? _s1Quest;
        private GameObject? _gameObject;
        
        internal override void InitializeInternal(GameObject gameObject, string guid = "")
        {
            MelonLogger.Msg("Adding Quest Component...");
            _gameObject = gameObject;
            _s1Quest = gameObject.AddComponent<S1Quests.Quest>();
            S1Quest.StaticGUID = guid;
            S1Quest.onActiveState = new UnityEvent();
            S1Quest.onComplete = new UnityEvent();
            S1Quest.onInitialComplete = new UnityEvent();
            S1Quest.onQuestBegin = new UnityEvent();
            S1Quest.onQuestEnd = new UnityEvent<S1Quests.EQuestState>();
            S1Quest.onTrackChange = new UnityEvent<bool>();
            S1Quest.TrackOnBegin = true;
            S1Quest.AutoCompleteOnAllEntriesComplete = true;
            // S1Quest.autoInitialize = false;
            MelonLogger.Msg("Assigning auto init...");
#if (MONO)
            FieldInfo autoInitField = AccessTools.Field(typeof(S1Quests.Quest), "autoInitialize");
            autoInitField.SetValue(S1Quest, false);
#elif (IL2CPP)
            S1Quest.autoInitialize = false;
#endif
            
            MelonLogger.Msg("Creating IconPrefab...");
            // Setup quest icon prefab
            GameObject iconPrefabObject = new GameObject("IconPrefab", 
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Image>()
            );
            iconPrefabObject.transform.SetParent(gameObject.transform);
            Image iconImage = iconPrefabObject.GetComponent<Image>();
            iconImage.sprite = S1Dev.PlayerSingleton<S1Contacts.ContactsApp>.Instance.AppIcon;
            S1Quest.IconPrefab = iconPrefabObject.GetComponent<RectTransform>();
            
            MelonLogger.Msg("Creating PoIUIPrefab...");
            // Setup UI for POI prefab
            var uiPrefabObject = new GameObject("PoIUIPrefab",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<EventTrigger>(),
                CrossType.Of<Button>()
            );
            uiPrefabObject.transform.SetParent(gameObject.transform);

            MelonLogger.Msg("Creating MainLabel...");
            var labelObject = new GameObject("MainLabel",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Text>()
            );
            labelObject.transform.SetParent(uiPrefabObject.transform);

            MelonLogger.Msg("Creating IconContainer...");
            var iconContainerObject = new GameObject("IconContainer",
                CrossType.Of<RectTransform>(), 
                CrossType.Of<CanvasRenderer>(), 
                CrossType.Of<Image>()
            );
            iconContainerObject.transform.SetParent(uiPrefabObject.transform);
            Image poiIconImage = iconContainerObject.GetComponent<Image>();
            poiIconImage.sprite = S1Dev.PlayerSingleton<S1Contacts.ContactsApp>.Instance.AppIcon;
            RectTransform iconRectTransform = poiIconImage.GetComponent<RectTransform>();
            iconRectTransform.sizeDelta = new Vector2(20, 20);
            
            // Setup POI prefab
            MelonLogger.Msg("Creating POIPrefab...");
            GameObject poiPrefabObject = new GameObject("POIPrefab");
            poiPrefabObject.SetActive(false);
            poiPrefabObject.transform.SetParent(gameObject.transform);
            S1Map.POI poi = poiPrefabObject.AddComponent<S1Map.POI>();
            poi.DefaultMainText = "Did it work?";
#if (MONO)
            FieldInfo uiPrefabField = AccessTools.Field(typeof(S1Map.POI), "UIPrefab");
            uiPrefabField.SetValue(poi, uiPrefabObject);
#elif (IL2CPP)
            poi.UIPrefab = uiPrefabObject;
#endif
            S1Quest.PoIPrefab = poiPrefabObject;
            
            MelonLogger.Msg("Quest created.");
            
            // Initialize the quest
            S1Quest.InitializeQuest(Title, Description, Array.Empty<S1Data.QuestEntryData>(), _s1Quest?.StaticGUID);
            MelonLogger.Msg("Quest initialized.");
            
            base.InitializeInternal(gameObject, guid);
        }

        internal override void StartInternal()
        {
            base.StartInternal();
            
            if (AutoBegin)
                _s1Quest?.Begin();
        }

        /// <summary>
        /// Adds a new quest entry to the quest.
        /// </summary>
        /// <param name="title">The title for the quest entry.</param>
        /// <param name="poiPosition">A position for the point-of-interest, if applicable.</param>
        /// <returns>A reference to the quest entry</returns>
        protected QuestEntry AddEntry(string title, Vector3? poiPosition = null)
        {
            var questEntryObject = new GameObject($"QuestEntry");
            questEntryObject.transform.SetParent(_gameObject?.transform);
            
            S1Quests.QuestEntry s1QuestEntry = questEntryObject.AddComponent<S1Quests.QuestEntry>();
            s1QuestEntry.PoILocation = questEntryObject.transform;
            S1Quest.Entries.Add(s1QuestEntry);
            
            QuestEntry questEntry = new QuestEntry(s1QuestEntry)
            {
                Title = title,
                POIPosition = poiPosition ?? Vector3.zero
            };
            QuestEntries.AddItem(questEntry);
            
            return questEntry;
        }
        
        /// <summary>
        /// Starts the quest for the save file.
        /// </summary>
        public void Begin() => _s1Quest?.Begin();
        
        /// <summary>
        /// Cancels the quest for the save file.
        /// </summary>
        public void Cancel() => _s1Quest?.Cancel();
        
        /// <summary>
        /// Expires the quest for the save file.
        /// </summary>
        public void Expire() => _s1Quest?.Expire();
        
        /// <summary>
        /// Fails the quest for the save file.
        /// </summary>
        public void Fail() => _s1Quest?.Fail();
        
        /// <summary>
        /// Completes the quest for the save file.
        /// </summary>
        public void Complete() => _s1Quest?.Complete();
        
        /// <summary>
        /// Ends the quest for the save file.
        /// NOTE: This is done upon completion of the entries by default.
        /// </summary>
        public void End() => _s1Quest?.End();
    }
}