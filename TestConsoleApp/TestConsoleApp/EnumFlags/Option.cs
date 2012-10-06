using System;

namespace TestConsoleApp.EnumFlags
{
    [Flags]
    enum Option
    {
        None = 0,
        HideRefine = 1,
        ShowExtranet = 256,
        ShowEForm = 1024
    }
}
