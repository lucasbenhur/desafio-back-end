using DesafioConexa.Api.Enums;
using DesafioConexa.Api.Models;

namespace DesafioConexa.Api.Interfaces
{
    public interface IStreamingService
    {
        PlayList GetPlaylistRecommendation(Category playListCategory);
    }
}
