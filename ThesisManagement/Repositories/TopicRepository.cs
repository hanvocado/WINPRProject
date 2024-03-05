using System.Data.SqlClient;
using System.Windows;
using ThesisManagement.Helpers;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface ITopicRepository
    {
        void Add(Topic topic);
        void Update(int id);
        void Delete(int id);
        Topic? Get(int id);
        IEnumerable<Topic> GetAll();
    }

    public class TopicRepository : DbRepository, ITopicRepository
    {
        public void Add(Topic topic)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = Query.Topic.Insert(topic);

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                    return;
                }

                ShowSuccessMessage(Message.Success);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Topic? Get(int id)
        {
            Topic? topic = null;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = Query.Topic.Select(id);

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                topic = RetrieveTopic(reader);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e.Message);
                }
            }
            return topic;
        }

        public IEnumerable<Topic> GetAll()
        {
            List<Topic> topics = new();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = Query.Topic.Select();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Topic topic = new();
                            while (reader.Read())
                            {
                                topic = RetrieveTopic(reader);
                                topics.Add(topic);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }

                return topics;
            }
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        private Topic RetrieveTopic(SqlDataReader reader)
        {
            return new Topic
            {
                Id = Convert.ToInt32(reader["Id"]),
                ProfessorId = "",
                StudentId = reader["StudentId"]?.ToString() ?? null,
                Name = reader["Name"]?.ToString()!,
                Description = reader["Description"]?.ToString()!,
                Category = reader["Category"]?.ToString() ?? null,
                Technology = reader["Technology"]?.ToString() ?? null
            };
        }
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
