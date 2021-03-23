﻿using Blazored.Toast.Services;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoiceLauncherBlazor.helpers;

namespace VoiceLauncherBlazor.Pages
{
	public partial class CustomIntelliSense
	{
		[Parameter] public int customIntellisenseId { get; set; } = 0;
		[Inject] NavigationManager NavigationManager { get; set; }
		[Inject] IToastService ToastService { get; set; }
#pragma warning disable 414
		private bool _loadFailed = false;
#pragma warning restore 414
		public DataAccessLibrary.Models.CustomIntelliSense intellisense { get; set; }
		public List<DataAccessLibrary.Models.GeneralLookup> generalLookups { get; set; }
		public List<DataAccessLibrary.Models.Language> languages { get; set; }
		public List<DataAccessLibrary.Models.Category> categories { get; set; }
		private List<string> customValidationErrors = new List<string>();
		public string Message { get; set; }
		protected override async Task OnInitializedAsync()
		{
			if (customIntellisenseId > 0)
			{
				try
				{
					intellisense = await CustomIntellisenseService.GetCustomIntelliSenseAsync(customIntellisenseId);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception.Message);
					_loadFailed = true;
				}
			}
			else
			{
				intellisense = new DataAccessLibrary.Models.CustomIntelliSense
				{
					DeliveryType = "Send Keys"
				};
			}
			generalLookups = await GeneralLookupService.GetGeneralLookUpsAsync("Delivery Type");
			languages = (await LanguageService.GetLanguagesAsync(activeFilter:true));
			categories = await CategoryService.GetCategoriesAsync();
		}

		protected override async Task OnParametersSetAsync()
		{
			if (intellisense.Id > 0)
			{
				intellisense = await CustomIntellisenseService.GetCustomIntelliSenseAsync(intellisense.Id);
			}
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await JSRuntime.InvokeVoidAsync("setFocus", "LanguageSelect");
			}
		}
		private async Task HandleValidSubmit()
		{
			intellisense.SendKeysValue = CarriageReturn.ReplaceForCarriageReturnChar(intellisense.SendKeysValue);
			customValidationErrors.Clear();
			if (intellisense.LanguageId == 0)
			{
				customValidationErrors.Add("Language is required");
			}
			if (intellisense.CategoryId == 0)
			{
				customValidationErrors.Add("Category is required");
			}
			if (customValidationErrors.Count == 0)
			{
				var result = await CustomIntellisenseService.SaveCustomIntelliSense(intellisense);
				if (result.Contains("Successfully"))
				{
					ToastService.ShowSuccess(result, "Success");
					return;
				}
				ToastService.ShowError(result, "Failure");
			}
		}
		private async Task CallChangeAsync(string elementId)
		{
			await JSRuntime.InvokeVoidAsync("CallChange", elementId);
		}
		private void GoBack()
		{
			NavigationManager.NavigateTo("/intellisenses");
		}
	}
}
