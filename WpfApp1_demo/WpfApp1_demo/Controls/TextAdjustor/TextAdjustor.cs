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

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using MigratorTool.WPF.View.Common.StaticResource;

namespace AvePoint.Migrator.Common.Controls
{
    public class TextAdjustor : ItemsControl
    {
        #region == Members ==

        private const string LEFT_BRACE = "{";
        private const string RIGHT_BRACE = "}";
        private const string CHINESE_JAPENESE_REGEX = "^[\u2E80-\u9FFF]+$";
        private const string THAI_REGEX = "^[\u0E00-\u0E7F]+$";

        private TextAdjustorPanel xAStackPanel;

        #endregion == Members ==

        #region == Init ==

        static TextAdjustor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextAdjustor), new FrameworkPropertyMetadata(typeof(TextAdjustor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            xAStackPanel = this.GetTemplateChild("xAStackPanel") as TextAdjustorPanel;
            InitChildren();
        }

        private void InitChildren()
        {
            if (xAStackPanel == null)
            {
                return;
            }
            if (DesignTimeEnvironment.DesignTime)
            {
                return;
            }

            xAStackPanel.Children.Clear();

            string format = FormatStringTrim ? FormatString.Trim() : FormatString;
            if (string.IsNullOrEmpty(format))
            {
                return;
            }

            int startBracePos;
            int endBracePos;
            string indexStr;
            int index;

            while (true)
            {
                startBracePos = format.IndexOf(LEFT_BRACE ,System.StringComparison.CurrentCultureIgnoreCase);
                endBracePos = format.IndexOf(RIGHT_BRACE, System.StringComparison.CurrentCultureIgnoreCase);

                // 没有“{n}”，截取字符串，结束
                if (startBracePos < 0 || endBracePos < 0)
                {
                    if (startBracePos >= 0 || endBracePos >= 0)
                    {
                        // No need to be internationalized
                        Debug.Assert(false, "An Error occured while parsing \"{ n }\".");
                    }
                    ToTextBlocks(format);
                    break;
                }
                // 起始为Content，插入，截取
                else if (startBracePos == 0)
                {
                    indexStr = format.Substring(startBracePos + 1, endBracePos - startBracePos - 1);
                    if (Items.Count == 0)
                    {
                        // Nothing to do, just wait for Substring()
                    }
                    else if (int.TryParse(indexStr.Trim(), out index))
                    {
                        // 容错：如果翻译过程中出现index问题，如：index大于Count，或者同index多次插入
                        // 这种时候显示为空格
                        if (index < this.Items.Count && xAStackPanel.Children.IndexOf(this.Items[index] as FrameworkElement) < 0)
                        {
                            FrameworkElement item = this.Items[index] as FrameworkElement;
                            this.RemoveLogicalChild(item);
                            xAStackPanel.Children.Add(item);
                        }
                        else
                        {
                            xAStackPanel.Children.Add(new TextBlock() { Text = " " });
                        }
                    }
                    else
                    {
                        // No need to be internationalized
                        Debug.Assert(false, "An Error occured while parsing \"{ n }\".");

                        ToTextBlocks(LEFT_BRACE + indexStr + RIGHT_BRACE);
                    }
                    format = format.Substring(endBracePos + 1);
                }
                else
                {
                    ToTextBlocks(format.Substring(0, startBracePos));
                    format = format.Substring(startBracePos);
                }
            }
        }

        #endregion == Init ==

        #region == Properties ==

        #region ==FormatString==
        /// <summary>
        /// 获取或设置需要进行国际化的语句。
        /// <para>可以使用{n}的语法作为控件的占位符，并将控件添加到<see cref="System.Windows.Controls.ItemsControl.Items"/>属性中；
        /// 其中，n为大于等于0的整数，代表<see cref="System.Windows.Controls.ItemsControl.Items"/>中的索引所对应的控件。</para>
        /// </summary>
        /// <remarks>
        /// 关于<see cref="Items"/>属性，请参考Silverlight文档中关于<see cref="System.Windows.Controls.ItemsControl"/>类中<see cref="System.Windows.Controls.ItemsControl.Items"/>属性的描述，
        /// 在此就不再赘述。
        /// </remarks>
        public string FormatString
        {
            get { return (string)GetValue(FormatStringProperty); }
            set { SetValue(FormatStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatStringProperty =
            DependencyProperty.Register("FormatString", typeof(string), typeof(TextAdjustor), new PropertyMetadata(string.Empty));

        #endregion ==FormatString==

        #region ==Orientation==
        /// <summary>
        /// Gets or sets the dimension by which child elements are stacked.
        /// </summary>
        public System.Windows.Controls.Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(TextAdjustor), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal));

        #endregion ==Orientation

        #region ==TextWrapping==

        /// <summary>
        /// 获取或设置国际化后的文字与控件组成的句子是否在横向显示不全的时候进行换行
        /// </summary>
        public ElementWrapping TextWrapping
        {
            get { return (ElementWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextWrapping.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(ElementWrapping), typeof(TextAdjustor), new PropertyMetadata(ElementWrapping.NoWrap));
        
        #endregion ==TextWrapping==

        #region ==FormatStringTrim==
        /// <summary>
        /// 获取或设置是否对<see cref="FormatString"/>的文字始末的空格进行清除。通常在<see cref="FormatString"/>的句子是以控件开头的情况下使用。
        /// </summary>
        public bool FormatStringTrim
        {
            get { return (bool)GetValue(FormatStringTrimProperty); }
            set { SetValue(FormatStringTrimProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatStringTrim.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatStringTrimProperty =
            DependencyProperty.Register("FormatStringTrim", typeof(bool), typeof(TextAdjustor), new PropertyMetadata(false));

        #endregion ==FormatStringTrim

        #region ==VerticalAlignmentInLine==

        /// <summary>
        /// 获取或设置一个值，该值表示在某一行的所有元素高度不一致时，控制高度比较小的元素相对于行高的内部位置。
        /// </summary>
        public VerticalAlignmentInLine VerticalAlignmentInLine
        {
            get { return (VerticalAlignmentInLine)GetValue(VerticalAlignmentInLineProperty); }
            set { SetValue(VerticalAlignmentInLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalAlignmentInLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalAlignmentInLineProperty =
            DependencyProperty.Register("VerticalAlignmentInLine", typeof(VerticalAlignmentInLine), typeof(TextAdjustor), new PropertyMetadata(VerticalAlignmentInLine.Center));

        #endregion ==VerticalAlignmentInLine==

        #region ==LineMinHeight==

        /// <summary>
        /// 获取或设置每行的最小高度
        /// </summary>
        public double LineMinHeight
        {
            get { return (double)GetValue(LineMinHeightProperty); }
            set { SetValue(LineMinHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineMinHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineMinHeightProperty =
            DependencyProperty.Register("LineMinHeight", typeof(double), typeof(TextAdjustor), new PropertyMetadata(double.NaN));

        #endregion ==LineMinHeight==

        #region ==HelpText==

        /// <summary>
        /// 获取或设置<see cref="AUITextAdjustor"/>中普通文字信息在有鼠标悬浮的时候所读取的信息。
        /// </summary>
        public string HelpText
        {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HelpText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HelpTextProperty =
            DependencyProperty.Register("HelpText", typeof(string), typeof(TextAdjustor), new PropertyMetadata("", OnHelpTextChanged));

        private static void OnHelpTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextAdjustor self = d as TextAdjustor;
            if (self != null)
            {
                if (self.xAStackPanel != null && self.xAStackPanel.Children.Count > 0)
                {
                    foreach (var t in self.xAStackPanel.Children)
                    {
                        if (t as TextBlock != null)
                        {
                            (t as TextBlock).SetValue(AutomationProperties.HelpTextProperty, e.NewValue.ToString());
                        }
                    }
                }
            }
        }

        #endregion ==HelpText==

        #endregion == Properties ==

        #region == Methods ==

        private void ToTextBlocks(string str)
        {
            if (string.IsNullOrEmpty(str) || xAStackPanel == null)
            {
                return;
            }
            else
            {
                string word = "";
                char[] chars = str.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    // 空格与之前的字母语言组成一个TextBlock
                    if (' ' == chars[i])
                    {
                        xAStackPanel.Children.Add(ToTextBlock(word + chars[i]));
                        word = "";
                    }
                    // 方块字(或泰语)每一个字本身为一个TextBlock
                    else if (Regex.IsMatch(chars[i].ToString(), CHINESE_JAPENESE_REGEX) || Regex.IsMatch(chars[i].ToString(), THAI_REGEX))
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            xAStackPanel.Children.Add(ToTextBlock(word));
                            word = "";
                        }
                        xAStackPanel.Children.Add(ToTextBlock(chars[i]));
                    }
                    // 字母类型的语言，一个单词+空格为一个TextBlock
                    else
                    {
                        word += chars[i];
                    }
                }

                if (!string.IsNullOrEmpty(word))
                {
                    xAStackPanel.Children.Add(ToTextBlock(word));
                }
            }
        }

        private TextBlock ToTextBlock(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            TextBlock tb = new TextBlock()
            {
                Text = str,
                Foreground = this.Foreground,
                FontFamily = this.FontFamily,
                FontSize = this.FontSize,
                FontWeight = this.FontWeight,
                TextWrapping=this.TextWrapping==ElementWrapping.Wrap? System.Windows.TextWrapping.Wrap:System.Windows.TextWrapping.NoWrap,
                Height = this.LineMinHeight
            };
            tb.SetValue(AutomationProperties.NameProperty, this.HelpText);
            return tb;
        }

        private TextBlock ToTextBlock(char c)
        {
            return ToTextBlock(c.ToString());
        }

        #endregion == Methods ==
    }
}
