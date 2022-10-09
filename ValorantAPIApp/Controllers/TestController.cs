using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using ValorantAPIApp.Data;
using ValorantAPIApp.Services;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly HttpService _httpService;
        private readonly ValorantDbContext _dbContext;

        public TestController(HttpService httpService, ValorantDbContext dbContext)
        {
            this._httpService = httpService;
            this._dbContext = dbContext;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DownloadAllSkinsIntoDatabase()
        {
            var weapons = await _httpService.Request("weapons", HttpMethod.Get);
            dynamic weaponsJson = JObject.Parse(weapons.Data);

            foreach (var weapon in weaponsJson.data)
            {
                string weaponUuid = weapon.uuid;
                Weapon weaponDb = await _dbContext.Weapons.FirstOrDefaultAsync(e => e.Uuid == weaponUuid);
                foreach (var skin in weapon.skins)
                {
                    string contentTier = skin.contentTierUuid;
                    if (string.IsNullOrEmpty(contentTier))
                        continue;
                    int price = await GetContentTierPrice(contentTier);
                    WeaponSkin newWeaponSkin = new WeaponSkin
                    {
                        Uuid = skin.uuid,
                        Price = price,
                        Weapon = weaponDb
                    };
                    await _dbContext.WeaponSkins.AddAsync(newWeaponSkin);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private async Task<int> GetContentTierPrice(string uuid)
        {
            var contentTiers = await _httpService.Request("contenttiers/" + uuid, HttpMethod.Get);
            dynamic tiersJson = JObject.Parse(contentTiers.Data);
            return tiersJson.data.juiceValue * 1000;
        }
    }
}
