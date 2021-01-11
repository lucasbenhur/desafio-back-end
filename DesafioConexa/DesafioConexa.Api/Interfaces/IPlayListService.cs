
using DesafioConexa.Api.Enums;
using DesafioConexa.Api.Models;

namespace DesafioConexa.Api.Interfaces
{
    public interface IPlayListService
    {
        PlayList GetPlayList(City city);

        Category GetPlayListCategory(float temperature);
    }
}
