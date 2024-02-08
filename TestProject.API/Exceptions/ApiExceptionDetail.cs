﻿namespace TestProject.API.Exceptions
{
    /// <summary>
    /// Информация об ошибке работы АПИ
    /// </summary>
    public class ApiExceptionDetail
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
