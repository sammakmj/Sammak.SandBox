namespace Sammak.SandBox.InterfaceExtension.SomeLibraryYouCanModify
{
    public class WrapperMethod
    {
        IStaticWrapper _wrapper;

        public WrapperMethod(IStaticWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public int SomeMethod(string input)
        {
            var value = _wrapper.SomeStaticMethod(input);

            return value ? 1 : 0;
        }
    }
}
