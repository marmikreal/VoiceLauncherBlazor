﻿using AutoMapper;

using DataAccessLibrary.DTO;
using DataAccessLibrary.Models;

using VoiceLauncher.DTOs;

namespace DataAccessLibrary.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SavedMousePosition, SavedMousePositionDTO>(); CreateMap<SavedMousePositionDTO, SavedMousePosition>();
            CreateMap<CustomWindowsSpeechCommand, CustomWindowsSpeechCommandDTO>(); CreateMap<CustomWindowsSpeechCommandDTO, CustomWindowsSpeechCommand>();
            CreateMap<WindowsSpeechVoiceCommand, WindowsSpeechVoiceCommandDTO>(); CreateMap<WindowsSpeechVoiceCommandDTO, WindowsSpeechVoiceCommand>();
            CreateMap<GrammarName, GrammarNameDTO>(); CreateMap<GrammarNameDTO, GrammarName>();
            CreateMap<GrammarItem, GrammarItemDTO>(); CreateMap<GrammarItemDTO, GrammarItem>();
            CreateMap<HtmlTag, HtmlTagDTO>(); CreateMap<HtmlTagDTO, HtmlTag>();
            CreateMap<ApplicationDetail, ApplicationDetailDTO>(); CreateMap<ApplicationDetailDTO, ApplicationDetail>();
            CreateMap<Idiosyncrasy, IdiosyncrasyDTO>(); CreateMap<IdiosyncrasyDTO, Idiosyncrasy>();
            CreateMap<PhraseListGrammar, PhraseListGrammarDTO>(); CreateMap<PhraseListGrammarDTO, PhraseListGrammar>();
            CreateMap<Launcher, LauncherDTO>(); CreateMap<LauncherDTO, Launcher>();
            CreateMap<Category, CategoryDTO>(); CreateMap<CategoryDTO, Category>();
            CreateMap<ValueToInsert, ValueToInsertDTO>(); CreateMap<ValueToInsertDTO, ValueToInsert>();
            CreateMap<SpokenForm, SpokenFormDTO>();
            CreateMap<SpokenFormDTO, SpokenForm>();
            CreateMap<Microphone, MicrophoneDTO>();
            CreateMap<MicrophoneDTO, Microphone>();
        }
    }
}
