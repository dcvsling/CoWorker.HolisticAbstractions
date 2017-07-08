using System;

namespace CoWorker.HolisticAbstractions.Tests
{

    public class SeedDTO
    {
        public string Word { get; set; }
        public long Number { get; set; }
        public DateTimeOffset Now { get; set; }
        public override String ToString()
        {
            return $"{nameof(Now)}:{Now.ToString()},{nameof(Number)}:{Number.ToString()},{nameof(Word)}:{Word}";
        }
    }
}
