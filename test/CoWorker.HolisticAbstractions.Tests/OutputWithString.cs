using System;
using System.IO;

namespace CoWorker.HolisticAbstractions.Tests
{

    public class OutputWithString<TCommand> : IWriteTo<TCommand> where TCommand : class
    {
        private readonly TextWriter _writer;

        public OutputWithString(TextWriter writer)
        {
            _writer = writer;
        }
        public void Invoke(TCommand command)
        {
            _writer.WriteLine(command.ToString());
        }
    }
}
