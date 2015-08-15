using System;


namespace Core
{
    /// <summary>
    /// interface for analytics
    /// </summary>
    public interface IAnalytics
    {

        /// <summary>
        /// sessions
        /// </summary>
        void StartSession();
        void StopSession();


        /// <summary>
        /// Log events
        /// </summary>
        void Track(string sEventCategory, string sEventAction, string sEventLabel, long lValue);
        void Track(string sEventCategory, string sEventAction);

        /// <summary>
        /// log screens name
        /// </summary>
        void TrackScreen(string sScreenName);


        /// <summary>
        /// track social events
        /// </summary>
        void TrackSocial(string socialNetwork, string socialAction, string socialTarget);
    }

}