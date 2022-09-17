
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast;
using Blazored.Toast.Services;
using System.Security.Claims;
using VoiceLauncher.DTOs;
using VoiceLauncher.Services;

namespace VoiceLauncher.Pages
{
    public partial class GrammarItemAddEdit : ComponentBase
    {
        [CascadingParameter] BlazoredModalInstance? ModalInstance { get; set; }
        [Inject] public IJSRuntime? JSRuntime { get; set; }
        [Parameter] public int? Id { get; set; }
      [Parameter] public int GrammarNameId { get; set; }
        public GrammarItemDTO GrammarItemDTO { get; set; } = new GrammarItemDTO();//{ };
        [Inject] public IGrammarItemDataService? GrammarItemDataService { get; set; }
        [Inject] public IToastService? ToastService { get; set; }
#pragma warning disable 414, 649
        string TaskRunning = "";
#pragma warning restore 414, 649
        protected override async Task OnInitializedAsync()
        {
            if (GrammarItemDataService == null)
            {
                return;
            }
            if (Id > 0)
            {
                var result = await GrammarItemDataService.GetGrammarItemById((int)Id);
                if (result != null)
                {
                    GrammarItemDTO = result;
                }
            }
            else
            {
			
                GrammarItemDTO.GrammarNameId = GrammarNameId;
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
                        await JSRuntime.InvokeVoidAsync("window.setFocus", "Value");
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
            if ((Id == 0 || Id == null) && GrammarItemDataService != null)
            {
                GrammarItemDTO? result = await GrammarItemDataService.AddGrammarItem(GrammarItemDTO);
                if (result == null)
                {
                    ToastService?.ShowError("Grammar Item failed to add, please investigate", "Error Adding New Grammar Item");
                    return;
                }
                ToastService?.ShowSuccess("Grammar Item added successfully", "SUCCESS");
            }
            else
            {
                if (GrammarItemDataService != null)
                {
                    await GrammarItemDataService!.UpdateGrammarItem(GrammarItemDTO, "");
                    ToastService?.ShowSuccess("The Grammar Item updated successfully", "SUCCESS");
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