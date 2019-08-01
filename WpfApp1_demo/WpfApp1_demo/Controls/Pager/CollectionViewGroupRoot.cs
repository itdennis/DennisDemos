/********************************************************************
 *
 *  PROPRIETARY and CONFIDENTIAL
 *
 *  This file is licensed from, and is a trade secret of:
 *
 *                   AvePoint, Inc.
 *                   Harborside Financial Center
 *                   9th Fl.   Plaza Ten
 *                   Jersey City, NJ 07311
 *                   United States of America
 *                   Telephone: +1-800-661-6588
 *                   WWW: www.avepoint.com
 *
 *  Refer to your License Agreement for restrictions on use,
 *  duplication, or disclosure.
 *
 *  RESTRICTED RIGHTS LEGEND
 *
 *  Use, duplication, or disclosure by the Government is
 *  subject to restrictions as set forth in subdivision
 *  (c)(1)(ii) of the Rights in Technical Data and Computer
 *  Software clause at DFARS 252.227-7013 (Oct. 1988) and
 *  FAR 52.227-19 (C) (June 1987).
 *
 *  Copyright © 2001-2014 AvePoint® Inc. All Rights Reserved. 
 *
 *  Unpublished - All rights reserved under the copyright laws of the United States.
 *  $Revision:  $
 *  $Author:  $        
 *  $Date:  $
 */

namespace MigratorTool.WPF.View.Controls.Pager
{
    #region ==using==
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    #endregion

    /// <summary>
    /// PagedCollectionView classes use this class as the manager 
    /// of all Grouping functionality
    /// </summary>
    internal class CollectionViewGroupRoot : CollectionViewGroupInternal, INotifyCollectionChanged
    {
        #region Static Fields

        //------------------------------------------------------
        //
        //  Static/Constant Fields
        //
        //------------------------------------------------------

        /// <summary>
        /// String constant used for the Root Name
        /// </summary>
        private const string RootName = "Root";

        /// <summary>
        /// Private accessor for empty object instance
        /// </summary>
        private static readonly object UseAsItemDirectly = new object();

        /// <summary>
        /// Private accessor for the top level GroupDescription
        /// </summary>
        private static GroupDescription topLevelGroupDescription;

        #endregion Static Fields

        #region Private Fields

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        /// <summary>
        /// Private accessor for an ObservableCollection containing group descriptions
        /// </summary>
        private ObservableCollection<GroupDescription> _groupBy = new ObservableCollection<GroupDescription>();

        /// <summary>
        /// Indicates whether the list of items (after applying the sort and filters, if any) 
        /// is already in the correct order for grouping.
        /// </summary>
        private bool _isDataInGroupOrder;

        /// <summary>
        /// Private accessor for the owning ICollectionView
        /// </summary>
        private ICollectionView _view;

        #endregion Private Fields

        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the CollectionViewGroupRoot class.
        /// </summary>
        /// <param name="view">PagedCollectionView that contains this grouping</param>
        /// <param name="isDataInGroupOrder">True if items are already in correct order for grouping</param>
        internal CollectionViewGroupRoot(ICollectionView view, bool isDataInGroupOrder)
            : base(RootName, null)
        {
            this._view = view;
            this._isDataInGroupOrder = isDataInGroupOrder;
        }

        #endregion Constructors

        #region Events

        //------------------------------------------------------
        //
        //  Events
        //
        //------------------------------------------------------

        /// <summary>
        /// Raise this event when the (grouped) view changes
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Raise this event when the GroupDescriptions change
        /// </summary>
        internal event EventHandler GroupDescriptionChanged;

        #endregion Events

        #region Public Properties

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        /// <summary>
        /// Gets or sets a delegate to select the group description as a 
        /// function of the parent group and its level.
        /// </summary>
        public virtual GroupDescriptionSelectorCallback GroupBySelector { get; set; }

        /// <summary>
        /// Gets the description of grouping, indexed by level.
        /// </summary>
        public virtual ObservableCollection<GroupDescription> GroupDescriptions
        {
            get { return this._groupBy; }
        }

        #endregion Public Properties

        #region Internal Properties

        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        /// <summary>
        /// Gets or sets the current IComparer being used
        /// </summary>
        internal IComparer ActiveComparer { get; set; }

