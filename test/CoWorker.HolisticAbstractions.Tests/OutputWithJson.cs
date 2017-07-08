using Newtonsoft.Json;
using System.IO;

namespace CoWorker.HolisticAbstractions.Tests
{

    public class OutputWithJson<TCommand> : IWriteTo<TCommand> where TCommand : class
    {
        private readonly TextWriter _writer;
        public OutputWithJson(TextWriter writer)
        {
            _writer = writer;
        }
        public void Invoke(TCommand command)
        {
            _writer.WriteLine(JsonConvert.SerializeObject(command));
        }
    }
}
