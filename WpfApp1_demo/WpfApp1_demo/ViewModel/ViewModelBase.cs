using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfApp1_demo.Extension;
using WpfApp1_demo.Logger;

namespace WpfApp1_demo.ViewModel
{
    public abstract class ViewModelBase : Observable
    {
        protected void RaiseEvent(EventHandler @event, EventArgs args)
        {
            if (@event != null)
            {
                @event(this, args);
            }
        }

        protected void RaiseEvent<T>(EventHandler<T> @event, T args) where T : EventArgs
        {
            if (@event != null)
            {
                @event(this, args);
            }
        }

        #region ==提供三个重用发布事件的重载函数==
        protected void RaiseEvent(Action action)
        {
            if (action != null)
            {
                action.Execute();
            }
        }

        protected void RaiseEvent<T>(Action<T> action, T args)
        {
            if (action != null)
            {
                action.Execute(args);
            }
        }

        protected void RaiseEvent<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action != null)
            {
                action(arg1, arg2);
            }
        }
        #endregion
    }

    public abstract class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static Logger.Logger logger = Logger.Logger.CreateInstance();
        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <remarks>If the propertyName parameter does not correspond to an existing property on the current class, 
        /// an exception is thrown in DEBUG configuration only.
        /// </remarks>
        /// <param name="propertyName">The name of the property that changed.</param>
        public void RaisePropertyChangedEvent(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property that changed.</param>
        public void RaisePropertyChangedEvent<T>(Expression<Func<T>> propertyExpression)
        {
            try
            {
                string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
                this.RaisePropertyChangedEvent(propertyName);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Debug, "An error occurred while raise property changed event. Reason:{0}", ex.ToString());
            }
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed. 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changed.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        protected void Set<T>(
            Expression<Func<T>> propertyExpression,
            ref T field,
            T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return;
            }

            field = newValue;
            this.RaisePropertyChangedEvent(propertyExpression);
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed. 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        protected void Set<T>(
            string propertyName,
            ref T field,
            T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return;
            }

            field = newValue;
            this.RaisePropertyChangedEvent(propertyName);
        }

        /// <summary>
        /// Verifies that a property name exists in this ViewModel. This method
        /// can be called before the property is used, for instance before
        /// calling RaisePropertyChanged. It avoids errors when a property name
        /// is changed but some places are missed.
        /// <para>This method is only active in DEBUG mode.</para>
        /// </summary>
        /// <param name="propertyName"></param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            var myType = this.GetType();
            if (myType.GetProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
        }
    }

    public static class PropertySupport
    {
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Param is not a MemberExpression", "propertyExpression");
            }
            PropertyInfo property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Param is not a property", "propertyExpression");
            }
            if (property.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException("Param is not a static property", "propertyExpression");
            }
            return memberExpression.Member.Name;
        }
    }
}
