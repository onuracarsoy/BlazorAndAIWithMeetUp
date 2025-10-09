using Microsoft.AspNetCore.Components;

namespace MeetUpWebApp.Shared.Services
{
    public class LayoutService
    {
        public RenderFragment? SectionContent { get; private set; }

        public Action? OnContentChanged;

        public void SetSectionContent(RenderFragment? content)
        {
            SectionContent = content;
            OnContentChanged?.Invoke();
        }
    }
}
