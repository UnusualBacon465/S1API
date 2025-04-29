﻿#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using UnityEngine;
using S1API.Internal.Abstraction;

namespace S1API.Items
{
    /// <summary>
    /// Represents an item definition in-game.
    /// Use this class to read and create new item definitions dynamically.
    /// </summary>
    public class ItemDefinition : IGUIDReference
    {
        /// <summary>
        /// INTERNAL: A reference to the native game item definition.
        /// </summary>
        internal S1ItemFramework.ItemDefinition S1ItemDefinition { get; }

        /// <summary>
        /// INTERNAL: Wraps an existing native item definition.
        /// </summary>
        internal ItemDefinition(S1ItemFramework.ItemDefinition definition)
        {
            S1ItemDefinition = definition;
        }

        /// <summary>
        /// The unique ID of this item.
        /// </summary>
        public string ID
        {
            get => S1ItemDefinition.ID;
            set => S1ItemDefinition.ID = value;
        }

        /// <summary>
        /// The display name for this item.
        /// </summary>
        public string Name
        {
            get => S1ItemDefinition.Name;
            set => S1ItemDefinition.Name = value;
        }

        /// <summary>
        /// A short description for this item.
        /// </summary>
        public string Description
        {
            get => S1ItemDefinition.Description;
            set => S1ItemDefinition.Description = value;
        }

        /// <summary>
        /// Stack limit for this item (max quantity per slot).
        /// </summary>
        public int StackLimit
        {
            get => S1ItemDefinition.StackLimit;
            set => S1ItemDefinition.StackLimit = value;
        }

        /// <summary>
        /// The category for inventory sorting.
        /// </summary>
        public ItemCategory Category
        {
            get => (ItemCategory)S1ItemDefinition.Category;
            set => S1ItemDefinition.Category = (S1ItemFramework.EItemCategory)value;
        }

        /// <summary>
        /// The icon for this item.
        /// </summary>
        public Sprite Icon
        {
            get => S1ItemDefinition.Icon;
            set => S1ItemDefinition.Icon = value;
        }

        /// <summary>
        /// Whether this item is available in the demo version of the game.
        /// </summary>
        public bool AvailableInDemo
        {
            get => S1ItemDefinition.AvailableInDemo;
            set => S1ItemDefinition.AvailableInDemo = value;
        }

        /// <summary>
        /// Legal status of the item (e.g., illegal drugs).
        /// </summary>
        public LegalStatus LegalStatus
        {
            get => (LegalStatus)S1ItemDefinition.legalStatus;
            set => S1ItemDefinition.legalStatus = (S1ItemFramework.ELegalStatus)value;
        }


        /// <summary>
        /// The color of the label shown in UI.
        /// </summary>
        public Color LabelDisplayColor
        {
            get => S1ItemDefinition.LabelDisplayColor;
            set => S1ItemDefinition.LabelDisplayColor = value;
        }

        /// <summary>
        /// Any keywords used to filter/search this item.
        /// </summary>
        public string[] Keywords
        {
            get => S1ItemDefinition.Keywords;
            set => S1ItemDefinition.Keywords = value;
        }

        /// <summary>
        /// Creates a new item instance with the specified quantity.
        /// </summary>
        public virtual ItemInstance CreateInstance(int quantity = 1)
        {
            var inst = S1ItemDefinition.GetDefaultInstance(quantity);
            return new ItemInstance(inst);
        }


        public string GUID => ID;

        public override bool Equals(object? obj) =>
            obj is ItemDefinition other && S1ItemDefinition == other.S1ItemDefinition;

        public override int GetHashCode() =>
            S1ItemDefinition?.GetHashCode() ?? 0;

        public static bool operator ==(ItemDefinition? a, ItemDefinition? b) =>
            ReferenceEquals(a, b) || (a is not null && b is not null && a.S1ItemDefinition == b.S1ItemDefinition);

        public static bool operator !=(ItemDefinition? a, ItemDefinition? b) =>
            !(a == b);
    }

    /// <summary>
    /// Represents the legal status of an item (e.g., legal or illegal).
    /// </summary>
    public enum LegalStatus
    {
        Legal,
        Illegal,
        // More if needed
    }
}
