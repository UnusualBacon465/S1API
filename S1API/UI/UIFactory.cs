#if IL2CPP
using UnityEngine;
using UnityEngine.UI;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
#else
using UnityEngine;
using UnityEngine.UI;
#endif

using UnityEngine.Events;
using System.Collections.Generic;

namespace S1API.UI
{
    /// <summary>
    /// Utility class for creating and managing UI elements in Unity projects.
    /// </summary>
    /// <remarks>
    /// Provides static methods to dynamically generate UI components such as panels, buttons, text blocks, and layouts.
    /// Includes utilities for configuring and organizing UI elements in a hierarchy.
    /// </remarks>
    public static class UIFactory
    {
        /// Creates a UI panel with a background color and optional anchoring.
        /// <param name="name">The name of the GameObject representing the panel.</param>
        /// <param name="parent">The transform to which the panel will be parented.</param>
        /// <param name="bgColor">The background color of the panel.</param>
        /// <param name="anchorMin">The minimum anchor point of the RectTransform. Defaults to (0.5, 0.5) if not specified.</param>
        /// <param name="anchorMax">The maximum anchor point of the RectTransform. Defaults to (0.5, 0.5) if not specified.</param>
        /// <param name="fullAnchor">Whether to stretch the panel across the entire parent RectTransform. Overrides anchorMin and anchorMax if true.</param>
        /// <returns>The created GameObject representing the panel.</returns>
        public static GameObject Panel(string name, Transform parent, Color bgColor, Vector2? anchorMin = null, Vector2? anchorMax = null, bool fullAnchor = false)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();

            if (fullAnchor)
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
            else
            {
                rt.anchorMin = anchorMin ?? new Vector2(0.5f, 0.5f);
                rt.anchorMax = anchorMax ?? new Vector2(0.5f, 0.5f);
            }

            var img = go.AddComponent<Image>();
            img.color = bgColor;
            return go;
        }

        /// Creates a Text UI element with specified properties.
        /// <param name="name">The name of the GameObject to create for the text element.</param>
        /// <param name="content">The content of the text to display.</param>
        /// <param name="parent">The Transform to which the created text GameObject will be assigned.</param>
        /// <param name="fontSize">The font size of the text. Defaults to 14.</param>
        /// <param name="anchor">The alignment of the text within its RectTransform. Defaults to `TextAnchor.UpperLeft`.</param>
        /// <param name="style">The font style of the text. Defaults to `FontStyle.Normal`.</param>
        /// <returns>The created Text component with the specified properties applied.</returns>
        public static Text Text(string name, string content, Transform parent, int fontSize = 14, TextAnchor anchor = TextAnchor.UpperLeft, FontStyle style = FontStyle.Normal)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();

