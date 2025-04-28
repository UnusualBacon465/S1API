using System;
using Il2CppScheduleOne.Dialogue;
using UnityEngine.Events;
#if IL2CPPBEPINEX || IL2CPPMELON
using Il2CppScheduleOne.Dialogue;
#elif MONOBEPINEX || MONOMELON
using ScheduleOne.Dialogue;
#endif

namespace S1API.Dialogues
{
    public static class DialogueChoiceListener
    {
        private static string expectedChoiceLabel;
        private static Action callback;

        public static void Register(DialogueHandler handlerRef, string label, Action action)
        {
            expectedChoiceLabel = label;
            callback = action;

            if (handlerRef != null)
            {
                void ForwardCall() => OnChoice();

                // ✅ IL2CPP-safe: explicit method binding via wrapper
                handlerRef.onDialogueChoiceChosen.AddListener((UnityAction<string>)delegate (string choice)
                {
                    if (choice == expectedChoiceLabel)
                        ((UnityAction)ForwardCall).Invoke();
                });
            }
        }

        private static void OnChoice()
        {
            callback?.Invoke();
            callback = null; // optional: remove if one-time use
        }
    }
}