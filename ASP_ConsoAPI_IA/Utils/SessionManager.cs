using Dal.Models;
using System.Text.Json;

namespace ASP_ConsoAPI_IA.Utils
{
    public class SessionManager
    {
        private ISession _session;
        public SessionManager(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public User CurrentUser
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_session.GetString(nameof(CurrentUser)))) return null;
                return JsonSerializer.Deserialize<User>(_session.GetString(nameof(CurrentUser)));
            }
            set
            {
                _session.SetString(nameof(CurrentUser), JsonSerializer.Serialize(value));
            }
        }

        public void LogOut()
        {
            CurrentUser = null;
            _session.Clear();
            
        }
    }
}
