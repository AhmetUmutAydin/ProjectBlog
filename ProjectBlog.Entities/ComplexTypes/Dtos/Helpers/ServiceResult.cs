using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Entities.ComplexTypes.Dtos.Helpers
{
    public class ServiceResult<T>
    {
        public ResultType ResultType { get; set; }
        public T Entity { get; set; }
        public string Message { get; set; }

        public ServiceResult(T entity)
        {
            Entity = entity;
            ResultType = ResultType.Success;
        }
        public ServiceResult(string message)
        {
            Message = message;
            ResultType = ResultType.Error;
        }
        public ServiceResult(T entity,ResultType resultType)
        {
            Entity = entity;
            ResultType = resultType;
        }
    }
}
