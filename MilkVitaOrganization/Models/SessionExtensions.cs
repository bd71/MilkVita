using System.Web;

namespace MilkVitaOrganization.Models
{
    public static class SessionExtensions
    {
        public static T GetDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            return (T) session[key];
        }

        public static void SetDataToSession(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }
        
        public static bool IsSessionHasValue(this HttpSessionStateBase session, string key)
        {
            
            return session[key] != null;
        }

        public static void RemoveDataFromSession(this HttpSessionStateBase session, string key)
        {
            session.Remove(key);
        }

        public static void RemoveAllFromSession(this HttpSessionStateBase session)
        {
            session.RemoveAll();
            session.Clear();
        }
    }
}