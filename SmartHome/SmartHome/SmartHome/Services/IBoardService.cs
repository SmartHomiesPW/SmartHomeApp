using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.BoardService
{
    public interface IBoardService
    {
        Task<List<Board>> GetBoards();
        Task<List<IBoardDevice>> GetAllDevicesForGivenBoard(string boardId);
    }
}
