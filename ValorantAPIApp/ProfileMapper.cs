using AutoMapper;
using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Dto.AgentLoadout;
using ValorantAPIApp.Dto.Mission;
using ValorantAPIApp.Dto.Player;
using ValorantAPIApp.Dto.PlayerWeapon;
using ValorantAPIApp.Dto.Team;
using ValorantAPIApp.Dto.Weapon;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Models;

namespace ValorantAPIApp
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Agent, AgentCreateDto>().ReverseMap();
            CreateMap<Agent, AgentGetDto>().ReverseMap();

            CreateMap<Weapon, WeaponCreateDto>().ReverseMap();
            CreateMap<Weapon, WeaponGetDto>().ReverseMap();

            CreateMap<Player, PlayerRegisterDto>().ReverseMap();
            CreateMap<Player, PlayerLoginDto>().ReverseMap();
            CreateMap<Player, PlayerGetDto>().ReverseMap();

            CreateMap<Mission, MissionCreateDto>().ReverseMap();
            CreateMap<Mission, MissionGetDto>().ReverseMap();

            CreateMap<AgentLoadout, AgentLoadoutBuyDto>().ReverseMap();
            CreateMap<AgentLoadout, AgentLoadoutGetDto>().ReverseMap();
            CreateMap<AgentLoadout, AgentLoadoutEditDto>().ReverseMap();

            CreateMap<Team, TeamCreateDto>().ReverseMap();
            CreateMap<Team, TeamGetDto>().ReverseMap();
            CreateMap<Team, TeamUpdateAgentDto>().ReverseMap();

            CreateMap<PlayerWeapon, PlayerWeaponBuyDto>().ReverseMap();
            CreateMap<PlayerWeapon, PlayerWeaponEditDto>().ReverseMap();
            CreateMap<PlayerWeapon, PlayerWeaponGetDto>().ReverseMap();

            CreateMap<WeaponSkin, WeaponSkinBuyDto>().ReverseMap();
            CreateMap<WeaponSkin, WeaponSkinGetDto>().ReverseMap();
        }
    }
}
