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

using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates"), TemplateVisualState(Name = "Disabled", GroupName = "CommonStates"), TemplateVisualState(Name = "Focused", GroupName = "FocusStates"), TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates"), TemplateVisualState(Name = "Pressed", GroupName = "CommonStates"), TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
    public class HyperlinkButton : ButtonBase
    {
        // Fields
        public static readonly DependencyProperty NavigateUriProperty = DependencyProperty.Register("NavigateUri", typeof(Uri), typeof(HyperlinkButton), null);
        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("TargetName", typeof(string), typeof(HyperlinkButton), null);

        // Methods
        static HyperlinkButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HyperlinkButton), new FrameworkPropertyMetadata(typeof(HyperlinkButton)));
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!base.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Disabled", useTransitions);
            }
            else if (base.IsPressed)
            {
                VisualStateManager.GoToState(this, "Pressed", useTransitions);
            }
            else if (base.IsMouseOver)
            {
                VisualStateManager.GoToState(this, "MouseOver", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            if (base.IsFocused && base.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Focused", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unfocused", useTransitions);
            }
        }

        protected override void OnClick()
        {
            if (AutomationPeer.ListenerExists(AutomationEvents.InvokePatternOnInvoked))
            {
                AutomationPeer orCreateAutomationPeer = OnCreateAutomationPeer();
                if (orCreateAutomationPeer != null)
                {
                    orCreateAutomationPeer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                }
            }
            base.OnClick();
            if (!string.IsNullOrEmpty(this.NavigateUrl) && !DesignerProperties.GetIsInDesignMode(this))
            {
                System.Diagnostics.Process.Start(this.NavigateUrl);
            }
        }

        public void FireClick()
        {
            this.OnClick();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new HyperlinkButtonAutomationPeer(this);
        }

        [SecuritySafeCritical]
        private bool ShouldVerifyThisIsUserInitiatedAction()
        {
            HyperlinkButtonAutomationPeer automationPeer = OnCreateAutomationPeer() as HyperlinkButtonAutomationPeer;
            return ((automationPeer == null) || !automationPeer.InUIAutomationInvoke);
        }

        // Properties
        public string NavigateUrl
        {
            get { return (string)GetValue(NavigateUrlProperty); }
            set { SetValue(NavigateUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NavigateUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateUrlProperty =
            DependencyProperty.Register("NavigateUrl", typeof(string), typeof(HyperlinkButton), new PropertyMetadata(string.Empty));

        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextWrapping.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(HyperlinkButton), new PropertyMetadata(TextWrapping.NoWrap));

        public Brush UnderlineStroke
        {
            get { return (Brush)GetValue(UnderlineStrokeProperty); }
            set { SetValue(UnderlineStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnderlineStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnderlineStrokeProperty =
            DependencyProperty.Register("UnderlineStroke", typeof(Brush), typeof(HyperlinkButton), new PropertyMetadata(null));

        public Brush UnderlineClickStroke
        {
            get { return (Brush)GetValue(UnderlineClickStrokeProperty); }
            set { SetValue(UnderlineClickStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnderlineClickStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnderlineClickStrokeProperty =
            DependencyProperty.Register("UnderlineClickStroke", typeof(Brush), typeof(HyperlinkButton), new PropertyMetadata(null));

        public TextTrimming TextTrimming
        {
            get { return (TextTrimming)GetValue(TextTrimmingProperty); }
            set { SetValue(TextTrimmingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextTrimming.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextTrimmingProperty =
            DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(HyperlinkButton), new PropertyMetadata(TextTrimming.WordEllipsis));
    }

    public class HyperlinkButtonAutomationPeer : ButtonBaseAutomationPeer
    {
        // Fields
        private bool inUIAutomationInvoke;

        // Methods
        public HyperlinkButtonAutomationPeer(HyperlinkButton owner)
            : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Hyperlink;
        }

        protected override string GetClassNameCore()
        {
            return "Hyperlink";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Invoke)
            {
                return this;
            }
            return null;
        }

        protected override bool IsControlElementCore()
        {
            return true;
        }

        // Properties
        internal bool InUIAutomationInvoke
        {
            [SecurityCritical]
            get
            {
                return this.inUIAutomationInvoke;
            }
            [SecurityCritical]
            set
            {
                this.inUIAutomationInvoke = value;
            }
        }
    }
}
