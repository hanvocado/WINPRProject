namespace ThesisManagement.Helpers
{
    public static class Query
    {
        public static class Topic
        {
            public static string Insert(Models.Topic topic)
            {
                return $"INSERT INTO Topic(ProfessorId, StudentId, Name, Description, Category, Technology) VALUES('{topic.ProfessorId}', '{topic.StudentId}', '{topic.Name}', '{topic.Description}', '{topic.Category}', '{topic.Technology}')";
            }

            public static string Update(Models.Topic topic)
            {
                return $"UPDATE Topic SET ProfessorId, StudentId, Name, Description, Category, Technology) VALUES('{topic.ProfessorId}', '{topic.StudentId}', '{topic.Name}', '{topic.Description}', '{topic.Category}', '{topic.Technology}')";
            }

            public static string Delete(int id)
            {
                return $"DELETE FROM Topic WHERE Id= '{id}'";
            }

            public static string Select(int id)
            {
                return $"SELECT * FROM Topic WHERE Id = {id}";
            }

            public static string Select()
            {
                return $"SELECT * FROM Topic";
            }
        }
    }
}
