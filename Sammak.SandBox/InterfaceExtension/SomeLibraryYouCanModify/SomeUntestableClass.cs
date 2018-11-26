using Sammak.SandBox.InterfaceExtension.SomeLibraryYouCantModify;

namespace Sammak.SandBox.InterfaceExtension.SomeLibraryYouCanModify
{
    public class SomeUntestableClass
    {
        public int SomeMethod(string input)
        {
            var value = SomeStaticClass.SomeStaticMethod(input);

            return value ? 1 : 0;
        }
    }
}
