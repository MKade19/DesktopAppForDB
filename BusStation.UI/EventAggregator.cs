using System;

namespace BusStation.UI
{
    internal class EventAggregator
    {
        private EventAggregator() { }

        private static EventAggregator? _instance;

        public static EventAggregator Instance
        {
            get => _instance ?? (_instance = new EventAggregator());
        }

        public event EventHandler? UserAuthorized;
        public event EventHandler? UserUnauthorized;

        public void RaiseUserAuthorizedEvent()
        {
            UserAuthorized?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseUserUnauthorizedEvent() 
        { 
            UserUnauthorized?.Invoke(this, EventArgs.Empty);
        }
    }
}