        /// <summary>
        /// Gets the culture to use during sorting.
        /// </summary>
        internal CultureInfo Culture
        {
            get
            {
                Debug.Assert(this._view != null, "this._view should have been set from the constructor");
                return this._view.Culture;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data is in group order
        /// </summary>
        internal bool IsDataInGroupOrder
        {
            get { return this._isDataInGroupOrder; }
            set { this._isDataInGroupOrder = value; }
        }

        #endregion Internal Properties

        #region Methods

        //------------------------------------------------------
        //
        //  Methods
        //
        //------------------------------------------------------

        /// <summary>
        /// Adds specified item to subgroups
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="loading">Whether we are currently loading</param>
        internal void AddToSubgroups(object item, bool loading)
        {
            this.AddToSubgroups(item, this, 0, loading);
        }

        /// <summary>
        /// Finds the index of the specified item
        /// </summary>
        /// <param name="item">Item we are looking for</param>
        /// <param name="seed">Seed of the item we are looking for</param>
        /// <param name="comparer">Comparer used to find the item</param>
        /// <param name="low">Low range of item index</param>
        /// <param name="high">High range of item index</param>
        /// <returns>Index of the specified item</returns>
        protected override int FindIndex(object item, object seed, IComparer comparer, int low, int high)
        {
            // root group needs to adjust the bounds of the search to exclude the new item (if any)
            IEditableCollectionView iecv = this._view as IEditableCollectionView;
            if (iecv != null && iecv.IsAddingNew)
            {
                --high;
            }

            return base.FindIndex(item, seed, comparer, low, high);
        }

        /// <summary>
        /// Initializes the group descriptions
        /// </summary>
        internal void Initialize()
        {
            if (topLevelGroupDescription == null)
            {
                topLevelGroupDescription = new TopLevelGroupDescription();
            }

            this.InitializeGroup(this, 0, null);
        }

        /// <summary>
        /// Inserts specified item into the collection
        /// </summary>
        /// <param name="index">Index to insert into</param>
        /// <param name="item">Item to insert</param>
        /// <param name="loading">Whether we are currently loading</param>
        internal void InsertSpecialItem(int index, object item, bool loading)
        {
            this.ChangeCounts(item, +1);
            this.ProtectedItems.Insert(index, item);

            if (!loading)
            {
                int globalIndex = this.LeafIndexFromItem(item, index);
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, globalIndex));
            }
        }

        /// <summary>
        /// Notify listeners that this View has changed
        /// </summary>
        /// <remarks>
        /// CollectionViews (and sub-classes) should take their filter/sort/grouping
        /// into account before calling this method to forward CollectionChanged events.
        /// </remarks>
        /// <param name="args">The NotifyCollectionChangedEventArgs to be passed to the EventHandler</param>
        public void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            Debug.Assert(args != null, "Arguments passed in should not be null");

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }

