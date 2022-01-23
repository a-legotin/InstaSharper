using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Models.Response.Base;
using LanguageExt;

namespace InstaSharper.API.Services.Interfaces;

internal interface IDeviceService
{
    Task<Either<ResponseStatusBase, LauncherSyncResponse>> LauncherSyncAsync();
    Task<Either<ResponseStatusBase, BaseStatusResponse>> GetZrTokenAsync();
}