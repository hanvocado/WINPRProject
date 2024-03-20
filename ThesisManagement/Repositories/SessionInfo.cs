namespace ThesisManagement.Repositories
{
    public static class SessionInfo
    {
        private static string userId = "P2";
        public static string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }
}
