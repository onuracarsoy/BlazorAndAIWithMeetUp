using Microsoft.AspNetCore.Components;

namespace MeetUpWebApp.Shared
{
    public class SharedHelper
    {
        private readonly NavigationManager NavigationManager;
        private const string ATTENDEE_ROLE = "Attendee";
        private const string ORGANIZER_ROLE = "Organizer";
        private const string ADMIN_ROLE = "Admin";
        private const string GOING_STATUS = "Going";
        private const string NOT_GOING_STATUS = "Not going";

        public SharedHelper(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public List<string> GetCategories()
        {
            return Enum.GetNames(typeof(MeetupCategoriesEnum)).ToList();
        }

        public string GetQueryParamValue(string paramName)
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return  queryParams[paramName] ?? "";
        }

        public static string AttendeeRole => ATTENDEE_ROLE;
        public static string OrganizerRole => ORGANIZER_ROLE;
        public static string AdminRole => ADMIN_ROLE;
        public static string GoingStatus => GOING_STATUS;
        public static string NotGoingStatus => NOT_GOING_STATUS;
    }
}
