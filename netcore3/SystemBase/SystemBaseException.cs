using Sorschia;
using System;
using System.Runtime.Serialization;

namespace SystemBase
{
    public class SystemBaseException : SorschiaException
    {
        public SystemBaseException(bool isMessageViewable = default) : base(isMessageViewable)
        {
        }

        public SystemBaseException(string message, bool isMessageViewable = default) : base(message, isMessageViewable)
        {
        }

        public SystemBaseException(string message, Exception innerException, bool isMessageViewable = default) : base(message, innerException, isMessageViewable)
        {
        }

        protected SystemBaseException(SerializationInfo info, StreamingContext context, bool isMessageViewable = default) : base(info, context, isMessageViewable)
        {
        }

        public static new SystemBaseException RepositoryIsNull<TRepository>() => new SystemBaseException($"Repository of type {typeof(TRepository).FullName} is null");
        public static new SystemBaseException DependencySettingsIsNull<TDependencySettings>() => new SystemBaseException($"Dependency settings of type {typeof(TDependencySettings).FullName} is null");
        public static SystemBaseException VariableIsNull<TVariable>(string variableName) => new SystemBaseException($"Variable of type {typeof(TVariable).FullName} with name '{variableName}' is null"); 
    }
}
