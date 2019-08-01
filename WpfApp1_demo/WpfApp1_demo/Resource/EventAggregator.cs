using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_demo.Resource
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }

    public interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }
        Action<object[]> GetExecutionStrategy();
    }
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, EventBase> events = new Dictionary<Type, EventBase>();

        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            EventBase existingEvent = null;

            if (!this.events.TryGetValue(typeof(TEventType), out existingEvent))
            {
                TEventType newEvent = new TEventType();
                this.events[typeof(TEventType)] = newEvent;

                return newEvent;
            }
            else
            {
                return (TEventType)existingEvent;
            }
        }
    }

    public abstract class EventBase
    {
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        protected ICollection<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        protected virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null) throw new System.ArgumentNullException("eventSubscription");

            eventSubscription.SubscriptionToken = new SubscriptionToken();
            lock (Subscriptions)
            {
                Subscriptions.Add(eventSubscription);
            }
            return eventSubscription.SubscriptionToken;
        }

        protected virtual void InternalPublish(params object[] arguments)
        {
            List<Action<object[]>> executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IEventSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (subscription != null)
                {
                    Subscriptions.Remove(subscription);
                }
            }
        }

        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IEventSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                return subscription != null;
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            List<Action<object[]>> returnList = new List<Action<object[]>>();

            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> listItem =
                        _subscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        // Prune from main list. Log?
                        _subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(listItem);
                    }
                }
            }

            return returnList;
        }
    }

    public class SubscriptionToken : IEquatable<SubscriptionToken>
    {
        private readonly Guid _token;

        public SubscriptionToken()
        {
            _token = Guid.NewGuid();
        }

        public bool Equals(SubscriptionToken other)
        {
            if (other == null) return false;
            return Equals(_token, other._token);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }
    }
}
