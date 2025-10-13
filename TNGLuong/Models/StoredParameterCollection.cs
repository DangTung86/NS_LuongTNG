using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TNGLuong.Models
{
    public class StoredParameterCollection : CollectionBase
    {

        /// <summary>
        /// Add a parameter to the collection.
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter data Type</param>
        /// <param name="parameterDirection">Parameter direction</param>
        /// <param name="parameterValue">Parameter value object</param>
        public void Add(string parameterName, SqlDbType parameterType, ParameterDirection parameterDirection, object parameterValue)
        {
            this.List.Add(new StoredProcedureParameter(parameterName, parameterType, parameterDirection, parameterValue));
        }

        /// <summary>
        /// Add a parameter to the collection.
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter data Type</param>
        /// <param name="parameterDirection">Parameter direction</param>
        /// <param name="parameterValue">Parameter value object</param>
        /// <param name="parameterSize">Parameter size</param>
        public void Add(string parameterName, SqlDbType parameterType, ParameterDirection parameterDirection, object parameterValue, int parameterSize)
        {
            this.List.Add(new StoredProcedureParameter(parameterName, parameterType, parameterDirection, parameterValue, parameterSize));
        }

        public bool Contains(string parameterName)
        {
            return this.Contains(parameterName);
        }

        /// <summary>
        /// Get the StoredProcedureParameter object from the list at the specific index position.
        /// </summary>
        /// <param name="index">The index to the parameter in the list</param>
        /// <returns>A StoredProcedureParameter object</returns>
        public StoredProcedureParameter Item(int index)
        {
            return (StoredProcedureParameter)List[index];
        }
    }
}