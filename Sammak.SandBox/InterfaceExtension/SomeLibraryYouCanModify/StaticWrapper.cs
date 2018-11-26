using Sammak.SandBox.InterfaceExtension.SomeLibraryYouCantModify;

namespace Sammak.SandBox.InterfaceExtension.SomeLibraryYouCanModify
{
    public class StaticWrapper : IStaticWrapper
    {
        public bool SomeStaticMethod(string input)
        {
            return SomeStaticClass.SomeStaticMethod(input);
        }
    }
}
