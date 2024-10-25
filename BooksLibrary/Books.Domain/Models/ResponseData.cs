using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Models
{
    /// <summary>
    /// Класс описывает формат данных получаемых от сервисов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessfull { get; set; } = true;
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Получить объект успешного ответа
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseData<T> Success(T? data) 
        {
            return new ResponseData<T> { Data = data };
        }
        /// <summary>
        /// получить объект ответа с ошибкой
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseData<T> Error(string message, T? data = default)
        {
            return new ResponseData<T>
            {
                ErrorMessage = message,
                IsSuccessfull = false,
                Data = data
            };
        }
    }
}
