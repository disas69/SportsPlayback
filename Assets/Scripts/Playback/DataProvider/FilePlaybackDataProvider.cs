using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Playback.DataProvider
{
    public class FilePlaybackDataProvider : IPlaybackDataProvider<string>
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        private readonly FileStream _stream;
        private readonly StreamReader _reader;

        public bool IsEnd
        {
            get
            {
                if (_reader != null)
                {
                    return _reader.EndOfStream;
                }

                return true;
            }
        }

        public FilePlaybackDataProvider(string path)
        {
            _stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions);
            _reader = new StreamReader(_stream, Encoding.UTF8);
        }

        public async Task<string> Get()
        {
            return await _reader.ReadLineAsync();
        }

        public void Dispose()
        {
            _reader?.Dispose();
            _stream?.Dispose();
        }
    }
}