using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TNGLuong.Models
{
    [Serializable()]
    public class StoredProcedureParameter
    {
        private readonly string _parameterName;
        private readonly SqlDbType _parameterType;
        private readonly ParameterDirection _parameterDirection = ParameterDirection.Input;
        private readonly object _parameterValue;
        private object _parameterOutputValue;
        private int _parameterSize = 50;

        /// <summary>
        /// A constructor of the StoredProcedureParameter object
        /// </summary>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="parameterType">Type of the parameter</param>
        /// <param name="parameterDirection">Direction of the parameter</param>
        public StoredProcedureParameter(string parameterName, SqlDbType parameterType, ParameterDirection parameterDirection)
        {
            this._parameterName = parameterName;
            this._parameterType = parameterType;
            this._parameterDirection = parameterDirection;
        }

        /// <summary>
        /// A constructor of the StoredProcedureParameter object
        /// </summary>
        public StoredProcedureParameter(string parameterName, SqlDbType parameterType,
                                        ParameterDirection parameterDirection, object parameterValue)
        {
            this._parameterName = parameterName;
            this._parameterType = parameterType;
            this._parameterDirection = parameterDirection;
            this._parameterValue = parameterValue;
            this._parameterOutputValue = parameterValue;
        }

        /// <summary>
        /// A constructor of the StoredProcedureParameter object
        /// </summary>
        public StoredProcedureParameter(string parameterName, SqlDbType parameterType,
                                        ParameterDirection parameterDirection, object parameterValue,
                                        int parameterSize)
        {
            this._parameterName = parameterName;
            this._parameterType = parameterType;
            this._parameterDirection = parameterDirection;
            this._parameterValue = parameterValue;
            this._parameterSize = parameterSize;
            this._parameterOutputValue = parameterValue;
        }

        /// <summary>
        /// Get the name of the stored precedure parameter.
        /// </summary>
        public string ParameterName
        {
            get
            {
                return this._parameterName;
            }
        }

        /// <summary>
        /// Get the type of the stored precedure parameter.
        /// </summary>
        public SqlDbType ParameterType
        {
            get
            {
                return this._parameterType;
            }
        }

        /// <summary>
        /// Get the direction of the stored precedure parameter.
        /// </summary>
        public ParameterDirection ParameterDirection
        {
            get
            {
                return this._parameterDirection;
            }
        }

        /// <summary>
        /// Get the value of the stored precedure parameter.
        /// </summary>
        public object ParameterValue
        {
            get
            {
                return this._parameterValue;
            }
        }

        /// <summary>
        /// Get or Set the output value of the stored precedure parameter.
        /// </summary>
        public object ParameterOutputValue
        {
            get
            {
                return this._parameterOutputValue;
            }
            set
            {
                this._parameterOutputValue = value;
            }
        }

        /// <summary>
        /// Get or Set the size of the stored precedure parameter.
        /// </summary>
        public int ParameterSize
        {
            get
            {
                return this._parameterSize;
            }
            set
            {
                this._parameterSize = value;
            }
        }
    }
}