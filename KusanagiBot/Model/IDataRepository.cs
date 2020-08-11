using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KusanagiBot.Model
{
    public interface IDataRepository
    {
        Task<Dictionary<string, string>>ReadAsync(string path);

        Task WriteAsync(string path, Dictionary<string, string> commandData);
    }
}
