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

namespace MigratorTool.WPF.View.Controls.Wizard
{
    #region ==using==
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using AvePoint.Migrator.Common.Controls;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    class WizardImageConverter : IValueConverter
    {
        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = (StepButtonStatus)value;
            switch (status)
            {
                case StepButtonStatus.Selected: return new Uri(@"pack://application:,,,/Resource/Image/Controls/Wizard/selected_23x27.png");
                case StepButtonStatus.Normal: return new Uri(@"pack://application:,,,/Resource/Image/Controls/Wizard/dis_23x23.png");
                case StepButtonStatus.Configured: return new Uri(@"pack://application:,,,/Resource/Image/Controls/Wizard/finish_23x23.png");
                default: return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
