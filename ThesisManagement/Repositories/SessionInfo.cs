namespace ThesisManagement.Repositories
{
    public static class SessionInfo
    {
        private static string userId;
        public static string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private static string name;

        public static string Name
        {
            get { return name; }
            set { name = value; }
        }


        private static Role role;
        public static Role Role
        {
            get { return role; }
            set { role = value; }
        }

        public static void Clear()
        {
            UserId = "";
            Role = Role.None;
        }
    }

    public enum Role
    {
        None,
        Professor,
        Student,
        Admin
    }
}
