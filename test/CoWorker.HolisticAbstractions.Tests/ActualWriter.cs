using System;
using System.IO;
using System.Text;

namespace CoWorker.HolisticAbstractions.Tests
{

    public class ActualWriter : TextWriter
    {
        public string Actual { get; private set; } = string.Empty;

        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(String value)
        {
            Actual += value;
        }
    }
}
