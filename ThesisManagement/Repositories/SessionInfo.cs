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

        private static Role role;
        public static Role Role
        {
            get { return role; }
            set { role = value; }
        }
    }

    public enum Role
    {
        Professor,
        Student,
        Admin
    }
}
