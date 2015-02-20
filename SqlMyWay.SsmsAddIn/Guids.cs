// Guids.cs
// MUST match guids.h
using System;

namespace SqlMyWay.SsmsAddIn
{
    static class GuidList
    {
        public const string guidSsmsAddInPkgString = "077103e7-890e-43cc-bebf-fe086c432670";
        public const string guidSsmsAddInCmdSetString = "6b89530f-4ed3-4053-8c2a-ef227ab92bef";

        public static readonly Guid guidSsmsAddInCmdSet = new Guid(guidSsmsAddInCmdSetString);
    };
}