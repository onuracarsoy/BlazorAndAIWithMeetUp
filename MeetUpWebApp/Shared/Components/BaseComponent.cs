using MeetUpWebApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MeetUpWebApp.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [Inject]
        protected LayoutService layoutService { get; set; } = default!;

        [Inject]
        protected AuthenticationStateProvider authenticationStateProvider { get; set; } = default!;

        protected virtual bool ShouldClearSectionContent => true;

        private AuthenticationState? authenticationState;

        private bool isAuthenticated = false;

        protected override void OnInitialized()
        {
            base.OnInitialized(); 
            if (ShouldClearSectionContent)
            {
                layoutService?.SetSectionContent(null);
            }
        }

        protected override async Task OnInitializedAsync()
        {
           authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            isAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;
        }

        protected bool IsAuthenticated
        {
            get { return isAuthenticated; }
        }

        protected string UserName
        {
            get
            {
                if (isAuthenticated)
                {
                    return authenticationState.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Name)?.Value;
                }
                return string.Empty;
            }
        }

        protected string UserMail
        {
            get
            {
                if (isAuthenticated)
                {
                    return authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                }
                return string.Empty;
            }
        }

        protected string UserId
        {
            get
            {
                if (isAuthenticated)
                {
                    return authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                }
                return string.Empty;
            }
        }

        protected bool IsOrganizer
        {
            get
            {
                if (isAuthenticated)
                {
                    return authenticationState.User.Claims.FirstOrDefault(c =>c.Type == ClaimTypes.Role)?.Value == SharedHelper.OrganizerRole;
                }
                return false;
            }
         
        }
    }
}
