using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;

using DataAccessLibrary.DTO;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using VoiceLauncher.Services;

namespace RazorClassLibrary.Pages
{
    public partial class HtmlTagAddEdit : ComponentBase
    {
        [CascadingParameter] BlazoredModalInstance? ModalInstance { get; set; }
        [Inject] public IJSRuntime? JSRuntime { get; set; }
        [Parameter] public int? Id { get; set; }
        public HtmlTagDTO HtmlTagDTO { get; set; } = new HtmlTagDTO();//{ };
        [Inject] public IHtmlTagDataService? HtmlTagDataService { get; set; }
        [Inject] public IToastService? ToastService { get; set; }
#pragma warning disable 414, 649
        string TaskRunning = "";
#pragma warning restore 414, 649
        protected override async Task OnInitializedAsync()
        {
            if (HtmlTagDataService == null)
            {
                return;
            }
            if (Id > 0)
            {
                var result = await HtmlTagDataService.GetHtmlTagById((int)Id);
                if (result != null)
                {
                    HtmlTagDTO = result;
                }
            }
            else
            {
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    if (JSRuntime != null)
                    {
                        await JSRuntime.InvokeVoidAsync("window.setFocus", "Tag");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        public void Close()
        {
            if (ModalInstance != null)
                ModalInstance.CancelAsync();
        }

        protected async Task HandleValidSubmit()
        {
            TaskRunning = "disabled";
            if ((Id == 0 || Id == null) && HtmlTagDataService != null)
            {
                HtmlTagDTO? result = await HtmlTagDataService.AddHtmlTag(HtmlTagDTO);
                if (result == null)
                {
                    ToastService?.ShowError("Html Tag failed to add, please investigate Error Adding New Html Tag");
                    return;
                }
                ToastService?.ShowSuccess("Html Tag added successfully");
            }
            else
            {
                if (HtmlTagDataService != null)
                {
                    await HtmlTagDataService!.UpdateHtmlTag(HtmlTagDTO, "");
                    ToastService?.ShowSuccess("The Html Tag updated successfully");
                }
            }
            if (ModalInstance != null)
            {
                await ModalInstance.CloseAsync(ModalResult.Ok(true));
            }
            TaskRunning = "";
        }
    }
}