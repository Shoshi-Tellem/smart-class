using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using File = smart_class.Core.Entities.File;

namespace smart_class.Core.Services
{
    public interface IFileService
    {
        Task<IEnumerable<File>> GetFilesAsync();
        Task<File?> GetFileByIdAsync(int id);
        Task<File> AddFileAsync(File file);
        Task<File?> UpdateFileAsync(int id, File file);
        Task<File?> DeleteAsync(int id);
    }
}