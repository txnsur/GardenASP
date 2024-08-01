using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class UserTemplateDAL
    {
        public void CreateUserTemplate(UserTemplate userTemplate)
        {
            string query = @"
                INSERT INTO USER_TEMPLATE (userId, templateId)
                VALUES (@UserId, @TemplateId);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userTemplate.UserID),
                new SqlParameter("@TemplateId", userTemplate.TemplateID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public UserTemplate GetUserTemplateById(int userId, int templateId)
        {
            string query = "SELECT * FROM USER_TEMPLATE WHERE userId = @UserId AND templateId = @TemplateId";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TemplateId", templateId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new UserTemplate
            {
                UserID = Convert.ToInt32(row["userId"]),
                TemplateID = Convert.ToInt32(row["templateId"])
            };
        }

        public List<UserTemplate> GetAllUserTemplates()
        {
            List<UserTemplate> userTemplates = new List<UserTemplate>();
            string query = "SELECT * FROM USER_TEMPLATE";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                UserTemplate userTemplate = new UserTemplate
                {
                    UserID = Convert.ToInt32(row["userId"]),
                    TemplateID = Convert.ToInt32(row["templateId"])
                };

                userTemplates.Add(userTemplate);
            }

            return userTemplates;
        }

        public void DeleteUserTemplate(int userId, int templateId)
        {
            string query = "DELETE FROM USER_TEMPLATE WHERE userId = @UserId AND templateId = @TemplateId";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TemplateId", templateId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
