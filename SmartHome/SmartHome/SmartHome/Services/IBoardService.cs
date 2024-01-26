using SmartHome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Services.BoardService
{
    public interface IBoardService
    {
        // Stub, as there is no counterpart on the backend.
        // Used only as a mock service in the 'Boards' screen

        Task<List<Board>> GetBoards();
        Task<List<IBoardDevice>> GetAllDevicesForGivenBoard(string boardId);
    }
}
