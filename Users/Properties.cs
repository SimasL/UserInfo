namespace Users
{
    public static class Properties
    {
        public static string DBName = "SIMAS-PC\\SQLEXPRESS";
        public static string User = "test";
        public static string Password = "test55";
        public static dbStatus UserDBStatus;
        public static bool Pageloaded = false;
    }

    public enum dbStatus
    {
        WrongConnection,
        NeedCreateDB,
        OK
    }
}