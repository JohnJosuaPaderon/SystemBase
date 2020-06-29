using Microsoft.Win32.SafeHandles;
using Sorschia.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SystemBase.Security;

namespace SystemBase.Data
{
    internal static class SqlCommandExtensions
    {
        public static SqlCommand AddSessionIdParameter(this SqlCommand instance, ICurrentSessionProvider currentSessionProvider, string parameterName = default) =>
            instance.AddInParameter(string.IsNullOrWhiteSpace(parameterName) ? "@SessionId" : parameterName, currentSessionProvider.GetId());

        public static SqlCommand AddSkipParameter(this SqlCommand instance, int? skip, string parameterName = default) =>
            instance.AddInParameter(string.IsNullOrWhiteSpace(parameterName) ? "@Skip" : parameterName, skip);

        public static SqlCommand AddTakeParameter(this SqlCommand instance, int? take, string parameterName = default) =>
            instance.AddInParameter(string.IsNullOrWhiteSpace(parameterName) ? "@Take" : parameterName, take);

        public static SqlCommand AddPaginationParameters(this SqlCommand instance, PaginationModel model) => instance
            .AddSkipParameter(model?.Skip)
            .AddTakeParameter(model?.Take);

        public static SqlCommand AddTypeParameter<T>(this SqlCommand instance, string parameterName, IEnumerable<T> values, string dbTypeName, ParameterDirection direction = ParameterDirection.Input)
        {
            var dataTable = new DataTable();

            var type = typeof(T);
            var properties = type.GetProperties();

            if (properties != null && properties.Any())
            {
                for (int i = 0; i < properties.Length; i++)
                    dataTable.Columns.Add(new DataColumn(properties[i].Name, properties[i].PropertyType));
            }

            foreach (var value in values)
                dataTable.Rows.Add(value);

            instance.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                TypeName = dbTypeName,
                SqlDbType = SqlDbType.Structured,
                Direction = direction,
                Value = dataTable
            });
            return instance;
        }

        public static SqlCommand AddSingleTypeParameter<T>(this SqlCommand instance, string parameterName, IEnumerable<T> values, string fieldName, string dbTypeName, ParameterDirection direction = ParameterDirection.Input)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(fieldName, typeof(T)));

            foreach (var value in values)
                dataTable.Rows.Add(value);

            instance.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                TypeName = dbTypeName,
                SqlDbType = SqlDbType.Structured,
                Direction = direction,
                Value = dataTable
            });

            return instance;
        }

        public static SqlCommand AddIntListParameter(this SqlCommand instance, string parameterName, IEnumerable<int> values, string fieldName = "Value", string dbTypeName = "dbo.IntList", ParameterDirection direction = ParameterDirection.Input) =>
            instance.AddSingleTypeParameter<int>(parameterName, values, fieldName, dbTypeName, direction);
    }
}
