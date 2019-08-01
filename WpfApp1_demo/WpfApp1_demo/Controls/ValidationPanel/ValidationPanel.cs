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

using System.Windows;
using System.Windows.Controls;
using MigratorTool.WPF.I18N;
using System.Diagnostics.CodeAnalysis;

namespace AvePoint.Migrator.Common.Controls
{
    public class ValidationPanel : ContentControl
    {
        #region == 常量 ==

        /// <summary>
        /// ErrorTextBlock部件的名称
        /// </summary>
        private const string PartErrorTextBlock = "ErrorTextBlock";

        /// <summary>
        /// ValidationStates状态组的名称
        /// </summary>
        private const string VSMGroup_ValidationStates = "ValidationStates";

        /// <summary>
        /// Error状态的名称
        /// </summary>
        private const string VSMState_Error = "Error";

        /// <summary>
        /// Right状态的名称
        /// </summary>
        private const string VSMState_Right = "Right";

        /// <summary>
        /// Warning状态的名称
        /// </summary>
        private const string VSMState_Warning = "Warning";

        /// <summary>
        /// Message状态对应的名称
        /// </summary>
        private const string VSMState_Message = "Message";

        #endregion

        #region == Properties ==

        #region == 添加图标读屏内容的国际化 by ftan ==

        /// <summary>
        /// 设置获取Error图标的读屏信息(国际化)
        /// </summary>
        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(string), typeof(ValidationPanel), new PropertyMetadata(I18NEntity.Get("Common_669f9b92_6406_b912_97e5_09bee74a4d8d", "error")));


        /// <summary>
        /// 设置获取Right图标的读屏信息(国际化)
        /// </summary>
        public string Right
        {
            get { return (string)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Right.  This enables animation, styling, binding, etc...
        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(string), typeof(ValidationPanel), new PropertyMetadata(I18NEntity.Get("Common_d6db5944_6f66_72fc_b9f6_ae0410e42192", "right")));


        /// <summary>
        /// 设置获取Warning图标的读屏信息(国际化)
        /// </summary>
        public string Warning
        {
            get { return (string)GetValue(WarningProperty); }
            set { SetValue(WarningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Warning.  This enables animation, styling, binding, etc...
        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty WarningProperty =
            DependencyProperty.Register("Warning", typeof(string), typeof(ValidationPanel), new PropertyMetadata(I18NEntity.Get("Common_c1cdd44e_49b7_6bb7_7895_9bd8f1356069", "warning")));


        /// <summary>
        /// 设置获取Message图标的读屏信息(国际化)
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ValidationPanel), new PropertyMetadata(I18NEntity.Get("Common_3556bdd7_f08d_15ec_b771_922d6f6d3cd9", "message")));
        
        #endregion

        public string ValidationInfo
        {
            get { return (string)GetValue(ValidationInfoProperty); }
            set
            {
                SetValue(ValidationInfoProperty, value);
                if (IsAutoVPAT && mPartErrorTextBlock != null && !string.Empty.Equals(value)) //VPAT支持 自动读屏  by ftan
                {
                    //mPartErrorTextBlock.Focus();
                }
            }
        }

        // Using a DependencyProperty as the backing store for ValidationInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidationInfoProperty =
            DependencyProperty.Register("ValidationInfo", typeof(string), typeof(ValidationPanel), new PropertyMetadata(string.Empty, OnValidationInfoChanged));

        private static void OnValidationInfoChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ValidationPanel current = obj as ValidationPanel;

            if (current != null)
            {
                if (string.IsNullOrEmpty(args.NewValue as string))
                {
                    current.Visibility = Visibility.Collapsed;
                }
                else
                {
                    current.Visibility = Visibility.Visible;
                    if (current.IsAutoVPAT && current.mPartErrorTextBlock != null && !string.Empty.Equals(args.NewValue as string))
                    {
                        //current.mPartErrorTextBlock.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// 设置获取是否支持自动读取提示信息 by ftan
        /// </summary>
        public bool IsAutoVPAT
        {
            get { return (bool)GetValue(IsAutoVPATProperty); }
            set { SetValue(IsAutoVPATProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoVPAT.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoVPATProperty =
            DependencyProperty.Register("IsAutoVPAT", typeof(bool), typeof(ValidationPanel), new PropertyMetadata(true));

        public ValidationType ValidationStatus
        {
            get { return (ValidationType)GetValue(ValidationStatusProperty); }
            set { SetValue(ValidationStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValidationStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidationStatusProperty =
            DependencyProperty.Register("ValidationStatus", typeof(ValidationType), typeof(ValidationPanel), new PropertyMetadata(ValidationType.Error, OnValidationStatusChanged));

        private static void OnValidationStatusChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ValidationPanel current = obj as ValidationPanel;

            if (current != null && current.mPartErrorTextBlock != null)
            {
                switch ((ValidationType)args.NewValue)
                {
                    case ValidationType.Error:
                        current.GoToValidationPanelState(VSMState_Error);
                        break;
                    case ValidationType.Right:
                        current.GoToValidationPanelState(VSMState_Right);
                        break;
                    case ValidationType.Warning:
                        current.GoToValidationPanelState(VSMState_Warning);
                        break;
                    case ValidationType.Message:
                        current.GoToValidationPanelState(VSMState_Message);
                        break;
                    default:
                        current.GoToValidationPanelState(VSMState_Error);
                        break;
                }
            }
        }

        #endregion

        #region == Members ==

        /// <summary>
        /// ValidationImage部件对应的成员
        /// </summary>
        private Image mPartValidationImage;

        /// <summary>
        /// ErrorTextBlock部件对应的成员
        /// </summary>
        private TextBlock mPartErrorTextBlock;

        #endregion

        #region == Init ==

        public ValidationPanel()
        {
            DefaultStyleKey = typeof(ValidationPanel);
            IsTabStop = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.mPartErrorTextBlock = GetTemplateChild("ErrorTextBlock") as TextBlock;
            InitializeTemplatePart();

            SetCurrentState();
        }

        #endregion

        #region == Method ==

        /// <summary>
        /// 初始化部件
        /// </summary>
        private void InitializeTemplatePart()
        {
            mPartErrorTextBlock = GetTemplateChild(PartErrorTextBlock) as TextBlock;
            if (IsAutoVPAT && mPartErrorTextBlock != null && !string.Empty.Equals(ValidationInfo)) //防止初始状态Visibility为Collapsed不初始化，读屏失败  by ftan
            {
                //mPartErrorTextBlock.Focus();
            }
        }

        /// <summary>
        /// 将控件设置成state所代表的状态
        /// </summary>
        /// <param name="state">要设置的状态</param>
        private void GoToValidationPanelState(string state)
        {
            VisualStateManager.GoToState(this, state, false);
        }

        /// <summary>
        /// 设置控件的的状态
        /// </summary>
        private void SetCurrentState()
        {
            if (mPartErrorTextBlock != null)
            {
                switch (ValidationStatus)
                {
                    case ValidationType.Error:
                        GoToValidationPanelState(VSMState_Error);
                        break;
                    case ValidationType.Right:
                        GoToValidationPanelState(VSMState_Right);
                        break;
                    case ValidationType.Warning:
                        GoToValidationPanelState(VSMState_Warning);
                        break;
                    case ValidationType.Message:
                        GoToValidationPanelState(VSMState_Message);
                        break;
                    default:
                        GoToValidationPanelState(VSMState_Error);
                        break;
                }

            }
        }

        #endregion
    }

    public enum ValidationType : int
    {
        Error,
        Right,
        Warning,
        Message
    }
}