            var txt = go.AddComponent<Text>();
            txt.text = content;
            txt.fontSize = fontSize;
            txt.alignment = anchor;
            txt.fontStyle = style;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.color = Color.white;
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            return txt;
        }

        /// Creates a scrollable vertical list UI component with its child hierarchy configured for Unity UI.
        /// The created hierarchy includes:
        /// - A parent GameObject containing a ScrollRect component.
        /// - A child "Viewport" GameObject for clipping and masking.
        /// - A "Content" GameObject inside the viewport with a vertical layout and content size fitter.
        /// <param name="name">The name of the scrollable list GameObject.</param>
        /// <param name="parent">The parent transform where the scrollable list will be added.</param>
        /// <param name="scrollRect">Outputs the ScrollRect component associated with the created scrollable list.</param>
        /// <returns>Returns the RectTransform of the "Content" GameObject, which items can be added to.</returns>
        public static RectTransform ScrollableVerticalList(string name, Transform parent, out ScrollRect scrollRect)
        {
            var scrollGO = new GameObject(name);
            scrollGO.transform.SetParent(parent, false);
            var scrollRT = scrollGO.AddComponent<RectTransform>();
            scrollRT.anchorMin = Vector2.zero;
            scrollRT.anchorMax = Vector2.one;
            scrollRT.offsetMin = Vector2.zero;
            scrollRT.offsetMax = Vector2.zero;

            scrollRect = scrollGO.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;

            var viewport = new GameObject("Viewport");
            viewport.transform.SetParent(scrollGO.transform, false);
            var viewportRT = viewport.AddComponent<RectTransform>();
            viewportRT.anchorMin = Vector2.zero;
            viewportRT.anchorMax = Vector2.one;
            viewportRT.offsetMin = Vector2.zero;
            viewportRT.offsetMax = Vector2.zero;
            viewport.AddComponent<Image>().color = new Color(0, 0, 0, 0.05f);
            viewport.AddComponent<Mask>().showMaskGraphic = false;
            scrollRect.viewport = viewportRT;

            var content = new GameObject("Content");
            content.transform.SetParent(viewport.transform, false);
            var contentRT = content.AddComponent<RectTransform>();
            contentRT.anchorMin = new Vector2(0, 1);
            contentRT.anchorMax = new Vector2(1, 1);
            contentRT.pivot = new Vector2(0.5f, 1);

            var layout = content.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childControlHeight = true;
            layout.childForceExpandHeight = false;

            content.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.content = contentRT;
            return contentRT;
        }

        /// Adjusts the height of the content in the RectTransform to fit its preferred size.
        /// Ensures the vertical size of the content adapts to its children's preferred layout.
        /// Adds a ContentSizeFitter component if one is not already present on the specified content.
        /// <param name="content">The RectTransform whose height should be adjusted to fit its content.</param>
        public static void FitContentHeight(RectTransform content)
        {
            var fitter = content.gameObject.GetComponent<ContentSizeFitter>();
            if (fitter == null)
                fitter = content.gameObject.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        /// Creates a button with a label inside a parent UI element.
        /// <param name="name">The name of the button GameObject.</param>
        /// <param name="label">The text to display on the button.</param>
        /// <param name="parent">The Transform to which the button will be attached.</param>
        /// <param name="bgColor">The background color of the button.</param>
        /// <returns>A tuple containing the button's GameObject, Button component, and Text component.</returns>
        public static (GameObject, Button, Text) ButtonWithLabel(string name, string label, Transform parent, Color bgColor)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(160f, 40f);

            var img = go.AddComponent<Image>();
            img.color = bgColor;
            img.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            img.type = Image.Type.Sliced;

            var btn = go.AddComponent<Button>();
            btn.targetGraphic = img;

            var textGO = new GameObject("Label");
            textGO.transform.SetParent(go.transform, false);
            var textRT = textGO.AddComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = Vector2.zero;
            textRT.offsetMax = Vector2.zero;

            var txt = textGO.AddComponent<Text>();
            txt.text = label;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.fontSize = 16;
            txt.fontStyle = FontStyle.Bold;
            txt.color = Color.white;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            return (go, btn, txt);
        }

        /// <summary>
        /// Sets an icon as a child of the specified parent transform with the given sprite.
        /// </summary>
        /// <param name="sprite">The sprite to use for the icon.</param>
        /// <param name="parent">The transform to set as the parent of the icon.</param>
        public static void SetIcon(Sprite sprite, Transform parent)
        {
            var icon = new GameObject("Icon");
            icon.transform.SetParent(parent, false);

            var rt = icon.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var img = icon.AddComponent<Image>();
            img.sprite = sprite;
            img.preserveAspect = true;
        }

        /// Creates a text block consisting of a title, subtitle, and an optional completed status label.
        /// <param name="parent">The parent transform where the text block will be added.</param>
        /// <param name="title">The title text of the text block, displayed in bold.</param>
        /// <param name="subtitle">The subtitle text of the text block, displayed below the title.</param>
        /// <param name="isCompleted">
        /// A boolean indicating whether the text block represents a completed state.
        /// If true, an additional label indicating "Already Delivered" will be added.
        /// </param>
        public static void CreateTextBlock(Transform parent, string title, string subtitle, bool isCompleted)
        {
            Text(parent.name + "Title", title, parent, 16, TextAnchor.MiddleLeft, FontStyle.Bold);
            Text(parent.name + "Subtitle", subtitle, parent, 14, TextAnchor.UpperLeft);
            if (isCompleted)
                Text("CompletedLabel", "<color=#888888><i>Already Delivered</i></color>", parent, 12, TextAnchor.UpperLeft);
        }

        /// <summary>
        /// Adds a clickable button component to the specified game object and sets its interactions and event handling.
        /// </summary>
        /// <param name="go">The game object to which the button component is added.</param>
        /// <param name="clickHandler">The UnityAction to invoke when the button is clicked.</param>
        /// <param name="enabled">A boolean value indicating whether the button should be interactable.</param>
        public static void CreateRowButton(GameObject go, UnityAction clickHandler, bool enabled)
        {
            var btn = go.AddComponent<Button>();
            var img = go.GetComponent<Image>();
            btn.targetGraphic = img;
            btn.interactable = enabled;

            btn.onClick.AddListener(clickHandler);
        }

        /// Clears all child objects of the specified parent transform.
        /// <param name="parent">The transform whose child objects will be destroyed.</param>
        public static void ClearChildren(Transform parent)
        {
            foreach (Transform child in parent)
                GameObject.Destroy(child.gameObject);
        }

        /// Configures a GameObject to use a VerticalLayoutGroup with the specified spacing and padding.
        /// <param name="go">The GameObject to which a VerticalLayoutGroup will be added or configured.</param>
        /// <param name="spacing">The spacing between child objects within the VerticalLayoutGroup. Default is 10.</param>
        /// <param name="padding">The padding around the edges of the VerticalLayoutGroup. If null, a default RectOffset of (10, 10, 10, 10) will be used.</param>
        public static void VerticalLayoutOnGO(GameObject go, int spacing = 10, RectOffset padding = null)
        {
            var layout = go.AddComponent<VerticalLayoutGroup>();
            layout.spacing = spacing;
            layout.padding = padding ?? new RectOffset(10, 10, 10, 10);
        }

        /// <summary>
        /// Creates a quest row GameObject with a specific layout, including an icon panel and text panel.
        /// </summary>
        /// <param name="name">The name for the row GameObject.</param>
        /// <param name="parent">The parent Transform to attach the row GameObject to.</param>
        /// <param name="iconPanel">An output parameter that receives the generated icon panel GameObject.</param>
        /// <param name="textPanel">An output parameter that receives the generated text panel GameObject.</param>
        /// <returns>The newly created quest row GameObject.</returns>
        public static GameObject CreateQuestRow(string name, Transform parent, out GameObject iconPanel, out GameObject textPanel)
        {
            // Create the main row object
            var row = new GameObject("Row_" + name);
            row.transform.SetParent(parent, false);
            var rowRT = row.AddComponent<RectTransform>();
            rowRT.sizeDelta = new Vector2(0f, 90f); // Let layout handle width
            row.AddComponent<LayoutElement>().minHeight = 50f;
            row.AddComponent<Outline>().effectColor = new Color(0, 0, 0, 0.2f); // or Image line separator below
            
            
            var line = UIFactory.Panel("Separator", row.transform, new Color(1,1,1,0.05f));
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 1f);

            // Add background + target graphic
            var bg = row.AddComponent<Image>();
            bg.color = new Color(0.12f, 0.12f, 0.12f);

            var button = row.AddComponent<Button>();
            button.targetGraphic = bg;

            // Layout group
            var layout = row.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 20;
            layout.padding = new RectOffset(75, 10, 10, 10);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var rowLE = row.AddComponent<LayoutElement>();
            rowLE.minHeight = 90f;
            rowLE.flexibleWidth = 1;

            // Icon panel
            iconPanel = Panel("IconPanel", row.transform, new Color(0.12f, 0.12f, 0.12f));
            var iconRT = iconPanel.GetComponent<RectTransform>();
            iconRT.sizeDelta = new Vector2(80f, 80f);
            var iconLE = iconPanel.AddComponent<LayoutElement>();
            iconLE.preferredWidth = 80f;
            iconLE.preferredHeight = 80f;

            // Text panel
            textPanel = Panel("TextPanel", row.transform, Color.clear);
            VerticalLayoutOnGO(textPanel, spacing: 2);
            var textLE = textPanel.AddComponent<LayoutElement>();
            textLE.minWidth = 200f;
            textLE.flexibleWidth = 1;

            return row;
        }


        /// Binds an action to the accept button and updates its label text.
        /// <param name="btn">The button to bind the action to.</param>
        /// <param name="label">The text label of the button to update.</param>
        /// <param name="text">The new text to display on the label.</param>
        /// <param name="callback">The action to invoke when the button is clicked.</param>
        public static void BindAcceptButton(Button btn, Text label, string text, UnityAction callback)
        {
            label.text = text;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(callback);
        }
    }
}

/// <summary>
/// Represents a handler that encapsulates a callback action to be invoked when a click event occurs.
/// </summary>
public class ClickHandler
{
    /// <summary>
    /// A private field that stores the UnityAction delegate to be invoked when a click event occurs.
    /// </summary>
    private readonly UnityAction _callback;

    /// <summary>
    /// Handles click events by invoking a specified callback action.
    /// </summary>
    public ClickHandler(UnityAction callback)
    {
        _callback = callback;
    }

    /// <summary>
    /// Invokes the callback action associated with the click event.
    /// </summary>
    /// <remarks>
    /// This method triggers the Unity action passed during the initialization
    /// of the ClickHandler object. It serves as the mechanism to handle click
    /// events and execute the associated logic.
    /// </remarks>
    public void OnClick()
    {
        _callback.Invoke();
    }
}
