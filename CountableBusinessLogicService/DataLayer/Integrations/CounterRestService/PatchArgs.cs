using System;

namespace Integrations.CounterRestService
{
    public class PatchArgs
    {
        public string Version { get; set; }
        public PatchOptionType PatchOption { get; set; }

        public PatchArgs(byte[] version, PatchOptionType type)
        {
            Version = Convert.ToBase64String(version);
            PatchOption = type;
        }
    }

    public enum PatchOptionType
    {
        Increment = 0,
        Decrement = 1,
    }
}
