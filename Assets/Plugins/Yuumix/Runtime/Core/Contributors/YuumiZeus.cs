namespace Yuumix.OdinToolkits.Core.Contributors
{
    public static class YuumiZeus
    {
        public const string Name = "Yuumi Zeus";
        public const string Email = "zeriying@gmail.com";
        public const string Link = "https://github.com/Yuumi-Zeus";

        public static ContributorInfo ToContributor(string time)
        {
            return new ContributorInfo(time, Name, Email, Link);
        }
    }
}
