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

namespace MigratorTool.WPF.View.Controls.Tree
{
    #region ==using==
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.ObjectModel;
    using MigratorTool.WPF.ViewModel.Common;

    #endregion

    public interface IMigrationTreeNodeModel
    {
        bool IsSelected { get; set; }
        bool IsLoaded { get; set; }
        bool IsExpanded { get; set; }
        bool IsChecked { get; set; }
        string Name { get; set; }
        string FullName { get; set; }
        MigrationTreeNodeLevel NodeLevel { get; set; }
        MigrationTreeNodeType NodeType { get; set; }
        ObservableCollection<MigrationTreeNodeModel> Children { get; set; }
    }

    public abstract class MigrationTreeNodeModel : ViewModelBase, IMigrationTreeNodeModel
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.RaisePropertyChangedEvent(() => this.IsSelected);
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                this.isExpanded = value;
                this.RaisePropertyChangedEvent(() => this.IsExpanded);
            }
        }

        private bool isLoaded;
        public bool IsLoaded
        {
            get { return this.isLoaded; }
            set
            {
                this.isLoaded = value;
                this.RaisePropertyChangedEvent(() => this.IsLoaded);
            }
        }

        protected bool m_IsChecked;
        public bool IsChecked
        {
            get { return this.m_IsChecked; }
            set
            {
                this.m_IsChecked = value;
                this.OnIsCheckedChanged(this);
                this.RaisePropertyChangedEvent(() => this.IsChecked);
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.RaisePropertyChangedEvent(() => this.Name);
            }
        }

        private string fullName;
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
                this.RaisePropertyChangedEvent(() => this.FullName);
            }
        }

        public bool IsSelectedAll
        {
            get
            {
                if (!this.IsChecked)
                {
                    return false;
                }
                else
                {
                    if (this.Children.Count > 0)
                    {
                        return CheckChildrenChecked(this.Children);
                    }
                }
                return true;
            }
        }

        private bool CheckChildrenChecked(ObservableCollection<MigrationTreeNodeModel> children)
        {
            foreach (var child in children)
            {
                if (!child.IsChecked)
                {
                    return false;
                }
                else
                {
                    if (child.Children.Count > 0)
                    {
                        return CheckChildrenChecked(child.Children);
                    }
                }
            }
            return true;
        }

        public void CheckedWithRaiseOrNot(bool @checked, bool raise)
        {
            if (raise)
            {
                this.IsChecked = @checked;
            }
            else
            {
                this.m_IsChecked = @checked;
            }
        }

        protected virtual void OnIsCheckedChanged(MigrationTreeNodeModel currentNode)
        {
            foreach (var item in currentNode.Children)
            {
                item.IsChecked = currentNode.IsChecked;
            }
        }

        protected ObservableCollection<MigrationTreeNodeModel> m_Children = new ObservableCollection<MigrationTreeNodeModel>();
        public ObservableCollection<MigrationTreeNodeModel> Children
        {
            get { return this.m_Children; }
            set
            {
                this.m_Children = value;
                this.RaisePropertyChangedEvent(() => this.Children);
            }
        }

        public MigrationTreeNodeLevel NodeLevel { get; set; }

        private MigrationTreeNodeType nodeType;
        public MigrationTreeNodeType NodeType
        {
            get
            {
                return this.nodeType;
            }
            set
            {
                this.nodeType = value;
                this.RaisePropertyChangedEvent(() => this.NodeType);
            }
        }

        public override String ToString()
        {
            return TextNode("", true);
        }

        /// <summary>
        /// 转换Tree型结构Text
        /// </summary>
        /// <param name="prefix">用于此节点之前的\t和连线</param>
        /// <param name="isLastChild">此节点是否是该层最后一个节点</param>
        /// <returns></returns>
        public String TextNode(String prefix, Boolean isLastChild)
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.Append(prefix + (isLastChild ? "└" : "├") + (this.IsChecked ? "√" : " ") + Name + " " + this.Children.Count + "\r\n");
            if (Children != null)
            {
                for (int i = 0; i < Children.Count - 1; i++)
                {
                    if (isLastChild)
                    {
                        textBuilder.Append((Children[i]).TextNode(prefix + "" + "\t", false));
                    }
                    else
                    {
                        textBuilder.Append((Children[i]).TextNode(prefix + "│" + "\t", false));
                    }
                }
                if (Children.Count > 0)
                {
                    if (isLastChild)
                    {
                        textBuilder.Append((Children[Children.Count - 1]).TextNode(prefix + "" + "\t", true));
                    }
                    else
                    {
                        textBuilder.Append((Children[Children.Count - 1]).TextNode(prefix + "│" + "\t", true));
                    }
                }
            }
            return textBuilder.ToString();
        }
    }

    public enum MigrationTreeNodeLevel
    {
        Unspecify = 0,

        LivelinkWorkspace,
        LivelinkProject,
        LivelinkList,
        LivelinkItem,
        NotesConnection,
        NotesServer
    }

    public enum MigrationTreeNodeType
    {
        Unspecify = 0,
        FileMigrationDriver = 1,
        FileMigrationFolder = 2,
        FileMigrationFile = 3,
        FileMigrationRootNode = 4,
        FileMigrationConnection = 5,

        PFMailFolder,
        PFContactFolder,
        PFTaskFolder,
        PFNoteFolder,
        PFJournalFolder,
        PFCalendarFolder,

        EMCCabinets,
        EMCCabinet,
        EMCFolder,
        EMCDocument,
        EMCSnapshot,
        EMCVirtualDocument,
        EMCRendition,

        eRoomCommunity,
        eRoomFacility,
        eRoomRoom,

        DatabaseTable,
        DatabaseColumn,
        DatabaseConnection,

        LivelinkEnterpriseWorkspace,
        LivelinkPersonalWorkspace,
        LivelinkProject,
        LivelinkDiscussion,
        LivelinkTopic,
        LivelinkReply,
        LivelinkTasklist,
        LivelinkTaskGroup,
        LivelinkTask,
        LivelinkMilestone,
        LivelinkChannel,
        LivelinkNews,
        LivelinkPoll,
        LivelinkCompoundDocument,
        LivelinkCompoundDocumentRelease,
        LivelinkCompoundDocumentRevision,
        LivelinkFolder,
        LivelinkAppearance,
        LivelinkAppearanceWorkspace,
        LivelinkXMLDocument,
        LivelinkEmailDocument,
        LivelinkDocument,
        LivelinkShortut,
        LivelinkGeneration,
        LivelinkCADDocument,
        LivelinkCategory,
        LivelinkUrl,
        LivelinkWorkflowMap,
        LivelinkCustomView,
        LivelinkWorkfolwStatus,
        LivelinkLiveReport,
        LivelinkProspector,

        LotusNotesDatabaseConnection,
        LotusNotesServer,
        LotusNotesLocal,

        QuickrServer,
        QuickrPlace,
        QuickrRoom,

    }
}