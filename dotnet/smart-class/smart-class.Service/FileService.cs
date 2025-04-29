using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;
using File = smart_class.Core.Entities.File;

namespace smart_class.Service
{
    public class FileService : IFileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepository<File> _fileRepository;

        public FileService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _fileRepository = repositoryManager.FileRepository;
        }

        public async Task<IEnumerable<File>> GetFilesAsync()
        {
            return await _fileRepository.GetAsync();
        }

        public async Task<File?> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetByIdAsync(id);
        }

        public async Task<File> AddFileAsync(File file)
        {
            File addedFile = await _fileRepository.AddAsync(file);
            await _repositoryManager.SaveAsync();
            return addedFile;
        }

        public async Task<File?> UpdateFileAsync(int id, File file)
        {
            File? existingFile = await GetFileByIdAsync(id);
            if (existingFile == null)
                return null;

            existingFile.Path = file.Path; // Assuming File has a Name property
            File updatedFile = await _fileRepository.UpdateAsync(existingFile);
            await _repositoryManager.SaveAsync();
            return updatedFile;
        }

        public async Task<File?> DeleteAsync(int id)
        {
            File? existingFile = await GetFileByIdAsync(id);
            if (existingFile == null)
                return null;

            File? deletedFile = await _fileRepository.DeleteAsync(existingFile);
            await _repositoryManager.SaveAsync();
            return deletedFile;
        }
    }
}