        /// <summary>
        /// Notify host that a group description has changed somewhere in the tree
        /// </summary>
        protected override void OnGroupByChanged()
        {
            if (this.GroupDescriptionChanged != null)
            {
                this.GroupDescriptionChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Remove specified item from subgroups
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>Whether the operation was successful</returns>
        internal bool RemoveFromSubgroups(object item)
        {
            return this.RemoveFromSubgroups(item, this, 0);
        }

        /// <summary>
        /// Remove specified item from subgroups using an exhaustive search
        /// </summary>
        /// <param name="item">Item to remove</param>
        internal void RemoveItemFromSubgroupsByExhaustiveSearch(object item)
        {
            this.RemoveItemFromSubgroupsByExhaustiveSearch(this, item);
        }

        /// <summary>
        /// Removes specified item into the collection
        /// </summary>
        /// <param name="index">Index to remove from</param>
        /// <param name="item">Item to remove</param>
        /// <param name="loading">Whether we are currently loading</param>
        internal void RemoveSpecialItem(int index, object item, bool loading)
        {
            Debug.Assert(Object.Equals(item, ProtectedItems[index]), "RemoveSpecialItem finds inconsistent data");
            int globalIndex = -1;

            if (!loading)
            {
                globalIndex = this.LeafIndexFromItem(item, index);
            }

            this.ChangeCounts(item, -1);
            this.ProtectedItems.RemoveAt(index);

            if (!loading)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, globalIndex));
            }
        }

        #endregion Methods

        #region Private Methods

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        /// <summary>
        /// Add an item to the subgroup with the given name
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="group">Group to add item to</param>
        /// <param name="level">The level of grouping.</param>
        /// <param name="name">Name of subgroup to add to</param>
        /// <param name="loading">Whether we are currently loading</param>
        private void AddToSubgroup(object item, CollectionViewGroupInternal group, int level, object name, bool loading)
        {
            CollectionViewGroupInternal subgroup;
            int index = (this._isDataInGroupOrder) ? group.LastIndex : 0;

            // find the desired subgroup
            for (int n = group.Items.Count; index < n; ++index)
            {
                subgroup = group.Items[index] as CollectionViewGroupInternal;
                if (subgroup == null)
                {
                    continue;           // skip children that are not groups
                }

                if (group.GroupBy.NamesMatch(subgroup.Name, name))
                {
                    group.LastIndex = index;
                    this.AddToSubgroups(item, subgroup, level + 1, loading);
                    return;
                }
            }

            // the item didn't match any subgroups.  Create a new subgroup and add the item.
            subgroup = new CollectionViewGroupInternal(name, group);
            this.InitializeGroup(subgroup, level + 1, item);


            if (loading)
            {
                group.Add(subgroup);
                group.LastIndex = index;
            }
            else
            {
                // using insert will find the correct sort index to
                // place the subgroup, and will default to the last
                // position if no ActiveComparer is specified
                group.Insert(subgroup, item, this.ActiveComparer);
            }

            this.AddToSubgroups(item, subgroup, level + 1, loading);
        }

        /// <summary>
        /// Add an item to the desired subgroup(s) of the given group
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="group">Group to add item to</param>
        /// <param name="level">The level of grouping</param>
        /// <param name="loading">Whether we are currently loading</param>
        private void AddToSubgroups(object item, CollectionViewGroupInternal group, int level, bool loading)
        {
            object name = this.GetGroupName(item, group.GroupBy, level);
            ICollection nameList;

            if (name == UseAsItemDirectly)
            {
                // the item belongs to the group itself (not to any subgroups)
                if (loading)
                {
                    group.Add(item);
                }
                else
                {
                    int localIndex = group.Insert(item, item, this.ActiveComparer);
                    int index = group.LeafIndexFromItem(item, localIndex);
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
                }
            }
            else if ((nameList = name as ICollection) == null)
            {
                // the item belongs to one subgroup
                this.AddToSubgroup(item, group, level, name, loading);
            }
            else
            {
                // the item belongs to multiple subgroups
                foreach (object o in nameList)
                {
                    this.AddToSubgroup(item, group, level, o, loading);
                }
            }
        }

        /// <summary>
        /// Returns the description of how to divide the given group into subgroups
        /// </summary>
        /// <param name="group">CollectionViewGroup to get group description from</param>
        /// <param name="level">The level of grouping</param>
        /// <returns>GroupDescription of how to divide the given group</returns>
        private GroupDescription GetGroupDescription(CollectionViewGroup group, int level)
        {
            GroupDescription result = null;
            if (group == this)
            {
                group = null;
            }

            if (result == null && this.GroupBySelector != null)
            {
                result = this.GroupBySelector(group, level);
            }

            if (result == null && level < this.GroupDescriptions.Count)
            {
                result = this.GroupDescriptions[level];
            }

            return result;
        }

        /// <summary>
        /// Get the group name(s) for the given item
        /// </summary>
        /// <param name="item">Item to get group name for</param>
        /// <param name="groupDescription">GroupDescription for the group</param>
        /// <param name="level">The level of grouping</param>
        /// <returns>Group names for the specified item</returns>
        private object GetGroupName(object item, GroupDescription groupDescription, int level)
        {
            if (groupDescription != null)
            {
                return groupDescription.GroupNameFromItem(item, level, this.Culture);
            }
            else
            {
                return UseAsItemDirectly;
            }
        }

        /// <summary>
        /// Initialize the given group
        /// </summary>
        /// <param name="group">Group to initialize</param>
        /// <param name="level">The level of grouping</param>
        /// <param name="seedItem">The seed item to compare with to see where to insert</param>
        private void InitializeGroup(CollectionViewGroupInternal group, int level, object seedItem)
        {
            // set the group description for dividing the group into subgroups
            GroupDescription groupDescription = this.GetGroupDescription(group, level);
            group.GroupBy = groupDescription;

            // create subgroups for each of the explicit names
            ObservableCollection<object> explicitNames =
                (groupDescription != null) ? groupDescription.GroupNames : null;
            if (explicitNames != null)
            {
                for (int k = 0, n = explicitNames.Count; k < n; ++k)
                {
                    CollectionViewGroupInternal subgroup = new CollectionViewGroupInternal(explicitNames[k], group);
                    this.InitializeGroup(subgroup, level + 1, seedItem);
                    group.Add(subgroup);
                }
            }

            group.LastIndex = 0;
        }

        /// <summary>
        /// Remove an item from the direct children of a group.
        /// </summary>
        /// <param name="group">Group to remove item from</param>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item could not be removed</returns>
        private bool RemoveFromGroupDirectly(CollectionViewGroupInternal group, object item)
        {
            int leafIndex = group.Remove(item, true);
            if (leafIndex >= 0)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, leafIndex));
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Remove an item from the subgroup with the given name.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="group">Group to remove item from</param>
        /// <param name="level">The level of grouping</param>
        /// <param name="name">Name of item to remove</param>
        /// <returns>Return true if the item was not in one of the subgroups it was supposed to be.</returns>
        private bool RemoveFromSubgroup(object item, CollectionViewGroupInternal group, int level, object name)
        {
            bool itemIsMissing = false;
            CollectionViewGroupInternal subgroup;

            // find the desired subgroup
            for (int index = 0, n = group.Items.Count; index < n; ++index)
            {
                subgroup = group.Items[index] as CollectionViewGroupInternal;
                if (subgroup == null)
                {
                    continue;           // skip children that are not groups
                }

                if (group.GroupBy.NamesMatch(subgroup.Name, name))
                {
                    if (this.RemoveFromSubgroups(item, subgroup, level + 1))
                    {
                        itemIsMissing = true;
                    }

                    return itemIsMissing;
                }
            }

            // the item didn't match any subgroups.  It should have.
            return true;
        }

        /// <summary>
        /// Remove an item from the desired subgroup(s) of the given group.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="group">Group to remove item from</param>
        /// <param name="level">The level of grouping</param>
        /// <returns>Return true if the item was not in one of the subgroups it was supposed to be.</returns>
        private bool RemoveFromSubgroups(object item, CollectionViewGroupInternal group, int level)
        {
            bool itemIsMissing = false;
            object name = this.GetGroupName(item, group.GroupBy, level);
            ICollection nameList;

            if (name == UseAsItemDirectly)
            {
                // the item belongs to the group itself (not to any subgroups)
                itemIsMissing = this.RemoveFromGroupDirectly(group, item);
            }
            else if ((nameList = name as ICollection) == null)
            {
                // the item belongs to one subgroup
                if (this.RemoveFromSubgroup(item, group, level, name))
                {
                    itemIsMissing = true;
                }
            }
            else
            {
                // the item belongs to multiple subgroups
                foreach (object o in nameList)
                {
                    if (this.RemoveFromSubgroup(item, group, level, o))
                    {
                        itemIsMissing = true;
                    }
                }
            }

            return itemIsMissing;
        }

        /// <summary>
        /// The item did not appear in one or more of the subgroups it
        /// was supposed to.  This can happen if the item's properties
        /// change so that the group names we used to insert it are
        /// different from the names used to remove it. If this happens,
        /// remove the item the hard way.
        /// </summary>
        /// <param name="group">Group to remove item from</param>
        /// <param name="item">Item to remove</param>
        private void RemoveItemFromSubgroupsByExhaustiveSearch(CollectionViewGroupInternal group, object item)
        {
            // try to remove the item from the direct children 
            // this function only returns true if it failed to remove from group directly
            // in which case we will step through and search exhaustively
            if (this.RemoveFromGroupDirectly(group, item))
            {
                // if that didn't work, recurse into each subgroup
                // (loop runs backwards in case an entire group is deleted)
                for (int k = group.Items.Count - 1; k >= 0; --k)
                {
                    CollectionViewGroupInternal subgroup = group.Items[k] as CollectionViewGroupInternal;
                    if (subgroup != null)
                    {
                        this.RemoveItemFromSubgroupsByExhaustiveSearch(subgroup, item);
                    }
                }
            }
        }

        #endregion Private Methods

        #region Private Classes

        //------------------------------------------------------
        //
        //  Private Classes
        //
        //------------------------------------------------------

        /// <summary>
        /// TopLevelGroupDescription class
        /// </summary>
        private class TopLevelGroupDescription : GroupDescription
        {
            /// <summary>
            /// Initializes a new instance of the TopLevelGroupDescription class.
            /// </summary>
            public TopLevelGroupDescription()
            {
            }

            /// <summary>
            /// We have to implement this abstract method, but it should never be called
            /// </summary>
            /// <param name="item">Item to get group name from</param>
            /// <param name="level">The level of grouping</param>
            /// <param name="culture">Culture used for sorting</param>
            /// <returns>We do not return a value here</returns>
            public override object GroupNameFromItem(object item, int level, System.Globalization.CultureInfo culture)
            {
                Debug.Assert(true, "We have to implement this abstract method, but it should never be called");
                return null;
            }
        }

        #endregion Private Classes
    }

    /// <summary>
    /// A CollectionViewGroupInternal, as created by a PagedCollectionView 
    /// according to a GroupDescription.
    /// </summary>
    internal class CollectionViewGroupInternal : CollectionViewGroup
    {
        #region Private Fields

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        /// <summary>
        /// GroupDescription used to define how to group the items
        /// </summary>
        private GroupDescription _groupBy;

        /// <summary>
        /// Parent group of this CollectionViewGroupInternal
        /// </summary>
        private CollectionViewGroupInternal _parentGroup;

        /// <summary>
        /// Used for detecting stale enumerators
        /// </summary>
        private int _version;

        #endregion Private Fields

        #region Constructors

        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the CollectionViewGroupInternal class.
        /// </summary>
        /// <param name="name">Name of the CollectionViewGroupInternal</param>
        /// <param name="parent">Parent node of the CollectionViewGroup</param>
        internal CollectionViewGroupInternal(object name, CollectionViewGroupInternal parent)
            : base(name)
        {
            this._parentGroup = parent;
        }

        #endregion Constructors

        #region Public Properties

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether this group 
        /// is at the bottom level (not further sub-grouped).
        /// </summary>
        public override bool IsBottomLevel
        {
            get { return this._groupBy == null; }
        }

        #endregion  Public Properties

        #region Internal Properties

        /// <summary>
        /// Gets or sets the number of items and groups in the subtree under this group
        /// </summary>
        [DefaultValue(1)]
        internal int FullCount { get; set; }

        /// <summary>
        /// Gets or sets how this group divides into subgroups
        /// </summary>
        internal GroupDescription GroupBy
        {
            get
            {
                return this._groupBy;
            }

            set
            {
                bool oldIsBottomLevel = this.IsBottomLevel;

                if (this._groupBy != null)
                {
                    ((INotifyPropertyChanged)this._groupBy).PropertyChanged -= new PropertyChangedEventHandler(this.OnGroupByChanged);
                }

                this._groupBy = value;

                if (this._groupBy != null)
                {
                    ((INotifyPropertyChanged)this._groupBy).PropertyChanged += new PropertyChangedEventHandler(this.OnGroupByChanged);
                }

                if (oldIsBottomLevel != this.IsBottomLevel)
                {
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsBottomLevel"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the most recent index where activity took place
        /// </summary>
        internal int LastIndex { get; set; }

        /// <summary>
        /// Gets the first item (leaf) added to this group.  If this can't be determined,
        /// DependencyProperty.UnsetValue.
        /// </summary>
        internal object SeedItem
        {
            get
            {
                if (this.ItemCount > 0 && (this.GroupBy == null || this.GroupBy.GroupNames.Count == 0))
                {
                    // look for first item, child by child
                    for (int k = 0, n = Items.Count; k < n; ++k)
                    {
                        CollectionViewGroupInternal subgroup = this.Items[k] as CollectionViewGroupInternal;
                        if (subgroup == null)
                        {
                            // child is an item - return it
                            return this.Items[k];
                        }
                        else if (subgroup.ItemCount > 0)
                        {
                            // child is a nonempty subgroup - ask it
                            return subgroup.SeedItem;
                        }
                        //// otherwise child is an empty subgroup - go to next child
                    }

                    // we shouldn't get here, but just in case...
                    return DependencyProperty.UnsetValue;
                }
                else
                {
                    // the group is empty, or it has explicit subgroups.
                    // In either case, we cannot determine the first item -
                    // it could have gone into any of the subgroups.
                    return DependencyProperty.UnsetValue;
                }
            }
        }

        #endregion Internal Properties

        #region Private Properties

        //------------------------------------------------------
        //
        //  Private Properties
        //
        //------------------------------------------------------

        /// <summary>
        /// Gets the parent node for this CollectionViewGroupInternal
        /// </summary>
        private CollectionViewGroupInternal Parent
        {
            get { return this._parentGroup; }
        }

        #endregion Private Properties

        #region Internal Methods

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        /// <summary>
        /// Adds the specified item to the collection
        /// </summary>
        /// <param name="item">Item to add</param>
        internal void Add(object item)
        {
            this.ChangeCounts(item, +1);
            this.ProtectedItems.Add(item);
        }

        /// <summary>
        /// Clears the collection of items
        /// </summary>
        internal void Clear()
        {
            this.ProtectedItems.Clear();
            this.FullCount = 1;
            this.ProtectedItemCount = 0;
        }

        /// <summary>
        /// Finds the index of the specified item
        /// </summary>
        /// <param name="item">Item we are looking for</param>
        /// <param name="seed">Seed of the item we are looking for</param>
        /// <param name="comparer">Comparer used to find the item</param>
        /// <param name="low">Low range of item index</param>
        /// <param name="high">High range of item index</param>
        /// <returns>Index of the specified item</returns>
        protected virtual int FindIndex(object item, object seed, IComparer comparer, int low, int high)
        {
            int index;

            if (comparer != null)
            {
                ListComparer listComparer = comparer as ListComparer;
                if (listComparer != null)
                {
                    // reset the IListComparer before each search. This cannot be done
                    // any less frequently (e.g. in Root.AddToSubgroups), due to the
                    // possibility that the item may appear in more than one subgroup.
                    listComparer.Reset();
                }

                CollectionViewGroupComparer groupComparer = comparer as CollectionViewGroupComparer;
                if (groupComparer != null)
                {
                    // reset the CollectionViewGroupComparer before each search. This cannot be done
                    // any less frequently (e.g. in Root.AddToSubgroups), due to the
                    // possibility that the item may appear in more than one subgroup.
                    groupComparer.Reset();
                }

                for (index = low; index < high; ++index)
                {
                    CollectionViewGroupInternal subgroup = this.ProtectedItems[index] as CollectionViewGroupInternal;
                    object seed1 = (subgroup != null) ? subgroup.SeedItem : this.ProtectedItems[index];
                    if (seed1 == DependencyProperty.UnsetValue)
                    {
                        continue;
                    }
                    if (comparer.Compare(seed, seed1) < 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                index = high;
            }

            return index;
        }

        /// <summary>
        /// Returns an enumerator over the leaves governed by this group
        /// </summary>
        /// <returns>Enumerator of leaves</returns>
        internal IEnumerator GetLeafEnumerator()
        {
            return new LeafEnumerator(this);
        }

        /// <summary>
        /// Insert a new item or subgroup and return its index.  Seed is a
        /// representative from the subgroup (or the item itself) that
        /// is used to position the new item/subgroup w.r.t. the order given
        /// by the comparer. (If comparer is null, just add at the end).
        /// </summary>
        /// <param name="item">Item we are looking for</param>
        /// <param name="seed">Seed of the item we are looking for</param>
        /// <param name="comparer">Comparer used to find the item</param>
        /// <returns>The index where the item was inserted</returns>
        internal int Insert(object item, object seed, IComparer comparer)
        {
            // never insert the new item/group before the explicit subgroups
            int low = (this.GroupBy == null) ? 0 : this.GroupBy.GroupNames.Count;
            int index = this.FindIndex(item, seed, comparer, low, ProtectedItems.Count);

            // now insert the item
            this.ChangeCounts(item, +1);
            ProtectedItems.Insert(index, item);

            return index;
        }

        /// <summary>
        /// Return the item at the given index within the list of leaves governed
        /// by this group
        /// </summary>
        /// <param name="index">Index of the leaf</param>
        /// <returns>Item at given index</returns>
        internal object LeafAt(int index)
        {
            for (int k = 0, n = this.Items.Count; k < n; ++k)
            {
                CollectionViewGroupInternal subgroup = this.Items[k] as CollectionViewGroupInternal;
                if (subgroup != null)
                {
                    // current item is a group - either drill in, or skip over
                    if (index < subgroup.ItemCount)
                    {
                        return subgroup.LeafAt(index);
                    }
                    else
                    {
                        index -= subgroup.ItemCount;
                    }
                }
                else
                {
                    // current item is a leaf - see if we're done
                    if (index == 0)
                    {
                        return this.Items[k];
                    }
                    else
                    {
                        index -= 1;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the index of the given item within the list of leaves governed
        /// by the full group structure.  The item must be a (direct) child of this
        /// group.  The caller provides the index of the item within this group,
        /// if known, or -1 if not.
        /// </summary>
        /// <param name="item">Item we are looking for</param>
        /// <param name="index">Index of the leaf</param>
        /// <returns>Number of items under that leaf</returns>
        internal int LeafIndexFromItem(object item, int index)
        {
            int result = 0;

            // accumulate the number of predecessors at each level
            for (CollectionViewGroupInternal group = this;
                    group != null;
                    item = group, group = group.Parent, index = -1)
            {
                // accumulate the number of predecessors at the level of item
                for (int k = 0, n = group.Items.Count; k < n; ++k)
                {
                    // if we've reached the item, move up to the next level
                    if ((index < 0 && Object.Equals(item, group.Items[k])) ||
                        index == k)
                    {
                        break;
                    }

                    // accumulate leaf count
                    CollectionViewGroupInternal subgroup = group.Items[k] as CollectionViewGroupInternal;
                    result += (subgroup == null) ? 1 : subgroup.ItemCount;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the index of the given item within the list of leaves governed
        /// by this group
        /// </summary>
        /// <param name="item">Item we are looking for</param>
        /// <returns>Number of items under that leaf</returns>
        internal int LeafIndexOf(object item)
        {
            int leaves = 0;         // number of leaves we've passed over so far
            for (int k = 0, n = Items.Count; k < n; ++k)
            {
                CollectionViewGroupInternal subgroup = Items[k] as CollectionViewGroupInternal;
                if (subgroup != null)
                {
                    int subgroupIndex = subgroup.LeafIndexOf(item);
                    if (subgroupIndex < 0)
                    {
                        leaves += subgroup.ItemCount;       // item not in this subgroup
                    }
                    else
                    {
                        return leaves + subgroupIndex;    // item is in this subgroup
                    }
                }
                else
                {
                    // current item is a leaf - compare it directly
                    if (Object.Equals(item, Items[k]))
                    {
                        return leaves;
                    }
                    else
                    {
                        leaves += 1;
                    }
                }
            }

            // item not found
            return -1;
        }

        /// <summary>
        /// The group's description has changed - notify parent 
        /// </summary>
        protected virtual void OnGroupByChanged()
        {
            if (this.Parent != null)
            {
                this.Parent.OnGroupByChanged();
            }
        }

        /// <summary>
        /// Removes the specified item from the collection
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="returnLeafIndex">Whether we want to return the leaf index</param>
        /// <returns>Leaf index where item was removed, if value was specified. Otherwise '-1'</returns>
        internal int Remove(object item, bool returnLeafIndex)
        {
            int index = -1;
            int localIndex = this.ProtectedItems.IndexOf(item);

            if (localIndex >= 0)
            {
                if (returnLeafIndex)
                {
                    index = this.LeafIndexFromItem(null, localIndex);
                }

                this.ChangeCounts(item, -1);
                this.ProtectedItems.RemoveAt(localIndex);
            }

            return index;
        }

        #endregion Internal Methods

        #region Internal Types

        /// <summary>
        /// This comparer is used to insert an item into a group in a position consistent
        /// with a given IList.  It only works when used in the pattern that FindIndex
        /// uses, namely first call Reset(), then call Compare(item, itemSequence) any number of
        /// times with the same item (the new item) as the first argument, and a sequence
        /// of items as the second argument that appear in the IList in the same sequence.
        /// This makes the total search time linear in the size of the IList.  (To give
        /// the correct answer regardless of the sequence of arguments would involve
        /// calling IndexOf and leads to O(N^2) total search time.) 
        /// </summary>
        internal class ListComparer : IComparer
        {
            /// <summary>
            /// Constructor for the ListComparer that takes
            /// in an IList.
            /// </summary>
            /// <param name="list">IList used to compare on</param>
            internal ListComparer(IList list)
            {
                this.ResetList(list);
            }

            /// <summary>
            /// Sets the index that we start comparing
            /// from to 0.
            /// </summary>
            internal void Reset()
            {
                this._index = 0;
            }

            /// <summary>
            /// Sets our IList to a new instance
            /// of a list being passed in and resets
            /// the index.
            /// </summary>
            /// <param name="list">IList used to compare on</param>
            internal void ResetList(IList list)
            {
                this._list = list;
                this._index = 0;
            }

            /// <summary>
            /// Compares objects x and y to see which one
            /// should appear first.
            /// </summary>
            /// <param name="x">The first object</param>
            /// <param name="y">The second object</param>
            /// <returns>-1 if x is less than y, +1 otherwise</returns>
            public int Compare(object x, object y)
            {
                if (Object.Equals(x, y))
                {
                    return 0;
                }

                // advance the index until seeing one x or y
                int n = (this._list != null) ? this._list.Count : 0;
                for (; this._index < n; ++this._index)
                {
                    object z = this._list[this._index];
                    if (Object.Equals(x, z))
                    {
                        return -1;  // x occurs first, so x < y
                    }
                    else if (Object.Equals(y, z))
                    {
                        return +1;  // y occurs first, so x > y
                    }
                }

                // if we don't see either x or y, declare x > y.
                // This has the effect of putting x at the end of the list.
                return +1;
            }

            private int _index;
            private IList _list;
        }

        /// <summary>
        /// This comparer is used to insert an item into a group in a position consistent
        /// with a given CollectionViewGroupRoot. We will only use this when dealing with
        /// a temporary CollectionViewGroupRoot that points to the correct grouping of the
        /// entire collection, and we have paging that requires us to keep the paged group
        /// consistent with the order of items in the temporary group.
        /// </summary>
        internal class CollectionViewGroupComparer : IComparer
        {
            /// <summary>
            /// Constructor for the CollectionViewGroupComparer that takes
            /// in an CollectionViewGroupRoot.
            /// </summary>
            /// <param name="group">CollectionViewGroupRoot used to compare on</param>
            internal CollectionViewGroupComparer(CollectionViewGroupRoot group)
            {
                this.ResetGroup(group);
            }

            /// <summary>
            /// Sets the index that we start comparing
            /// from to 0.
            /// </summary>
            internal void Reset()
            {
                this._index = 0;
            }

            /// <summary>
            /// Sets our group to a new instance of a
            /// CollectionViewGroupRoot being passed in
            /// and resets the index.
            /// </summary>
            /// <param name="group">CollectionViewGroupRoot used to compare on</param>
            internal void ResetGroup(CollectionViewGroupRoot group)
            {
                this._group = group;
                this._index = 0;
            }

            /// <summary>
            /// Compares objects x and y to see which one
            /// should appear first.
            /// </summary>
            /// <param name="x">The first object</param>
            /// <param name="y">The second object</param>
            /// <returns>-1 if x is less than y, +1 otherwise</returns>
            public int Compare(object x, object y)
            {
                if (Object.Equals(x, y))
                {
                    return 0;
                }

                // advance the index until seeing one x or y
                int n = (this._group != null) ? this._group.ItemCount : 0;
                for (; this._index < n; ++this._index)
                {
                    object z = this._group.LeafAt(this._index);
                    if (Object.Equals(x, z))
                    {
                        return -1;  // x occurs first, so x < y
                    }
                    else if (Object.Equals(y, z))
                    {
                        return +1;  // y occurs first, so x > y
                    }
                }

                // if we don't see either x or y, declare x > y.
                // This has the effect of putting x at the end of the list.
                return +1;
            }

            private int _index;
            private CollectionViewGroupRoot _group;
        }

        #endregion Internal Types

        #region Private Methods

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        /// <summary>
        /// Removes an empty group from the PagedCollectionView grouping
        /// </summary>
        /// <param name="group">Empty subgroup to remove</param>
        private static void RemoveEmptyGroup(CollectionViewGroupInternal group)
        {
            CollectionViewGroupInternal parent = group.Parent;

            if (parent != null)
            {
                GroupDescription groupBy = parent.GroupBy;
                int index = parent.ProtectedItems.IndexOf(group);

                // remove the subgroup unless it is one of the explicit groups
                if (index >= groupBy.GroupNames.Count)
                {
                    parent.Remove(group, false);
                }
            }
        }

        /// <summary>
        /// Update the item count of the CollectionViewGroup
        /// </summary>
        /// <param name="item">CollectionViewGroup to update</param>
        /// <param name="delta">Delta to change count by</param>
        protected void ChangeCounts(object item, int delta)
        {
            bool changeLeafCount = !(item is CollectionViewGroup);

            for (CollectionViewGroupInternal group = this;
                    group != null;
                    group = group._parentGroup)
            {
                group.FullCount += delta;
                if (changeLeafCount)
                {
                    group.ProtectedItemCount += delta;

                    if (group.ProtectedItemCount == 0)
                    {
                        RemoveEmptyGroup(group);
                    }
                }
            }

            unchecked
            {
                // this invalidates enumerators
                ++this._version;
            }
        }

        /// <summary>
        /// Handler for the GroupBy PropertyChanged event
        /// </summary>
        /// <param name="sender">CollectionViewGroupInternal whose GroupBy property changed</param>
        /// <param name="e">The args for the PropertyChanged event</param>
        private void OnGroupByChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnGroupByChanged();
        }

        #endregion Private Methods

        #region Private Classes

        //------------------------------------------------------
        //
        //  Private Classes
        //
        //------------------------------------------------------

        /// <summary>
        /// Enumerator for the leaves in the CollectionViewGroupInternal class.
        /// </summary>
        private class LeafEnumerator : IEnumerator
        {
            private object _current;   // current item
            private CollectionViewGroupInternal _group; // parent group
            private int _index;     // current index into Items
            private IEnumerator _subEnum;   // enumerator over current subgroup
            private int _version;   // parent group's version at ctor

            /// <summary>
            /// Initializes a new instance of the LeafEnumerator class.
            /// </summary>
            /// <param name="group">CollectionViewGroupInternal that uses the enumerator</param>
            public LeafEnumerator(CollectionViewGroupInternal group)
            {
                this._group = group;
                this.DoReset();  // don't call virtual Reset in ctor
            }

            /// <summary>
            /// Private helper to reset the enumerator
            /// </summary>
            private void DoReset()
            {
                Debug.Assert(this._group != null, "_group should have been initialized in constructor");
                this._version = this._group._version;
                this._index = -1;
                this._subEnum = null;
            }

            #region Implement IEnumerator

            /// <summary>
            /// Reset implementation for IEnumerator
            /// </summary>
            void IEnumerator.Reset()
            {
                this.DoReset();
            }

            /// <summary>
            /// MoveNext implementation for IEnumerator
            /// </summary>
            /// <returns>Returns whether the MoveNext operation was successful</returns>
            bool IEnumerator.MoveNext()
            {
                Debug.Assert(this._group != null, "_group should have been initialized in constructor");

                // check for invalidated enumerator
                if (this._group._version != this._version)
                {
                    throw new InvalidOperationException();
                }

                // move forward to the next leaf
                while (this._subEnum == null || !this._subEnum.MoveNext())
                {
                    // done with the current top-level item.  Move to the next one.
                    ++this._index;
                    if (this._index >= this._group.Items.Count)
                    {
                        return false;
                    }

                    CollectionViewGroupInternal subgroup = this._group.Items[this._index] as CollectionViewGroupInternal;
                    if (subgroup == null)
                    {
                        // current item is a leaf - it's the new Current
                        this._current = this._group.Items[this._index];
                        this._subEnum = null;
                        return true;
                    }
                    else
                    {
                        // current item is a subgroup - get its enumerator
                        this._subEnum = subgroup.GetLeafEnumerator();
                    }
                }

                // the loop terminates only when we have a subgroup enumerator
                // positioned at the new Current item
                this._current = this._subEnum.Current;
                return true;
            }

            /// <summary>
            /// Gets the current implementation for IEnumerator
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    Debug.Assert(this._group != null, "_group should have been initialized in constructor");

                    if (this._index < 0 || this._index >= this._group.Items.Count)
                    {
                        throw new InvalidOperationException();
                    }

                    return this._current;
                }
            }

            #endregion Implement IEnumerator
        }

        #endregion Private Classes
    }
}
